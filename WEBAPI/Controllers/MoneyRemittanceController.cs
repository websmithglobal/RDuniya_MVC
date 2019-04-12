using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ENT = Web.Framework.Entity;
using COM = Web.Framework.Common;
using BAL = Web.Framework.BusinessLayer;
using Web.Framework.Common;
using WEBAPI.WEBRECHARGE;
using WEBAPI.WEBRECHARGE.ENTITIES;
using WEBAPI.Models;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;

namespace WEBAPI.Controllers
{
    public class MoneyRemittanceController : BaseApiController
    {
        #region DMT CUSTOMERS

        /// <summary>
        /// This api is used list of logedin users DMT Customer.
        /// </summary>
        /// <returns>return list of all downline users .</returns>
        /// <remarks></remarks>
        [HttpPost]
        [Authorize]
        [ActionName("GetDmtCustomers")]
        public HttpResponseMessage GetDmtCustomers()
        {
            GlobalVarible.Clear();

            List<ENT.DMT_CustomerRegisterApiView> lstEntity = new List<ENT.DMT_CustomerRegisterApiView>();

            try
            {
                using (BAL.DMT_CustomerRegister objBAL = new BAL.DMT_CustomerRegister())
                {
                    lstEntity = objBAL.GetAllApiCustomer(_LOGINUSERID);
                }

                GlobalVarible.AddMessage("User Customers Get Successfully.");
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { GlobalVarible.FormResult, lstEntity });
            }
            catch (Exception ex)
            {
                GlobalVarible.AddError(ex.Message);
                ERRORREPORTING.Report(ex, _REQUESTURL, _LOGINUSERID, _ERRORKEY, string.Empty);
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { GlobalVarible.FormResult });
            }
        }

        /// <summary>
        /// This api is used to register customer in our dmt panel.
        /// </summary>
        /// <remarks>If customer already registered with provider then it will just add to our customer list.</remarks>
        [HttpPost]
        [Authorize]
        [ActionName("RegisterCustomer")]
        [Route("api/MoneyRemittance/RegisterCustomer")]
        public HttpResponseMessage RegisterCustomer(ENT.DMT_CustomerRegister model)
        {
            GlobalVarible.Clear();
            registerresponse rr = new registerresponse();
            try
            {
                List<string> result = new List<string>();
                result = ValidateRequest(model);

                if (result.Count <= 0)
                {
                    model.CreatedBy = _LOGINUSERID;
                    model.dmt_userid = _LOGINUSERID;
                    model.CreatedDateTime = DateTime.Now;
                    model.dmt_customerid = Guid.NewGuid();

                    using (BAL.DMT_CustomerRegister objBAL = new BAL.DMT_CustomerRegister())
                    {
                        if (objBAL.Insert(model))
                        {
                            // api call for register customer in api
                            DMT dmt = new DMT(APICONSTANT.USERNAME, APICONSTANT.PASSWORD, APICONSTANT.URL);

                            GENERALRESPONSE RR = new GENERALRESPONSE();

                            CUSTOMERREGISTRATIONRESPONSE CR = new CUSTOMERREGISTRATIONRESPONSE();

                            // prepare post data object to check if customer is available or not
                            object postdata = new
                            {
                                CustomerMobile = model.dmt_mobile
                            };

                            string response = dmt.CheckUser(postdata, "DMT_CustomerValidate");

                            CR = JsonConvert.DeserializeObject<CUSTOMERREGISTRATIONRESPONSE>(response);

                            if (CR.Code == "224")
                            {
                                postdata = new
                                {
                                    FirstName = model.dmt_firstname,
                                    LastName = model.dmt_lastname,
                                    MobileNo = model.dmt_mobile,
                                    DateOfBirth = model.dmt_dateofbirth,
                                    Address = model.dmt_address,
                                    Pincode = model.dmt_pincode,
                                };

                                response = dmt.RegisterCustomer(postdata, "DMT_CustomerRegistration");

                                RR = new GENERALRESPONSE();

                                RR = JsonConvert.DeserializeObject<GENERALRESPONSE>(response);

                                if (RR.code == 0)
                                {
                                    Message message = JsonConvert.DeserializeObject<Message>(RR.message.ToString());

                                    // update remitter id of registered customer.
                                    model.dmt_requestno = message.data.remitter.id;
                                    objBAL.UpdateRemitter(model);

                                    rr.code = 1;
                                    rr.message = "Customer Added Successfully.Please verify Customer Now.";
                                    rr.customerid = model.dmt_customerid;
                                    GlobalVarible.AddMessage("SUCCESS");
                                }
                                else
                                {
                                    GlobalVarible.AddError(RR.message.ToString());
                                    // unable to register user so deleteing from our local database.
                                    objBAL.Delete(model);
                                }
                            }
                            else if (CR.Code == "1")
                            {
                                string message = CR.Message.ToString();
                                GlobalVarible.AddError(message);
                                // unable to register user so deleteing from our local database.
                                objBAL.Delete(model);
                            }
                            else
                            {
                                Message message = JsonConvert.DeserializeObject<Message>(CR.Message.ToString());

                                if (!String.IsNullOrEmpty(message.data.remitter.mobile))
                                {
                                    // update remitter id of registered customer.
                                    model.dmt_requestno = message.data.remitter.id;
                                    model.dmt_status = 1;
                                    objBAL.UpdateStatusWithRemitter(model);

                                    // add all beneficary which already registered.
                                    if (message.data.beneficiary.Count > 0)
                                    {
                                        List<ENT.DMT_BeneficiaryRegister> lstEntity = new List<ENT.DMT_BeneficiaryRegister>();

                                        foreach (BENEFICIARY B in message.data.beneficiary)
                                        {
                                            lstEntity.Add(new ENT.DMT_BeneficiaryRegister
                                            {
                                                dmt_beneficiaryid = Guid.NewGuid(),
                                                userid = _LOGINUSERID,
                                                dmt_beneficiaryname = B.name,
                                                dmt_beneficiarymobile = B.mobile,
                                                dmt_customerid = model.dmt_customerid,
                                                dmt_bankname = B.bank,
                                                dmt_ifsc = B.ifsc,
                                                dmt_accountnumber = B.account,
                                                dmt_address = string.Empty,
                                                dmt_addharcard = string.Empty,
                                                dmt_status = 1,
                                                dmt_requestno = B.id,
                                                dmt_response = string.Empty,
                                                dmt_pincode = string.Empty
                                            });
                                        }

                                        using (BAL.DMT_BeneficiaryRegister objBenificary = new BAL.DMT_BeneficiaryRegister())
                                        {
                                            objBenificary.BulkInsert(lstEntity);
                                        }
                                    }
                                    rr.code = 2;
                                    rr.message = "Customer Added Successfully.";
                                    rr.customerid = model.dmt_customerid;
                                    GlobalVarible.AddMessage("SUCCESS");
                                }
                                else {

                                    rr.code = 1;
                                    rr.message = "Customer Added Successfully.Please verify Customer Now.";
                                    rr.customerid = model.dmt_customerid;
                                    GlobalVarible.AddMessage("SUCCESS");
                                }
                            }
                            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { GlobalVarible.FormResult, rr });
                        }
                        else
                        {
                            GlobalVarible.AddError("Some error occured while saving the customer.");
                            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { GlobalVarible.FormResult, rr });
                        }
                    }
                }
                else
                {
                    GlobalVarible.AddErrors(result);
                    return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { GlobalVarible.FormResult, rr });
                }
            }
            catch (Exception ex)
            {
                GlobalVarible.AddError(ex.Message);
                ERRORREPORTING.Report(ex, _REQUESTURL, _LOGINUSERID, _ERRORKEY, string.Empty);
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { GlobalVarible.FormResult, rr });
            }
        }

        /// <summary>
        /// This api is used to verify the customer OTP.
        /// </summary>
        /// <remarks></remarks>
        [HttpPost]
        [Authorize]
        [ActionName("VerifyCustomer")]
        [Route("api/MoneyRemittance/VerifyCustomer")]
        public HttpResponseMessage VerifyCustomer(customerverificationmodel model)
        {
            GlobalVarible.Clear();
            try
            {
                DMT dmt = new DMT(APICONSTANT.USERNAME, APICONSTANT.PASSWORD, APICONSTANT.URL);

                OTPVALIDATIONRESPONSE OV = new OTPVALIDATIONRESPONSE();

                // get customer information by id
                using (BAL.DMT_CustomerRegister objBAL = new BAL.DMT_CustomerRegister())
                {
                    objBAL.Entity.dmt_customerid = model.dmt_customerid;
                    ENT.DMT_CustomerRegister customerRegister = (ENT.DMT_CustomerRegister)objBAL.GetByPrimaryKey(objBAL.Entity);

                    var postdata = new
                    {
                        RemitterId = customerRegister.dmt_requestno,
                        OTP = model.verificationotp,
                        CustomerMobile = customerRegister.dmt_mobile
                    };

                    string response = dmt.ValidateCustomerOTP(postdata, "DMT_OTP_Customer_Confirmation");

                    OV = JsonConvert.DeserializeObject<OTPVALIDATIONRESPONSE>(response);

                    if (OV.code == 0)
                    {
                        objBAL.Entity.dmt_status = 1;
                        objBAL.UpdateStatus(objBAL.Entity);

                        GlobalVarible.AddMessage("Customer Verified Successfully.");
                    }
                    else
                    {
                        GlobalVarible.AddError(OV.message.ToString());
                    }
                }
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { GlobalVarible.FormResult });
            }
            catch (Exception ex)
            {
                GlobalVarible.AddError(ex.Message);
                ERRORREPORTING.Report(ex, _REQUESTURL, _LOGINUSERID, _ERRORKEY, string.Empty);
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { GlobalVarible.FormResult });
            }
        }
        #endregion

        #region BENIFICARY

        /// <summary>
        /// This api is used list of logedin users DMT Customer.
        /// </summary>
        /// <returns>return list of all downline users .</returns>
        /// <remarks></remarks>
        [HttpPost]
        [Authorize]
        [ActionName("GetBenificary")]
        public HttpResponseMessage GetBenificary(GetBenificaryModel model)
        {
            GlobalVarible.Clear();

            List<ENT.DMT_BeneficiaryRegisterApiView> lstEntity = new List<ENT.DMT_BeneficiaryRegisterApiView>();

            try
            {
                using (BAL.DMT_BeneficiaryRegister objBAL = new BAL.DMT_BeneficiaryRegister())
                {
                    lstEntity = objBAL.GetBenificarybyCustomerForApi(model.customerid);
                }

                GlobalVarible.AddMessage("User Customers Get Successfully.");
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { GlobalVarible.FormResult, lstEntity });
            }
            catch (Exception ex)
            {
                GlobalVarible.AddError(ex.Message);
                ERRORREPORTING.Report(ex, _REQUESTURL, _LOGINUSERID, _ERRORKEY, string.Empty);
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { GlobalVarible.FormResult });
            }
        }

        #endregion

        #region internal validation methods
        internal List<String> ValidateRequest(ENT.DMT_CustomerRegister model)
        {
            List<string> result = new List<string>();

            if (!model.dmt_mobile.ValidateMobile())
            {
                result.Add("Please add mobile number in proper format.");
            }

            return result;
        }
        #endregion
    }
}

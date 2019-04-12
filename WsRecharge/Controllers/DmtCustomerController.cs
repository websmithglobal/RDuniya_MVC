using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ENT = Web.Framework.Entity;
using COM = Web.Framework.Common;
using BAL = Web.Framework.BusinessLayer;
using Web.Framework.Common;
using WEBRECHARGE;
using WsRecharge.WEBRECHARGE.ENTITIES;
using Newtonsoft.Json;
using WsRecharge.Models;
using WsRecharge.WEBRECHARGE;

namespace WsRecharge.Controllers
{
    [Authorize]
    public class DmtCustomerController : BaseController
    {
        BAL.DMT_CustomerRegister objBAL = new BAL.DMT_CustomerRegister();

        // GET: DmtCustomer
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public JsonResult GetAll()
        {
            List<ENT.DMT_CustomerRegisterAdminView> lstEntity = new List<ENT.DMT_CustomerRegisterAdminView>();

            //jQuery DataTables Param
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            //Find paging info
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            //Find order columns info

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt16(start) : 1;
            skip = (skip / pageSize) + 1;
            COM.TTPagination.isPageing = true;
            COM.TTPagination.PageSize = pageSize;
            COM.TTPagination.PageNo = Convert.ToInt64(skip);

            lstEntity = objBAL.GetAllCustomer(_LoginUserId);

            foreach (ENT.DMT_CustomerRegisterAdminView c in lstEntity)
            {
                c.dmt_transferlimit = 25000;
                c.dmt_walletbalance = 25000;
            }

            return Json(new
            {
                draw = draw,
                recordsTotal = lstEntity.Count(),
                recordsFiltered = COM.TTPagination.RecordCount,
                data = lstEntity
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public JsonResult SaveEntry(ENT.DMT_CustomerRegister model)
        {
            // in this method we return one json with two properties.with code and message.
            // if code is 1 then we have to open box for otp verification.

            registerresponse rr = new registerresponse();

            try
            {
                List<string> result = new List<string>();

                result = ValidateRequest(model);

                if (result.Count <= 0)
                {
                    if (model.EntryMode == COM.Enumration.EntryMode.ADD)
                    {
                        model.CreatedBy = _LoginUserId;
                        model.dmt_userid = _LoginUserId;
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
                                                    userid = _LoginUserId,
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
                                }
                            }
                            else
                            {
                                GlobalVarible.AddError("Some error occured while saving the customer.");
                            }
                        }
                    }
                    else
                    {
                        model.UpdatedDateTime = DateTime.Now;
                        model.UpdatedBy = _LoginUserId;
                        GlobalVarible.AddMessage("Customer Updated Successfully.");
                    }
                }
                else
                {
                    GlobalVarible.AddErrors(result);
                }
            }
            catch (Exception ex)
            {
                GlobalVarible.AddError(ex.Message);
            }
            MySession.Current.MessageResult.MessageHtml = GlobalVarible.GetMessageHTML();
            return Json(new { MySession.Current.MessageResult, rr }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public JsonResult VerifyCustomer(customerverificationmodel model)
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
            }
            catch (Exception ex)
            {
                GlobalVarible.AddError(ex.Message);
            }
            MySession.Current.MessageResult.MessageHtml = GlobalVarible.GetMessageHTML();
            return Json(MySession.Current.MessageResult, JsonRequestBehavior.AllowGet);
        }

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
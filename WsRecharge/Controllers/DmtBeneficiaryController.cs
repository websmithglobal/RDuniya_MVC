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
using Microsoft.AspNet.Identity;

namespace WsRecharge.Controllers
{
    public class DmtBeneficiaryController : BaseController
    {
        // GET: DmtBeneficiary
        public ActionResult Index(Guid id)
        {
            ENT.DMT_CustomerRegister customer = null;

            using (BAL.DMT_CustomerRegister customerBAL = new BAL.DMT_CustomerRegister())
            {
                customerBAL.Entity.dmt_customerid = id;
                customer = (ENT.DMT_CustomerRegister)customerBAL.GetByPrimaryKey(customerBAL.Entity);
            }

            ViewBag.CUSTOMERID = customer.dmt_customerid;
            ViewBag.CUSTOMERNAME = customer.dmt_firstname + " " + customer.dmt_lastname + " - " + customer.dmt_mobile;
            return View();
        }

        [HttpPost]
        [Authorize]
        public JsonResult GetAll(Guid userguid)
        {
            BAL.DMT_BeneficiaryRegister objBAL = new BAL.DMT_BeneficiaryRegister();

            List<ENT.DMT_BeneficiaryRegisterAdminView> lstEntity = new List<ENT.DMT_BeneficiaryRegisterAdminView>();

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

            lstEntity = objBAL.GetBenificarybyCustomer(userguid);

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
        public JsonResult SaveEntry(ENT.DMT_BeneficiaryRegister model)
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
                        model.userid = _LoginUserId;
                        model.CreatedDateTime = DateTime.Now;
                        model.dmt_beneficiaryid = Guid.NewGuid();

                        using (BAL.DMT_BeneficiaryRegister objBAL = new BAL.DMT_BeneficiaryRegister())
                        {
                            if (objBAL.Insert(model))
                            {
                                // api call for register benificary in api
                                DMT dmt = new DMT(APICONSTANT.USERNAME, APICONSTANT.PASSWORD, APICONSTANT.URL);

                                GENERALRESPONSE RR = new GENERALRESPONSE();

                                BENEFICIARYREGISTRATIONRESPONSE BR = new BENEFICIARYREGISTRATIONRESPONSE();

                                ENT.DMT_CustomerRegister customer = null;

                                using (BAL.DMT_CustomerRegister customerBAL = new BAL.DMT_CustomerRegister())
                                {
                                    customerBAL.Entity.dmt_customerid = model.dmt_customerid;
                                    customer = (ENT.DMT_CustomerRegister)customerBAL.GetByPrimaryKey(customerBAL.Entity);
                                }

                                // prepare post data object to check if beneficiary is available
                                object postdata = new
                                {
                                    FirstName = model.dmt_beneficiaryname,
                                    LastName = model.dmt_beneficiaryname,
                                    BeneficiaryMobile = model.dmt_beneficiarymobile,
                                    CustomerMobile = customer.dmt_mobile,
                                    BankName = model.dmt_bankname,
                                    IfscCode = model.dmt_ifsc,
                                    AccountNumber = model.dmt_accountnumber,
                                    BranchName = model.dmt_branchname,
                                    Address = model.dmt_address,
                                    AddharCard = model.dmt_addharcard,
                                    Pincode = model.dmt_pincode,
                                    RemitterId = customer.dmt_requestno
                                };

                                string response = dmt.RegisterBenificary(postdata, "DMT_BeneficiaryRegistration");

                                BR = JsonConvert.DeserializeObject<BENEFICIARYREGISTRATIONRESPONSE>(response);

                                if (BR.Code == 0)
                                {
                                    if (BR.Message.data.beneficiary.id > 0)
                                    {
                                        model.dmt_status = 1;
                                        model.dmt_requestno = BR.Message.data.beneficiary.id.ToString();

                                        objBAL.UpdateStatusWithId(model);

                                        rr.code = 2;
                                        rr.message = "Benificary Added Successfully.";
                                        rr.customerid = model.dmt_beneficiaryid;

                                        GlobalVarible.AddMessage("SUCCESS");
                                    }
                                }
                                else if (BR.Code == 1)
                                {
                                    GlobalVarible.AddError(BR.Message.ToString());
                                }
                            }
                            else
                            {
                                GlobalVarible.AddError("Some error occured while saving the benificary.");
                            }
                        }
                    }
                    else
                    {
                        model.UpdatedDateTime = DateTime.Now;
                        model.UpdatedBy = _LoginUserId;
                        GlobalVarible.AddMessage("Benificary Updated Successfully.");
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
        public JsonResult DeleteBeneficary(Guid benificaryid)
        {
            registerresponse rr = new registerresponse();
            try
            {
                using (BAL.DMT_BeneficiaryRegister objBAL = new BAL.DMT_BeneficiaryRegister())
                {
                    ENT.DMT_BeneficiaryRegisterAdminView BR = objBAL.GetBenificarybyId(benificaryid);

                    if (BR != null)
                    {
                        DMT dmt = new DMT(APICONSTANT.USERNAME, APICONSTANT.PASSWORD, APICONSTANT.URL);

                        GENERALRESPONSE GR = new GENERALRESPONSE();

                        object postdata = new
                        {
                            CustomerMobile = BR.dmt_beneficiarymobile,
                            BeneficiaryCode = BR.dmt_requestno,
                            RemitterId = BR.dmt_remitterId
                        };

                        string response = dmt.DeleteBeneficary(postdata, "DMT_DeleteBeneficiary");

                        GR = JsonConvert.DeserializeObject<GENERALRESPONSE>(response);

                        if (GR.code == 0)
                        {
                            if (GR.message.ToString().ToUpper() == "OTC SEND SUCCESSFULLY")
                            {
                                rr.message = GR.message.ToString().ToUpper();
                                rr.code = 1;
                                rr.customerid = BR.dmt_beneficiaryid;
                            }
                            GlobalVarible.AddMessage("SUCCESS");
                        }
                        else
                        {
                            GlobalVarible.AddError(GR.message.ToString());
                        }
                    }
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
        public JsonResult DeleteBeneficiaryConfirm(customerverificationmodel model)
        {
            GlobalVarible.Clear();
            try
            {
                DMT dmt = new DMT(APICONSTANT.USERNAME, APICONSTANT.PASSWORD, APICONSTANT.URL);

                OTPVALIDATIONRESPONSE OV = new OTPVALIDATIONRESPONSE();

                // get customer information by id
                using (BAL.DMT_BeneficiaryRegister objBAL = new BAL.DMT_BeneficiaryRegister())
                {
                    ENT.DMT_BeneficiaryRegisterAdminView BR = objBAL.GetBenificarybyId(model.dmt_customerid);

                    //ENT.DMT_BeneficiaryRegisterAdminView BR = objBAL.GetBenificarybyId(Guid.Parse("1E2C4B19-1229-4ECE-9C04-464AA8907ADC"));

                    var postdata = new
                    {
                        BeneficiaryCode = BR.dmt_requestno,
                        RemitterId = BR.dmt_remitterId,
                        OTP = model.verificationotp,
                        CustomerMobile = BR.dmt_requestno
                    };

                    string response = dmt.ValidateCustomerOTP(postdata, "DMT_DeleteBeneficiaryConfirm");

                    OV = JsonConvert.DeserializeObject<OTPVALIDATIONRESPONSE>(response);

                    if (OV.code == 0)
                    {
                        objBAL.Entity.dmt_beneficiaryid = model.dmt_customerid;

                        objBAL.Delete(objBAL.Entity);

                        GlobalVarible.AddMessage("Beneficiary Deleted Successfully.");
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

        [HttpPost]
        [Authorize]
        public JsonResult TransferMoney(moneytransfermodel model)
        {
            GlobalVarible.Clear();
            try
            {
                // check for user password
                if (checkpassword(model.password))
                {
                    using (BAL.DMT_MoneyRemittance objTranfer = new BAL.DMT_MoneyRemittance())
                    {
                        // check balance in local.

                        MEMBERS.SQLReturnMessageNValue retVal = objTranfer.DMT_VALIDATE_Transaction(_LoginUserId.ToString(), model.transferamount.ToString());


                        if (retVal.Outval == 1)
                        {
                            DMT dmt = new DMT(APICONSTANT.USERNAME, APICONSTANT.PASSWORD, APICONSTANT.URL);

                            GENERALRESPONSE GR = new GENERALRESPONSE();

                            ENT.DMT_BeneficiaryRegisterAdminView BD = new ENT.DMT_BeneficiaryRegisterAdminView();

                            ENT.DMT_CustomerRegisterAdminView CD = new ENT.DMT_CustomerRegisterAdminView();

                            // get customer information by id
                            using (BAL.DMT_BeneficiaryRegister objBAL = new BAL.DMT_BeneficiaryRegister())
                            {
                                BD = objBAL.GetBenificarybyId(model.benificaryid);
                            }

                            using (BAL.DMT_CustomerRegister objBAL = new BAL.DMT_CustomerRegister())
                            {
                                CD = objBAL.GetCustomerById(model.customerid);
                            }

                            var postdata = new
                            {
                                BeneficiryCode = BD.dmt_requestno.ToString(),
                                BeneficiryMobile = BD.dmt_beneficiarymobile.ToString(),
                                CustomerMobile = CD.dmt_mobile.ToString(),
                                IFSC = BD.dmt_ifsc.ToString(),
                                AccountNo = BD.dmt_accountnumber.ToString(),
                                RountingType = model.transfertype.ToString(),
                                BeneficiaryName = BD.dmt_beneficiaryname.ToString(),
                                Remakrs = model.remarks.ToString(),
                                Amount = model.transferamount,
                                Mode = "WEB",
                                RemitterId = CD.dmt_requestno.ToString(),
                            };

                            string response = dmt.TransferAmount(postdata, "DMT_MoneyRemittance");

                            GR = JsonConvert.DeserializeObject<GENERALRESPONSE>(response);

                            if (GR.code == 0)
                            {
                                // amount credited successfully then calculate the commission and charge.

                                objTranfer.Entity.userid = _LoginUserId;
                                objTranfer.Entity.mt_beneficiarycode = BD.dmt_requestno;
                                objTranfer.Entity.mt_BeneficiryMobile = BD.dmt_beneficiarymobile;
                                objTranfer.Entity.mt_customermobile = CD.dmt_mobile;
                                objTranfer.Entity.mt_ifsc = BD.dmt_ifsc;
                                objTranfer.Entity.mt_accountnumber = BD.dmt_accountnumber;
                                objTranfer.Entity.mt_routingtype = model.transfertype;
                                objTranfer.Entity.mt_BeneficiaryName = BD.dmt_beneficiaryname;
                                objTranfer.Entity.mt_remarks = model.remarks;
                                objTranfer.Entity.mt_amount = (decimal)model.transferamount;
                                objTranfer.Entity.mt_mode = "WEB";
                                objTranfer.Entity.mt_Ipaddress = GlobalVarible.GETIPADDRESS();
                                objTranfer.Entity.mt_RemitterId = CD.dmt_requestno;
                                objTranfer.Entity.mt_RequestNo = string.Empty;
                                objTranfer.Entity.mt_Response = response;

                                retVal = objTranfer.DMT_REMITTANCE(objTranfer.Entity);

                                if (retVal.Outval == 1)
                                {
                                    GlobalVarible.AddMessage("Amount Transfered Successfully.");
                                }
                                else
                                {
                                    GlobalVarible.AddError(retVal.Outmsg);
                                }
                            }
                            else
                            {
                                GlobalVarible.AddError(GR.message.ToString());
                            }
                        }
                        else
                        {
                            GlobalVarible.AddError(retVal.Outmsg);
                        }
                    }
                }
                else
                {
                    GlobalVarible.AddError("Invalid User Password.");
                }
            }
            catch (Exception ex)
            {
                GlobalVarible.AddError(ex.Message);
            }
            MySession.Current.MessageResult.MessageHtml = GlobalVarible.GetMessageHTML();
            return Json(MySession.Current.MessageResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public JsonResult GetTransferCharge(moneytransfermodel model)
        {
            GlobalVarible.Clear();

            var loginuser = User.Identity.GetUserId();

            string transferCharge = "-1";

            try
            {
                using (BAL.DMT_MoneyRemittance objTranfer = new BAL.DMT_MoneyRemittance())
                {
                    MEMBERS.SQLReturnMessageNValue retVal = objTranfer.DMT_GetCharge(model.transferamount.ToString(),_LoginUserId, (int)User.GetUserlevel());
                    if (retVal.Outval == 1)
                    {
                        transferCharge = retVal.Outmsg;
                        GlobalVarible.AddMessage("Transfer Charge Get Successfully.");
                    }
                    else {
                        GlobalVarible.AddError(retVal.Outmsg);
                    }
                }
            }
            catch (Exception ex)
            {
                GlobalVarible.AddError(ex.Message);
            }
            return Json(new { MySession.Current.MessageResult, transferCharge }, JsonRequestBehavior.AllowGet);
        }

        #region internal validation methods
        internal List<String> ValidateRequest(ENT.DMT_BeneficiaryRegister model)
        {
            List<string> result = new List<string>();

            if (!model.dmt_beneficiarymobile.ValidateMobile())
            {
                result.Add("Please add mobile number in proper format.");
            }

            return result;
        }
        #endregion
    }
}
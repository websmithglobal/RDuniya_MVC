using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ENT = Web.Framework.Entity;
using COM = Web.Framework.Common;
using BAL = Web.Framework.BusinessLayer;

namespace WsRecharge.Controllers
{
    [Authorize]
    public class DmtDocumentsController : BaseController
    {
        BAL.DMT_Documents objBAL = new BAL.DMT_Documents();
        // GET: DmtDocuments
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SaveEntry(string backimage, string frontimage, int dd_type)
        {
            ENT.DMT_Documents model = new ENT.DMT_Documents();

            try
            {
                if (String.IsNullOrEmpty(frontimage))
                {
                    GlobalVarible.AddError("Please select Front Image.");
                    return Json(MySession.Current.MessageResult);
                }
                else
                {
                    model.dd_page1 = SaveDmtDocument(frontimage);
                }

                if (String.IsNullOrEmpty(backimage))
                {
                    model.dd_page2 = string.Empty;
                }
                else
                {
                    model.dd_page2 = SaveDmtDocument(backimage);
                }

                model.dd_status = (int)COM.MyEnumration.DMTDOCUMENTSTATUS.PENDING;
                model.dd_remarks = string.Empty;
                model.CreatedDateTime = DateTime.Now;
                model.CreatedBy = _LoginUserId;
                model.dd_userid = _LoginUserId;
                model.dd_type = dd_type;

                using (BAL.DMT_Documents objBAL = new BAL.DMT_Documents())
                {
                    if (objBAL.Insert(model))
                    {
                        GlobalVarible.AddMessage("User document saved successfully.");
                    }
                    else
                    {
                        GlobalVarible.AddError("Unable to save user document");
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

        [HttpGet]
        public JsonResult GetStatus()
        {
            int status = 0;
            ENT.DMT_Documents dMT_Documents = null;
            using (BAL.DMT_Documents objBal = new BAL.DMT_Documents())
            {
                dMT_Documents = objBal.GetByUserId(_LoginUserId);
            }

            if (dMT_Documents == null)
            {
                status = 1;
            }
            else
            {
                if (dMT_Documents.dd_status == 1)
                {
                    status = 2;
                }
                else if (dMT_Documents.dd_status == 2)
                {
                    status = 3;
                }
                else
                {
                    status = 4;
                }
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UserRequest()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public JsonResult GetAll()
        {
            List<ENT.DMT_DocumentsView> lstEntity = new List<ENT.DMT_DocumentsView>();

            //jQuery DataTables Param
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            //Find paging info
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            //Find order columns info

            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][data]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            var search = Request.Form.Get("search[value]").FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt16(start) : 1;
            skip = (skip / pageSize) + 1;
            COM.TTPagination.isPageing = true;
            COM.TTPagination.PageSize = pageSize;
            COM.TTPagination.PageNo = Convert.ToInt64(skip);

            lstEntity = objBAL.GetAllBySearch();

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
        public JsonResult ApproveRequest(string id)
        {
            GlobalVarible.Clear();
            try
            {
                if (id != null)
                {
                    ENT.DMT_Documents Entity = new ENT.DMT_Documents();

                    using (BAL.DMT_Documents objBAL = new BAL.DMT_Documents())
                    {
                        objBAL.Entity.dd_id = Guid.Parse(id);
                        Entity = (ENT.DMT_Documents)objBAL.GetByPrimaryKey(objBAL.Entity);

                        objBAL.Entity.dd_status = (int)COM.MyEnumration.DMTDOCUMENTSTATUS.APPROVED;
                        objBAL.Entity.UpdatedBy = _LoginUserId;
                        objBAL.Entity.UpdatedDateTime = DateTime.Now;

                        if (objBAL.UpdateApprove(objBAL.Entity))
                        {
                            using (BAL.DMT_Approval dMT_Approval = new BAL.DMT_Approval())
                            {
                                dMT_Approval.Entity.approval_userid = Entity.dd_userid;
                                dMT_Approval.Entity.approval_status = (int)COM.MyEnumration.DMTAPPROVALSTATUS.APPROVED;
                                dMT_Approval.Entity.CreatedBy = _LoginUserId;
                                dMT_Approval.Entity.CreatedDateTime = DateTime.Now;
                                if (dMT_Approval.Insert(dMT_Approval.Entity))
                                {
                                    GlobalVarible.AddMessage("Request Approved Successfully.");
                                }
                            }
                            GlobalVarible.AddMessage("Request Approved Successfully.");
                        }
                        else
                        {
                            GlobalVarible.AddMessage("Unable to Approve Request.");
                        }
                    }
                }
                else
                {
                    GlobalVarible.AddMessage("Unable Approve Request.");
                }
            }
            catch (Exception ex)
            {
                GlobalVarible.AddError(ex.Message);
            }
            MySession.Current.MessageResult.MessageHtml = GlobalVarible.GetMessageHTML();
            return Json(MySession.Current.MessageResult, JsonRequestBehavior.AllowGet);
        }
    }
}
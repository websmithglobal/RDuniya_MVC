using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ENT = Web.Framework.Entity;
using COM = Web.Framework.Common;
using BAL = Web.Framework.BusinessLayer;
using Microsoft.AspNet.Identity;
using WsRecharge.Models;
using DAL = Web.Framework.DataLayer;

namespace WsRecharge.Controllers
{
    [Authorize]
    public class RechargeSlabController : BaseController
    {

        BAL.RechargeSlab objBAL = new BAL.RechargeSlab();
        BAL.SlabCommission objBAL2 = new BAL.SlabCommission();
      
        // GET: RechargeSlab
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Index(Guid id)
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public JsonResult SaveEntry(ENT.RechargeSlab model, string slabid)
        {
            try
            {
                if (model.EntryMode == COM.Enumration.EntryMode.ADD)
                {
                    model.Status = COM.MyEnumration.MyStatus.Active;
                    if (objBAL.Insert(model))
                    {
                        GlobalVarible.AddMessage("Record Save Successfully");
                    }
                }
                else
                {
                    model.UpdatedDateTime = DateTime.Now;
                    model.UpdatedBy = Guid.Parse(User.Identity.GetUserId());
                    model.slabid = new Guid(slabid.Replace("/", ""));
                    if (objBAL.UpdatePartial(model))
                    {
                        GlobalVarible.AddMessage("Record Update Successfully");
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
        public JsonResult GetSlab()
        {
            List<ENT.RechargeSlab> lstResult = new List<Web.Framework.Entity.RechargeSlab>();
            BAL.RechargeSlab objBALV = new BAL.RechargeSlab();
            lstResult = objBALV.GetAllSlab();
            return Json(lstResult, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [Authorize]
        public JsonResult GetAll(string slabid)
        {
            List<ENT.SlabCommission> lstEntity = new List<ENT.SlabCommission>();
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
            lstEntity = objBAL2.GetAll(Guid.Parse(slabid));
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
        public JsonResult SaveAll(String[] hfoperatorcode, String[] md, String[] sd, String[] r, String[] charge, Guid rechargeslabid2)
        {

            GlobalVarible.Clear();
            try
            {
                if (hfoperatorcode == null || md == null || sd == null || r == null || charge == null)
                {
                    GlobalVarible.AddError("please add pricing details!");
                    return Json(MySession.Current.MessageResult, JsonRequestBehavior.AllowGet);
                }
                if (hfoperatorcode.Length > 0)
                {

                    bool outval = new BAL.SlabCommission().DeleteByID(rechargeslabid2);

                    List<ENT.SlabCommission> priceList = new List<ENT.SlabCommission>();
                    for (int sc = 0; sc < hfoperatorcode.Length; sc++)
                    {
                        if ((md[sc].ToString() != "0") || (sd[sc].ToString() != "0") || (r[sc].ToString() != "0") || (charge[sc].ToString() != "0"))
                        {
                            ENT.SlabCommission pdmodel = new Web.Framework.Entity.SlabCommission();
                            pdmodel.rechargeslabid = Guid.NewGuid();
                            pdmodel.CreatedDateTime = DateTime.Now;
                            pdmodel.CreatedBy = Guid.Parse(User.Identity.GetUserId());
                            pdmodel.slabid = rechargeslabid2;
                            pdmodel.operatorcode = int.Parse(hfoperatorcode[sc]);
                            pdmodel.md = decimal.Parse(md[sc]);
                            pdmodel.sd = decimal.Parse(sd[sc]);
                            pdmodel.r = decimal.Parse(r[sc]);
                            pdmodel.charge = decimal.Parse(charge[sc]);
                            priceList.Add(pdmodel);
                        }
                    }
                    using (BAL.SlabCommission objPrice = new BAL.SlabCommission())
                    {
                        int Val = objPrice.BulkInsert(priceList);
                        if (Val > 0)
                        {
                            GlobalVarible.AddMessage("Record Save Successfully.");
                        }
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
    }
}
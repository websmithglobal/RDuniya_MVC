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

namespace WsRecharge.Controllers
{
    [Authorize]
    public class DMTSlabCommiController : BaseController
    {
        BAL.DMTSlabCommi objBAL = new BAL.DMTSlabCommi();
        // GET: DMTSlabCommi
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public JsonResult SaveEntry(ENT.DMTSlabCommi model, string dmtslabid)
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
                    model.dmtslabid = new Guid(dmtslabid.Replace("/", ""));
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
            List<ENT.DMTSlabCommi> lstResult = new List<Web.Framework.Entity.DMTSlabCommi>();
            BAL.DMTSlabCommi objBALV = new BAL.DMTSlabCommi();
            lstResult = objBALV.GetAllSlab();
            return Json(lstResult, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveCommision(String[] hffrom, String[] hfto, String[] hfmd, String[] hfsd, String[] hfrd, String[] hftype, Guid up_dmtslabid)
        {
            GlobalVarible.Clear();
            try
            {
                if (hffrom == null || hfto == null || hfmd == null || hfsd == null || hfrd == null || hftype == null)
                {
                    GlobalVarible.AddError("please add pricing details for all level !");
                    return Json(MySession.Current.MessageResult, JsonRequestBehavior.AllowGet);
                }
                if (hffrom.Length > 0)
                {
                    List<ENT.DMT_SlabCommission> priceList = new List<ENT.DMT_SlabCommission>();
                    for (int sc = 0; sc < hffrom.Length; sc++)
                    {
                        ENT.DMT_SlabCommission pdmodel = new Web.Framework.Entity.DMT_SlabCommission();
                        pdmodel.dmtslabcommid = Guid.NewGuid();
                        pdmodel.CreatedDateTime = DateTime.Now;
                        pdmodel.CreatedBy = Guid.Parse(User.Identity.GetUserId());
                        pdmodel.dmtslabid = up_dmtslabid;
                        pdmodel.dmtfromval = decimal.Parse(hffrom[sc]);
                        pdmodel.dmttoval = decimal.Parse(hfto[sc]);
                        pdmodel.dmtmd = decimal.Parse(hfmd[sc]);
                        pdmodel.dmtsd = decimal.Parse(hfsd[sc]);
                        pdmodel.dmtrd = decimal.Parse(hfrd[sc]);
                        pdmodel.dmttype = int.Parse(hftype[sc]);
                        priceList.Add(pdmodel);
                    }
                    using (BAL.DMT_SlabCommission objPrice = new BAL.DMT_SlabCommission())
                    {
                        int Val = objPrice.BulkInsert(priceList);
                        if (Val > 0)
                        {
                            GlobalVarible.AddMessage("Dmt Commission Saved Successfully.");
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

        [HttpPost]
        public JsonResult GetSlabByID(string id)
        {
            List<ENT.DMT_SlabCommission> lstResult = new List<Web.Framework.Entity.DMT_SlabCommission>();
            BAL.DMT_SlabCommission objBALV = new BAL.DMT_SlabCommission();
            if (id != null)
            {
                Guid Byid = new Guid(id);
                lstResult = objBALV.GetSlabBYID(Byid);
            }
            return Json(lstResult, JsonRequestBehavior.AllowGet);
        }
    }
}
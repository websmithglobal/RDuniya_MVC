using System;
using System.Collections.Generic;
using ENT = Web.Framework.Entity;
using BAL = Web.Framework.BusinessLayer;

public static class GlobalVarible
{
    //public static List<ENT.PincodeMaster> lstPincode = new BAL.PincodeMaster().GetListWithParent();
    //public static List<ENT.VendorFileTypes> lstVendorFileType = new ENT.VendorFileTypes().GetList();

    public static string GetMessage()
    {
        try
        {
            if (MySession.Current.MessageResult.Message.Count != 0)
            {
                string strResult = "";
                if (!MySession.Current.MessageResult.isReadData)
                {
                    string errorlist = "<ul>";
                    foreach (string str in MySession.Current.MessageResult.Message)
                    {
                        errorlist += string.Format("<li>{0}</li>", str);
                    }
                    errorlist += "</ul>";
                    if (MySession.Current.MessageResult.EntryStatus)
                    {
                        strResult = string.Format("<div class='alert alert-success'><a href ='#' class='close' data-dismiss='alert' aria-label='close'></a>{0}</div>", errorlist);
                    }
                    else
                    {
                        strResult = string.Format("<div class='alert alert-danger'><a href ='#' class='close' data-dismiss='alert' aria-label='close'></a>{0}</div>", errorlist);
                    }
                }
                return strResult;
            }
            else { return string.Empty; }
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
        finally
        {
            Clear();
        }
    }

    public static string GetMessageHTML()
    {
        try
        {
            if (MySession.Current.MessageResult.Message.Count != 0)
            {
                string strResult = "";
                if (!MySession.Current.MessageResult.isReadData)
                {
                    string errorlist = "<ul>";
                    foreach (string str in MySession.Current.MessageResult.Message)
                    {
                        errorlist += string.Format("<li>{0}</li>", str);
                    }
                    errorlist += "</ul>";
                    if (MySession.Current.MessageResult.EntryStatus)
                    {
                        strResult = string.Format("<div class='alert alert-success'><a href ='#' class='close' data-dismiss='alert' aria-label='close'></a>{0}</div>", errorlist);
                    }
                    else
                    {
                        strResult = string.Format("<div class='alert alert-danger'><a href ='#' class='close' data-dismiss='alert' aria-label='close'></a>{0}</div>", errorlist);
                    }
                }
                return strResult;
            }
            else { return string.Empty; }
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public static void Clear()
    {
        MySession.Current.MessageResult.isReadData = true;
        MySession.Current.MessageResult.EntryStatus = false;
        MySession.Current.MessageResult.Message.Clear();
    }

    public static void AddError(string Error)
    {
        MySession.Current.MessageResult.isReadData = false;
        MySession.Current.MessageResult.EntryStatus = false;
        MySession.Current.MessageResult.Message.Add(Error);
        MySession.Current.MessageResult.MessageHtml = GetMessageHTML();
    }
    public static void AddErrors(List<string> Error)
    {
        MySession.Current.MessageResult.isReadData = false;
        MySession.Current.MessageResult.EntryStatus = false;
        foreach (var item in Error)
        {
            MySession.Current.MessageResult.Message.Add(item);
        }
        MySession.Current.MessageResult.MessageHtml = GetMessageHTML();
    }

    public static void AddMessage(string Error)
    {
        MySession.Current.MessageResult.isReadData = false;
        MySession.Current.MessageResult.EntryStatus = true;
        MySession.Current.MessageResult.Message.Add(Error);
        MySession.Current.MessageResult.MessageHtml = GetMessageHTML();
    }
    public static void LogError(ENT.ErrorLog errorlog)
    {
        using (BAL.ErrorLog objError = new Web.Framework.BusinessLayer.ErrorLog())
        {
            objError.Insert(errorlog);
        }
    }

    public static string GETIPADDRESS()
    {
        string strIpAddress;
        strIpAddress = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (strIpAddress == null)
        {
            strIpAddress = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }
        return strIpAddress;
    }

}

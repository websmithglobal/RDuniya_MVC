using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web;
using ENT = Web.Framework.Entity;
using BAL = Web.Framework.BusinessLayer;
using System.Net;
using System.IO;


public static class GlobalVarible
{
    public static FormResultEntity FormResult = new FormResultEntity();

    public static string GetMessage()
    {
        try
        {
            if (FormResult.Message.Count != 0)
            {
                string strResult = "";
                if (!FormResult.EntryStatus)
                {
                    string errorlist = "<ul>";
                    foreach (string str in FormResult.Message)
                    {
                        errorlist += string.Format("<li>{0}</li>", str);
                    }
                    errorlist += "</ul>";
                    if (FormResult.EntryStatus)
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
            if (FormResult.Message.Count != 0)
            {
                string strResult = "";
                if (!FormResult.EntryStatus)
                {
                    string errorlist = "<ul>";
                    foreach (string str in FormResult.Message)
                    {
                        errorlist += string.Format("<li>{0}</li>", str);
                    }
                    errorlist += "</ul>";
                    if (FormResult.EntryStatus)
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
        FormResult.EntryStatus = false;
        FormResult.Message.Clear();
    }

    public static void AddError(string Error)
    {
        FormResult.EntryStatus = false;
        FormResult.isExecption = true;
        FormResult.Message.Add(Error);
    }

    public static void AddMessage(string Error)
    {
        FormResult.EntryStatus = true;
        FormResult.isExecption = false;
        FormResult.Message.Add(Error);
    }

    public static void AddErrors(List<string> Error)
    {
        FormResult.EntryStatus = false;
        FormResult.isExecption = true;

        foreach (var item in Error)
        {
            FormResult.Message.Add(item);
        }
    }

    public static void SendMessage(string mobile , string message)
    {
        String messagerequest = "http://sms.jajasms.com/submitsms.jsp?user=shreemkt&key=33aaa719e5XX&mobile="+mobile+"&message="+message+"&senderid=WSMITH&accusage=1";
        var request = (HttpWebRequest)WebRequest.Create(messagerequest);
        var response = (HttpWebResponse)request.GetResponse();
        var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
    }
}

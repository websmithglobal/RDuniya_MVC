using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using ENT = Web.Framework.Entity;

[Serializable]
public class MySession
{
    // private constructor
    private MySession()
    {
        Username = null;
        PasswordChange = null;
        UserFullname = null;
        MessageResult = new FormResultEntity();
       
        UserProfile = new Web.Framework.Entity.UserProfile();
        ExportVendor = new GridView();
        ExportCollection = new GridView();
        ExportVoucher = new GridView();
    }

    // Gets the current session.
    public static MySession Current
    {
        get
        {
            MySession session =
              (MySession)HttpContext.Current.Session["__MySession__"];
            if (session == null)
            {
                session = new MySession();
                HttpContext.Current.Session["__MySession__"] = session;
            }
            return session;
        }
    }

    // **** add your session properties here, e.g like this:

    public string Username { get; set; }
    public string PasswordChange { get; set; }
    public FormResultEntity MessageResult { get; set; }
    public string UserFullname { get; set; }
    
    public ENT.UserProfile UserProfile { get; set; }
    public GridView ExportVendor { get; set; }
    public GridView ExportCollection { get; set; }
    public GridView ExportVoucher { get; set; }
    public GridView ExportProduct { get; set; }
    public string SearchParam { get; set; }
}




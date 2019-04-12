using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class FormResultEntity
{
    public bool EntryStatus { get; set; }

    public bool isExecption { get; set; }

    public List<string> Message { get; set; }

    public string Messages
    {
        get
        {
            string errorlist = "";
            foreach (string str in this.Message)
            {
                errorlist += str;
            }
            Message.Clear();
            return errorlist;

        }
    }

    //public string MessageHtml
    //{
    //    get; set;
        
    //}

    // public Guid ReturnID { get; set; }

    // public bool isReadData { get; set; }

    public FormResultEntity()
    {
        this.Message = new List<string>();
    }
}

public class ExternalToken
{
    public bool isServerRegistred { get; set; }
    //public bool isNewUser { get; set; }
    public string userName { get; set; }
    public string access_token { get; set; }
    public string token_type { get { return "bearer"; } }
    public string expires_in { get; set; }
    public string issued { get; set; }
    public string expires { get; set; }
    public string modeltoken { get; set; }

}

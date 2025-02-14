using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
    }
    protected void OnSuccess()
    {
        Response.Write("Done!");
        captcha.Visible = false;
    }
    protected void OnFailure()
    {
        captcha.Message = "The text you entered was not correct. Please try again:";
    }
}
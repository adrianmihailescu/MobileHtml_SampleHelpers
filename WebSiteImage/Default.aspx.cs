using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Bitmap bmpImageToAdd = new Bitmap(@"d:\full access\crt\IMG_0036.jpg");
        int width = bmpImageToAdd.Width; int height = bmpImageToAdd.Height;

        string strResponseString = String.Empty;
        strResponseString += "<div id=\"divMain\" style=\"background-image:url(Copy%20(6)%20of%20IMG_0036.jpg)\">";
        strResponseString += "<marquee dir=\"rtl\" style=\"height:" + height + "px;width:" + width + "px;\"><img src=\"http://www.ase.ro/site/Banner/CONF1.jpg\"></marquee>";
        strResponseString += "</div>";

        Response.Write(strResponseString);
    }
}
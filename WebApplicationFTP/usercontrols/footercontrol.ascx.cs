using System;
using System.Data;
using System.Configuration;
// using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
// using System.Web.UI.WebControls.WebParts;
// using System.Web.UI.HtmlControls;

public partial class footercontrol : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (ftp.ftp_main.ftplib.IsConnected)
                {// if the user is connected, display connection information
                    lblConnectionStatus.Text = ftp.ftp_main.ftplib.ShowGeneralMessage("color_user.jpg", "Connected to: " + ftp.ftp_main.ftplib.server.ToString() + " as: " + ftp.ftp_main.ftplib.user.ToString(), "black");
                    lbtTdDisconnect.Visible = true;
                    // show / don't show upload, download, home links
                    lnkTdHome.Visible = true;
                    lnkTdHelp.Visible = true;
                    lnkTdUpload.Visible = true;
                    lnkTdDownload.Visible = true;
                    lnkTdAdmin.Visible = true;
                }
                else
                {// else display disconnected message
                    lblConnectionStatus.Text = ftp.ftp_main.ftplib.ShowGeneralMessage("black_white_user.jpg", "disconnected", "black");
                    lbtTdDisconnect.Visible = false;
                    // show / don't show upload, download, home links
                    lnkTdHome.Visible = false;
                    lnkTdHelp.Visible = false;
                    lnkTdUpload.Visible = false;
                    lnkTdDownload.Visible = false;
                    lnkTdAdmin.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ftp.ftp_main.ftplib.SendMail("footercontrol", ex.Message, Request.ServerVariables);
                Response.Redirect("error.aspx");
            }
        }
    }
    protected void lbtDisconnect_Click(object sender, EventArgs e)
    {
        // if the user is connected, then disconnect
        if (ftp.ftp_main.ftplib.IsConnected)
        {
            ftp.ftp_main.ftplib.Disconnect();
            lblConnectionStatus.Text = ftp.ftp_main.ftplib.ShowGeneralMessage("black_white_user.jpg", "disconnected", "black");
            lbtTdDisconnect.Visible = false;
            // show / don't show upload, download, home links
            lnkTdHome.Visible = false;
            lnkTdHelp.Visible = false;
            lnkTdUpload.Visible = false;
            lnkTdDownload.Visible = false;
            lnkTdAdmin.Visible = false;

            Response.Redirect("default.aspx");
        }
    }
}

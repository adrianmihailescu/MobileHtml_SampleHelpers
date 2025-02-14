using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
// using System.Web.UI.WebControls.WebParts;
// using System.Web.UI.HtmlControls;

public partial class catalog : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // get the alert's date from the quesr string. If the date is null or empty, we'll consider it to be the current date
            string sentItemsDate = ((Request.QueryString["alert_date"] == null) || (Request.QueryString["alert_date"] == String.Empty) ? DateTime.Now.ToString("yyyy.MM.dd") : Request.QueryString["alert_date"]);
            string imagesDirectory = ftp.ftp_main.ftplib.GetXmlValue("FtpRootDirectory") + "/" + Request.QueryString["alert_type"] + "/" + sentItemsDate + "/images/";
            string descriptionDirectory = ftp.ftp_main.ftplib.GetXmlValue("FtpRootDirectory") + "/" + Request.QueryString["alert_type"] + "/" + sentItemsDate + "/txt/";

            // first get the name of the description file from the description directory
            string descriptionFileName = String.Empty, userNameFromDescription = String.Empty;
            if (ftp.ftp_main.ftplib.GetFilesAndDirectoriesList(descriptionDirectory).Length > 0)
            {
                descriptionFileName = ftp.ftp_main.ftplib.GetFilesAndDirectoriesList(descriptionDirectory)[0];
                // look for the first position of "_" in the description file name
                // the name has the pattern description_<<name of the user who uploaded the file>>.txt
                if (descriptionFileName.Substring(descriptionFileName.IndexOf("_") + 1).Length > 0)
                    userNameFromDescription = descriptionFileName.Substring(descriptionFileName.IndexOf("_") + 1).Substring(0, descriptionFileName.Substring(descriptionFileName.IndexOf("_") + 1).Length - 4);
            }
            // lblShowCatalogPage.Text = "<img src=\"images/kiwee_logo.jpg\">";
            lblShowCatalogPage.Text = "<table border=\"0\" width=\"100%\">";
            lblShowCatalogPage.Text += "<tr><td align=\"center\">" + ftp.ftp_main.ftplib.GetXmlValue("VARSIID_HEADER") + "</td><td align=\"center\">"
            + ftp.ftp_main.ftplib.GetXmlValue("VARSIIMG1") + "</td><td align=\"center\">" + ftp.ftp_main.ftplib.GetXmlValue("VARSITXT1") + "</td><td align=\"center\">"
            + ftp.ftp_main.ftplib.GetXmlValue("VARSIIMG2") + "</td><td align=\"center\">" + ftp.ftp_main.ftplib.GetXmlValue("VARSITXT2_HEADER") + "</td><td align=\"center\">" + ftp.ftp_main.ftplib.GetXmlValue("VARSILOGO_HEADER") + "</td>";
            lblShowCatalogPage.Text += "<tr><td align=\"center\">" + ftp.ftp_main.ftplib.GetXmlValue("VARSIID_TXT") + "</td>";
            // here goes the first image from the images directory
            lblShowCatalogPage.Text += "<td align=\"center\"><img alt=\"This file is not an image\" border=\"0\" title=\"" + ftp.ftp_main.ftplib.GetFilesAndDirectoriesList(imagesDirectory)[0].ToString() + "\" src=\"ftp://" + ftp.ftp_main.ftplib.user + ":" + ftp.ftp_main.ftplib.pass
            + "@" + ftp.ftp_main.ftplib.server
            + "/" + imagesDirectory + ftp.ftp_main.ftplib.GetFilesAndDirectoriesList(imagesDirectory)[0].ToString() + "\" /></td>"
            + "<td align=\"center\">"
            + "<a target=\"_blank\" href=\"ftp://" + ftp.ftp_main.ftplib.user + ":" + ftp.ftp_main.ftplib.pass + "@" + ftp.ftp_main.ftplib.server
            + "/" + descriptionDirectory + "description_" + userNameFromDescription + ".txt\"" + ">Show description</a>"
            + "</td>"
            + "<td align=\"center\">"
                // here goes the second image from the images directory
            + "<img alt=\"This file is not an image\" border=\"0\" title=\"" + ftp.ftp_main.ftplib.GetFilesAndDirectoriesList(imagesDirectory)[1].ToString() + "\" src=\"ftp://" + ftp.ftp_main.ftplib.user + ":" + ftp.ftp_main.ftplib.pass
            + "@" + ftp.ftp_main.ftplib.server
            + "/" + imagesDirectory + ftp.ftp_main.ftplib.GetFilesAndDirectoriesList(imagesDirectory)[1].ToString() + "\" />"
            + "</td><td align=\"center\">" + ftp.ftp_main.ftplib.GetXmlValue("VARSITXT2_TXT") + "</td><td align=\"center\"><img src=\"" + ftp.ftp_main.ftplib.GetXmlValue("VARSILOGO_IMG") + "\"></td>";
            lblShowCatalogPage.Text += "</tr></table>";
        }
    }
}

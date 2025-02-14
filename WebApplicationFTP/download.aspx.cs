using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
// using System.Web.UI.HtmlControls;
using System.IO;

namespace WebApplicationFTP
{
    /// <summary>
	/// Summary description for download.
	/// </summary>
	public partial class download : System.Web.UI.Page
	{
        #region page display methods

        /// <summary>
        /// shows today's panel header
        /// </summary>
        private void ShowTodayPanelHeader()
        {
            DateTime sentItemsDate = (Calendar1.SelectedDate == DateTime.MaxValue || Calendar1.SelectedDate == DateTime.MinValue) ? DateTime.Now : Calendar1.SelectedDate;
            Label lblAlertDetailsTitle = new Label();
            lblAlertDetailsTitle.Text = "<table border=\"0\" width=\"100%\"><tr><td align=\"center\" bgcolor=\"#dcdcdc\"><b>Active alerts for this day</b><br /></td></tr></table><br />";
            pnlImagePreviews.Controls.Add(lblAlertDetailsTitle);

            // for each alert type from Config.xml
            // add message to the right panel

            //<-- shows the panel with linkbuttons for showing alert informations regarding each type of alerts
            Panel pnlShowAlertType = new Panel();
            pnlShowAlertType.BorderStyle = BorderStyle.Solid; pnlShowAlertType.BorderColor = Color.Black; pnlShowAlertType.BorderWidth = 1;

            Label lblAlertTypeTitle = new Label();
            // at least one alert type has been found in alertconfig.xml
            if (ftp.ftp_main.ftplib.GetNodes("Alerts").Count > 0)
                lblAlertTypeTitle.Text = "<table border=\"0\"><tr><td><img src=\"images/folder_big.gif\" border=\"0\"></td><td>Click on the links below to show active alerts for each category</td></tr></table>";
            else
                lblAlertTypeTitle.Text = "<table border=\"0\"><tr><td><img src=\"images/folder_big.gif\" border=\"0\"></td><td>No alert type has been found. Please add one <a href=\"admin.aspx\">here</a></td></tr></table>";
            // no alert type has been found in alertconfig.xml
            pnlShowAlertType.Controls.Add(lblAlertTypeTitle);

            foreach (string alertTypeItem in ftp.ftp_main.ftplib.GetNodes("Alerts"))
            {
                LinkButton lbtShowImagePreviewsLink = new LinkButton();
                lbtShowImagePreviewsLink.Text = alertTypeItem; lbtShowImagePreviewsLink.ID = alertTypeItem;
                lbtShowImagePreviewsLink.ForeColor = Color.FromName(ftp.ftp_main.ftplib.GetAlertColorByType(alertTypeItem));
                lbtShowImagePreviewsLink.Click += new EventHandler(lbtShowImagePreviewsLink_Click);

                Label lblShowImagePreviewsLinkStart = new Label();
                lblShowImagePreviewsLinkStart.Text = " | ";
                Label lblShowImagePreviewsLinkEnd = new Label();
                lblShowImagePreviewsLinkEnd.Text = " | ";

                pnlShowAlertType.Controls.Add(lblShowImagePreviewsLinkStart);
                pnlShowAlertType.Controls.Add(lbtShowImagePreviewsLink);
                pnlShowAlertType.Controls.Add(lblShowImagePreviewsLinkEnd);
            }
            Label lblBrTag = new Label(); lblBrTag.Text = "<br />";
            pnlImagePreviews.Controls.Add(pnlShowAlertType);
            pnlImagePreviews.Controls.Add(lblBrTag);
            //--> shows the panel with linkbuttons for showing alert informations regarding each type of alerts

            //<-- shows the panel with linkbuttons for deleting alert informations regarding each type of alerts
            pnlShowAlertType = new Panel();
            pnlShowAlertType.BorderStyle = BorderStyle.Solid; pnlShowAlertType.BorderColor = Color.Black; pnlShowAlertType.BorderWidth = 1;

            lblAlertTypeTitle = new Label();
            // at least one alert type has been found in alertconfig.xml
            if (ftp.ftp_main.ftplib.GetNodes("Alerts").Count > 0)
                lblAlertTypeTitle.Text = "<table border=\"0\"><tr><td><img src=\"images/trash.jpg\" border=\"0\"></td><td>Delete alerts</td></tr></table>";
            else
                // no alert type has been found
                lblAlertTypeTitle.Text = "<table border=\"0\"><tr><td><img src=\"images/trash.jpg\" border=\"0\"></td><td>No alert type has been found</td></tr></table>";
            pnlShowAlertType.Controls.Add(lblAlertTypeTitle);

            foreach (string alertTypeItem in ftp.ftp_main.ftplib.GetNodes("Alerts"))
            {
                //if (hasAlertFilesInDirectory("images", alertTypeItem, sentItemsDate))
                //{
                LinkButton lbtShowImagePreviewsDelete = new LinkButton();

                lbtShowImagePreviewsDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete alert <<" + alertTypeItem + ">> for " + sentItemsDate.ToString("dd.MM.yyyy") + "?');");
                // if the confirm returns true, then the Click eventhandler is executed
                lbtShowImagePreviewsDelete.Text = alertTypeItem; lbtShowImagePreviewsDelete.ID = "delete_" + alertTypeItem;
                lbtShowImagePreviewsDelete.ForeColor = Color.FromName(ftp.ftp_main.ftplib.GetAlertColorByType(alertTypeItem));
                // the event handler for the delete linkbutton when clicked
                lbtShowImagePreviewsDelete.Click += new EventHandler(lbtShowImagePreviewsDelete_Click);

                Label lbtShowImagePreviewsDeleteStart = new Label();
                lbtShowImagePreviewsDeleteStart.Text = " | ";
                Label lbtShowImagePreviewsDeleteEnd = new Label();
                lbtShowImagePreviewsDeleteEnd.Text = " | ";

                pnlShowAlertType.Controls.Add(lbtShowImagePreviewsDeleteStart);
                pnlShowAlertType.Controls.Add(lbtShowImagePreviewsDelete);
                pnlShowAlertType.Controls.Add(lbtShowImagePreviewsDeleteEnd);
                //}
            }
            // lblBrTag = new Label(); lblBrTag.Text = "<br />";
            pnlImagePreviews.Controls.Add(pnlShowAlertType);
        }

        /// <summary>
        /// checks if there exists any description file in a directory depending on alert type and a date
        /// </summary>
        /// <param name="strDirectory">directory to be checked for files</param>
        /// <param name="alertType">alert type to be checked</param>
        /// <param name="alertDate">alert date to be checked</param>
        /// <returns></returns>
        private bool hasAlertFilesInDirectory(string strDirectory, string alertType, DateTime alertDate)
        {
            DateTime sentItemsDate = (alertDate == DateTime.MaxValue || alertDate == DateTime.MinValue) ? DateTime.Now : alertDate;
            string directoryToLookup = ftp.ftp_main.ftplib.GetXmlValue("FtpRootDirectory") + "/" + alertType + "/" + sentItemsDate.ToString("yyyy.MM.dd") + "/" + strDirectory + "/";

            return ((ftp.ftp_main.ftplib.GetFilesAndDirectoriesList(directoryToLookup) != null) && (ftp.ftp_main.ftplib.GetFilesAndDirectoriesList(directoryToLookup)[0] != String.Empty));
        }

        /// <summary>
        /// shows the table's legend
        /// according to the alert types found in the Config.xml file
        /// </summary>
        private void ShowTableLegend()
        {
            lblShowLegend.Text = "<table style=\"border: solid 1px Black; background-color: White;\">"
            + "<tr><td colspan=\"10\" align=\"center\" style=\"font-weight:bold\">Legend</td></tr>";

            // if there is at least one alert type in the alertconfig.xml file
            // add each found alert type
            if (ftp.ftp_main.ftplib.GetNodes("Alerts").Count > 0)
            {
                lblShowLegend.Text += "<tr>";
                int tempTdItemCounter = 0;
                foreach (string alertTypeItem in ftp.ftp_main.ftplib.GetNodes("Alerts"))
                {
                    // foreach line found in the xml file add a row to the table
                    lblShowLegend.Text += "<td style=\"background-color: " + ftp.ftp_main.ftplib.GetAlertColorByType(alertTypeItem) + ";\">&nbsp;</td><td>" + alertTypeItem + "</td>";
                    tempTdItemCounter++;
                    if ((tempTdItemCounter % 4 == 0) && (tempTdItemCounter != 0))
                        lblShowLegend.Text += "</tr><tr>"; // force new row after each 4th element
                }
                // add current and today
                if ((tempTdItemCounter % 4) == 0)
                    lblShowLegend.Text += "</tr><tr>";
                // if we've already shown 4, 8,  16 alert types in the legend,
                // put the "current" and "selected" labels in the legend in the next row
                // else put them in the same row
                lblShowLegend.Text += "<td style=\"background-color: #CCCCCC\">&nbsp;</td>"
                    + "<td>[today]</td>"
                    + "<td style=\"background-color: #666666;\">&nbsp;</td><td>[selected]</td>"
                    + "</tr>";
            }
            // else warn the user to create a new alert type
            else lblShowLegend.Text += "<tr><td>No alert type has been found.<br />Please add one <a href=\"admin.aspx\">here</a>";

            lblShowLegend.Text += "</table>";
        }

        /// <summary>
        /// fills the right panel with informations regarding the selected date's alerts
        /// </summary>
        private void FillTodayPanel(string alertType)
        {
            // the char separator for folders
            char pathSeparator = ftp.ftp_main.ftplib.GetPathSeparator();

            Label lblHrTag = new Label(); lblHrTag.Text = "<hr />";
            pnlImagePreviews.Controls.Add(lblHrTag);

            DateTime sentItemsDate = (Calendar1.SelectedDate == DateTime.MaxValue || Calendar1.SelectedDate == DateTime.MinValue) ? DateTime.Now : Calendar1.SelectedDate;
            string imagesDirectory = pathSeparator + ftp.ftp_main.ftplib.GetXmlValue("FtpRootDirectory") + pathSeparator + alertType + pathSeparator + sentItemsDate.ToString("yyyy.MM.dd") + pathSeparator + "images" + pathSeparator;
            string descriptionDirectory = pathSeparator + ftp.ftp_main.ftplib.GetXmlValue("FtpRootDirectory") + pathSeparator + alertType + pathSeparator + sentItemsDate.ToString("yyyy.MM.dd") + pathSeparator + "txt" + pathSeparator;

            // first get the name of the description file from the description directory
            string descriptionFileName = String.Empty, userNameFromDescription = String.Empty;
            
            if (ftp.ftp_main.ftplib.GetFilesAndDirectoriesList(descriptionDirectory) != null)
            {
                descriptionFileName = ftp.ftp_main.ftplib.GetFilesAndDirectoriesList(descriptionDirectory)[0];
                // look for the first position of "_" in the description file name
                // the name has the pattern description_<<name of the user who uploaded the file>>.txt
                if (descriptionFileName.Substring(descriptionFileName.IndexOf("_") + 1).Length > 0)
                    userNameFromDescription = descriptionFileName.Substring(descriptionFileName.IndexOf("_") + 1).Substring(0, descriptionFileName.Substring(descriptionFileName.IndexOf("_") + 1).Length - 4);
            }
            
            Label lblShowImagePreviews = new Label();

            if (hasAlertFilesInDirectory("txt", alertType, sentItemsDate))
                lblShowImagePreviews.Text = "uploaded by: <b>" + userNameFromDescription + "</b><br />";
            lblShowImagePreviews.Text += "date: "; lblShowImagePreviews.Text += "<b>";
            lblShowImagePreviews.Text += ((Calendar1.SelectedDate == DateTime.MinValue) || (Calendar1.SelectedDate == DateTime.MaxValue)) ? DateTime.Now.ToString("dd MMMM yyyy") : Calendar1.SelectedDate.ToString("dd MMMM yyyy");
            lblShowImagePreviews.Text += "</b>"; lblShowImagePreviews.Text += "<br />alert's type: <b>" + alertType + "</b>";

            Panel pnlShowImagePreviews = new Panel(); pnlShowImagePreviews.ID = "pnlShowImagePreviews_" + alertType;
            pnlShowImagePreviews.BorderStyle = BorderStyle.Solid;
            pnlShowImagePreviews.BorderWidth = 1;

            CheckBoxList cbkShowImagePreviews = new CheckBoxList();
            // set border color for panel, according to content

            // get the color for representing the alert according to the corresponding row
            // <Alerts><Alert value="colorName" color="alertColor" /></Alerts>
            // from the Config.xml file

            pnlShowImagePreviews.BorderColor = Color.FromName(ftp.ftp_main.ftplib.GetAlertColorByType(alertType));
            lblShowImagePreviews.ForeColor = Color.FromName(ftp.ftp_main.ftplib.GetAlertColorByType(alertType));

            try
            {
                // <-- here is the problem with aspne_wp 99%
                ftp.ftp_main.ftplib.ChangeDirLongPathName(pathSeparator.ToString() + imagesDirectory);
                // here is the problem with aspne_wp 99% -->
                
                cbkShowImagePreviews.Items.Clear(); lblSearchStatus.Text = String.Empty;
                // for every found file, add the corresponding information to the right panel's label
                for (int tempItemCounter = 0; tempItemCounter < ftp.ftp_main.ftplib.GetFilesAndDirectoriesList(imagesDirectory).Length; tempItemCounter++)
                {
                    cbkShowImagePreviews.Items.Add(
                "<table><tr><td align=\"center\">"
                + "<a target=\"_blank\" href=\"ftp://" + ftp.ftp_main.ftplib.user + ":" + ftp.ftp_main.ftplib.pass + "@" + ftp.ftp_main.ftplib.server
                + (imagesDirectory.StartsWith(ftp.ftp_main.ftplib.GetPathSeparator().ToString()) ? String.Empty : pathSeparator.ToString())
                + imagesDirectory + ftp.ftp_main.ftplib.GetFilesAndDirectoriesList(imagesDirectory)[tempItemCounter].ToString()
                + "\"><img title=\"Click to see full size\" alt=\"This file is not an image\" border=\"0\" width=\"100\" height=\"100\" src=\"ftp://" + ftp.ftp_main.ftplib.user + ":" + ftp.ftp_main.ftplib.pass
                + "@" + ftp.ftp_main.ftplib.server
                + (imagesDirectory.StartsWith(ftp.ftp_main.ftplib.GetPathSeparator().ToString()) ? String.Empty : pathSeparator.ToString())
                + imagesDirectory + ftp.ftp_main.ftplib.GetFilesAndDirectoriesList(imagesDirectory)[tempItemCounter].ToString() + "\" /></a><br />"
                + ftp.ftp_main.ftplib.GetFilesAndDirectoriesList(imagesDirectory)[tempItemCounter].ToString()
                + "</td></tr></table><br />"
                    
                    + ((tempItemCounter == (ftp.ftp_main.ftplib.GetFilesAndDirectoriesList(imagesDirectory).Length - 1)) ?
                    (
                    ftp.ftp_main.ftplib.ShowLinkWithIcon("file.gif", "Show files", "ftp://" + ftp.ftp_main.ftplib.user + ":" + ftp.ftp_main.ftplib.pass + "@" + ftp.ftp_main.ftplib.server
                    + pathSeparator + "images" + pathSeparator + imagesDirectory, "_blank")
                    + ftp.ftp_main.ftplib.ShowLinkWithIcon("open_book.jpg", "Show description", "ftp://" + ftp.ftp_main.ftplib.user + ":" + ftp.ftp_main.ftplib.pass + "@" + ftp.ftp_main.ftplib.server
                    + pathSeparator + "images" + pathSeparator + descriptionDirectory + "description_" + userNameFromDescription + ".txt\"", "_blank")
                    + ftp.ftp_main.ftplib.ShowLinkWithIcon("folder.gif", "Show catalog", "catalog.aspx?alert_type=" + alertType + "&alert_date=" + sentItemsDate.ToString("yyyy.MM.dd"), "_blank")
                    ) : String.Empty)
                    
                    + ((tempItemCounter < ftp.ftp_main.ftplib.GetFilesAndDirectoriesList(imagesDirectory).Length -1) ? "<hr /><br /><br />" : String.Empty)
                    );
                    cbkShowImagePreviews.Items[tempItemCounter].Selected = true;
                }

            }
            // if no item found, display warning message
            catch (Exception ex)
            {
                lblShowImagePreviews.Text += "<br />" + ftp.ftp_main.ftplib.ShowGeneralMessage("warning.jpg", "No alert has been found !", "black");
                lblShowImagePreviews.Text += "<br />";
                lblShowImagePreviews.Text += ftp.ftp_main.ftplib.ShowLinkWithIcon("alert_create_one.jpg", "Create one", "upload.aspx?date=" + sentItemsDate + "&type=" +alertType, "_self");
            }
            finally
            {
                pnlShowImagePreviews.Controls.Add(lblShowImagePreviews);
                pnlShowImagePreviews.Controls.Add(cbkShowImagePreviews);
                Label lblBreakTag = new Label(); lblBreakTag.Text = "<br />";
                pnlImagePreviews.Controls.Add(pnlShowImagePreviews);
                pnlImagePreviews.Controls.Add(lblBreakTag);
            }
        }
        #endregion page display methods

        #region event handlers
        #region Web Form Designer generated code
        protected void Page_Load(object sender, System.EventArgs e)
        {
            ShowTodayPanelHeader();
            // if there is at least one alert type in alertconfig.xml, show the first one's preview
            if (ftp.ftp_main.ftplib.GetNodes("Alerts").Count > 0)
                FillTodayPanel(ftp.ftp_main.ftplib.GetNodes("Alerts")[0].ToString());
            //--> shows the panel with linkbuttons for deleting alert informations regarding each type of alerts
            ShowTableLegend();
        }

        protected override void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
        private void InitializeComponent()
        {
        }
		#endregion

        /// <summary>
        /// displays informations in the right panel regarding the selected alert type from the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbtShowImagePreviewsLink_Click(object sender, EventArgs e)
        {
            pnlImagePreviews.Controls.Clear();
            ShowTodayPanelHeader();
            // refresh the today panel
            FillTodayPanel(((LinkButton)sender).ID);
        }

        /// <summary>
        /// deletes the informations regarding an alert tyle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbtShowImagePreviewsDelete_Click(object sender, EventArgs e)
        {
            // the char separator for folders
            char pathSeparator = ftp.ftp_main.ftplib.GetPathSeparator();

            string strIdSender = ((LinkButton)sender).ID;
            strIdSender = strIdSender.Substring(strIdSender.IndexOf("_") + 1); // type of the alert to be deleted

            DateTime sentItemsDate = (Calendar1.SelectedDate == DateTime.MaxValue || Calendar1.SelectedDate == DateTime.MinValue) ? DateTime.Now : Calendar1.SelectedDate;
            string imagesDirectory = pathSeparator + "images" + pathSeparator + ftp.ftp_main.ftplib.GetXmlValue("FtpRootDirectory") + pathSeparator + strIdSender + pathSeparator + sentItemsDate.ToString("yyyy.MM.dd") + pathSeparator + "images" + pathSeparator;
            string descriptionDirectory = pathSeparator + "images" + pathSeparator + ftp.ftp_main.ftplib.GetXmlValue("FtpRootDirectory") + pathSeparator + strIdSender + pathSeparator + sentItemsDate.ToString("yyyy.MM.dd") + pathSeparator + "txt" + pathSeparator;

            // code to delete the alert file
            try
            {
                string directoryToDelete = pathSeparator + "images" + pathSeparator + ftp.ftp_main.ftplib.GetXmlValue("FtpRootDirectory") + pathSeparator + strIdSender + pathSeparator + sentItemsDate.ToString("yyyy.MM.dd");

                ftp.ftp_main.ftplib.ChangeDir(imagesDirectory);
                // first, delete all files from the images directory
                while (hasAlertFilesInDirectory("images", strIdSender, sentItemsDate))
                {
                    ftp.ftp_main.ftplib.RemoveFile(ftp.ftp_main.ftplib.GetFilesAndDirectoriesList(imagesDirectory)[0]);
                }

                // second, delete the description file
                ftp.ftp_main.ftplib.ChangeDir(descriptionDirectory);
                while (hasAlertFilesInDirectory("txt", strIdSender, sentItemsDate))
                {
                    ftp.ftp_main.ftplib.RemoveFile(ftp.ftp_main.ftplib.GetFilesAndDirectoriesList(descriptionDirectory)[0]);
                }
                // delete the two alert's directories
                ftp.ftp_main.ftplib.RemoveDir(imagesDirectory); ftp.ftp_main.ftplib.RemoveDir(descriptionDirectory);
                ftp.ftp_main.ftplib.RemoveDir(directoryToDelete);
                
                // refresh the right panel
                FillTodayPanel(strIdSender);
                // Response.Redirect("upload.aspx");
                ftp.ftp_main.ftplib.ShowInformationMessage(lblSearchStatus, "Alert files have been deleted");
            }
            catch (Exception ex)
            {
                lblSearchStatus.Text = String.Empty;
                ftp.ftp_main.ftplib.ShowWarningMessage(lblSearchStatus, "No files have been found for \"" + strIdSender + "\"");
            }
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            DateTime sentItemsDate = (Calendar1.SelectedDate == DateTime.MaxValue || Calendar1.SelectedDate == DateTime.MinValue) ? DateTime.Now : Calendar1.SelectedDate;
            pnlImagePreviews.Controls.Clear();
            lblSearchStatus.Text = String.Empty;

            Label lblAlertDetailsTitle = new Label();
            lblAlertDetailsTitle.Text = "<table border=\"0\" width=\"100%\"><tr><td align=\"center\" bgcolor=\"#dcdcdc\"><b>Active alerts for this day</b><br /></td></tr></table><br />";
            pnlImagePreviews.Controls.Add(lblAlertDetailsTitle);
            // for each alert type from Config.xml
            // add message to the right panel
            //<-- shows the panel with linkbuttons for showing alert informations regarding each type of alerts
            Panel pnlShowAlertType = new Panel();
            pnlShowAlertType.BorderStyle = BorderStyle.Solid; pnlShowAlertType.BorderColor = Color.Black; pnlShowAlertType.BorderWidth = 1;

            Label lblAlertTypeTitle = new Label();
            lblAlertTypeTitle.Text = "Click on the links below to show active alerts for each category<br /><br />";
            pnlShowAlertType.Controls.Add(lblAlertTypeTitle);

            foreach (string alertTypeItem in ftp.ftp_main.ftplib.GetNodes("Alerts"))
            {
                LinkButton lbtShowImagePreviewsLink = new LinkButton();
                lbtShowImagePreviewsLink.Text = alertTypeItem; lbtShowImagePreviewsLink.ID = alertTypeItem;
                lbtShowImagePreviewsLink.ForeColor = Color.FromName(ftp.ftp_main.ftplib.GetAlertColorByType(alertTypeItem));
                lbtShowImagePreviewsLink.Click += new EventHandler(lbtShowImagePreviewsLink_Click);

                Label lblShowImagePreviewsLinkStart = new Label();
                lblShowImagePreviewsLinkStart.Text = " | ";
                Label lblShowImagePreviewsLinkEnd = new Label();
                lblShowImagePreviewsLinkEnd.Text = " | ";

                pnlShowAlertType.Controls.Add(lblShowImagePreviewsLinkStart);
                pnlShowAlertType.Controls.Add(lbtShowImagePreviewsLink);
                pnlShowAlertType.Controls.Add(lblShowImagePreviewsLinkEnd);
            }
            Label lblBrTag = new Label(); lblBrTag.Text = "<br />";
            pnlImagePreviews.Controls.Add(pnlShowAlertType);
            pnlImagePreviews.Controls.Add(lblBrTag);
            //--> shows the panel with linkbuttons for showing alert informations regarding each type of alerts

            //<-- shows the panel with linkbuttons for deleting alert informations regarding each type of alerts
            pnlShowAlertType = new Panel();
            pnlShowAlertType.BorderStyle = BorderStyle.Solid; pnlShowAlertType.BorderColor = Color.Black; pnlShowAlertType.BorderWidth = 1;

            lblAlertTypeTitle = new Label();
            lblAlertTypeTitle.Text = "Delete alerts<br /><br />";
            pnlShowAlertType.Controls.Add(lblAlertTypeTitle);

            foreach (string alertTypeItem in ftp.ftp_main.ftplib.GetNodes("Alerts"))
            {
                // add only the alert types which contain images
                // so we display a linkbutton only for those alerts which can be deleted
                
                //if (hasAlertFilesInDirectory("images", alertTypeItem, sentItemsDate))
                //{
                    LinkButton lbtShowImagePreviewsDelete = new LinkButton();

                    lbtShowImagePreviewsDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete alert <<" + alertTypeItem + ">> for " + sentItemsDate.ToString("dd.MM.yyyy") + "?');");
                    // if the confirm returns true, then the Click eventhandler is executed
                    lbtShowImagePreviewsDelete.Text = alertTypeItem; lbtShowImagePreviewsDelete.ID = "delete_" + alertTypeItem;
                    lbtShowImagePreviewsDelete.ForeColor = Color.FromName(ftp.ftp_main.ftplib.GetAlertColorByType(alertTypeItem));
                    // the event handler for the delete linkbutton when clicked
                    lbtShowImagePreviewsDelete.Click += new EventHandler(lbtShowImagePreviewsDelete_Click);

                    Label lbtShowImagePreviewsDeleteStart = new Label();
                    lbtShowImagePreviewsDeleteStart.Text = " | ";
                    Label lbtShowImagePreviewsDeleteEnd = new Label();
                    lbtShowImagePreviewsDeleteEnd.Text = " | ";

                    pnlShowAlertType.Controls.Add(lbtShowImagePreviewsDeleteStart);
                    pnlShowAlertType.Controls.Add(lbtShowImagePreviewsDelete);
                    pnlShowAlertType.Controls.Add(lbtShowImagePreviewsDeleteEnd);
                //}
            }
            // lblBrTag = new Label(); lblBrTag.Text = "<br />";
            pnlImagePreviews.Controls.Add(pnlShowAlertType);
            //--> shows the panel with linkbuttons for deleting alert informations regarding each type of alerts
            ShowTableLegend();
        }

        /// <summary>
        /// handles te dropdownlist after databound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlAlertType_DataBound(object sender, EventArgs e)
        {
            // if the dropdownlist with alert types has no item, add a default one
            if (ddlAlertType.Items.Count == 0)
            {
                ListItem liItemToAdd = new ListItem();
                liItemToAdd.Text = "-- select --"; liItemToAdd.Value = "-- select --";
                ddlAlertType.Items.Add(liItemToAdd);
                liItemToAdd.Selected = true;
            }
        }
        #endregion event handlers
    }
}

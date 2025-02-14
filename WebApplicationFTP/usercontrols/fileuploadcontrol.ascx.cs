using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.IO;
using System.Net;

/// <summary>
/// attachme allows for multiple files to be uploaded to your web server while using only
/// one HttpInputFile control and a listbox.
/// </summary>
public partial class fileuploadcontrol : System.Web.UI.UserControl
{
    public ArrayList files = new ArrayList();
    // public static ArrayList hif = new ArrayList();
    // public ArrayList hif = new ArrayList();
    public int filesUploaded = 0;

    DateTime sentItemsDate;

    #region file page display methods
    
    /// <summary>
    /// notfies all users from the distribution list in Config.xml after uploading an alert
    /// </summary>
    private void NotifyUsersFromDistributionList()
    {
        // if the user hasn't entered any date, we'll consider current date
        DateTime sentItemsDate = (Calendar1.SelectedDate == DateTime.MaxValue || Calendar1.SelectedDate == DateTime.MinValue) ? DateTime.Now : Calendar1.SelectedDate;

        string strMessageBody;
        strMessageBody = "<table style=\"border: solid 1px black; width: auto;\"><tr><td><b>Alert's details:</b><br />";
        strMessageBody += "<br /><b>Type:</b> " + DropDownList1.SelectedItem.Value;
        strMessageBody += "<br /><b>Date:</b> " + sentItemsDate.ToString("dd MMMM yyyy");
        strMessageBody += "<br /><b>Server:</b> " + ftp.ftp_main.ftplib.server;
        strMessageBody += "<br /><b>User:</b> " + ftp.ftp_main.ftplib.user;
        strMessageBody += "</td></tr></table><br /><table style=\"border: solid 1px black; width: auto;\"><tr><td><b>Uploaded files:</b><br />";

        for (int listCounter = 0; listCounter < ListBox1.Items.Count; listCounter++)
            strMessageBody += "<br />" + ListBox1.Items[listCounter].Value;
        strMessageBody += "</td></tr></table>";
        // for each user found in Config.xml file
        // in <Notifications><Notify>, send an e-mail
        foreach (string strUserToBeNotified in ftp.ftp_main.ftplib.GetDistributionListForAlerts())
        {
            ftp.ftp_main.ftplib.NotifyUserAtAlert(strUserToBeNotified, strMessageBody);
        }
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
        char pathSeparator = ftp.ftp_main.ftplib.GetPathSeparator();
        DateTime sentItemsDate = (alertDate == DateTime.MaxValue || alertDate == DateTime.MinValue) ? DateTime.Now : alertDate;
        string directoryToLookup = ftp.ftp_main.ftplib.GetXmlValue("FtpRootDirectory") + pathSeparator + alertType + pathSeparator + sentItemsDate.ToString("yyyy.MM.dd") + pathSeparator + strDirectory + pathSeparator;

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
        else lblShowLegend.Text += "<tr><td>No alert type has been found. Please add one <a href=\"admin.aspx\">here</a>";

        lblShowLegend.Text += "</table>";
    }

    /// <summary>
    /// fills the right panel with informations regarding existing alert typess
    /// </summary>
    /// <param name="alertType"></param>
    private void FillTodayPanel(string alertType)
    {
        // the char separator for ftp folders
        char pathSeparator = ftp.ftp_main.ftplib.GetPathSeparator();

        Label lblBreakTag = new Label(); lblBreakTag.Text = "<hr />";
        pnlImagePreviews.Controls.Add(lblBreakTag);

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
        lblShowImagePreviews.Text += "date: ";
        lblShowImagePreviews.Text += "<b>";
        lblShowImagePreviews.Text += ((Calendar1.SelectedDate == DateTime.MinValue) || (Calendar1.SelectedDate == DateTime.MaxValue)) ? DateTime.Now.ToString("dd MMMM yyyy") : Calendar1.SelectedDate.ToString("dd MMMM yyyy");
        lblShowImagePreviews.Text += "</b>";
        lblShowImagePreviews.Text += "<br />type: <b>" + alertType + "</b>";

        // set border color for panel, according to content
        // get the color for representing the alert according to the corresponding row
        // <Alerts><Alert value="colorName" color="alertColor" /></Alerts>
        // from the Config.xml file
        Panel pnlShowImagePreviews = new Panel();
        pnlShowImagePreviews.BorderStyle = BorderStyle.Solid;
        pnlShowImagePreviews.BorderWidth = 1;
        pnlShowImagePreviews.BorderColor = Color.FromName(ftp.ftp_main.ftplib.GetAlertColorByType(alertType));
        lblShowImagePreviews.ForeColor = Color.FromName(ftp.ftp_main.ftplib.GetAlertColorByType(alertType));

        try
        {
            ftp.ftp_main.ftplib.ChangeDirLongPathName(imagesDirectory);
            //
            LinkButton lbtDeleteAlertFiles = new LinkButton(); lbtDeleteAlertFiles.Text = "Delete alert";
            // LinkButton's attribute to show confirmation for delete
            lbtDeleteAlertFiles.Attributes.Add("onclick", "return confirm('Are you sure you want to delete alert <<" + alertType + ">> for " + sentItemsDate.ToString("dd.MM.yyyy") + "?');");
            // if the confirm returns true, then the Click eventhandler is executed
            // handle events for the two linkbuttons

            // for every found file, add the corresponding information to the right panel's label
            for (int tempItemCounter = 0; tempItemCounter < ftp.ftp_main.ftplib.GetFilesAndDirectoriesList(imagesDirectory).Length; tempItemCounter++)
            {
                lblShowImagePreviews.Text +=
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
                (ftp.ftp_main.ftplib.ShowLinkWithIcon("open_book.jpg", "Show description", "ftp://" + ftp.ftp_main.ftplib.user + ":" + ftp.ftp_main.ftplib.pass + "@" + ftp.ftp_main.ftplib.server
                + pathSeparator + descriptionDirectory + "description_" + userNameFromDescription + ".txt\"", "_blank")
                + ftp.ftp_main.ftplib.ShowLinkWithIcon("folder.gif", "Show catalog", "catalog.aspx?alert_type=" + alertType + "&alert_date=" + sentItemsDate.ToString("yyyy.MM.dd"), "_blank")
                ) : String.Empty);

                if (tempItemCounter < (ftp.ftp_main.ftplib.GetFilesAndDirectoriesList(imagesDirectory).Length - 1))
                    lblShowImagePreviews.Text += "<hr /><br /><br />";
            }
        }
        // if no item found, display warning message
        catch (Exception ex)
        {
            ftp.ftp_main.ftplib.ShowWarningMessage(lblShowImagePreviews, "No alert has been found !");
        }

        finally
        {
            pnlShowImagePreviews.Controls.Add(lblShowImagePreviews);
            pnlImagePreviews.Controls.Add(pnlShowImagePreviews);
        }
    }


    /// <summary>
    /// shows today's panel header
    /// </summary>
    private void ShowTodayPanelHeader()
    {
        DateTime sentItemsDate = (Calendar1.SelectedDate == DateTime.MaxValue || Calendar1.SelectedDate == DateTime.MinValue) ? DateTime.Now : Calendar1.SelectedDate;
        pnlImagePreviews.Controls.Clear();

        Label lblAlertDetailsTitle = new Label();
        lblAlertDetailsTitle.Text = "<table border=\"0\" width=\"100%\"><tr><td align=\"center\" bgcolor=\"#dcdcdc\"><b>Active alerts for this day</b><br /></td></tr></table><br />";
        pnlImagePreviews.Controls.Add(lblAlertDetailsTitle);

        ShowTableLegend();

        if ((Request.QueryString["date"] != null || Request.QueryString["date"] != String.Empty) && (Request.QueryString["type"] != null || Request.QueryString["type"] != String.Empty))
        {
            try
            {
                // if the date querystring's parameter "date" is not null, go to that date
                // and set the alert type from the dropdownlist to that value
                Calendar1.SelectedDate = Convert.ToDateTime(Request.QueryString["date"]);
                DropDownList1.SelectedValue = Request.QueryString["type"];
            }
            catch (FormatException ex)
            {
            }
        }
        //<-- shows the panel with linkbuttons for showing alert informations regarding each type of alerts
        Panel pnlShowAlertType = new Panel();
        pnlShowAlertType.BorderStyle = BorderStyle.Solid; pnlShowAlertType.BorderColor = Color.Black; pnlShowAlertType.BorderWidth = 1;

        Label lblAlertTypeTitle = new Label();

        // at least one alert type has been found in alertconfig.xml
        if (ftp.ftp_main.ftplib.GetNodes("Alerts").Count > 0)
            lblAlertTypeTitle.Text = "<table border=\"0\"><tr><td><img src=\"images/folder_big.gif\" border=\"0\"></td><td>Click on the links below to show active alerts for each category</td></tr></table>";
        else
            // no alert type has been found in alertconfig.xml
            lblAlertTypeTitle.Text = "<table border=\"0\"><tr><td><img src=\"images/folder_big.gif\" border=\"0\"></td><td>No alert type has been found. Please add one <a href=\"admin.aspx\">here</a></td></tr></table>";
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
        if (ftp.ftp_main.ftplib.GetNodes("Alerts").Count > 0)
        {
            lblAlertTypeTitle.Text = "<table border=\"0\"><tr><td><img src=\"images/trash.jpg\" border=\"0\"></td><td>Delete alerts</td></tr></table>";
        }
        else
        {
            lblAlertTypeTitle.Text = "<table border=\"0\"><tr><td><img src=\"images/trash.jpg\" border=\"0\"></td><td>No alert type has been found.</td></tr></table>";
        }
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
    }
    #endregion file page display methods

    #region event handlers
    private void Page_Load(object sender, System.EventArgs e)
    {
        ShowTodayPanelHeader();
        // if there is at least one alert type in alertconfig.xml, show the first one's preview
        /*
        if (ftp.ftp_main.ftplib.GetNodes("Alerts").Count > 0)
            FillTodayPanel(ftp.ftp_main.ftplib.GetNodes("Alerts")[0].ToString());
        */
        //--> shows the panel with linkbuttons for deleting alert informations regarding each type of alerts
    }

    /// <summary>
    /// deletes the informations regarding an alert tyle
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void lbtShowImagePreviewsDelete_Click(object sender, EventArgs e)
    {
        // the char separator for ftp folders
        char pathSeparator = ftp.ftp_main.ftplib.GetPathSeparator();

        // clear the previous message
        lblUploadStatus.Text = String.Empty;

        string strIdSender = ((LinkButton)sender).ID;
        strIdSender = strIdSender.Substring(strIdSender.IndexOf("_") + 1); // type of the alert to be deleted

        DateTime sentItemsDate = (Calendar1.SelectedDate == DateTime.MaxValue || Calendar1.SelectedDate == DateTime.MinValue) ? DateTime.Now : Calendar1.SelectedDate;
        string imagesDirectory = pathSeparator + ftp.ftp_main.ftplib.GetXmlValue("FtpRootDirectory") + pathSeparator + "images" + pathSeparator + strIdSender + pathSeparator + "images" + pathSeparator + sentItemsDate.ToString("yyyy.MM.dd") + pathSeparator + "images" + pathSeparator;
        string descriptionDirectory = pathSeparator + ftp.ftp_main.ftplib.GetXmlValue("FtpRootDirectory") + pathSeparator + "images" + pathSeparator + strIdSender + pathSeparator + "images" + pathSeparator + sentItemsDate.ToString("yyyy.MM.dd") + pathSeparator + "txt" + pathSeparator;
        
        // code to delete the alert file
        try
        {
            string directoryToDelete = pathSeparator + ftp.ftp_main.ftplib.GetXmlValue("FtpRootDirectory") + pathSeparator + "images" + pathSeparator + strIdSender + pathSeparator + "images" + pathSeparator + sentItemsDate.ToString("yyyy.MM.dd");

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
            Response.Redirect("upload.aspx");
            ftp.ftp_main.ftplib.ShowInformationMessage(lblUploadStatus, "Alert files have been deleted");
        }
        catch (Exception ex)
        {
            lblUploadStatus.Text = String.Empty;
            ftp.ftp_main.ftplib.ShowWarningMessage(lblUploadStatus, "No files have been found for \"" + strIdSender + "\"");
        }
        
    }

    /// <summary>
    /// displays informations in the right panel regarding the selected alert type from the list
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void lbtShowImagePreviewsLink_Click(object sender, EventArgs e)
    {
        // clear the previous message
        lblUploadStatus.Text = String.Empty;
        
        pnlImagePreviews.Controls.Clear();
        ShowTodayPanelHeader();
        // refresh the today panel
        FillTodayPanel(((LinkButton)sender).ID);
    }

    #region Web Form Designer generated code
    override protected void OnInit(EventArgs e)
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
        this.Load += new System.EventHandler(this.Page_Load);
    }
    #endregion

    /// <summary>
    /// AddFile will add the path of the client side file that is currently in the PostedFile
    /// property of the HttpInputFile control to the listbox.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void AddFile_Click(object sender, System.EventArgs e)
    {
        string strDiskBrowserFoundFile = ((Label)diskBrowser1.FindControl("lblFoundFile")).Text;
        // clear the messages from the previous operation
        lblUploadStatus.Visible = true;
        lblUploadStatus.Text = String.Empty;

        if (strDiskBrowserFoundFile != String.Empty)
        {
            bool fileAlreadyInList = false;
            // check if the file is not already in list
            for (int counterCheckFile = 0; counterCheckFile < ListBox1.Items.Count; counterCheckFile++)
            {
                if (strDiskBrowserFoundFile == ListBox1.Items[counterCheckFile].Value)
                    fileAlreadyInList = true;
            }
            // if the file is not already in list, add id
            if (!fileAlreadyInList)
            {
                // hif.Add(strDiskBrowserFoundFile);
                ListBox1.Items.Add(strDiskBrowserFoundFile);
            }
            // else warn the user
            else
                ftp.ftp_main.ftplib.ShowWarningMessage(lblUploadStatus, ftp.ftp_main.ftplib.GetXmlValue("FileAlreadyInList"));
        }
        else
            ftp.ftp_main.ftplib.ShowWarningMessage(lblUploadStatus, ftp.ftp_main.ftplib.GetXmlValue("SelectFilesToUpload"));
    }

    /// <summary>
    /// RemvFile will remove the currently selected file from the listbox.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void RemvFile_Click(object sender, System.EventArgs e)
    {
        lblUploadStatus.Text = String.Empty;
        if (ListBox1.Items.Count != 0)
        {
            // if an item is selected, delete it
            for (int tempCounter = 0; tempCounter < ListBox1.Items.Count; tempCounter++)
            {
                if (ListBox1.Items[tempCounter].Selected)
                {
                    // delete the item from the array of files
                    // hif.Remove(ListBox1.Items[tempCounter]);
                    ListBox1.Items.Remove(ListBox1.Items[tempCounter]);
                }
            }
        }
    }

    protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
    {
        e.Cell.Text = "<a href =\"upload.aspx?date=" + e.Day.Date + "&type=" + DropDownList1.SelectedItem.Value + "\">" + e.Day.Date.Day + "</a>";
    }

    /// <summary>
    /// handles te dropdownlist after databound
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DropDownList1_DataBound(object sender, EventArgs e)
    {
        // if the dropdownlist with alert types has no item, add a default one
        if (DropDownList1.Items.Count == 0)
        {
            ListItem liItemToAdd = new ListItem();
            liItemToAdd.Text = "-- select --"; liItemToAdd.Value = "-- select --";
            DropDownList1.Items.Add(liItemToAdd);
            liItemToAdd.Selected = true;
        }
    }

    /// <summary>
    /// Upload_Click is the server side script that will upload the files to the web server
    /// by looping through the files in the listbox.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void Upload_Click(object sender, System.EventArgs e)
    {
        // the char separator for ftp folders
        char pathSeparator = ftp.ftp_main.ftplib.GetPathSeparator();

        // if the ftp root directory is not found, create it
        try
        {
            ftp.ftp_main.ftplib.ChangeDir(pathSeparator.ToString() + ftp.ftp_main.ftplib.GetXmlValue("FtpRootDirectory"));
        }
        catch (Exception ex)
        {
            ftp.ftp_main.ftplib.MakeDir(pathSeparator.ToString() + ftp.ftp_main.ftplib.GetXmlValue("FtpRootDirectory"));
        }

        // if the user hasn't entered any date, we'll consider current date
        DateTime sentItemsDate = (Calendar1.SelectedDate == DateTime.MaxValue || Calendar1.SelectedDate == DateTime.MinValue) ? DateTime.Now : Calendar1.SelectedDate;

        // the folder for the file description:
        // alert_type/day/txt/description.txt
        string descriptionDirectory = pathSeparator.ToString() + ftp.ftp_main.ftplib.GetXmlValue("FtpRootDirectory") + pathSeparator + DropDownList1.SelectedItem.Value + pathSeparator + sentItemsDate.ToString("yyyy.MM.dd") + pathSeparator + "txt" + pathSeparator;
        string descriptionFile = Server.MapPath(ftp.ftp_main.ftplib.GetXmlValue("TemporaryUploadsFolder") + @"\description_" + ftp.ftp_main.ftplib.user + ".txt");
        // first step will be uploading the description file of the alerts

        ftp.ftp_main.ftplib.ChangeDir(pathSeparator.ToString());
        UploadDescriptionFile(descriptionDirectory, descriptionFile);
     
        // the folder for the stored files:
        // alert_type/day/images/image_name
        string imagesDirectory = pathSeparator + ftp.ftp_main.ftplib.GetXmlValue("FtpRootDirectory") + pathSeparator + DropDownList1.SelectedItem.Value + pathSeparator + sentItemsDate.ToString("yyyy.MM.dd") + pathSeparator + "images" + pathSeparator;
        
        // the second step will upload the alerts
        ftp.ftp_main.ftplib.ChangeDir(pathSeparator.ToString());
        UploadAlertFiles(imagesDirectory);
        ftp.ftp_main.ftplib.ChangeDir(pathSeparator.ToString());

        ftp.ftp_main.ftplib.ShowInformationMessage(lblUploadStatus, "Alert files have been uploaded.");

        // start notifying users from the distribution list
        NotifyUsersFromDistributionList();
        // end notify users from the distribution list
        // clear the form
        ListBox1.Items.Clear();
        FillTodayPanel(DropDownList1.SelectedItem.Value);
    }


    #endregion event handlers

    #region file upload methods
    /// <summary>
     /// upload file on the ftp server
     /// </summary>
     /// <param name="file"></param>
     private void UploadFile(string filename)
     {
         /////
         try
         {
             int perc = 0;
             // open an upload
             ftp.ftp_main.ftplib.OpenUpload(filename, System.IO.Path.GetFileName(filename));

             while (ftp.ftp_main.ftplib.DoUpload() > 0)
             {
                 perc = (int)(((ftp.ftp_main.ftplib.BytesTotal) * 100) / ftp.ftp_main.ftplib.FileSize);
             }

             // the application needs read access right on the folder
         }
         catch (Exception ex)
         {
             ftp.ftp_main.ftplib.ShowErrorMessage(lblUploadStatus, ex.Message);
         }
     }

    /// <summary>
    /// Uploads the alert file in the destination folder from the ftp server
    /// </summary>
    /// <param name="imagesDirectory"></param>
    private void UploadAlertFiles(string imagesDirectory)
    {
        /*
        try
        {
            // check if the directory exists
            ftp.ftp_main.ftplib.MakeDir(imagesDirectory);
        }
        catch (Exception ex)
        {
            ftp.ftp_main.ftplib.ShowWarningMessage(lblUploadStatus, ftp.ftp_main.ftplib.GetXmlValue("AlertAlreadyExists"));
        }
        */
        try
        {
            ftp.ftp_main.ftplib.MakeDirLongPathName(imagesDirectory);
        }

        catch (Exception ex)
        {
            throw ex;
        }

        // ftp.ftp_main.ftplib.ChangeDir(imagesDirectory);
        
        int uplodedFiles = 0;
        ftp.ftp_main.ftplib.ShowInformationMessage(lblUploadStatus, "Uploaded files:<br />");
        for (int listCounter = 0; listCounter < ListBox1.Items.Count; listCounter++)
        {
            UploadFile(ListBox1.Items[listCounter].ToString());
            uplodedFiles++;
            // ftp.ftp_main.ftplib.ShowInformationMessage(lblUploadStatus, "File(s) uploaded: " + uplodedFiles.ToString() + " of " + ListBox1.Items.Count);
            lblUploadStatus.Text += "<br />" + ListBox1.Items[listCounter].ToString();
        }
        // if no file have been uploaded, show warning message
        if (uplodedFiles == 0)
        {
            lblUploadStatus.Text += "<br />";
            ftp.ftp_main.ftplib.ShowWarningMessage(lblUploadStatus, ftp.ftp_main.ftplib.GetXmlValue("NoFileHaveBeenUploaded"));
        }
        if (ListBox1.Items.Count == 0)
        {
            ftp.ftp_main.ftplib.ShowWarningMessage(lblUploadStatus, ftp.ftp_main.ftplib.GetXmlValue("SelectFilesToUpload"));
        }
    }

    /// <summary>
    /// adds text to a file from a FileStream
    /// </summary>
    /// <param name="fs">file stream with the file to be appended</param>
    /// <param name="value">the text to be written in the file</param>
    private void AddTextToFile(FileStream fs, string value)
    {
        byte[] info = new System.Text.UTF8Encoding(true).GetBytes(value);
        fs.Write(info, 0, info.Length);
    }

    /// <summary>
    /// Uploads the description file in the destination folder on the ftp server
    /// </summary>
    /// <param name="descriptionDirectory"></param>
    /// <param name="descriptionFile"></param>
    private void UploadDescriptionFile(string descriptionDirectory, string descriptionFile)
    {
        try
        {
            ftp.ftp_main.ftplib.MakeDirLongPathName(descriptionDirectory);
            // here is the problem with dir not found
            // ftp.ftp_main.ftplib.ChangeDir(descriptionDirectory);
            ftp.ftp_main.ftplib.ChangeDirLongPathName(descriptionDirectory);
            FileStream fs = File.Create(descriptionFile);
            AddTextToFile(fs, txtDescription.Text); fs.Close();
            UploadFile(descriptionFile);
            FileInfo tempFileInfo = new FileInfo(descriptionFile);
            tempFileInfo.Delete();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion file upload methods

}

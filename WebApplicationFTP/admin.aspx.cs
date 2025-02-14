using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;

public partial class admin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// checks if an alert type is already in the list
    /// </summary>
    /// <param name="strAlertTypeToCheck"></param>
    /// <returns></returns>
    protected bool isAlreadyInAlertList(string strAlertTypeToCheck)
    {
        bool alertTypeAlreadyInList = false;
        
        foreach (string alertTypeItem in ftp.ftp_main.ftplib.GetNodes("Alerts"))
            if (strAlertTypeToCheck.ToLower() == alertTypeItem.ToLower())
                alertTypeAlreadyInList = true;

        return alertTypeAlreadyInList;
    }

    /// <summary>
    /// checks if an alert color is already defined in the xml file
    /// </summary>
    /// <param name="strAlertTypeToCheck"></param>
    /// <returns></returns>
    protected bool isAlreadyInAlertColors(string alertColorToCheck)
    {
        bool alertColorAlreadyInList = false;

        foreach (string alertTypeItem in ftp.ftp_main.ftplib.GetAlertColors())
            if (alertColorToCheck.ToLower() == alertTypeItem.ToLower())
                alertColorAlreadyInList = true;

        return alertColorAlreadyInList;
    }

    /// <summary>
    /// adds a new row in the xml file
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddAlertToFile_Click(object sender, EventArgs e)
    {
        string strAlertColorToAdd = ((DropDownList)ucColorPicker1.FindControl("ddlMultiColor")).SelectedValue;
        string strAlertNameToAdd = tbxAlertName.Text; string strAlertValueToAdd = tbxAlertName.Text;
        
        // write in the xml file
        //// start writing in alertconfig.xml

        // if this alert type is already defined in the xml file
        if (isAlreadyInAlertList(tbxAlertName.Text))
        {
            lblInsertAlertStatus.Text = String.Empty;
            ftp.ftp_main.ftplib.ShowWarningMessage(lblInsertAlertStatus, "Alert type has already been chosen .<br />Please choose another !");
        }
        
        // if the selected alert color has already been defined in the xml file
        else
            if ((isAlreadyInAlertColors(((DropDownList)ucColorPicker1.FindControl("ddlMultiColor")).SelectedValue)))
            {
                Response.Write("Alert color has already been chosen .<br />Please choose another !");
            }
        
        // else the alert type can be added to the xml file
        else
        {
            try
            {
                // use the xml file for alert configuration
                string strFileName = Server.MapPath("~/xml/alertconfig.xml");

                XmlDocument xmlDoc = new XmlDocument();

                try
                {
                    xmlDoc.Load(strFileName);
                }
                catch (System.IO.FileNotFoundException)
                {
                    //if file is not found, create a new xml file
                    XmlTextWriter xmlWriter = new XmlTextWriter(strFileName, System.Text.Encoding.UTF8);
                    xmlWriter.Formatting = Formatting.Indented;
                    xmlWriter.WriteProcessingInstruction("xml", "version='1.0' encoding='UTF-8'");
                    xmlWriter.WriteStartElement("Alerts");
                    xmlWriter.Close();
                    xmlDoc.Load(strFileName);
                }
                XmlNode root = xmlDoc.DocumentElement;
                XmlElement childNode = xmlDoc.CreateElement("Alert");
                childNode.SetAttribute("value", strAlertValueToAdd); childNode.SetAttribute("name", strAlertNameToAdd);
                childNode.SetAttribute("color", strAlertColorToAdd);
                root.AppendChild(childNode);
                xmlDoc.Save(strFileName);

                ftp.ftp_main.ftplib.ShowInformationMessage(lblInsertAlertStatus, "The new alert type has been added.");
            }
            catch (Exception ex)
            {
                lblInsertAlertStatus.Text = String.Empty;
                ftp.ftp_main.ftplib.ShowErrorMessage(lblInsertAlertStatus, ex.Message);
            }
            finally
            {
                GridView1.DataBind();
            }
        }
    }
}

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
// using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;

public partial class usercontrols_diskbrowser : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {
            // get the names of all drives on the system
            string[] drives = Environment.GetLogicalDrives();
            // Loop into the string array
            foreach (string strDrive in drives)
            {
                // don't add the a:\ drive
                if (strDrive.ToUpper() != "A:\\")
                {
                    // Add items (drives) to the list
                    ListItem liDriveName = new ListItem();
                    liDriveName.Text = strDrive.ToString();
                    liDriveName.Text = strDrive.ToString();
                    DropDownList1.Items.Add(liDriveName);
                }
            }
        }

        //read our query string, if its null, assign the default folder
        //to our variable else assign the query string value.
        string folderToBrowse = Request.QueryString["d"] == null ? DropDownList1.SelectedValue.ToString() : Request.QueryString["d"];
        // Response.Write(folderToBrowse.ToString());

        CreateFolderDataSource(folderToBrowse);
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
        this.FileSystem.ItemCommand += new DataGridCommandEventHandler(FileSystem_ItemCommand);

    }
    #endregion

    /// <summary>
    /// formats the size of a file to bytes, kylobytes, megabytes, gigabytes, terabytes
    /// </summary>
    /// <param name="bytes">the size to convert</param>
    /// <returns></returns>
    private string ToByteString(long bytes)
    {
        if (bytes >= 1073741824)
        {
            Decimal size = Decimal.Divide(bytes, 1073741824);
            return String.Format("{0:##.##} GB", size);
        }
        else if (bytes >= 1048576)
        {
            Decimal size = Decimal.Divide(bytes, 1048576);
            return String.Format("{0:##.##} MB", size);
        }
        else if (bytes >= 1024)
        {
            Decimal size = Decimal.Divide(bytes, 1024);
            return String.Format("{0:##.##} KB", size);
        }
        else if (bytes > 0 & bytes < 1024)
        {
            Decimal size = bytes;
            return String.Format("{0:##.##} Bytes", size);
        }
        return "0 Bytes";
    }

    /// <summary>
    /// creates the datasource from found disk items
    /// </summary>
    /// <param name="folderToBrowse"></param>
    private void CreateFolderDataSource(string folderToBrowse)
    {
        //read the folder		
        DirectoryInfo DirInfo = new DirectoryInfo(folderToBrowse);
        //create our datatable that would hold the list
        //of folders in the specified directory
        DataTable fileSystemFolderTable = new DataTable();
        //create our datatable that would hold the list
        //of files in the specified directory
        DataTable fileSystemFileTable = new DataTable();

        //create our datatable that would hold the list
        //of files and folders when we combine the two previously declared datatable
        DataTable fileSystemCombinedTable = new DataTable();

        //create the columns for our file datatable
        DataColumn dcFileType = new DataColumn("Type");
        DataColumn dcFileFullName = new DataColumn("FullName");
        DataColumn dcFileName = new DataColumn("Name");
        DataColumn dcFileCreationTime = new DataColumn("CreationTime");
        DataColumn dcFileSize = new DataColumn("Size");

        //create the columns for our folder datatable
        DataColumn dcFolderType = new DataColumn("Type");
        DataColumn dcFolderFullName = new DataColumn("FullName");
        DataColumn dcFolderName = new DataColumn("Name");
        DataColumn dcFolderCreationTime = new DataColumn("CreationTime");
        DataColumn dcFolderSize = new DataColumn("Size");

        //add the columns to our datatable
        fileSystemFolderTable.Columns.Add(dcFileType);
        fileSystemFolderTable.Columns.Add(dcFileName);
        fileSystemFolderTable.Columns.Add(dcFileFullName);
        fileSystemFolderTable.Columns.Add(dcFileCreationTime);
        fileSystemFolderTable.Columns.Add(dcFileSize);
        fileSystemFileTable.Columns.Add(dcFolderType);
        fileSystemFileTable.Columns.Add(dcFolderName);
        fileSystemFileTable.Columns.Add(dcFolderFullName);
        fileSystemFileTable.Columns.Add(dcFolderCreationTime);
        fileSystemFileTable.Columns.Add(dcFolderSize);

        try
        {
            //loop thru each directoryinfo object in the specified directory
            foreach (DirectoryInfo di in DirInfo.GetDirectories())
            {
                //create a new row in ould folder table
                DataRow fileSystemRow = fileSystemFolderTable.NewRow();

                //assign the values to our table members
                fileSystemRow["Type"] = "Directory";
                fileSystemRow["Name"] = di.Name;
                fileSystemRow["FullName"] = di.FullName;
                fileSystemRow["CreationTime"] = di.CreationTime.ToString("dd.MM.yyyy hh:mm");
                fileSystemRow["Size"] = "-";
                fileSystemFolderTable.Rows.Add(fileSystemRow);
            }

            //loop thru each fileinfo object in the specified directory
            foreach (FileInfo fi in DirInfo.GetFiles())
            {
                //create a new row in ould folder table
                DataRow fileSystemRow = fileSystemFileTable.NewRow();

                //assign the values to our table members
                fileSystemRow["Type"] = "File";
                fileSystemRow["Name"] = fi.Name;
                fileSystemRow["FullName"] = fi.FullName;
                fileSystemRow["CreationTime"] = fi.CreationTime.ToString("dd.MM.yyyy hh:mm");
                fileSystemRow["Size"] = ToByteString(fi.Length);
                fileSystemFileTable.Rows.Add(fileSystemRow);
            }
        }

        catch (System.IO.IOException ex)
        {
            // the device is not ready
            DataRow fileSystemRow = fileSystemFileTable.NewRow();
            fileSystemRow["Type"] = "<img border=\"0\" src=\"images/warning.jpg\">";
            fileSystemRow["Name"] = "this drive is not ready !";
            fileSystemRow["FullName"] = String.Empty;
            fileSystemRow["CreationTime"] = String.Empty;
            fileSystemRow["Size"] = String.Empty;
            fileSystemFileTable.Rows.Add(fileSystemRow);
        }

        //copy the folder table to our main datatable,
        //this is necessary so that the parent table would have the
        //schema of our child tables.
        fileSystemCombinedTable = fileSystemFolderTable.Copy();

        //loop thru each row of our file table
        foreach (DataRow drw in fileSystemFileTable.Rows)
        {
            //import the rows from our child table to the parent table
            fileSystemCombinedTable.ImportRow(drw);
        }
        
        //assign our file system parent table to our grid and bind it.
        FileSystem.DataSource = fileSystemCombinedTable;
        FileSystem.DataBind();
    }

    private void FileSystem_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        //get the filepath from the specified command arguments for our linkbutton
        string filePath = e.CommandArgument.ToString();
        //get the file system type of the selected ite
        string fileSystemType = FileSystem.Items[e.Item.ItemIndex].Cells[0].Text;

        //if its a directory, redirect to our page and passing
        //the new file path to our query string

        // if the item is a folder
        if (fileSystemType == "Directory")
        {
            // Response.Redirect("usercontrols/diskbrowser.ascx?d=" + e.CommandArgument.ToString());
            string strUrlToNavigate;
            strUrlToNavigate = Request.Url + "?d=" + e.CommandArgument.ToString();
            string shortString = strUrlToNavigate.Substring(strUrlToNavigate.LastIndexOf("="), strUrlToNavigate.Length - strUrlToNavigate.LastIndexOf("="));
            
            Response.Redirect(System.Web.HttpContext.Current.Request.Url.AbsolutePath + "?d=" + shortString.Replace("=", String.Empty) + "&date=" + Request.QueryString["date"] + "&type=" + Request.QueryString["type"]);
        }
        // else if the item is a file, select it
        else if (fileSystemType == "File")
        {
            string folderToBrowse = Request.QueryString["d"] == null ? ConfigurationSettings.AppSettings["InitialPath"].ToString() : Request.QueryString["d"];
            //get the filename
            
            string filename = e.CommandName;
            lblFoundFile.Text = folderToBrowse + "\\" + filename;
        }
    }
    
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string folderToBrowse = DropDownList1.SelectedValue;
        // Response.Write(folderToBrowse.ToString());
        CreateFolderDataSource(folderToBrowse);

        DropDownList1.SelectedValue = (Request.QueryString["d"] != null) ? Request.QueryString["d"].Substring(0, 3) : DropDownList1.SelectedValue;
    }
}

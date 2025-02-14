using System;
using System.Web;
using System.Reflection;
using System.Drawing;
using System.Collections.Generic;


public partial class usercontrols_colorpicker : System.Web.UI.UserControl
{
    #region Protected method
    protected void Page_Load(object sender, EventArgs e)
    {
        populateDdlMultiColor();
        colorManipulation();
    }

    #endregion
    #region private method

    /// <summary>
    /// sets each listitem's background color, according to its name
    /// </summary>
    private void colorManipulation()
    {
        for (int row = 0; row < ddlMultiColor.Items.Count - 1; row++)
        {
            ddlMultiColor.Items[row].Attributes.Add("style", "background-color:" + ddlMultiColor.Items[row].Value);
            ddlMultiColor.BackColor = Color.FromName(ddlMultiColor.SelectedItem.Text);
        }        
    }
    
    /// <summary>
    /// sets the datasource for the dropdownlist
    /// </summary>
    /// <returns></returns>
    private List<string> finalColorList()
    {

        string[] allColors = Enum.GetNames(typeof(System.Drawing.KnownColor));
        string[] systemEnvironmentColors = new string[(typeof(System.Drawing.SystemColors)).GetProperties().Length];

        int index = 0;

        // get the names of all avaliable system colors
        foreach (MemberInfo member in (typeof(System.Drawing.SystemColors)).GetProperties())
        {
            systemEnvironmentColors[index++] = member.Name;
        }

        List<string> finalColorList = new List<string>();

        foreach (string color in allColors)
        {
            if (Array.IndexOf(systemEnvironmentColors, color) < 0)
            {
                finalColorList.Add(color);
            }
        }
        return finalColorList;
    }
    
    /// <summary>
    /// populates the dropdownlist with known colors and values
    /// </summary>
    private void populateDdlMultiColor()
    {
        ddlMultiColor.DataSource = finalColorList();
        ddlMultiColor.DataBind();
    }
    #endregion
}

using System;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Caching;
using System.Xml;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace tools
{
	/// <summary>
	/// Summary description for tools.
	/// </summary>
	public class tools
	{
		public tools()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		
		

		#region image transformations
		/// <summary>
		/// rotates the image with a specified angle
		/// </summary>
		/// <param name="b"></param>
		/// <param name="angle"></param>
		/// <returns></returns>
		public static Bitmap RotateImage(Bitmap b, float angle)
		{
			//create a new empty bitmap to hold rotated image
			Bitmap returnBitmap = new Bitmap(b.Width, b.Height);
			//make a graphics object from the empty bitmap
			Graphics g = Graphics.FromImage(returnBitmap);
			//move rotation point to center of image
			g.TranslateTransform((float)b.Width/2, (float)b.Height / 2);
			//rotate
			g.RotateTransform(angle);
			//move image back
			g.TranslateTransform(-(float)b.Width/2,-(float)b.Height / 2);
			//draw passed in image onto graphics object
			g.DrawImage(b, new Point(0, 0)); 
			return returnBitmap;
		}

		/// <summary>
		/// Write a fancy text on an image
		/// </summary>
		/// <param name="imgWidth">image's width</param>
		/// <param name="imgHeight">images height</param>
		/// <param name="strTextForImage">text to write on image</param>
		/// <returns></returns>
		public static Bitmap GenerateSwirlImageFromText(int imgWidth, int imgHeight, string strTextForImage, Color backColor, Color foreColor)
		{
			// Create a new 32-bit bitmap image.
			Bitmap bitmap = new Bitmap(imgWidth, imgHeight, PixelFormat.Format32bppArgb);

			// Create a graphics object for drawing.
			Graphics g = Graphics.FromImage(bitmap);
			Rectangle rect = new Rectangle(0, 0, imgWidth, imgHeight);
			g.SmoothingMode = SmoothingMode.AntiAlias;

			using (SolidBrush b = new SolidBrush(backColor))
			{
				g.FillRectangle(b, rect);
			}

			// Set up the text font.
			int emSize = (int)(imgWidth * 2 / strTextForImage.Length);
			
			FontFamily family = FontFamily.GenericSerif;
			
			Font font = new Font(family, emSize);
			// Adjust the font size until the text fits within the image.
			SizeF measured = new SizeF(0, 0);
			SizeF workingSize = new SizeF(imgWidth, imgHeight);
			while (emSize > 2 &&
				(measured = g.MeasureString(strTextForImage, font)).Width > workingSize.Width ||
				measured.Height > workingSize.Height)
			{
				font.Dispose();
				font = new Font(family, emSize -= 2);
			}

			// Set up the text format.
			StringFormat format = new StringFormat();
			format.Alignment = StringAlignment.Center;
			format.LineAlignment = StringAlignment.Center;

			// Create a path using the text and warp it randomly.
			GraphicsPath path = new GraphicsPath();
			path.AddString(strTextForImage, font.FontFamily, (int)font.Style, font.Size, rect, format);

			// Set font color to a color that is visible within background color
			int bcR = Convert.ToInt32(backColor.R);
			// This prevents font color from being near the bg color
			
			// int red = RNG.Next(255), green = RNG.Next(255), blue = RNG.Next(255);
			int red = 250, green=250, blue=250;

			SolidBrush sBrush = new SolidBrush(Color.FromArgb(red, green, blue));
			g.FillPath(sBrush, path);

			// Iterate over every pixel
			Random rndDistort = new Random();
			double distort = rndDistort.Next(5, 20) * (rndDistort.Next(10) == 1 ? 1 : -1);
			
			// Copy the image so that we're always using the original for source color
			using (Bitmap copy = (Bitmap)bitmap.Clone())
			{
				for (int y = 0; y < imgHeight; y++)
				{
					for (int x = 0; x < imgWidth; x++)
					{
						// Adds a simple wave
						int newX = (int)(x + (distort * Math.Sin(Math.PI * y / 50.0))); // 84
						int newY = (int)(y + (distort * Math.Cos(Math.PI * x / 50.0))); // 44
						if (newX < 0 || newX >= imgWidth) newX = 0;
						if (newY < 0 || newY >= imgHeight) newY = 0;
						bitmap.SetPixel(x, y, copy.GetPixel(newX, newY));
					}
				}
			}

			// Clean up.
			font.Dispose();
			sBrush.Dispose();
			g.Dispose();

			return bitmap;
		}
		#endregion image transformations

		#region xml        
		private static XmlDocument _xmlDoc;
		/// <summary>
		/// iterates through nodes collection and extract an array of node/value
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public static XmlNode GetXmlNode(string path)
		{
			string pathXmlFile = HttpContext.Current.Server.MapPath("~/xml/Config.xml"); // Gets Physical path of the "Config.xml" on server
			Cache cache = HttpContext.Current.Cache;

			try
			{
				_xmlDoc = (XmlDocument)cache[pathXmlFile];
				if (_xmlDoc == null)
				{
					_xmlDoc = new XmlDocument();
					_xmlDoc.Load(pathXmlFile);  // loads "ConfigSite.xml file 
					cache.Add(pathXmlFile, _xmlDoc, new CacheDependency(pathXmlFile), DateTime.Now.AddHours(6), TimeSpan.Zero, CacheItemPriority.High, null);
				}
				XmlNode root = _xmlDoc.DocumentElement;
				return root.SelectSingleNode(path);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		} 

		/// <summary>
		/// gets a collection of nodes with (name, value) from the specified xml file
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public static ArrayList GetNodes(string node, string attribute)
		{
			ArrayList h = new ArrayList();
			// iterates through the collection
			foreach (XmlNode n in GetXmlNode(node))
			{
				// for each node in the collection add the corresponding value in the array
				h.Add(n.Attributes[attribute].Value);
			}
			return h;
		}

		/// <summary>
		/// gets instructions for displaying images, according to image's type
		/// </summary>
		/// <param name="imageType">image type to be checked against</param>
		/// <param name="attribute">attribute's value to get</param>
		/// <returns></returns>
		public static string GetTextDisplayInstructionsOnImage(string imageType, string attribute)
		{
			ArrayList arrImageNames = new ArrayList(), arrImageTypes = new ArrayList();
			// iterates through the collection
			foreach (XmlNode n in GetXmlNode("Images"))
			{
				// for each node in the collection add the corresponding value in the array
				arrImageNames.Add(n.Attributes[attribute].Value);
				// h.Add(n.Attributes["name"].Value);
			}
			
			foreach (XmlNode n in GetXmlNode("Images"))
			{
				// for each node in the collection add the corresponding value in the array
				arrImageTypes.Add(n.Attributes["Type"].Value);
				// h.Add(n.Attributes["name"].Value);
			}

			for (int tempCounter = 0; tempCounter < arrImageTypes.Count; tempCounter++)
				if (arrImageTypes[tempCounter].ToString() == imageType)
					return arrImageNames[tempCounter].ToString();
			return String.Empty;
		}

		/// <summary>
		/// returns a fontstyle field depending on the xml tag value of that image type.
		/// </summary>
		/// <param name="strStyle"></param>
		/// <returns></returns>
		public static FontStyle GetTextStyleFromXmlValue(string strStyle)
		{
			switch (strStyle.ToLower())
			{
				case "bold":
					return FontStyle.Bold;

				case "italic":
					return FontStyle.Italic;
				
				case "regular":
					return FontStyle.Regular;
				
				case "strikeout":
					return FontStyle.Strikeout;
				
				case "underline":
					return FontStyle.Underline;
			}
			return FontStyle.Italic;
		}
		#endregion xml

		#region messages
		/// <summary>
		/// returns a general message with an icon
		/// </summary>
		/// <param name="icon">icon's name to be displayed with the message</param>
		/// <param name="message">message to be displayed</param>
		/// <param name="color">the color message to be displayed</param>
		/// <returns></returns>
		public static string ShowGeneralMessage(string icon, string message, string color)
		{
			string strTempMessageToDisplay = String.Empty;
			strTempMessageToDisplay += "<table border=\"0\"><tr>";
			strTempMessageToDisplay += "<td valign=\"middle\">";
			strTempMessageToDisplay += "<img src=\"images/" + icon + "\" border=\"0\" />";
			strTempMessageToDisplay += "</td>";
			strTempMessageToDisplay += "<td valign=\"middle\"><font color=\"" + color + "\">" + message + "</font></td></tr></table>";

			return strTempMessageToDisplay;
		}

		/// <summary>
		/// returns text to display a link with an icon and a message. The link will be displayed in a table
		/// </summary>
		/// <param name="icon">icon to be displayed with the link</param>
		/// <param name="message">link's message</param>
		/// <param name="navigationLink">link's url</param>
		/// <returns></returns>
		public static string ShowLinkWithIcon(string icon, string message, string navigationLink, string target)
		{
			string strTempMessageToDisplay = String.Empty;
			strTempMessageToDisplay += "<table border=\"0\"><tr>";
			strTempMessageToDisplay += "<td valign=\"middle\">";
			strTempMessageToDisplay += "<a target=\"" + target + "\" href=\"" + navigationLink + "\"><img src=\"images/" + icon + "\" border=\"0\" /></a>";
			strTempMessageToDisplay += "</td><td valign=\"middle\">";
			strTempMessageToDisplay += "<a target=\"" + target + "\" href=\"" + navigationLink + "\">" + message + "</a>" + "</td></tr></table>";

			return strTempMessageToDisplay;
		}

		/// <summary>
		/// shows an error message
		/// </summary>
		/// <param name="lbl"></param>
		/// <param name="message"></param>
		public void ShowErrorMessage(Label lbl, string message)
		{
			lbl.Text += ShowGeneralMessage("error.jpg", message, "red");
		}

		/// <summary>
		/// shows an information message
		/// </summary>
		/// <param name="lbl"></param>
		/// <param name="message"></param>
		public static void ShowInformationMessage(Label lbl, string message)
		{
			lbl.Text += ShowGeneralMessage("information.jpg", message, "black");
		}

		/// <summary>
		/// shows a warning message
		/// </summary>
		/// <param name="lbl"></param>
		/// <param name="message"></param>
		public static void ShowWarningMessage(Label lbl, string message)
		{
			lbl.Text += ShowGeneralMessage("warning.jpg", message, "black");
		}

		#endregion messages
	}
}

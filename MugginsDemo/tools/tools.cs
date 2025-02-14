using System;
using System.Collections;
using System.Xml;
using System.Web;
using System.Web.Caching;

namespace MugginsDemo.tools
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

		#region xml        
		private static XmlDocument _xmlDoc;
		/// <summary>
		/// iterates through nodes collection and extract an array of node/value
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public static XmlNode GetXmlNode(string path, string strXmlFile)
		{
			string pathXmlFile = HttpContext.Current.Server.MapPath("~/xml/" + strXmlFile); // Gets Physical path of the "Config.xml" on server
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
		//

		/// <summary>
		/// gets an xml value for a corresponding node
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public static string GetXmlValue(string path, string value)
		{
			try 
			{
				return GetXmlNode(path, "config.xml").Attributes[value].Value;
			}
			catch (Exception ex)
			{
				return "";
			}
		}
		#endregion xml
	}
}

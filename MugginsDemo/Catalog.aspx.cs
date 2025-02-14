using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using System.Configuration;
using xhtml.Tools;
using KMobile.Catalog.Presentation;
using KMobile.Catalog.Services;

namespace xhtml.si.movistar.es
{
	/// <summary>
	/// Summary description for catalog.
	/// </summary>
	public class catalog : XCatalogBrowsing
	{
		protected XhtmlTable tbContentSets, tbShowLinks, tbShowPager, tbCompositeItems;
		private int first = 0, page;
		protected string cg_temp;
		protected XhtmlTable tbSportsIllustratedHeader, tbCatalogItems;

		private ArrayList _contentCollImg = new ArrayList(); 
		private ArrayList _contentCollAnim = new ArrayList();
		private ArrayList _contentCollContentSet = new ArrayList();
		private ArrayList _contentCollComposite = new ArrayList();
		private ArrayList _contentCollVideo = new ArrayList();

		int init = 0;
	
		/// <summary>
		/// sets the caching properties and response type
		/// </summary>
		private void PageSetup()
		{
			try
			{
				WapTools.SetCachingProperties(this.Context);
				WapTools.AddUIDatLog(Request, Response);					
			}
			catch
			{
			}
		}
		
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			try 
			{
				_mobile = (MobileCaps)Request.Browser;
				PageSetup();
				// Response.Write(Request.ServerVariables["SERVER_NAME"] + Request.ServerVariables["URL"] + "?" + WapTools.GetBackNavigationLinkUrl(this.Request));
				if (_mobile.MobileType != null && _mobile.IsCompatible("IMG_COLOR"))
				{
					_idContentSet = (Request.QueryString["cs"] != null) ? Convert.ToInt32(Request.QueryString["cs"]) : 0;
					page = (Request.QueryString["n"] != null) ? Convert.ToInt32(Request.QueryString["n"]) : 1;
					_contentGroup = (Request.QueryString["cg"] != String.Empty) ? Request.QueryString["cg"].ToString() : ""; // Request.QueryString["cg"].ToString() : ""

					// _contentGroup = (Request.QueryString["cg"] != null) ? Request.QueryString["cg"].ToString() : "ANIM"; // Request.QueryString["cg"].ToString() : ""
					_contentType = WapTools.GetDefaultContentType(_contentGroup);
					_displayKey = WapTools.GetXmlValue("DisplayKey"); 
					//
					string paramBack = String.Format("a1=n&a2={0}&a3=cg&a4={1}&a5=cs&a6={2}",
						page, _contentGroup, _idContentSet);
				
					// Here we load the contents into a contentSet
					ContentSet contentSet = BrowseContentSetExtended();
			
					int numberOfRows = Convert.ToInt32(WapTools.GetXmlValue("Home/Nb_Rows"));
					int numberOfColumns = (_mobile.ScreenPixelsWidth > 140) ? 2 : 1; // 2 : 1 //140

					if( _contentGroup == "COMPOSITE")    
					{
						#region COMPOSITE
						try
						{
							for (int i=0; i < contentSet.Count; i++)
								try
								{
									if (Convert.ToInt32(WapTools.FindProperty(contentSet.ContentCollection[i].PropertyCollection, "IDComposite")) > 0 && contentSet.ContentCollection[i].PropertyCollection["CompositeContentGroup"].Value.ToString() != "COMPOSITE")
									{
										// Response.Write(contentSet.ContentCollection[i].PropertyCollection["IDContentSet"].Value.ToString());
										first = Convert.ToInt32(WapTools.FindProperty(contentSet.ContentCollection[i].PropertyCollection, "IDComposite"));
										cg_temp = contentSet.ContentCollection[i].PropertyCollection["CompositeContentGroup"].Value.ToString();
										// break;
									}
								}
								catch (Exception ex)
								{
								}
						}
						catch
						{
							first = 0;
						}
						numberOfColumns = Convert.ToInt32(WapTools.GetXmlValue("Home/Nb_PreviewsComposite"));

						// set the contentset properties
						_contentSetDisplayInst = new ContentSetDisplayInstructions(_mobile);
						_contentSetDisplayInst.UrlPicto = WapTools.GetImage(this.Request, "Picto");
						_contentSetDisplayInst.UrlDwld = String.Format("./catalog.aspx?cs={0}&cg={1}&{2}", "{0}", "{1}", paramBack);
						if (Request.QueryString["ref"] != "" && Request.QueryString["ref"] != null) _contentSetDisplayInst.UrlDwld += "&ref=" + Request.QueryString["ref"];
						
						ReadCatalogContentSets(contentSet, tbCatalogItems, page, numberOfColumns);

						page_max = (contentSet.Count / numberOfColumns);
						if (contentSet.Count % numberOfColumns > 0) page_max++;
						_contentSetDisplayInst = null;
						#endregion
					}
					
					else
						if (_contentGroup == "") // here we display animations, contentsets and mix of videos
					{
						// Detect if we have anim or videos
						// here we display previews and some contentsets
						#region Animations and contentsets
						if (_idContentSet.ToString() == WapTools.GetXmlValue("Home/ANIM")) // if we display animations and contentsets
						{
							try
							{							
								BrowseContentSetExtended(null, -1, -1);
								// display ANIM previews
								
								// here I display four rows of two previews
								int columnsToDisplay = (_mobile.ScreenPixelsWidth > 140) ? 2 : 1; // 2 : 1
								// if the device has a small screen, display for rows of one column
								// else display four rows of two columns
								int itemsToDisplay = (_mobile.ScreenPixelsWidth > 140) ? 8 : 4; // 2 : 1

								init = DateTime.Now.Millisecond;

								for (int tempI = 0; tempI < itemsToDisplay; tempI += columnsToDisplay) // display by default four rows
								{				
									if (_contentCollAnim.Count > 0) // check if we can display animations
									{
										// display a row for the corresponding one or two previews
										XhtmlTableRow temporaryRow = new XhtmlTableRow();
										tbCatalogItems.Rows.Add(temporaryRow);									
										
										// ensure that the methods display random previews 
										DisplayImages(temporaryRow, "ANIM", "ANIM_COLOR", tempI + init, tempI + init + columnsToDisplay,0); // rowImg2
									}
								}
							
								// here add contentset with content type = composite and content group = null
								XhtmlTools.AddLinkTable("", tbContentSets, WapTools.GetText("TopAnimacionesAnim"), "./catalog.aspx?cs=" + WapTools.GetXmlValue("Home/Top_Animaciones_Anim") + "&cg=ANIM", Color.Empty, Color.Empty, 1, HorizontalAlign.Left, VerticalAlign.NotSet, false, FontUnit.Empty, WapTools.GetImage(this.Request, "Picto")); //linkto.aspx
								DisplayCatalogContentSets(tbContentSets, "COMPOSITE", 0, -1);
								// DisplayContentSets(tbContentSets, "", 0, -1); // display the contentsets

							} // _contentCollContentSet.Count - 2
							catch (Exception ex)
							{
							}
						}
							#endregion Animations and contentsets
						
						else if (_idContentSet.ToString() == WapTools.GetXmlValue("Home/VIDEO")) // if we display VIDEO
						{
							// display videos here (VIDEO_DWL and VIDEO_RGT)
							#region VIDEOS						
							
							if (contentSet.ContentCollection.Count > 0)
							{
								_imgDisplayInst = new ImgDisplayInstructions(_mobile);
								_imgDisplayInst.UrlPicto = WapTools.GetImage(this.Request, "Picto");
								_imgDisplayInst.PreviewMaskUrl = WapTools.GetXmlValue(String.Format("Url_{0}", _contentGroup));								
													
								if (Request["p"] != null && Request["t"] != null && Request["p"] != "" && Request["t"] != "")
									_imgDisplayInst.UrlDwld += "&p=" + Request.QueryString["p"] + "&t=" + Request.QueryString["t"];					
								// if (_mobile.IsXHTML) nbrows += 1;
								
								ReadCatalogContentSets(contentSet, tbCatalogItems, page, numberOfRows * numberOfColumns);
						
								page_max = contentSet.Count / (numberOfRows * numberOfColumns);
								if (contentSet.Count % (numberOfRows * numberOfColumns) > 0) page_max++;
//								if (WapTools.noPreview(contentSet.IDContentSet)) page_max=1;					
								_imgDisplayInst = null;
							}
							#endregion VIDEOS
							////
						}
					}

					else // display images
					{
						#region IMAGES						
						_imgDisplayInst = new ImgDisplayInstructions(_mobile);
						_imgDisplayInst.UrlPicto = WapTools.GetImage(this.Request, "Picto");
						// _imgDisplayInst.UrlPicto = content.PropertyCollection["PreviewURL"].Value.ToString();
						_imgDisplayInst.PreviewMaskUrl = WapTools.GetXmlValue(String.Format("Url_{0}", _contentGroup));
											
						if (Request["p"] != null && Request["t"] != null && Request["p"] != "" && Request["t"] != "")
							_imgDisplayInst.UrlDwld += "&p=" + Request.QueryString["p"] + "&t=" + Request.QueryString["t"];										

						// if (_mobile.IsXHTML) nbrows += 1;
						ReadCatalogContentSets(contentSet, tbCatalogItems, page, numberOfRows * numberOfColumns);
						
						page_max = contentSet.Count / (numberOfRows * numberOfColumns);
						if (contentSet.Count % (numberOfRows * numberOfColumns) > 0) page_max++;
//						if (WapTools.noPreview(contentSet.IDContentSet)) page_max=1;					
						_imgDisplayInst = null;
						////
						#endregion IMAGES
					}

					#region HEADER
					XhtmlImage img = new XhtmlImage();
					img.ImageUrl = WapTools.GetImage(this.Request, "siswimsuit", _mobile.ScreenPixelsWidth);
					tbSportsIllustratedHeader.Rows.Clear(); //clear table rows before
					//
					XhtmlTools.AddImageTable(tbSportsIllustratedHeader, img); // tbHeader
					#endregion
                                     
					#region PAGER
					// display page numbers
					TableCell cellTemp = new TableCell();
					cellTemp.HorizontalAlign = HorizontalAlign.Center;
					XhtmlTableRow rowTemp = new XhtmlTableRow();
					int premiere = 0, derniere = 0, cont = 0;
					int[] limits = new int[page_max/3];
					while (cont<(page_max/3))
						limits[cont]=(++cont)*3;
					
					// displaying from page x to y
					for (cont=0;cont<page_max/3;cont++)
						if (page<=limits[cont])
						{
							premiere=limits[cont]-2;
							derniere=limits[cont];
							break;
						}
					if (premiere==0 && derniere==0 && page>0)
					{
						derniere = page_max;
						if (limits.Length==0)
							premiere = 1; 
						else
							premiere = limits[cont-1]+1;
					}				
					string URL_Suivant = String.Format("./catalog.aspx?&cg={0}&cs={1}&n={2}&p={3}&{4}",  _contentGroup, _idContentSet, derniere + 1, Request.QueryString["p"], paramBack);
					string URL_Precedent = String.Format("./catalog.aspx?cg={0}&cs={1}&n={2}&p={3}&{4}", _contentGroup, _idContentSet, premiere - 1, Request.QueryString["p"], paramBack);
					if (derniere>1)
					{
						XhtmlLink link = new XhtmlLink();				
						if (premiere > 1)
						{
							//link.ImageUrl = WapTools.GetImage(this.Request, "Previous");
							link.Text = WapTools.GetText("Menos");
							link.NavigateUrl = URL_Precedent;
							cellTemp.Controls.Add(link);
						}
						else
							cellTemp.Visible = false;
						// cellTemp.Text = "&nbsp;"; // &nbsp;
						rowTemp.Cells.Add(cellTemp);
						link = null;
						cellTemp = new TableCell();
						cellTemp.HorizontalAlign = HorizontalAlign.Center;

						for (cont=premiere;cont<=derniere;cont++)
						{
							if (cont!=page) // not the current page
							{
								link = new XhtmlLink();
								link.CssClass = "";
								link.NavigateUrl = String.Format("./catalog.aspx?cg={0}&cs={1}&n={2}&p={3}&{4}", _contentGroup, _idContentSet, cont, Request.QueryString["p"], paramBack);
								link.Text = cont.ToString();
								cellTemp.Controls.Add(link);
							}
							else // current page
							{
								cellTemp.Font.Size = FontUnit.XSmall;
								cellTemp.ForeColor = Color.FromName(WapTools.GetText("Color_" +  _contentGroup));
								cellTemp.Text = cont.ToString();
							}
							rowTemp.Cells.Add(cellTemp);
							link = null;
							cellTemp = new TableCell();
							cellTemp.HorizontalAlign = HorizontalAlign.Center;
						}
			
						if (derniere < page_max)
						{
							link = new XhtmlLink();
							//link.ImageUrl = WapTools.GetImage(this.Request, "Next");
							link.Text = WapTools.GetText("Mas");
							link.NavigateUrl = URL_Suivant;
							cellTemp.Controls.Add(link);
						}
						else
							cellTemp.Text = "&nbsp;"; // &nbsp;
						rowTemp.Cells.Add(cellTemp);
						link = null;
						tbShowPager.Rows.Add(rowTemp);
					}
					else // no need to display page numbers
					{
						tbShowPager.Visible = false; // false
					}

					#endregion

					#region Show links
					try
					{					
						if (_contentGroup == "IMG" || _contentGroup == "COMPOSITE")
						{
							// add cross-links for:
							// ANIM // VIDEO
							tbSportsIllustratedHeader.Rows.Clear(); //clear table rows before
							//
							XhtmlTools.AddImageTable(tbSportsIllustratedHeader, img); // tbShowLinks
							tbShowLinks.Visible = true;
							if (_mobile.IsCompatible("ANIM_COLOR"))
							{
								XhtmlTools.AddLinkTable("normal", tbShowLinks, WapTools.GetText("ANIM"), String.Format("./catalog.aspx?cg=&cs={0}", WapTools.GetXmlValue("Home/ANIM")), Color.Empty, Color.Empty, 1, HorizontalAlign.Left, VerticalAlign.NotSet, true, FontUnit.XXSmall, WapTools.GetImage(this.Request, "Picto"));
							}
							if (_mobile.IsCompatible("VIDEO_DWL") || _mobile.IsCompatible("VIDEO_CLIP"))
							{
								XhtmlTools.AddLinkTable("normal", tbShowLinks, WapTools.GetText("VIDEO"), String.Format("./catalog.aspx?cg=&cs={0}", WapTools.GetXmlValue("Home/VIDEO")), Color.Empty, Color.Empty, 1, HorizontalAlign.Left, VerticalAlign.NotSet, true, FontUnit.XXSmall, WapTools.GetImage(this.Request, "Picto"));
							}
							if (_hasPreviousPage)
								XhtmlTools.AddLinkTable("normal", tbShowLinks, WapTools.GetText("BackSI"), String.Format(WapTools.GetBackNavigationLinkUrl(this.Request), _contentGroup, _idContentSet), Color.Empty, Color.Empty, 1, HorizontalAlign.Left, VerticalAlign.NotSet, true, FontUnit.XXSmall, WapTools.GetImage(this.Request, "Picto"));
							else
								// else it points to the home page
								XhtmlTools.AddLinkTable("normal", tbShowLinks, WapTools.GetText("BackSI"), WapTools.GetText("SportsIllustratedHomeLink"), Color.Empty, Color.Empty, 1, HorizontalAlign.Left, VerticalAlign.NotSet, true, FontUnit.XXSmall, WapTools.GetImage(this.Request, "Picto"));
							XhtmlTools.AddLinkTable("normal", tbShowLinks, WapTools.GetText("InicioLink"), WapTools.GetText("EntrarLinkNavigateUrl"), Color.Empty, Color.Empty, 1, HorizontalAlign.Left, VerticalAlign.NotSet, true, FontUnit.XXSmall, WapTools.GetImage(this.Request, "Picto"));
						}
						
						else if (_idContentSet.ToString() == WapTools.GetXmlValue("Home/ANIM") || _contentGroup == "ANIM")
						{
							// add cross-links for:
							// IMG // VIDEO
							tbSportsIllustratedHeader.Rows.Clear(); //clear table rows before
							//
							XhtmlTools.AddImageTable(tbSportsIllustratedHeader, img); // tbShowLinks
							tbShowLinks.Visible = true;
							if (_mobile.IsCompatible("IMG_COLOR"))
							{
								XhtmlTools.AddLinkTable("normal", tbShowLinks, WapTools.GetText("IMG"), String.Format("./catalog.aspx?cg=COMPOSITE&cs={0}", WapTools.GetXmlValue("Home/IMG")), Color.Empty, Color.Empty, 1, HorizontalAlign.Left, VerticalAlign.NotSet, true, FontUnit.XXSmall, WapTools.GetImage(this.Request, "Picto"));
							}
							if (_mobile.IsCompatible("VIDEO_DWL") || _mobile.IsCompatible("VIDEO_CLIP"))
							{
								XhtmlTools.AddLinkTable("normal", tbShowLinks, WapTools.GetText("VIDEO"), String.Format("./catalog.aspx?cg=&cs={0}", WapTools.GetXmlValue("Home/VIDEO")), Color.Empty, Color.Empty, 1, HorizontalAlign.Left, VerticalAlign.NotSet, true, FontUnit.XXSmall, WapTools.GetImage(this.Request, "Picto"));
							}
							if (_hasPreviousPage)
								XhtmlTools.AddLinkTable("normal", tbShowLinks, WapTools.GetText("BackSI"), String.Format(WapTools.GetBackNavigationLinkUrl(this.Request), _contentGroup, _idContentSet), Color.Empty, Color.Empty, 1, HorizontalAlign.Left, VerticalAlign.NotSet, true, FontUnit.XXSmall, WapTools.GetImage(this.Request, "Picto"));
							else
								// else it points to the home page
								XhtmlTools.AddLinkTable("normal", tbShowLinks, WapTools.GetText("BackSI"), WapTools.GetText("SportsIllustratedHomeLink"), Color.Empty, Color.Empty, 1, HorizontalAlign.Left, VerticalAlign.NotSet, true, FontUnit.XXSmall, WapTools.GetImage(this.Request, "Picto"));
							XhtmlTools.AddLinkTable("normal", tbShowLinks, WapTools.GetText("InicioLink"), WapTools.GetText("EntrarLinkNavigateUrl"), Color.Empty, Color.Empty, 1, HorizontalAlign.Left, VerticalAlign.NotSet, true, FontUnit.XXSmall, WapTools.GetImage(this.Request, "Picto"));
						}

						else if (_idContentSet.ToString() == WapTools.GetXmlValue("Home/VIDEO"))
						{
							// add cross-links for:
							// IMG // ANIM + contentsets
							tbSportsIllustratedHeader.Rows.Clear(); //clear table rows before
							//
							XhtmlTools.AddImageTable(tbSportsIllustratedHeader, img); // tbShowLinks
							tbShowLinks.Visible = true;
							if (_mobile.IsCompatible("IMG_COLOR"))
							{
								XhtmlTools.AddLinkTable("normal", tbShowLinks, WapTools.GetText("IMG"), String.Format("./catalog.aspx?cg=COMPOSITE&cs={0}", WapTools.GetXmlValue("Home/IMG")), Color.Empty, Color.Empty, 1, HorizontalAlign.Left, VerticalAlign.NotSet, true, FontUnit.XXSmall, WapTools.GetImage(this.Request, "Picto"));
							}
							if (_mobile.IsCompatible("ANIM_COLOR"))
							{
								XhtmlTools.AddLinkTable("normal", tbShowLinks, WapTools.GetText("ANIM"), String.Format("./catalog.aspx?cg=&cs={0}", WapTools.GetXmlValue("Home/ANIM")), Color.Empty, Color.Empty, 1, HorizontalAlign.Left, VerticalAlign.NotSet, true, FontUnit.XXSmall, WapTools.GetImage(this.Request, "Picto"));
							}
							// if we are not on the home page, the back link points to the previous page
							if (_hasPreviousPage)
								XhtmlTools.AddLinkTable("normal", tbShowLinks, WapTools.GetText("BackSI"), String.Format(WapTools.GetBackNavigationLinkUrl(this.Request), _contentGroup, _idContentSet), Color.Empty, Color.Empty, 1, HorizontalAlign.Left, VerticalAlign.NotSet, true, FontUnit.XXSmall, WapTools.GetImage(this.Request, "Picto"));
							else
							// else it points to the home page
								XhtmlTools.AddLinkTable("normal", tbShowLinks, WapTools.GetText("BackSI"), WapTools.GetText("SportsIllustratedHomeLink"), Color.Empty, Color.Empty, 1, HorizontalAlign.Left, VerticalAlign.NotSet, true, FontUnit.XXSmall, WapTools.GetImage(this.Request, "Picto"));
							XhtmlTools.AddLinkTable("normal", tbShowLinks, WapTools.GetText("InicioLink"), WapTools.GetText("EntrarLinkNavigateUrl"), Color.Empty, Color.Empty, 1, HorizontalAlign.Left, VerticalAlign.NotSet, true, FontUnit.XXSmall, WapTools.GetImage(this.Request, "Picto"));
						}					
					}
					catch
					{
					}					 
					#endregion
					contentSet = null;
				}
				else
				{
					tbCatalogItems.Visible = false;			
					tbShowPager.Visible = false;
					tbCompositeItems.Visible = false;
					tbContentSets.Visible = false;
					XhtmlTableRow row = new XhtmlTableRow();
					XhtmlTools.AddTextTableRow(row, WapTools.GetText("PhoneNotCompatibleWithServices"), Color.Empty, Color.Empty, 2, HorizontalAlign.Left, VerticalAlign.NotSet, false, FontUnit.XSmall);
					tbShowLinks.Rows.Add(row);
					row = null;
				}	
			}
			catch(Exception caught) 
			{
				// error occured in the catalog page
				WapTools.SendMail("catalog", Request.UserAgent, caught.ToString(), Request.ServerVariables);
				Log.LogError(String.Format("Site emocion : Unexpected exception in emocion\\xhtml\\catalog.aspx - UA : {0} - QueryString : {1}", Request.UserAgent, Request.ServerVariables["QUERY_STRING"]), caught);
				Response.Redirect(WapTools.GetText("ErrorRedirectLink"));				
			}
		}

		#region Display	
		/// <summary>
		/// Displays contentset in the catalog page
		/// </summary>
		/// <param name="t"></param>
		/// <param name="cg"></param>
		/// <param name="rangeInf"></param>
		/// <param name="rangeSup"></param>
		public void DisplayCatalogContentSets(XhtmlTable t, string cg, int rangeInf, int rangeSup)
		{
			// ArrayList array = (cg=="IMG") ? _contentCollContentSet : _contentCollContentSetAnim;
			_contentSetDisplayInst = new ContentSetDisplayInstructions(_mobile);
			
			XhtmlTableCell cell = new XhtmlTableCell();
			XhtmlTableRow row = new XhtmlTableRow();

			if (rangeSup == -1) rangeSup = _contentCollContentSet.Count;
			if (rangeSup > _contentCollContentSet.Count) rangeSup = _contentCollContentSet.Count;

			for( int i = rangeInf; i < rangeSup; i++ )
			{
			
				Content content = (Content)_contentCollContentSet[i];
				//if (WapTools.FindProperty(content.PropertyCollection, "CompositeContentGroup") != cg) continue;
				ContentSetDisplay contentSetDisplay = new ContentSetDisplay(_contentSetDisplayInst);
				
				// _contentSetDisplayInst.UrlPicto = WapTools.GetImage(this.Request, "Picto");
				
				// if the content has no preview, display default preview
				_contentSetDisplayInst.UrlPicto = content.Preview.URL.ToString() == String.Empty ? WapTools.GetImage(this.Request, "Picto") : content.Preview.URL.ToString();
				_contentSetDisplayInst.UrlDwld = "./catalog.aspx?cs={0}&cg={1}"; // linkto.aspx; id={0}

				contentSetDisplay.Display(cell, content, true);			
				contentSetDisplay = null;
				row.Controls.Add(cell);
				cell = new XhtmlTableCell();
				t.Controls.Add(row);
				row = new XhtmlTableRow();
				content = null;
			}
			//t.Controls.Add(row);
			_contentSetDisplayInst = null;
			cell = null; row = null;
		}

		/// <summary>
		/// Displays images in the catalog page
		/// </summary>
		/// <param name="row"></param>
		/// <param name="contentGroup"></param>
		/// <param name="contentType"></param>
		/// <param name="start"></param>
		/// <param name="count"></param>
		/// <param name="drm"></param>		
		public void DisplayImages(TableRow row, string contentGroup, string contentType, int start, int count, int drm)
		{
			Content content = null;
			_imgDisplayInst = new ImgDisplayInstructions(_mobile);
			_imgDisplayInst.PreviewMaskUrl = WapTools.GetXmlValue(String.Format("Url_{0}", contentGroup));
			_imgDisplayInst.TextDwld = WapTools.GetText("Download");
            			
			_imgDisplayInst.UrlPicto = WapTools.GetImage(this.Request, "Img");

			// _imgDisplayInst.UrlDwld = WapTools.GetUrlBilling(this.Request, drm, contentGroup, contentType, HttpUtility.UrlEncode("xhtml|HOME"), "", _idContentSet.ToString());
			
			TableItemStyle tableStyle = new TableItemStyle();
			tableStyle.HorizontalAlign = HorizontalAlign.Center;
			int previews = (_mobile.ScreenPixelsWidth > 140) ? 2 : 1; // 1 : 2
                     
			for( int i = start; i < start + previews; i++ )  
			{
				if (contentGroup == "IMG" && drm==0)
					content = (Content)_contentCollImg[ (i) % _contentCollImg.Count];
				else if (contentGroup == "ANIM")
					content = (Content)_contentCollAnim[ (i) % _contentCollAnim.Count];
				else if (contentGroup.StartsWith("VIDEO"))
					content = (Content)_contentCollVideo[ (i) % _contentCollVideo.Count];				
				//				else if (contentgroup == "VIDEI_RGT")
				//					content = (Content)_contentCollVideo[ (i) % _contentcollVideo.Count];
				if (content != null)  
				{
					XhtmlLink lnk = WapTools.XhtmlLinkConstructor(WapTools.GetText("Download15eur"), String.Format(String.Format(WapTools.GetUrlBilling(this.Request, drm, contentGroup, contentType, HttpUtility.UrlEncode("xhtml|HOME"), "", _idContentSet.ToString()), content.IDContent.ToString()), content.IDContent.ToString()));
					// XhtmlLink lnk = WapTools.XhtmlLinkConstructor(WapTools.GetText("Download15eur"), String.Format(String.Format(WapTools.GetUrlBilling(this.Request, drm, contentGroup, contentType, HttpUtility.UrlEncode("xhtml|HOME"), "", "4506"), content.IDContent.ToString()), content.IDContent.ToString()));
					XhtmlLink lnk2 = WapTools.XhtmlLinkConstructor(content.Name + "<br /><br />", "");

					XhtmlTableCell tempCell = new XhtmlTableCell();
					// _imgDisplayInst.UrlDwld = String.Format(WapTools.GetUrlBilling(this.Request, drm, contentGroup, contentType, HttpUtility.UrlEncode("xhtml|HOME"), "", _idContentSet.ToString()), content.IDContent.ToString());
					ImgDisplay imgDisplay = new ImgDisplay(_imgDisplayInst);
					imgDisplay.Display(tempCell, content);				
					imgDisplay = null;
					lnk.CssClass = "CatalogAnimationsLink"; // style only for animations previews in catalog page
					
					tempCell.Controls.Add(lnk);
					tempCell.Controls.Add(lnk2);
					tempCell.ApplyStyle(tableStyle);
					row.Cells.Add(tempCell);

					tempCell = null;
				}
			}  
			content = null;
			_imgDisplayInst = null;
			tableStyle = null;
		}

		#endregion

		#region Override
		protected override void DisplayContentSet(Content content, System.Web.UI.MobileControls.Panel pnl)
		{
			_contentCollContentSet.Add(content);
		}

		protected override void DisplayImg(Content content, System.Web.UI.MobileControls.Panel pnl)
		{
			if (content.ContentGroup.Name == "IMG") _contentCollImg.Add(content); // if it's image, we add it to the images collection
			else if (content.ContentGroup.Name == "ANIM") _contentCollAnim.Add(content); // if it's animation, we add it to the animations collection
			else _contentCollVideo.Add(content); // else it's video for ringtone, or video for download
		}
		#endregion

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
	}
}

<?xml version="1.0" encoding="UTF-8" ?>
<%@ Register tagprefix="xhtml" Namespace="xhtml.Tools" Assembly="xhtml.si.movistar.es" %>
<%@ Page language="c#" Codebehind="catalog.aspx.cs" AutoEventWireup="false" Inherits="xhtml.si.movistar.es.catalog" %>
<!DOCTYPE html PUBLIC "-//WAPFORUM//DTD XHTML Mobile 1.0//EN" "http://www.wapforum.org/DTD/xhtml-mobile10.dtd" >
<html>
	<head>
		<title>Sports Illustrated </title>
		<link href="css/xhtml.css" type="text/css" rel="stylesheet" />
			<meta http-equiv="Cache-Control" content="no-cache, max-age=0, must-revalidate, proxy-revalidate, s-maxage=0" forua="true" />
	</head>
	<body>
		<xhtml:XhtmlTable id="tbSportsIllustratedHeader" CellSpacing="0" CellPadding="0" CssClass="normal"
			Runat="server"></xhtml:XhtmlTable>
		<xhtml:XhtmlTable id="tbCatalogItems" CssClass="normal" Runat="server" cellPadding="2" cellspacing="2"></xhtml:XhtmlTable>
		<xhtml:XhtmlTable id="tbContentSets" CssClass="normal" Runat="server" cellspacing="2" celpadding="2"></xhtml:XhtmlTable>
		<xhtml:XhtmlTable id="tbCompositeItems" CssClass="normal" Runat="server" cellpadding="2" cellspacing="2"></xhtml:XhtmlTable>
		<xhtml:XhtmlTable id="tbShowPager" CssClass="normal" Runat="server"></xhtml:XhtmlTable><br>
		<xhtml:XhtmlTable id="tbShowLinks" CssClass="normal" Runat="server"></xhtml:XhtmlTable>
	</body>
</html>

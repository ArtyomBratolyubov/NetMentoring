<?xml version="1.0" encoding="utf-8"?>
<xsl:transform version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xs="http://library.by/catalog">

  <xsl:key name="groups" match="xs:genre" use="."/>

  <xsl:template match="/xs:catalog">
	<html>
	  <head>
		<meta charset="utf-8" />
		<title>Текущие фонды по жанрам</title>
	  </head>
	  <style>
		table, th, td {
		border: 1px solid black;
		border-collapse: collapse;
		}
	  </style>
	  <body>
		<xsl:apply-templates select="xs:book/xs:genre[generate-id() = generate-id(key('groups', .)[1])]"/>
		<p>
			Total count = <xsl:value-of select="count(xs:book)"/>
		</p>
	  </body>
	</html>
  </xsl:template>

  <xsl:template match="xs:genre">
	<xsl:variable name="currentGroup" select="."/>

	<h3>
	  <xsl:value-of select="$currentGroup" />
	</h3>
	<table style="width:100%">
	  <tr>
		<th>Author</th>
		<th>Title</th>
		<th>Publish date</th>
		<th>Registration date</th>
	  </tr>
	  <xsl:for-each select="key('groups', $currentGroup)">
		<tr>
		  <td>
			<xsl:value-of select="../xs:author"/>
		  </td>
		  <td>
			<xsl:value-of select="../xs:title"/>
		  </td>
		  <td>
			<xsl:value-of select="../xs:publish_date"/>
		  </td>
		  <td>
			<xsl:value-of select="../xs:registration_date"/>
		  </td>
		</tr>
	  </xsl:for-each>
	  <tr>
		<td colspan="3">Total</td>
		<td>
			<xsl:value-of select="count(key('groups', $currentGroup))"/>
		</td>
	  </tr>
	</table>
	<p></p>
  </xsl:template>

</xsl:transform>
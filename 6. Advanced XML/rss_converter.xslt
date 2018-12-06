<?xml version="1.0" encoding="UTF-8"?>
<xsl:transform version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xs="http://library.by/catalog">

  <xsl:template match="/">
	<rss version="2.0">
	  <channel>
		<xsl:for-each select="xs:catalog/xs:book">
		  <item>
			<title>
			  <xsl:value-of select="xs:title"/>
			</title>
			<xsl:if test="xs:genre='Computer' and xs:isbn!=''">
			  <link>
				http://my.safaribooksonline.com/<xsl:value-of select="xs:isbn"/>
			  </link>
			</xsl:if>
			<description>
			  <xsl:value-of select="xs:description"/>
			</description>
			<pubDate>
			  <xsl:value-of select="xs:registration_date"/>
			</pubDate>
		  </item>
		</xsl:for-each>
	  </channel>
	</rss>
  </xsl:template>

</xsl:transform>
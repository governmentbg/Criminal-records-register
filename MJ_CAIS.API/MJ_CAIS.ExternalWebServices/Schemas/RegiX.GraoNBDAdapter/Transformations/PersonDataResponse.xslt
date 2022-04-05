<?xml version="1.0" encoding="UTF-8"?>
<!--Designed and generated by Altova StyleVision Professional Edition 2013 sp1 - see http://www.altova.com/stylevision for more information.-->
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:altova="http://www.altova.com" xmlns:altovaext="http://www.altova.com/xslt-extensions" xmlns:clitype="clitype" xmlns:common="http://egov.bg/RegiX/GRAO/NBD" xmlns:fn="http://www.w3.org/2005/xpath-functions" xmlns:iso4217="http://www.xbrl.org/2003/iso4217" xmlns:ix="http://www.xbrl.org/2008/inlineXBRL" xmlns:java="java" xmlns:link="http://www.xbrl.org/2003/linkbase" xmlns:n1="http://egov.bg/RegiX/GRAO/NBD/PersonDataResponse" xmlns:sps="http://www.altova.com/StyleVision/user-xpath-functions" xmlns:xbrldi="http://xbrl.org/2006/xbrldi" xmlns:xbrli="http://www.xbrl.org/2003/instance" xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" exclude-result-prefixes="altova altovaext clitype common fn iso4217 ix java link n1 sps xbrldi xbrli xlink xs xsi">
	<xsl:output version="4.0" method="html" indent="no" encoding="UTF-8" doctype-public="-//W3C//DTD HTML 4.01 Transitional//EN" doctype-system="http://www.w3.org/TR/html4/loose.dtd"/>
	<xsl:param name="SV_OutputFormat" select="'HTML'"/>
	<xsl:variable name="XML" select="/"/>
	<xsl:variable name="altova:nPxPerIn" select="96"/>
	<xsl:decimal-format name="format1" grouping-separator=" " decimal-separator=","/>
	<xsl:template match="/">
		<html>
			<head>
				<title/>
				<meta name="generator" content="Altova StyleVision Professional Edition 2013 sp1 (http://www.altova.com)"/>
				<meta http-equiv="X-UA-Compatible" content="IE=9"/>
				<xsl:comment>[if IE]&gt;&lt;STYLE type=&quot;text/css&quot;&gt;.altova-rotate-left-textbox{filter: progid:DXImageTransform.Microsoft.BasicImage(rotation=3)} .altova-rotate-right-textbox{filter: progid:DXImageTransform.Microsoft.BasicImage(rotation=1)} &lt;/STYLE&gt;&lt;![endif]</xsl:comment>
				<xsl:comment>[if !IE]&gt;&lt;!</xsl:comment>
				<style type="text/css">.altova-rotate-left-textbox{-webkit-transform: rotate(-90deg) translate(-100%, 0%); -webkit-transform-origin: 0% 0%;-moz-transform: rotate(-90deg) translate(-100%, 0%); -moz-transform-origin: 0% 0%;-ms-transform: rotate(-90deg) translate(-100%, 0%); -ms-transform-origin: 0% 0%;}.altova-rotate-right-textbox{-webkit-transform: rotate(90deg) translate(0%, -100%); -webkit-transform-origin: 0% 0%;-moz-transform: rotate(90deg) translate(0%, -100%); -moz-transform-origin: 0% 0%;-ms-transform: rotate(90deg) translate(0%, -100%); -ms-transform-origin: 0% 0%;}</style>
				<xsl:comment>&lt;![endif]</xsl:comment>
				<style type="text/css">@page { margin-left:0.60in; margin-right:0.60in; margin-top:0.79in; margin-bottom:0.79in } @media print { br.altova-page-break { page-break-before: always; } }</style>
			</head>
			<body>
				<xsl:for-each select="$XML">
					<xsl:for-each select="n1:PersonDataResponse">
						<h3 style="text-align:center; ">
							<span style="font-weight:bold; ">
								<xsl:text>Справка за физическо лице</xsl:text>
							</span>
						</h3>
						<xsl:if test="string-length(.) = 0">
							<span>
								<xsl:text>Не са намерени данни за физическо лице.</xsl:text>
							</span>
						</xsl:if>
						<table border="0" width="100%">
							<xsl:variable name="altova:CurrContextGrid_0" select="."/>
							<tbody>
								<xsl:for-each select="n1:PersonNames">
									<tr>
										<td style="width:1.67in; " valign="top">
											<span style="font-weight:bold; ">
												<xsl:text>Имена:</xsl:text>
											</span>
										</td>
										<td valign="top">
											<xsl:for-each select="common:FirstName">
												<xsl:apply-templates/>
											</xsl:for-each>
											<span>
												<xsl:text>&#160;</xsl:text>
											</span>
											<xsl:for-each select="common:SurName">
												<xsl:apply-templates/>
											</xsl:for-each>
											<span>
												<xsl:text>&#160;</xsl:text>
											</span>
											<xsl:for-each select="common:FamilyName">
												<xsl:apply-templates/>
											</xsl:for-each>
										</td>
									</tr>
								</xsl:for-each>
								<xsl:for-each select="n1:Alias">
									<tr>
										<td style="width:1.67in; " valign="top">
											<span style="font-weight:bold; ">
												<xsl:text>Псевдоним:</xsl:text>
											</span>
										</td>
										<td valign="top">
											<xsl:apply-templates/>
										</td>
									</tr>
								</xsl:for-each>
								<xsl:for-each select="n1:LatinNames">
									<xsl:choose>
										<xsl:when test="count(common:FirstName)&gt;0 or count(common:SurName)&gt;0 or count(common:FamilyName)&gt;0">
											<tr>
												<td style="width:1.67in; " valign="top">
													<span style="font-weight:bold; ">
														<xsl:text>Имена на латиница:</xsl:text>
													</span>
												</td>
												<td valign="top">
													<xsl:for-each select="common:FirstName">
														<xsl:apply-templates/>
													</xsl:for-each>
													<xsl:for-each select="common:SurName">
														<span>
															<xsl:text>&#160;</xsl:text>
														</span>
														<xsl:apply-templates/>
													</xsl:for-each>
													<xsl:for-each select="common:FamilyName">
														<span>
															<xsl:text>&#160;</xsl:text>
														</span>
														<xsl:apply-templates/>
													</xsl:for-each>
												</td>
											</tr>
										</xsl:when>
										<xsl:otherwise/>
									</xsl:choose>
								</xsl:for-each>
								<xsl:for-each select="n1:ForeignNames">
									<xsl:choose>
										<xsl:when test="count(common:FirstName)&gt;0 or count(common:SurName)&gt;0 or count(common:FamilyName)&gt;0">
											<tr>
												<td style="width:1.67in; " valign="top">
													<span style="font-weight:bold; ">
														<xsl:text>Други известни имена в чужбина:</xsl:text>
													</span>
												</td>
												<td valign="top">
													<xsl:for-each select="common:FirstName">
														<xsl:apply-templates/>
													</xsl:for-each>
													<xsl:for-each select="common:SurName">
														<span>
															<xsl:text>&#160;</xsl:text>
														</span>
														<xsl:apply-templates/>
													</xsl:for-each>
													<xsl:for-each select="common:FamilyName">
														<span>
															<xsl:text>&#160;</xsl:text>
														</span>
														<xsl:apply-templates/>
													</xsl:for-each>
												</td>
											</tr>
										</xsl:when>
										<xsl:otherwise/>
									</xsl:choose>
								</xsl:for-each>
								<xsl:for-each select="n1:Gender">
									<tr>
										<td style="width:1.67in; " valign="top">
											<span style="font-weight:bold; ">
												<xsl:text>Пол:</xsl:text>
											</span>
											<span>
												<xsl:text>&#160;</xsl:text>
											</span>
										</td>
										<td valign="top">
											<xsl:for-each select="common:GenderName">
												<xsl:apply-templates/>
											</xsl:for-each>
										</td>
									</tr>
								</xsl:for-each>
								<xsl:for-each select="n1:EGN">
									<tr>
										<td style="width:1.67in; " valign="top">
											<span style="font-weight:bold; ">
												<xsl:text>ЕГН:</xsl:text>
											</span>
										</td>
										<td valign="top">
											<xsl:apply-templates/>
										</td>
									</tr>
								</xsl:for-each>
								<xsl:for-each select="n1:BirthDate">
									<tr>
										<td style="width:1.67in; " valign="top">
											<span style="font-weight:bold; ">
												<xsl:text>Дата на раждане:</xsl:text>
											</span>
											<span>
												<xsl:text>&#160;</xsl:text>
											</span>
										</td>
										<td valign="top">
											<span>
												<xsl:variable name="altova:seqContentStrings_1">
													<xsl:value-of select="format-number(number(substring(string(string(.)), 9, 2)), '00', 'format1')"/>
													<xsl:text>.</xsl:text>
													<xsl:value-of select="format-number(number(substring(string(string(.)), 6, 2)), '00', 'format1')"/>
													<xsl:text>.</xsl:text>
													<xsl:value-of select="format-number(number(substring(string(string(string(.))), 1, 4)), '0000', 'format1')"/>
												</xsl:variable>
												<xsl:variable name="altova:sContent_1" select="string($altova:seqContentStrings_1)"/>
												<xsl:value-of select="$altova:sContent_1"/>
											</span>
										</td>
									</tr>
								</xsl:for-each>
								<xsl:for-each select="n1:PlaceBirth">
									<tr>
										<td style="width:1.67in; " valign="top">
											<span style="font-weight:bold; ">
												<xsl:text>Място на раждане:</xsl:text>
											</span>
										</td>
										<td valign="top">
											<xsl:apply-templates/>
										</td>
									</tr>
								</xsl:for-each>
								<xsl:for-each select="n1:Nationality">
									<tr>
										<td style="width:1.67in; " valign="top">
											<span style="font-weight:bold; ">
												<xsl:text>Гражданство:</xsl:text>
											</span>
										</td>
										<td valign="top">
											<xsl:for-each select="common:NationalityName">
												<xsl:apply-templates/>
											</xsl:for-each>
											<xsl:for-each select="common:NationalityName2">
												<span>
													<xsl:text>, </xsl:text>
												</span>
												<xsl:apply-templates/>
											</xsl:for-each>
										</td>
									</tr>
								</xsl:for-each>
								<xsl:for-each select="n1:DeathDate">
									<tr>
										<td style="width:1.67in; " valign="top">
											<span style="font-weight:bold; ">
												<xsl:text>Дата на смърт:</xsl:text>
											</span>
										</td>
										<td valign="top">
											<span>
												<xsl:variable name="altova:seqContentStrings_2">
													<xsl:value-of select="format-number(number(substring(string(string(.)), 9, 2)), '00', 'format1')"/>
													<xsl:text>.</xsl:text>
													<xsl:value-of select="format-number(number(substring(string(string(.)), 6, 2)), '00', 'format1')"/>
													<xsl:text>.</xsl:text>
													<xsl:value-of select="format-number(number(substring(string(string(string(.))), 1, 4)), '0000', 'format1')"/>
												</xsl:variable>
												<xsl:variable name="altova:sContent_2" select="string($altova:seqContentStrings_2)"/>
												<xsl:value-of select="$altova:sContent_2"/>
											</span>
										</td>
									</tr>
								</xsl:for-each>
							</tbody>
						</table>
					</xsl:for-each>
				</xsl:for-each>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>

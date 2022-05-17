using Saxon.Api;

var processor = new Processor();

var xdmNode = processor.NewXPathCompiler().EvaluateSingle(@"
parse-xml-fragment(""
<xsl:stylesheet xmlns:xsl='http://www.w3.org/1999/XSL/Transform' version='3.0'>
    <xsl:mode on-no-match='shallow-copy'/>
    <xsl:template match='/'>
      <xsl:next-match/>
      <xsl:comment>Fragment 1</xsl:comment>
    </xsl:template>
</xsl:stylesheet>
<xsl:stylesheet xmlns:xsl='http://www.w3.org/1999/XSL/Transform' version='3.0'>
    <xsl:mode on-no-match='shallow-copy'/>
    <xsl:template match='/'>
      <xsl:next-match/>
      <xsl:comment>Fragment 2</xsl:comment>
    </xsl:template>
</xsl:stylesheet>
"")", null);

var xsltCompiler = processor.NewXsltCompiler();
var xsltExecutable = xsltCompiler.Compile(xdmNode as XdmNode);

var transformer = xsltExecutable.Load30();

transformer.ApplyTemplates(new StringReader(@"<root>Test</root>"), processor.NewSerializer(Console.Out));

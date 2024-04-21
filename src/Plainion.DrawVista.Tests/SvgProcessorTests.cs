using System.Xml.Linq;
using Plainion.DrawVista.Adapters;
using Plainion.DrawVista.UseCases;
using Moq;

namespace Plainion.DrawVista.Tests;

[TestFixture]
public class SvgProcessorTests
{
    private const string SvgDocument = """
        <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" version="1.1" width="204px" height="464px" viewBox="-0.5 -0.5 204 464">
        <defs />
        <g>
            <a xlink:href="https://docs.server.org/Comonent/Introduction.md" target="_blank">
                <rect x="380" y="345" width="100" height="20" fill="none" stroke="none" pointer-events="all" />
                <g transform="translate(-0.5 -0.5)">
                    <switch>
                    <foreignObject pointer-events="none" width="100%" height="100%" requiredFeatures="http://www.w3.org/TR/SVG11/feature#Extensibility" style="overflow: visible; text-align: left;">
                        <div xmlns="http://www.w3.org/1999/xhtml" style="display: flex; align-items: unsafe center; justify-content: unsafe center; width: 98px; height: 1px; padding-top: 355px; margin-left: 381px;">
                        <div style="box-sizing: border-box; font-size: 0px; text-align: center;">
                            <div style="display: inline-block; font-size: 12px; font-family: Helvetica; color: rgb(0, 0, 0); line-height: 1.2; pointer-events: all; white-space: normal; overflow-wrap: normal;">EventBroker</div>
                        </div>
                        </div>
                    </foreignObject>
                    <text x="430" y="359" fill="#000000" font-family="Helvetica" font-size="12px" text-anchor="middle">EventBroker</text>
                    </switch>
                </g>
            </a>
            <path d="M 101 281 L 101 347.9" fill="none" stroke="#000000" stroke-width="3" stroke-miterlimit="10" pointer-events="stroke" />
            <path d="M 101 357.65 L 94.5 344.65 L 101 347.9 L 107.5 344.65 Z" fill="#000000" stroke="#000000" stroke-width="3" stroke-miterlimit="10" pointer-events="all" />
            <rect x="1" y="181" width="200" height="100" rx="15" ry="15" fill="#fff2cc" stroke="#d6b656" stroke-width="3" pointer-events="all" />
            <g transform="translate(-0.5 -0.5)">
            <switch>
                <foreignObject pointer-events="none" width="100%" height="100%" requiredFeatures="http://www.w3.org/TR/SVG11/feature#Extensibility" style="overflow: visible; text-align: left;">
                <div xmlns="http://www.w3.org/1999/xhtml" style="display: flex; align-items: unsafe center; justify-content: unsafe center; width: 198px; height: 1px; padding-top: 231px; margin-left: 2px;">
                    <div style="box-sizing: border-box; font-size: 0px; text-align: center;">
                    <div style="display: inline-block; font-size: 20px; font-family: Helvetica; color: rgb(0, 0, 0); line-height: 1.2; pointer-events: all; font-weight: bold; white-space: normal; overflow-wrap: normal;">Processor</div>
                    </div>
                </div>
                </foreignObject>
                <text x="101" y="237" fill="#000000" font-family="Helvetica" font-size="20px" text-anchor="middle" font-weight="bold">Processor</text>
            </switch>
            </g>
            <path d="M 101 101 L 101 170.9" fill="none" stroke="#000000" stroke-width="3" stroke-miterlimit="10" pointer-events="stroke" />
            <path d="M 101 177.65 L 96.5 168.65 L 101 170.9 L 105.5 168.65 Z" fill="#000000" stroke="#000000" stroke-width="3" stroke-miterlimit="10" pointer-events="all" />
            <rect x="1" y="1" width="200" height="100" rx="15" ry="15" fill="#dae8fc" stroke="#6c8ebf" stroke-width="3" pointer-events="all" />
            <g transform="translate(-0.5 -0.5)">
            <switch>
                <foreignObject pointer-events="none" width="100%" height="100%" requiredFeatures="http://www.w3.org/TR/SVG11/feature#Extensibility" style="overflow: visible; text-align: left;">
                <div xmlns="http://www.w3.org/1999/xhtml" style="display: flex; align-items: unsafe center; justify-content: unsafe center; width: 198px; height: 1px; padding-top: 51px; margin-left: 2px;">
                    <div style="box-sizing: border-box; font-size: 0px; text-align: center;">
                    <div style="display: inline-block; font-size: 20px; font-family: Helvetica; color: rgb(0, 0, 0); line-height: 1.2; pointer-events: all; font-weight: bold; white-space: normal; overflow-wrap: normal;">Parser</div>
                    </div>
                </div>
                </foreignObject>
                <text x="101" y="57" fill="#000000" font-family="Helvetica" font-size="20px" text-anchor="middle" font-weight="bold">Parser</text>
            </switch>
            </g>
            <rect x="1" y="361" width="200" height="100" rx="15" ry="15" fill="#d5e8d4" stroke="#82b366" stroke-width="3" pointer-events="all" />
            <g transform="translate(-0.5 -0.5)">
            <switch>
                <foreignObject pointer-events="none" width="100%" height="100%" requiredFeatures="http://www.w3.org/TR/SVG11/feature#Extensibility" style="overflow: visible; text-align: left;">
                <div xmlns="http://www.w3.org/1999/xhtml" style="display: flex; align-items: unsafe center; justify-content: unsafe center; width: 198px; height: 1px; padding-top: 411px; margin-left: 2px;">
                    <div style="box-sizing: border-box; font-size: 0px; text-align: center;">
                    <div style="display: inline-block; font-size: 20px; font-family: Helvetica; color: rgb(0, 0, 0); line-height: 1.2; pointer-events: all; font-weight: bold; white-space: normal; overflow-wrap: normal;">Processor</div>
                    </div>
                </div>
                </foreignObject>
                <text x="101" y="417" fill="#000000" font-family="Helvetica" font-size="20px" text-anchor="middle" font-weight="bold">Processor</text>
            </switch>
            </g>
        </g>
        <switch>
            <g requiredFeatures="http://www.w3.org/TR/SVG11/feature#Extensibility" />
            <a transform="translate(0,-5)" xlink:href="https://www.diagrams.net/doc/faq/svg-export-text-problems" target="_blank">
            <text text-anchor="middle" font-size="10px" x="50%" y="100%">Viewer does not support full SVG 1.1</text>
            </a>
        </switch>
        </svg>
    """;

    [Test]
    public void LinksShouldBeAddedForExistingPage()
    {
        var store = new FakeDocumentStore();
        var systemPage = new RawDocument("System", SvgDocument);
        var parserPage = new RawDocument("Parser", new XElement("doc", new XAttribute("width", "100%")).ToString());

        var svgProcessor = new SvgProcessor(new SvgCaptionParser(), new SvgHyperlinkFormatter(), store, new IndexPageGenerator());
        svgProcessor.Process([systemPage, parserPage]);

        var parserElement = XElement.Parse(store.GetPage("System").Content).Descendants()
            .Single(x => x.Elements().Count() == 0 && x.Name.LocalName == "div" && x.Value == "Parser");
        Assert.That(parserElement.Attribute("onclick"), Is.Not.Null);
    }

    [Test]
    public void ExistingLinksShouldBeStyled()
    {
        var store = new FakeDocumentStore();
        var systemPage = new RawDocument("System", SvgDocument);

        var svgProcessor = new SvgProcessor(new SvgCaptionParser(), new SvgHyperlinkFormatter(), store, new IndexPageGenerator());
        svgProcessor.Process([systemPage]);

        var linkElement = XElement.Parse(store.GetPage("System").Content).Descendants()
            .Single(x => x.Name.LocalName == "a" &&
                x.Attributes().Single(x => x.Name.LocalName == "href").Value.Contains("docs.server.org") == true)
            .Descendants()
            .SingleOrDefault(x =>  x.Elements().Count() == 0 && x.Name.LocalName == "div");
        Assert.That(linkElement.Attribute("style"), Is.Not.Null);

        var attr = new SvgStyleAttribute(linkElement.Attribute("style").Value);

        Assert.That(attr["color"], Is.EqualTo("blue"));
        Assert.That(attr["text-decoration"], Is.EqualTo("underline"));
    }
}

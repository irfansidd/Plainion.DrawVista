using System.Xml.Linq;

namespace Plainion.DrawVista.UseCases
{
    /// <summary>
    /// Automatically generates an index page with links to all known pages.
    /// </summary>
    public class IndexPageGenerator : IIndexPageGenerator
    {
        private const string PageName = "index";

        /// <summary>
        /// Checks if an index page already exists.
        /// </summary>
        public bool IndexPageExists(List<string> knownPageNames)
        {
            return knownPageNames.Contains(PageName);
        }

        /// <summary>
        /// Generates an index page with links to all known pages.
        /// </summary>
        public RawDocument GenerateIndexPage(List<string> knownPageNames)
        {
            var svgElement = XElement.Parse(@"<svg xmlns=""http://www.w3.org/2000/svg"" xmlns:xlink=""http://www.w3.org/1999/xlink"" version=""1.1"" width=""932px"" height=""492px"" viewBox=""-0.5 -0.5 932 492"" />");
            
            var gElement = new XElement("g");
            AddPageTitle(gElement);

            for (int i = 0; i < knownPageNames.Count; i++)
            {
                var page = knownPageNames[i];
                Console.WriteLine($"Index: Creating link for: {page}");
                int nodePositionFromTop = i * 85 + 100;
                CreateLinkedNodeForPage(gElement, page, nodePositionFromTop);
            }

            svgElement.Add(gElement);
            var indexDocument = new RawDocument(PageName, svgElement.ToString());
            return indexDocument;
        }

        private void AddPageTitle(XElement rootElement)
        {
            string ellipseXml =
                @"   	<rect x=""0"" y=""0"" width=""930"" height=""490"" fill=""rgb(255, 255, 255)"" stroke=""rgb(0, 0, 0)"" pointer-events=""all"" />";
            rootElement.Add(XElement.Parse(ellipseXml));

            string bodyXml =
                @"<g transform=""translate(-0.5 -0.5)"">
                  <switch>
                    <foreignObject pointer-events=""none"" width=""100%"" height=""100%"" requiredFeatures=""http://www.w3.org/TR/SVG11/feature#Extensibility"" style=""overflow: visible; text-align: left;"">
                      <div xmlns=""http://www.w3.org/1999/xhtml"" style=""display: flex; align-items: unsafe flex-start; justify-content: unsafe center; width: 928px; height: 1px; padding-top: 7px; margin-left: 1px;"">
                        <div data-drawio-colors=""color: rgb(0, 0, 0); "" style=""box-sizing: border-box; font-size: 0px; text-align: center;"">
                          <div style=""display: inline-block; font-size: 15px; font-family: Helvetica; color: rgb(0, 0, 0); line-height: 1.2; pointer-events: all; font-weight: bold; white-space: normal; overflow-wrap: normal;"">
                            <br />Index</div>
                        </div>
                      </div>
                    </foreignObject>
                    <text x=""465"" y=""22"" fill=""rgb(0, 0, 0)"" font-family=""Helvetica"" font-size=""15px"" text-anchor=""middle"" font-weight=""bold"">Index</text>
                  </switch>
                </g>";
            rootElement.Add(XElement.Parse(bodyXml));
        }

        private void CreateLinkedNodeForPage(XElement rootElement, string pageName, int y)
        {
            string ellipseXml =
                $@"   	<ellipse cx=""110"" cy=""{y}"" rx=""60"" ry=""35"" fill=""#dae8fc"" stroke=""#6c8ebf"" pointer-events=""all"" />";
            rootElement.Add(XElement.Parse(ellipseXml));

            string bodyXml =
                $@"   	<g transform=""translate(-0.5 -0.5)"">
                      <switch>
                        <foreignObject pointer-events=""none"" width=""100%"" height=""100%"" requiredFeatures=""http://www.w3.org/TR/SVG11/feature#Extensibility"" style=""overflow: visible; text-align: left;"">
                          <div xmlns=""http://www.w3.org/1999/xhtml"" style=""display: flex; align-items: unsafe center; justify-content: unsafe center; width: 118px; height: 1px; padding-top: {y}px; margin-left: 51px;"">
                            <div data-drawio-colors=""color: rgb(0, 0, 0); "" style=""box-sizing: border-box; font-size: 0px; text-align: center;"">
                              <div style=""display: inline-block; font-size: 12px; font-family: Helvetica; color: rgb(0, 0, 0); line-height: 1.2; pointer-events: all; white-space: normal; overflow-wrap: normal;"">{pageName}</div>
                            </div>
                          </div>
                        </foreignObject>
                      </switch>
                    </g>";
            rootElement.Add(XElement.Parse(bodyXml));
        }
    }
}

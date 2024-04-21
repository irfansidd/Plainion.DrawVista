using Plainion.DrawVista.UseCases;
using System.Xml.Linq;

namespace Plainion.DrawVista.Tests
{
    [TestFixture]
    public class IndexPageGeneratorTests
    {
        
        [Test]
        public void IndexPageExists_WhenIndexPageExists_ReturnsTrue()
        {
            //Arrange
            var indexPageGenerator = new IndexPageGenerator();
            var knownPageNames = new List<string> { "index", "page1", "page2" };

            //Act
            var result = indexPageGenerator.IndexPageExists(knownPageNames);

            //Assert
            Assert.IsTrue(result, "Unexpectedly returns false when index page exists.");
        }

        [Test]
        public void IndexPageExists_WhenIndexPageDoesNotExist_ReturnsFalse()
        {
            //Arrange
            var indexPageGenerator = new IndexPageGenerator();
            var knownPageNames = new List<string> { "page1", "page2" };

            //Act
            var result = indexPageGenerator.IndexPageExists(knownPageNames);

            //Assert
            Assert.IsFalse(result, "Unexpectedly returns true when index page does not exist.");
        }

        [Test]
        public void GenerateIndexPage_WithThreePages_CreatesCorrectNumberOfLinks()
        {
            //Arrange
            var indexPageGenerator = new IndexPageGenerator();
            var knownPageNames = new List<string> { "page1", "page2", "page3" };

            //Act
            var result = indexPageGenerator.GenerateIndexPage(knownPageNames);

            //Assert
            var svgElement = XElement.Parse(result.Content);
            var linkElements = svgElement.Descendants("ellipse").ToList();
            Assert.That(linkElements.Count, Is.EqualTo(knownPageNames.Count));
        }

        [Test]
        public void GenerateIndexPage_WithThreePages_IncludesAllKnownPageNames()
        {
            //Arrange
            var indexPageGenerator = new IndexPageGenerator();
            var knownPageNames = new List<string> { "page1", "page2", "page3" };

            //Act
            var result = indexPageGenerator.GenerateIndexPage(knownPageNames);

            //Assert
            var svgElement = XElement.Parse(result.Content);
            var textElements = svgElement.Descendants("div").Select(e => e.Value).ToList();

            foreach (var pageName in knownPageNames)
            {
                Assert.IsTrue(textElements.Contains(pageName));
            }
        }
    }
}

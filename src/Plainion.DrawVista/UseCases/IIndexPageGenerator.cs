namespace Plainion.DrawVista.UseCases
{
    public interface IIndexPageGenerator
    {
        bool IndexPageExists(List<string> knownPageNames);
        RawDocument GenerateIndexPage(List<string> knownPageNames);
    }
}

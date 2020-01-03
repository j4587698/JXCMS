namespace JXCMSFramework
{
    public interface IPluginInfo
    {
        string PluginName { get; set; }

        string Author { get; set; }

        string Version { get; set; }

        int BuilderNumber { get; set; }

        string Description { get; set; }
    }
}
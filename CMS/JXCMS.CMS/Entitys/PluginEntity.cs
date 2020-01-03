using FreeSql;

namespace JXCMS.CMS.Entity
{
    public class PluginEntity: BaseEntity<PluginEntity, int>
    {
        public string PluginPath { get; set; }

        public bool IsEnable { get; set; }
    }
}
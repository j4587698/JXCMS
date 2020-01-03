using FreeSql;

namespace JXCMS.CMS.Entity
{
    public class SettingsEntity: BaseEntity<SettingsEntity, int>
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }
}
using FreeSql;

namespace JXCMS.CMS.Entity
{
    public class SpiderRuleEntity: BaseEntity<SpiderRuleEntity, int>
    {
        public string Name { get; set; }

        public string Author { get; set; }

        public string WebRule { get; set; }

        public string DetailRule { get; set; }

        public string ContentRule { get; set; }

        public string BookNameRule { get; set; }

        public string BookAuthorRule { get; set; }

        public string BookCoverRule { get; set; }
    }
}
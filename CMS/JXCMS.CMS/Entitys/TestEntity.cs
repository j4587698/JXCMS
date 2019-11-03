using System;
using FreeSql;

namespace JXCMS.CMS.Entity
{
    public class TestEntity : BaseEntity<TestEntity, int>
    {
        public string Name { get; set; }

        public string Des { get; set; }

        public DateTime ModifyTime { get; set; }
    }
}
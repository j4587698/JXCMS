using FreeSql;

namespace JXCMS.CMS.Entity
{
    public class AdminEntity: BaseEntity<AdminEntity, int>
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
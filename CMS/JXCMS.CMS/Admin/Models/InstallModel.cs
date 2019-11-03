using JXCMS.Core.Db;

namespace JXCMS.CMS.Admin.Models
{
    public class InstallModel
    {
        public string DbType { get; set; }

        public string HostName { get; set; }

        public string Port { get; set; }

        public string DbUser { get; set; }

        public string DbPass { get; set; }

        public string DbName { get; set; }

        public string Prefix { get; set; }

        public string AdminUser { get; set; }

        public string AdminPass { get; set; }

        public DbConfig ToDbConfig()
        {
            return new DbConfig()
            {
                DbName = DbName,
                DbPort = Port,
                DbType = DbType,
                DbUrl = HostName,
                Password = DbPass,
                Prefix = Prefix,
                Username = DbUser
            };
        }
    }
}
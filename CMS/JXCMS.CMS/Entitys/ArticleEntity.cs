using FreeSql;
using FreeSql.DataAnnotations;

namespace JXCMS.CMS.Entity
{
    public class ArticleEntity : BaseEntity<ArticleEntity, int> 
    {
        public string Title { get; set; }

        public string Author { get; set; }

        [Column(DbType = "text")]
        public string Content { get; set; }
    }
}
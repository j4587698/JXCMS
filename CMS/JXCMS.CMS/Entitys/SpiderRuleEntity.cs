using FreeSql;

namespace JXCMS.CMS.Entity
{
    public class SpiderRuleEntity: BaseEntity<SpiderRuleEntity, int>
    {
        /// <summary>
        /// 规则名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        /// 是否下载图片
        /// </summary>
        public bool DownloadPic { get; set; }

        /// <summary>
        /// 网站域名
        /// </summary>
        public string Url { get; set; }
        
        public string StartUrl { get; set; }

        public string DetailRule { get; set; }

        public string DetailPageRex { get; set; }

        public string ContentRule { get; set; }

        public string ContentPageRex { get; set; }

        public string BookNameRule { get; set; }

        public string BookAuthorRule { get; set; }

        public string BookCoverRule { get; set; }
    }
}
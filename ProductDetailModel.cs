using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_API.Model
{[Table("ProductDetail")]
    public class ProductDetailModel
    {
        [Key]  
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string Title { get; set; }
        public int LanguageId { get; set; }
        public string DownloadUrl { get; set; }
        public string Comment { get; set; }
        public string MetaTagTitle { get; set; }
        public string MetaTagDescription { get; set; }
    }
}

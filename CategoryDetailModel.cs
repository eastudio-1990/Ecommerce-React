using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_API.Model
{
    [Table("CategoryDetail")]
    public class CategoryDetailModel
    {
        [Key]
        public int Id { get; set; }
        public int CatId { get; set; }
        public string Title { get; set; }
        public int LanguageId { get; set; }
        public int Order { get; set; }
        public string Comment { get; set; }
        public int HomeShow { get; set; }
    }
}

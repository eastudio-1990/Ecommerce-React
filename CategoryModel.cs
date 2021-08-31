﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_API.Model
{
    [Table("Category")]
    public class CategoryModel
    {
        [Key]
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
    }
}

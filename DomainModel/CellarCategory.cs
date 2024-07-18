using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModel
{
    [Table("CellarCategory")]
    public class CellarCategory
    {
        public int CellarCategoryId { get; set; }
        public string CategoryName { get; set; }
        public ICollection<Cellar> Cellars { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.DTO
{
    public class CellarCategoryDto
    {
        public int CellarCategoryId { get; set; }
        public string CategoryName { get; set; }
    }

    public class CreateCellarCategoryDto
    {
        public string CategoryName { get; set; }
    }

    public class UpdateCellarCategoryDto
    {
        public string CategoryName { get; set; }
    }
}

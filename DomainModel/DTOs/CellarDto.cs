using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.DTO
{
    public class CellarDto
    {
        public int CellarId { get; set; }
        public string CellarName { get; set; }
        public int NbRow { get; set; }
        public int NbStackRow { get; set; }
        public UserDto User { get; set; }
        public int? CellarCategoryId { get; set; }
        public int? CellarModelId { get; set; }
    }
}
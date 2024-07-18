using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomainModel.DTO.cellar
{
    public class CreateCellarDto
    {
        [Required]
        [StringLength(50)]
        public string CellarName { get; set; }
        [Required]
        public int NbRow { get; set; }
        [Required]
        public int NbStackRow { get; set; }
        public int CellarCategoryId { get; set; }
        public int CellarModelId { get; set; }
    }
}

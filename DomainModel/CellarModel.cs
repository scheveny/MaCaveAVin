﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    [Table("CellarModel")]
    public class CellarModel
    {
        public int CellarModelId { get; set; }
        public string CellarBrand { get; set; }
        public int CellarTemperature { get; set; }
        public ICollection<Cellar> Cellars { get; set; }
    }
}

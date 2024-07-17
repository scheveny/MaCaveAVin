﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.DTOs.Bottle
{
    public class CreateBottleDto
    {
        [Required]
        [StringLength(50)]
        public string BottleName { get; set; }
        public int BottleYear { get; set; }
        [Required]
        [StringLength(50)]
        public string WineColor { get; set; }
        [Required]
        [StringLength(50)]
        public string Appellation { get; set; }
        [Required]
        public int PeakStart { get; set; }
        [Required]
        public int PeakEnd { get; set; }
        public DateTime IdealPeak { get; set; }
        [Required]
        public int CellarId { get; set; }

        [Required]
        public int DrawerNb { get; set; }

        // Position X bouteille
        [Required]
        public int StackInDrawerNb { get; set; }

        public Cellar Cellar { get; set; }

        
    }
}

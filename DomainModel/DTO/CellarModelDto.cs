using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.DTO
{
    public class CellarModelDto
    {
        public int CellarModelId { get; set; }
        public string CellarBrand { get; set; }
        public int CellarTemperature { get; set; }
    }

    public class CreateCellarModelDto
    {
        public string CellarBrand { get; set; }
        public int CellarTemperature { get; set; }
    }

    public class UpdateCellarModelDto
    {
        public string CellarBrand { get; set; }
        public int CellarTemperature { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnerjiSA.GenerationService.Common
{
    public class PowerPlantDataResponseDto
    {
        public int Id { get; set; }
        public int WebId { get; set; }
        public virtual PowerPlantResponseDto PowerPlant { get; set; }
        public bool? Good { get; set; }
        public string Value { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}

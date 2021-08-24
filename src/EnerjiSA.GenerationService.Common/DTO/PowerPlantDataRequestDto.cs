using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnerjiSA.GenerationService.Common
{
   public  class PowerPlantDataRequestDto
    {
        public string WebId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}

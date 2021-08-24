using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnerjiSA.GenerationService.Common
{
   public class PowerPlantResponseDto
    {
        public int Id { get; set; }
        public string WebId { get; set; }
        public ICollection<PowerPlantDataResponseDto> PowerPlantDatas { get; set; }
    }
}

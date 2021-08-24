using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnerjiSA.GenerationService.Entity.Entities
{
    public class PowerPlantData:URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; }
        public int PowerPlantId { get; set; }
        public virtual PowerPlant PowerPlant { get; set; }
        public bool? Good { get; set; }
        public string Value { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}

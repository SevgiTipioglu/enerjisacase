using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URF.Core.EF.Trackable;
namespace EnerjiSA.GenerationService.Entity.Entities
{
    public class PowerPlant : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; }
        public string WebId { get; set; }
        public virtual ICollection<PowerPlantData> PowerPlantDatas { get; set; }
    }
}

using AutoMapper;
using EnerjiSA.GenerationService.Common;
using EnerjiSA.GenerationService.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnerjiSA.GenerationService.Automapper
{  
    public class PowerPlantProfile : Profile
    {
        public PowerPlantProfile()
        {
            CreateMap<PowerPlant, PowerPlantResponseDto>();
        }
    }
}

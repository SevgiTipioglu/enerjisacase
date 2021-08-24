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
   public class PowerPlantDataProfile : Profile
    {
        public PowerPlantDataProfile()
        {
            CreateMap<PowerPlantData, PowerPlantDataResponseDto>();
        }
    }
}


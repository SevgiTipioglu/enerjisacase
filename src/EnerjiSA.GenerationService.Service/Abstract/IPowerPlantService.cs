using EnerjiSA.GenerationService.Common;
using EnerjiSA.GenerationService.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnerjiSA.GenerationService.Service
{
    public interface IPowerPlantService
    {
        Task<ServiceResponse<List<PowerPlantResponseDto>>> GetGenerationData(PowerPlantDataRequestDto request);
        Task<bool> GenerateData();
        Task<ServiceResponse<List<int>>> GetPowerPlantId();
        Task<List<PowerPlant>> GetAll();
        Task<PowerPlant> GetById(int id);
        Task<int> UpdatePowerPlant(PowerPlant powerPlant);
        Task<int> DeleteById(int id);
        Task<int> AddPowerPlant(PowerPlant powerPlant);
    }
}

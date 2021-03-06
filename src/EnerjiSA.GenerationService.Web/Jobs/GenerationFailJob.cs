using EnerjiSA.GenerationService.Entity.Entities;
using EnerjiSA.GenerationService.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnerjiSA.GenerationService.Web
{
    public class GenerationFailJob
    {
        public readonly IPowerPlantService _powerPlantService;
        public readonly IPowerPlantDataService _powerPlantDataService;
        public GenerationFailJob(IPowerPlantService powerPlantService, IPowerPlantDataService powerPlantDataService)
        {
            _powerPlantService = powerPlantService;
            _powerPlantDataService = powerPlantDataService;
        }

        public async Task Generate()
        {
            var powerPlants = await _powerPlantService.GetPowerPlantId();
            var list = new List<PowerPlantData>();
            foreach (var item in powerPlants.Data)
            {
                for (int i = 0; i < 10; i++)
                {
                    PowerPlantData powerPlantData = new PowerPlantData();
                    powerPlantData.Value = "Failed";
                    powerPlantData.PowerPlantId = item;
                    powerPlantData.TimeStamp = DateTime.Now;
                    powerPlantData.Good = true;
                    list.Add(powerPlantData);
                }
            }
            await _powerPlantDataService.Insert(list);
        }
    }
}

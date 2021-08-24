using EnerjiSA.GenerationService.Common;
using EnerjiSA.GenerationService.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnerjiSA.GenerationService.Web
{
    public class HourlyQueryJob
    {
        private readonly IPowerPlantService _powerPlantService;

        public HourlyQueryJob(IPowerPlantService powerPlantService)
        {
            _powerPlantService = powerPlantService;
        }

        public async Task Run()
        {
            PowerPlantDataRequestDto request = new PowerPlantDataRequestDto();
            request.WebId = "d8af4523-3f46-43ff-9946-67c67ff135ac";
            request.StartDate = DateTime.Now.AddHours(-10).ToString();
            request.EndDate = DateTime.Now.AddHours(-1).ToString();

            var result =await _powerPlantService.GetGenerationData(request);
        }
    }
}

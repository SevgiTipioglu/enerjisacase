using EnerjiSA.GenerationService.API.Extensions;
using EnerjiSA.GenerationService.Common;
using EnerjiSA.GenerationService.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnerjiSA.GenerationService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SorgulamaController : ControllerBase
    {
        private readonly IPowerPlantService _generationDataService;
        public SorgulamaController(IPowerPlantService generationDataService)
        {
            _generationDataService = generationDataService;
        }

        [HttpPost("GetGenerationData")]
        public async Task<ServiceResponse<List<PowerPlantResponseDto>>> Sorgula([FromBody] PowerPlantDataRequestDto request)
        {
            var result = await _generationDataService.GetGenerationData(request);
            return result;
        }

        [HttpGet("{message}")]
        public async Task<IActionResult> Sorgula(string message)
        {
            var result =  $"pong {message}";
            return  Ok(result);
        }        
    }
}

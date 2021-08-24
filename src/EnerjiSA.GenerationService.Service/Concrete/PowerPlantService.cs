using EnerjiSA.GenerationService.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URF.Core.Abstractions;
using URF.Core.Abstractions.Trackable;
using URF.Core.Services;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using EnerjiSA.GenerationService.Common;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.Extensions.Logging;

namespace EnerjiSA.GenerationService.Service
{
    public class PowerPlantService : Service<PowerPlant>, IPowerPlantService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfigurationProvider _mapperConfiguration;
        private readonly ILogger<PowerPlantService> _logger;

        public PowerPlantService(ITrackableRepository<PowerPlant> repository, IUnitOfWork unitOfWork, IMapper mapper, ILogger<PowerPlantService> logger) : base(repository)
        {
            _mapper = mapper;
            _mapperConfiguration = _mapper.ConfigurationProvider;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ServiceResponse<List<PowerPlantResponseDto>>> GetGenerationData(PowerPlantDataRequestDto request)
        {
            var StartDate = Convert.ToDateTime(request.StartDate);
            var EndDate = Convert.ToDateTime(request.EndDate);

            var data = await Repository
             .Queryable()
             .Include(x => x.PowerPlantDatas.Where(y => y.TimeStamp >= StartDate && y.TimeStamp <= EndDate))
             .Where(x => x.WebId == request.WebId)
             .ProjectTo<PowerPlantResponseDto>(_mapperConfiguration)
             .ToListAsync();

            _logger.LogInformation($"GetGenerationData invoked");

            return ServiceResponse<List<PowerPlantResponseDto>>.SuccessResult(data);
        }

        public async Task<ServiceResponse<List<int>>> GetPowerPlantId()
        {
            var data = await Repository
               .Queryable().Select(x => x.Id)
               .ToListAsync();

            _logger.LogInformation($"GetPowerPlantId invoked");

            return ServiceResponse<List<int>>.SuccessResult(data);
        }


        public async Task<bool> GenerateData()
        {
            var powerPlantData = new PowerPlantData();
            powerPlantData.Value = new Random().NextDouble().ToString();
            powerPlantData.TimeStamp = DateTime.Now;
            powerPlantData.TrackingState = TrackableEntities.Common.Core.TrackingState.Added;

            PowerPlant powerPlant = new PowerPlant();
            powerPlant.WebId = Guid.NewGuid().ToString();
            powerPlant.PowerPlantDatas = new List<PowerPlantData>();
            powerPlant.PowerPlantDatas.Add(powerPlantData);
            powerPlantData.PowerPlantId = powerPlant.Id;
            powerPlantData.PowerPlant = powerPlant;
            Repository.Insert(powerPlant);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation($"GenerateData invoked");  

            return true;
        }


        public async Task<int> AddPowerPlant(PowerPlant powerPlant)
        {
            _logger.LogInformation($"AddPowerPlant invoked");

            Repository.Insert(powerPlant);
            var affected = await _unitOfWork.SaveChangesAsync();
            return affected;
        }

        public async Task<int> UpdatePowerPlant(PowerPlant powerPlant)
        {
            _logger.LogInformation($"UpdatePowerPlant invoked");

            Repository.Update(powerPlant);
            var affected = await _unitOfWork.SaveChangesAsync();
            return affected;
        }

        public async Task<int> DeleteById(int id)
        {
            _logger.LogInformation($"DeleteById invoked");

            var findsPlants = Repository.Queryable().Where(a => a.Id == id).FirstOrDefault();
            Repository.Delete(findsPlants);

            var affected = await _unitOfWork.SaveChangesAsync();
            return affected;
        }

        public async Task<List<PowerPlant>> GetAll()
        {
            _logger.LogInformation($"GetAll invoked");

            var findsPlants = Repository.Queryable().ToList();
            return findsPlants;
        }

        public async Task<PowerPlant> GetById(int id)
        {
            _logger.LogInformation($"GetById invoked");

            var findsPlant = Repository.Queryable().Where(x => x.Id == id).FirstOrDefault();
            return findsPlant;
        }
    }
}

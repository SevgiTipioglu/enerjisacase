using URF.Core.Services;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using EnerjiSA.GenerationService.Common;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EnerjiSA.GenerationService.Entity.Entities;
using URF.Core.Abstractions;
using URF.Core.Abstractions.Trackable;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace EnerjiSA.GenerationService.Service.Concrete
{
    public class PowerPlantDataService : Service<PowerPlantData>, IPowerPlantDataService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfigurationProvider _mapperConfiguration;

        public PowerPlantDataService(ITrackableRepository<PowerPlantData> repository, IUnitOfWork unitOfWork, IMapper mapper) : base(repository)
        {
            _mapper = mapper;
            _mapperConfiguration = _mapper.ConfigurationProvider;
            _unitOfWork = unitOfWork;
        }     

        public async Task<bool> Insert(List<PowerPlantData> list)
        {
            foreach (var item in list)
            {
                Repository.Insert(item);
            }
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}

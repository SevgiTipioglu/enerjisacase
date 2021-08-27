using EnerjiSA.GenerationService.Entity.Entities;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using EnerjiSA.GenerationService.Common;
using URF.Core.Abstractions.Trackable;
using URF.Core.Abstractions;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace EnerjiSA.GenerationService.Service.Tests
{
    public class PowerPlantServiceTests
    {
        Mock<IPowerPlantService> powerPlantServiceMock = new Mock<IPowerPlantService>();
        Mock<ITrackableRepository<PowerPlant>> trackablePowerPlantRepositoryMock = new Mock<ITrackableRepository<PowerPlant>>();
        Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
        Mock<IMapper> mapperMock = new Mock<IMapper>();
        Mock<ILogger<PowerPlantService>> loggerMock = new Mock<ILogger<PowerPlantService>>();

        [Fact]
        public async void Should_AddPowerPlant_Is_Success()
        {
            var expected = 1;

            unitOfWorkMock.Setup(st => st.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(expected));

            PowerPlantService powerPlantService = new PowerPlantService(
                trackablePowerPlantRepositoryMock.Object,
                unitOfWorkMock.Object,
                mapperMock.Object,
                loggerMock.Object
                );

            var result = await powerPlantService.AddPowerPlant(It.IsAny<PowerPlant>());

            result.Should().Be(expected);
            trackablePowerPlantRepositoryMock.Verify(x => x.Insert(It.IsAny<PowerPlant>()), Times.Once);
            unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async void Should_UpdatePowerPlant_Is_Success()
        {
            var expected = 1;
            unitOfWorkMock.Setup(st => st.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(expected));

            PowerPlantService powerPlantService = new PowerPlantService(
               trackablePowerPlantRepositoryMock.Object,
               unitOfWorkMock.Object,
               mapperMock.Object,
               loggerMock.Object
               );

            var result = await powerPlantService.UpdatePowerPlant(new PowerPlant());

            result.Should().Be(expected);
            trackablePowerPlantRepositoryMock.Verify(x => x.Update(It.IsAny<PowerPlant>()), Times.Once);
            unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async void Should_GetById_Is_Success()
        {
            var expected = new PowerPlant();
            expected.Id = 101;
            expected.WebId = Guid.NewGuid().ToString();
            expected.PowerPlantDatas = new List<PowerPlantData>();
            expected.PowerPlantDatas.Add(new PowerPlantData { Id = 201, Good = true, PowerPlantId = expected.Id, TimeStamp = DateTime.Now, Value = new Random().NextDouble().ToString() });
            expected.PowerPlantDatas.Add(new PowerPlantData { Id = 202, Good = true, PowerPlantId = expected.Id, TimeStamp = DateTime.Now, Value = new Random().NextDouble().ToString() });
            powerPlantServiceMock.Setup(st => st.GetById(It.IsAny<int>())).Returns(Task.FromResult(expected));

            List<PowerPlant> list = new List<PowerPlant>();
            list.Add(expected);

            trackablePowerPlantRepositoryMock.Setup(st => st.Queryable()).Returns(ToDbSet<PowerPlant>(list));

            PowerPlantService powerPlantService = new PowerPlantService(
             trackablePowerPlantRepositoryMock.Object,
             unitOfWorkMock.Object,
             mapperMock.Object,
             loggerMock.Object
             );

            var actual = await powerPlantService.GetById(expected.Id);

            actual.Should().NotBeNull();
            actual.Id.Should().Be(expected.Id);
            actual.WebId.Should().Be(expected.WebId);
            actual.PowerPlantDatas.Should().NotBeNull();
            actual.PowerPlantDatas.Should().HaveCount(2);
            actual.PowerPlantDatas.Should().BeEquivalentTo(expected.PowerPlantDatas);
        }

        [Fact]
        public async void Should_GetAll_Is_Success()
        {
            var expected = new List<PowerPlant>();
            expected.Add(new PowerPlant { Id = 501, WebId = Guid.NewGuid().ToString(), PowerPlantDatas = new List<PowerPlantData> { { new PowerPlantData { Id = 601, PowerPlantId = 501, Value = new Random().NextDouble().ToString() } } } });
            expected.Add(new PowerPlant { Id = 502, WebId = Guid.NewGuid().ToString(), PowerPlantDatas = new List<PowerPlantData> { { new PowerPlantData { Id = 602, PowerPlantId = 502, Value = new Random().NextDouble().ToString() } } } });
            expected.Add(new PowerPlant { Id = 503, WebId = Guid.NewGuid().ToString(), PowerPlantDatas = new List<PowerPlantData> { { new PowerPlantData { Id = 603, PowerPlantId = 503, Value = new Random().NextDouble().ToString() } } } });

            trackablePowerPlantRepositoryMock.Setup(st => st.Queryable()).Returns(ToDbSet<PowerPlant>(expected));

            PowerPlantService powerPlantService = new PowerPlantService(
                 trackablePowerPlantRepositoryMock.Object,
                 unitOfWorkMock.Object,
                 mapperMock.Object,
                 loggerMock.Object
                 );

            var actual = await powerPlantService.GetAll();


            actual.Should().NotBeNull();
            actual.Should().HaveCount(expected.Count());
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async void Should_Delete_Is_Success()
        {
            var expected = 1;

            unitOfWorkMock.Setup(st => st.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(expected));

            PowerPlantService powerPlantService = new PowerPlantService(
             trackablePowerPlantRepositoryMock.Object,
             unitOfWorkMock.Object,
             mapperMock.Object,
             loggerMock.Object
             );

            var actual = await powerPlantService.DeleteById(It.IsAny<int>());

            actual.Should().Be(expected);
            trackablePowerPlantRepositoryMock.Verify(x => x.Delete(It.IsAny<PowerPlant>()), Times.Once);
            unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async void Should_GetPowerPlantId_Is_Success()
        {
            List<PowerPlant> list = new List<PowerPlant>();
            var data = new List<int> { 601, 701, 801, 901 };
            foreach (var item in data)
            {
                list.Add(new PowerPlant { Id = item });
            }

            var expected = ServiceResponse<List<int>>.SuccessResult(data);
            trackablePowerPlantRepositoryMock.Setup(st => st.Queryable()).Returns(ToDbSet<PowerPlant>(list));

            PowerPlantService powerPlantService = new PowerPlantService(
                trackablePowerPlantRepositoryMock.Object,
                unitOfWorkMock.Object,
                mapperMock.Object,
                loggerMock.Object
            );

            var actual = await powerPlantService.GetPowerPlantId();

            actual.Should().BeEquivalentTo(expected);
            actual.Data.Should().NotBeEmpty();
            actual.Data.Should().HaveCount(expected.Data.Count);
            actual.Success.Should().BeTrue();
            actual.Message.Should().BeEmpty();
        }

        //[Fact]
        //public async void Should_GetGenerationData_Is_Success()
        //{
        //    var data = new List<PowerPlantResponseDto>();
        //    data.Add(new PowerPlantResponseDto { Id = 801, WebId = Guid.NewGuid().ToString(), PowerPlantDatas = new List<PowerPlantDataResponseDto> { { new PowerPlantDataResponseDto { Id = 901, Value = new Random().NextDouble().ToString(), TimeStamp = DateTime.Now } } } });
        //    data.Add(new PowerPlantResponseDto { Id = 802, WebId = Guid.NewGuid().ToString(), PowerPlantDatas = new List<PowerPlantDataResponseDto> { { new PowerPlantDataResponseDto { Id = 902, Value = new Random().NextDouble().ToString(), TimeStamp = DateTime.Now } } } });
        //    data.Add(new PowerPlantResponseDto { Id = 803, WebId = Guid.NewGuid().ToString(), PowerPlantDatas = new List<PowerPlantDataResponseDto> { { new PowerPlantDataResponseDto { Id = 903, Value = new Random().NextDouble().ToString(), TimeStamp = DateTime.Now } } } });
        //    data.Add(new PowerPlantResponseDto { Id = 804, WebId = Guid.NewGuid().ToString(), PowerPlantDatas = new List<PowerPlantDataResponseDto> { { new PowerPlantDataResponseDto { Id = 904, Value = new Random().NextDouble().ToString(), TimeStamp = DateTime.Now } } } });
        //    var expected = ServiceResponse<List<PowerPlantResponseDto>>.SuccessResult(data);

        //    powerPlantServiceMock.Setup(st => st.GetGenerationData(It.IsAny<PowerPlantDataRequestDto>())).Returns(Task.FromResult(expected));
        //    var actual = await powerPlantServiceMock.Object.GetGenerationData(It.IsAny<PowerPlantDataRequestDto>());

        //    actual.Should().Be(expected);
        //    actual.Should().BeEquivalentTo(expected);
        //    actual.Data.Should().NotBeEmpty();
        //    actual.Data.Should().HaveCount(expected.Data.Count);
        //    actual.Success.Should().BeTrue();
        //    actual.Message.Should().BeEmpty();
        //}

        public static DbSet<T> ToDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());
            dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>(sourceList.Add);
            return dbSet.Object;
        }
    }
}

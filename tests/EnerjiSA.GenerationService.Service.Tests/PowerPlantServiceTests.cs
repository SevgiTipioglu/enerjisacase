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

namespace EnerjiSA.GenerationService.Service.Tests
{
    public class PowerPlantServiceTests  
    {
        Mock<IPowerPlantService> powerPlantServiceMock = new Mock<IPowerPlantService>();

        [Fact]
        public async void Should_AddPowerPlant_Is_Success()
        {
            var expected = 1;
            powerPlantServiceMock.Setup(st => st.AddPowerPlant(It.IsAny<PowerPlant>())).Returns(Task.FromResult(expected));

            var result = await powerPlantServiceMock.Object.AddPowerPlant(It.IsAny<PowerPlant>());

            result.Should().Be(expected);
            powerPlantServiceMock.Verify(x => x.AddPowerPlant(It.IsAny<PowerPlant>()), Times.Once);
        }

        [Fact]
        public async void Should_UpdatePowerPlant_Is_Success()
        {
            var expected = 1;
            powerPlantServiceMock.Setup(st => st.UpdatePowerPlant(It.IsAny<PowerPlant>())).Returns(Task.FromResult(expected));

            var result = await powerPlantServiceMock.Object.UpdatePowerPlant(new PowerPlant());

            result.Should().Be(expected);
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

            var actual = await powerPlantServiceMock.Object.GetById(It.IsAny<int>());

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
            powerPlantServiceMock.Setup(st => st.GetAll()).Returns(Task.FromResult(expected));

            var actual = await powerPlantServiceMock.Object.GetAll();


            actual.Should().NotBeNull();
            actual.Should().HaveCount(expected.Count());
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async void Should_Delete_Is_Success()
        {
            var expected = 1;
            powerPlantServiceMock.Setup(st => st.DeleteById(It.IsAny<int>())).Returns(Task.FromResult(expected));

            var actual = await powerPlantServiceMock.Object.DeleteById(It.IsAny<int>());

            actual.Should().Be(expected);
        }

        [Fact]
        public async void Should_GetPowerPlantId_Is_Success()
        {
            var data = new List<int> { 601, 701, 801, 901 };
            var expected = ServiceResponse<List<int>>.SuccessResult(data);
            powerPlantServiceMock.Setup(st => st.GetPowerPlantId()).Returns(Task.FromResult(expected));

            var actual = await powerPlantServiceMock.Object.GetPowerPlantId();

            actual.Should().Be(expected);
            actual.Should().BeEquivalentTo(expected);
            actual.Data.Should().NotBeEmpty();
            actual.Data.Should().HaveCount(expected.Data.Count);
            actual.Success.Should().BeTrue();
            actual.Message.Should().BeEmpty();
        }

        [Fact]
        public async void Should_GetGenerationData_Is_Success()
        {
            var data = new List<PowerPlantResponseDto>();
            data.Add(new PowerPlantResponseDto { Id = 801, WebId = Guid.NewGuid().ToString(), PowerPlantDatas = new List<PowerPlantDataResponseDto> { { new PowerPlantDataResponseDto { Id = 901, Value = new Random().NextDouble().ToString(), TimeStamp = DateTime.Now } } } });
            data.Add(new PowerPlantResponseDto { Id = 802, WebId = Guid.NewGuid().ToString(), PowerPlantDatas = new List<PowerPlantDataResponseDto> { { new PowerPlantDataResponseDto { Id = 902, Value = new Random().NextDouble().ToString(), TimeStamp = DateTime.Now } } } });
            data.Add(new PowerPlantResponseDto { Id = 803, WebId = Guid.NewGuid().ToString(), PowerPlantDatas = new List<PowerPlantDataResponseDto> { { new PowerPlantDataResponseDto { Id = 903, Value = new Random().NextDouble().ToString(), TimeStamp = DateTime.Now } } } });
            data.Add(new PowerPlantResponseDto { Id = 804, WebId = Guid.NewGuid().ToString(), PowerPlantDatas = new List<PowerPlantDataResponseDto> { { new PowerPlantDataResponseDto { Id = 904, Value = new Random().NextDouble().ToString(), TimeStamp = DateTime.Now } } } });
            var expected = ServiceResponse<List<PowerPlantResponseDto>>.SuccessResult(data);

            powerPlantServiceMock.Setup(st => st.GetGenerationData(It.IsAny<PowerPlantDataRequestDto>())).Returns(Task.FromResult(expected));
            var actual = await powerPlantServiceMock.Object.GetGenerationData(It.IsAny<PowerPlantDataRequestDto>());

            actual.Should().Be(expected);
            actual.Should().BeEquivalentTo(expected);
            actual.Data.Should().NotBeEmpty();
            actual.Data.Should().HaveCount(expected.Data.Count);
            actual.Success.Should().BeTrue();
            actual.Message.Should().BeEmpty();
        }
    }
}

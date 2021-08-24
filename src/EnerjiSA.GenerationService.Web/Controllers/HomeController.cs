using EnerjiSA.GenerationService.Entity.Entities;
using EnerjiSA.GenerationService.Service;
using EnerjiSA.GenerationService.Web.Models;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EnerjiSA.GenerationService.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPowerPlantService _powerPlantService;

        public HomeController(ILogger<HomeController> logger, IPowerPlantService powerPlantService)
        {
            _logger = logger;
            _powerPlantService = powerPlantService;
        }

        public async Task<IActionResult> Index()
        {
            //BackgroundJob.Enqueue(() => dbManager.DoSomething());
           var powerPlantList= await _powerPlantService.GetAll();
           List<PowerPlantViewModel> vm = new List<PowerPlantViewModel>();
            foreach (var item in powerPlantList)
            {
                PowerPlantViewModel model = new PowerPlantViewModel();
                model.Id = item.Id;
                model.WebId = item.WebId;
                vm.Add(model);
            }

            return View(vm.ToList());
        }

        public async Task<IActionResult> Edit(int id)
        {
            var powerPlant = await _powerPlantService.GetById(id);
            PowerPlantViewModel model = new PowerPlantViewModel();
            model.Id = powerPlant.Id;
            model.WebId = powerPlant.WebId;
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(PowerPlantViewModel model)
        {

            PowerPlant powerPlant = new PowerPlant();
            powerPlant.Id = model.Id;
            powerPlant.WebId = model.WebId;

            await _powerPlantService.UpdatePowerPlant(powerPlant);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _powerPlantService.DeleteById(id);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(PowerPlantViewModel model)
        {
            PowerPlant powerPlant = new PowerPlant();
            powerPlant.WebId = model.WebId;

            await _powerPlantService.AddPowerPlant(powerPlant);
            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using Domain.ViewModel;
using Infrastructure.Service.CustomService.Flights;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly IFlightService _flightService;
        private readonly IWebHostEnvironment _environment;

        public FlightController(IFlightService flightService, IWebHostEnvironment environment)
        {
            _flightService = flightService;
            _environment = environment;
        }

        [Route("GetAllFlight")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var res = await _flightService.GetAll();
            return Ok(res);
        }

        [Route("GetFlightByID")]
        [HttpGet]
        public async Task<IActionResult> Get(Guid id)
        {
            if (id != Guid.Empty)
            {
                var res = await _flightService.Get(id);
                return Ok(res);
            }
            return NotFound();
        }

        [Route("AddFlight")]
        [HttpPost]
        public async Task<IActionResult> Add(FlightInsertViewModel flightInsertViewModel)
        {
            if (ModelState.IsValid)
            {
                var img = await UploadImage(flightInsertViewModel.Image, flightInsertViewModel.Name);
                var res = await _flightService.Add(flightInsertViewModel, img);
                return Ok(res);
            }
            return BadRequest("Something Went Wrong");
        }

        [Route("EditFlight")]
        [HttpPut]
        public async Task<IActionResult> update(FlightUpdateViewModel flat)
        {
            if (ModelState.IsValid)
            {
                var img = await UploadImage(flat.Image, flat.Name);
                await _flightService.Update(flat, img);
                return Ok();
            }
            return BadRequest("Something Went Wrong");
        }

        [Route("RemoveFlight")]
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id != Guid.Empty)
            {
                await _flightService.Delete(id);
                return Ok();
            }
            return NotFound();
        }

        private async Task<String> UploadImage(IFormFile images, String id)
        {
            String file;
            String ContentPath = this._environment.ContentRootPath;
            var extension = "." + images.FileName.Split('.')[images.FileName.Split('.').Length - 1];
            if (extension == ".png" || extension == ".jpg" || extension == ".jpeg" || extension == "JFIF")
            {
                file = id.ToLower() + "-" + extension;
                String newFileName = Regex.Replace(file, @"[^0-9a-zA-Z.]+", "");
                var paths = Path.Combine(ContentPath, "Images\\Flight");

                if (!Directory.Exists(paths))
                {
                    Directory.CreateDirectory(paths);
                }
                var path = Path.Combine(ContentPath, "Images\\Flight", newFileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await images.CopyToAsync(stream);
                }
                return newFileName;
            }
            else
                return "";

        }
    }
}

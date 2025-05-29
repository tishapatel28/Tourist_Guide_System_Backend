using Domain.ViewModel;
using Infrastructure.Service.CustomService.Cars;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;
        private readonly IWebHostEnvironment environment;

        public CarController(ICarService carService, IWebHostEnvironment environment)
        {
            _carService = carService;
            this.environment = environment;
        }

        [Route("GetAllCar")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var res = await _carService.GetAll();
            return Ok(res);
        }

        [Route("GetCarByID")]
        [HttpGet]
        public async Task<IActionResult> Get(Guid id)
        {
            if (id != Guid.Empty)
            {
                var res = await _carService.Get(id);
                return Ok(res);
            }
            return NotFound();
        }

        [Route("AddCar")]
        [HttpPost]
        public async Task<IActionResult> Add(CarInsertViewModel cats)
        {
            if (ModelState.IsValid)
            {
                var img = await UploadImage(cats.image, cats.Name);
                var res= await _carService.Add(cats, img);
                return Ok(res);
            }
            return BadRequest("Something Went Wrong");
        }

        [Route("EditCar")]
        [HttpPost]
        public async Task<IActionResult> update(CarUpdateViewModel cat)
        {
            if (ModelState.IsValid)
            {
                var img = await UploadImage(cat.image, cat.Name);
                await _carService.Update(cat,img);
                return Ok();
            }
            return BadRequest("Something Went Wrong");
        }

        [Route("RemoveCar")]
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id != Guid.Empty)
            {
                await _carService.Delete(id);
                return Ok();
            }
            return NotFound();
        }

        private async Task<String> UploadImage(IFormFile images, String id)
        {
            String file;
            String ContentPath = this.environment.ContentRootPath;
            var extension = "." + images.FileName.Split('.')[images.FileName.Split('.').Length - 1];
            if (extension == ".png" || extension == ".jpg" || extension == ".jpeg")
            {
                file = id.ToLower() + "-" + extension;
                String newFileName = Regex.Replace(file, @"[^0-9a-zA-Z.]+", "");
                var paths = Path.Combine(ContentPath, "Images\\Car");

                if (!Directory.Exists(paths))
                {
                    Directory.CreateDirectory(paths);
                }
                var path = Path.Combine(ContentPath, "Images\\Car", newFileName);
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

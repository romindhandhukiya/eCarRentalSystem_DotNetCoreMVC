using eCar_Rental_System.Models;
using eCar_Rental_System.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace eCar_Rental_System.Controllers
{
    [Authorize]
    public class CarController : Controller
    {
        private readonly ICarRepository carRepository;
        private readonly IWebHostEnvironment hostingEnvironment;
        public CarController(ICarRepository carRepository, IWebHostEnvironment hostingEnvironment)
        {
            this.carRepository = carRepository;
            this.hostingEnvironment = hostingEnvironment;
        }

        public ViewResult AllCar()
        {
            IEnumerable<Car> allcars = carRepository.GetAllCars();
            return View(allcars);
        }

        public IActionResult GetAllCar()
        {
            IEnumerable<Car> allcars = carRepository.GetAllCars();
            return View(allcars);
        }

        [HttpGet]
        public ViewResult AddCar()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddCar(CarAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if(model.Photo != null)
                {
                    string uploadFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);
                    model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                Car new_car = new Car
                {
                    Name = model.Name,
                    No_Seat = model.No_Seat,
                    Rate = model.Rate,
                    FuelType = model.FuelType,
                    Description = model.Description,
                    PhotoPath = uniqueFileName
                };
                carRepository.Add(new_car);
                return RedirectToAction("GetAllCar", "Car");
            }
            return View();
        }


        [HttpGet]
        public ViewResult EditCar(int id)
        {
            Car car = carRepository.GetCar(id);
            CarEditViewModel carEditViewModel = new CarEditViewModel
            {
                Id = car.Id,
                Name = car.Name,
                No_Seat = car.No_Seat,
                Rate = car.Rate,
                FuelType = car.FuelType,
                Description = car.Description,
                ExistingPhotoPath = car.PhotoPath
            };
            return View(carEditViewModel);
        }
        [HttpPost]
        public IActionResult EditCar(CarEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Car car = carRepository.GetCar(model.Id);
                car.Name = model.Name;
                car.No_Seat = model.No_Seat;
                car.Rate = model.Rate;
                car.FuelType = model.FuelType;
                car.Description = model.Description;

                if (model.Photo != null)
                {
                    if(model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(hostingEnvironment.WebRootPath, "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    string uploadFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    string filePath2 = Path.Combine(uploadFolder, uniqueFileName);
                    model.Photo.CopyTo(new FileStream(filePath2, FileMode.Create));
                    car.PhotoPath = uniqueFileName;
                }

                Car updatedCar = carRepository.Update(car);
                return RedirectToAction("GetAllCar", "Car");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult DeleteCar(int id)
        {
            Car car = carRepository.GetCar(id);
            if(car == null)
            {
                return NotFound();
            }
            return View(car);
        }
        [HttpPost, ActionName("DeleteCar")]
        public IActionResult DeleteConfirmedCar(int id)
        {
            Car car = carRepository.GetCar(id);
            carRepository.Delete(car.Id);
            return RedirectToAction("GetAllCar", "Car");
        }
    }
}

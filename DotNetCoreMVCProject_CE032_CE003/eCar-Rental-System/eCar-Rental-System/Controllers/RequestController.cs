using eCar_Rental_System.Models;
using eCar_Rental_System.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCar_Rental_System.Controllers
{
    [Authorize]
    public class RequestController : Controller
    {
        private readonly IRequestRepository requestRepository;
        private readonly ICarRepository carRepository;
        private readonly UserManager<IdentityUser> userManager;
        public RequestController(IRequestRepository requestRepository, ICarRepository carRepository, UserManager<IdentityUser> userManager)
        {
            this.requestRepository = requestRepository;
            this.carRepository = carRepository;
            this.userManager = userManager;
        }

        public IActionResult MyRequests()
        {
            IEnumerable<Request> requests = requestRepository.GetRequests();
            
            return View(requests);
        }

        public IActionResult AllPendingRequest()
        {
            IEnumerable<Request> requests = requestRepository.GetPendingRequests();
            return View(requests);
        }

        [HttpGet]
        public IActionResult MakeRequest(int id)
        {
            Car car = carRepository.GetCar(id);
            ViewData["car"] = car;
            return View();
        }
        [HttpPost]
        public IActionResult MakeRequest(RequestViewModel model, int id)
        {
            if (ModelState.IsValid)
            {
                Request request = new Request
                {
                    StartingDate = model.StartingDate,
                    EndingDate = model.EndingDate,
                    Status = "Pending"
                };
                requestRepository.Add(request,id);
                return RedirectToAction("AllCar", "Car");
            }
            return View();
        }

        [HttpGet]
        public IActionResult EditRequest(int id)
        {
            Request request = requestRepository.GetRequest(id);
            
            return View(request);
        }
        [HttpPost]
        public IActionResult EditRequest(RequestViewModel model, int id)
        {
            if (ModelState.IsValid)
            {
                Request request = requestRepository.GetRequest(id);
                request.StartingDate = model.StartingDate;
                request.EndingDate = model.EndingDate;

                Request updatedRequest = requestRepository.Update(request);
                return RedirectToAction("MyRequests", "Request");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult DeleteRequest(int id)
        {
            Request request = requestRepository.GetRequest(id);

            return View(request);
        }
        [HttpPost, ActionName("DeleteRequest")]
        public IActionResult DeleteConfirmedRequest(int id)
        {
            Request request = requestRepository.GetRequest(id);
            requestRepository.Delete(request.Id);
            return RedirectToAction("MyRequests", "Request");
        }

        public IActionResult AcceptRequest(int id)
        {
            requestRepository.AcceptRequest(id);
            return RedirectToAction("AllPendingRequest", "Request");
        }

        public IActionResult DeclineRequest(int id)
        {
            requestRepository.DeclineRequest(id);
            return RedirectToAction("AllPendingRequest", "Request");
        }
    }
}

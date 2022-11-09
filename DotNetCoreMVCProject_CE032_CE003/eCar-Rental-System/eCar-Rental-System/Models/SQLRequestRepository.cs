using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace eCar_Rental_System.Models
{
    public class SQLRequestRepository : IRequestRepository
    {
        private readonly AppDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        public SQLRequestRepository(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
        }

        IEnumerable<Request> IRequestRepository.GetRequests()
        {
            var usrid = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return context.requests.Where(r => r.user.Id == usrid).Include(r => r.car).ToList();
        }

        IEnumerable<Request> IRequestRepository.GetPendingRequests()
        {
            return context.requests.Where(r => r.Status == "Pending").Include(r => r.car).ToList();
        }

        Request IRequestRepository.GetRequest(int id)
        {
            return context.requests.Where(r => r.Id==id).Include(r => r.car).FirstOrDefault();
        }

        Request IRequestRepository.Add(Request request, int carid)
        {
            var userid = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            request.user = context.Users.Find(userid);
            request.car = context.cars.Find(carid);
            context.requests.Add(request);
            context.SaveChanges();
            return request;
        }

        Request IRequestRepository.Update(Request requestChanges)
        {
            //Car car = context.cars.Attach(carChanges);
            //car.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.Entry(requestChanges).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return requestChanges;
        }
        Request IRequestRepository.Delete(int id)
        {
            Request request = context.requests.Find(id);
            if (request != null)
            {
                context.requests.Remove(request);
                context.SaveChanges();
            }
            return request;
        }

        void IRequestRepository.AcceptRequest(int id)
        {
            Request request = context.requests.Find(id);
            request.Status = "Accepted";
            context.Entry(request).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }

        void IRequestRepository.DeclineRequest(int id)
        {
            Request request = context.requests.Find(id);
            request.Status = "Declined";
            context.Entry(request).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }
    }
}

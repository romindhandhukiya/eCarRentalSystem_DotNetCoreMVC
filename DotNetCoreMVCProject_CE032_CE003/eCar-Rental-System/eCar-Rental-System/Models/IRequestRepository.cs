using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCar_Rental_System.Models
{
    public interface IRequestRepository
    {
        Request GetRequest(int id);
        IEnumerable<Request> GetRequests();
        IEnumerable<Request> GetPendingRequests();
        Request Add(Request request, int carid);
        Request Update(Request requestChanges);
        Request Delete(int id);
        void AcceptRequest(int id);
        void DeclineRequest(int id);
    }
}

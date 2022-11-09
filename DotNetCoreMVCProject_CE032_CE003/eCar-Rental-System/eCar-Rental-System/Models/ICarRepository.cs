using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCar_Rental_System.Models
{
    public interface ICarRepository
    {
        Car GetCar(int id);
        IEnumerable<Car> GetAllCars();
        Car Add(Car car);
        Car Update(Car carChanges);
        Car Delete(int id);
    }
}

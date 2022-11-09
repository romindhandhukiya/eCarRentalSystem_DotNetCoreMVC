using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCar_Rental_System.Models
{
    public class SQLCarRepository : ICarRepository
    {
        private readonly AppDbContext context;
        public SQLCarRepository(AppDbContext context)
        {
            this.context = context;
        }

        IEnumerable<Car> ICarRepository.GetAllCars()
        {
            return context.cars;
        }

        Car ICarRepository.GetCar(int id)
        {
            return context.cars.Find(id);
        }

        Car ICarRepository.Add(Car car)
        {
            context.cars.Add(car);
            context.SaveChanges();
            return car;
        }

        Car ICarRepository.Update(Car carChanges)
        {
            //Car car = context.cars.Attach(carChanges);
            //car.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.Entry(carChanges).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return carChanges;
        }
         Car ICarRepository.Delete(int id)
         {
            Car car = context.cars.Find(id);
            if(car != null)
            {
                context.cars.Remove(car);
                context.SaveChanges();
            }
            return car;
         }
    }
}

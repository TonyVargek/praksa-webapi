using Example.Common;
using Example.Model;
using Example.Repository;
using Example.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Service
{
    public class FoodService : IFoodService
    {
        public async Task<bool> AddAsync(Food food)
        {
            FoodRepository repository = new FoodRepository();
            return await repository.AddAsync(food);
        }

        public async Task<bool> AddAsync(List<Food> foods)
        {
            FoodRepository repository = new FoodRepository();
            return await repository.AddAsync(foods);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            FoodRepository repository = new FoodRepository();
            return await repository.DeleteAsync(id);
        }

        public async Task<List<Food>> GetAllAsync(FoodFilter filter)
        {
            FoodRepository repository = new FoodRepository();
            return await repository.GetAllAsync(filter);
        }

        public async Task<Food> GetByIdAsync(int id)
        {
            FoodRepository repository = new FoodRepository();
            return await repository.GetByIdAsync(id);
        }

        public async Task<bool> UpdateAsync(int id, Food newFood)
        {
            FoodRepository repository = new FoodRepository();
            return await repository.UpdateAsync(id, newFood);
        }
    }
}

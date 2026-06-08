using Example.Common;
using Example.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Service.Common
{
    public interface IFoodService
    {
        public Task<bool> AddAsync(Food food);
        public Task<bool> AddAsync(List<Food> foods);
        public Task<bool> UpdateAsync(int id, Food newFood);
        public Task<bool> DeleteAsync(int id);
        public Task<List<Food>> GetAllAsync(FoodFilter filter);
        public Task<Food> GetByIdAsync(int id);
    }
}

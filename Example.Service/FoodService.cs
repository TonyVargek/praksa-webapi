using Example.Common;
using Example.Model;
using Example.Service.Common;
using Example.Repository.Common;

namespace Example.Service
{
    public class FoodService : IFoodService
    {
        protected IFoodRepository FoodRepository { get; }

        public FoodService(IFoodRepository foodRepository)
        {
            FoodRepository = foodRepository;
        }

        public async Task<bool> AddAsync(Food food)
        {
            return await FoodRepository.AddAsync(food);
        }

        public async Task<bool> AddAsync(List<Food> foods)
        {
            return await FoodRepository.AddAsync(foods);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await FoodRepository.DeleteAsync(id);
        }

        public async Task<List<Food>> GetAllAsync(FoodFilter filter)
        {
            return await FoodRepository.GetAllAsync(filter);
        }

        public async Task<Food> GetByIdAsync(int id)
        {
            return await FoodRepository.GetByIdAsync(id);
        }

        public async Task<bool> UpdateAsync(int id, Food newFood)
        {
            return await FoodRepository.UpdateAsync(id, newFood);
        }
    }
}
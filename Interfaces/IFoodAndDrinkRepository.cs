﻿using GoTravnikApi.Models;

namespace GoTravnikApi.Interfaces
{
    public interface IFoodAndDrinkRepository
    {
        public Task<List<FoodAndDrink>> GetFoodAndDrinks();
        public Task<FoodAndDrink> GetFoodAndDrink(int id);
        public Task<bool> FoodAndDrinkExists(int id);
    }
}

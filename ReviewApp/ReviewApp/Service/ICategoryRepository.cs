﻿using ReviewApp.Models;

namespace ReviewApp.Service
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
        Category? GetCategory(int id);

        ICollection<Pokemon> GetPokemonByCategory(int categoryId);
        bool IsCategoryExists(int id);

    }
}

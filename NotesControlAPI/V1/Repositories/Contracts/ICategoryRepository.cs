using NotesControlAPI.V1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesControlAPI.V1.Repositories.Contracts
{
    public interface ICategoryRepository
    {
        int InsertCategory(CategoryModel category, int userId);
        CategoryModel GetCategoryById(int categoryID, int userId);
        List<CategoryModel> GetCategoryByName(string name , int userId);
        void UpdateCategoryArchives();
        void UpdateCategoryById(CategoryModel category);
    }
}

using Microsoft.EntityFrameworkCore;
using NotesControlAPI.Database;
using NotesControlAPI.V1.Models;
using NotesControlAPI.V1.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesControlAPI.V1.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly NotesControlContext _context;

        public CategoryRepository(NotesControlContext context)
        {
            _context = context;
        }

        public int InsertCategory(CategoryModel category, int userId)
        {
            category.UserId = userId;
            _context.Categories.Add(category);
            _context.SaveChanges();

            return category.Category_Id;
        }

        public CategoryModel GetCategoryById(int categoryID, int userId)
        {
            var category = _context.Categories.AsNoTracking().Where(x => x.Category_Id == categoryID && x.UserId == userId).FirstOrDefault();
            return category;
        }

        public List<CategoryModel> GetCategoryByName(string name, int userId)
        {
            var categories = _context.Categories.Where(x => x.Name.Contains(name) && x.UserId == userId).ToList();
            return categories;
        }

        public void UpdateCategoryArchives()
        {
            
        }

        public void UpdateCategoryById(CategoryModel category)
        {
            _context.Update(category);
            _context.SaveChanges();
        }
    }
}

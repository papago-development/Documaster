using Documaster.Business.Models;
using Documaster.Model.Entities;
using System.Collections.Generic;

namespace Documaster.Business.Services
{
    public interface ICategoryService
    {
        List<Category> GetCategories();
        Category CreateCategory(Category category);
        Category GetById(int id);
        bool EditCategory(Category category);
        Category DeleteById(int id);
        bool DeleteCategory(Category category);
        IEnumerable<Category> GetListOfCategories();
        IEnumerable<AssignedCategory> GetCategoriesByAssignedCategory(int id);
        bool DoesNumberExist(Category category);

    }
}

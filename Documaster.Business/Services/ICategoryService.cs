using Documaster.Business.Models;
using Documaster.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documaster.Business.Services
{
    public interface ICategoryService
    {
        List<Category> GetCategories();
        Category CreateCategory(Category category); // ???
        Category GetById(int id);
        bool EditCategory(Category category);

        Category DeleteById(int id);
        bool DeleteCategory(Category category);

        IEnumerable<Category> GetListOfCategories();

        // ???
        IEnumerable<AssignedCategory> GetCategoriesByAssignedCategory(int id);
    }
}

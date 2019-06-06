using System.Collections.Generic;
using System.Linq;
using Documaster.Business.Models;
using Documaster.Model.Entities;
using System.Data.Entity.Infrastructure;

namespace Documaster.Business.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Category> _categoryRepository;

        public CategoryService(IGenericRepository<Category> categoryRepository,
                               IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        //Create new category
        public Category CreateCategory(Category category)
        {
           var newCategory = _categoryRepository.Create(category);
           _unitOfWork.SaveChanges();
           return newCategory;
        }

        //Get category by specific Id
        public Category GetById(int id)
        {
            return _categoryRepository.Get(id);
        }

        //Get all categories
        public List<Category> GetCategories()
        {
            return _categoryRepository.GetAll().OrderBy(x => x.Name).ToList();
        }

        //Delete category by specific Id
        public Category DeleteById(int id)
        {
            return _categoryRepository.Get(id);
        }

        //Delete category
        public bool DeleteCategory(Category category)
        {
            try
            {
                var deleteCategory = _categoryRepository.Delete(category.Id);
                _unitOfWork.SaveChanges();
                return deleteCategory;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }

        //Edit category
        public bool EditCategory(Category category)
        {
            var edit = _categoryRepository.Update(category, new List<string> { "Name" });
            _unitOfWork.SaveChanges();
            return edit;
        }

        public IEnumerable<Category> GetListOfCategories()
        {
            return _categoryRepository.GetAll().ToList();
        }

        public IEnumerable<AssignedCategory> GetCategoriesByAssignedCategory(int id)
        {
            var allCategories = _categoryRepository.GetAll();
            var categoriesWithAssignments =  allCategories.Where(x => x.Requirements.Any());

            var assignedCategories = categoriesWithAssignments
                     .OrderBy(x => x.Number).ThenBy(x => x.Name)
                     .Select(x => new AssignedCategory
                     {
                         Id = x.Id,
                         Name = x.Name,
                         AssignedRequirements = x.Requirements.OrderBy(m => m.Number).ThenBy(m => m.Name).Select(y => new AssignedRequirement
                         {
                            Assigned = y.ProjectRequirements.Any(z => z.ProjectId == id),
                            Name = y.Name,
                            Id = y.Id
                         }).ToList()
                     });

            return assignedCategories;
        }

        public bool DoesNumberExist(Category category)
        {
            var categories = _categoryRepository.Get(x => x.Number == category.Number && x.Id != category.Id);
            return categories.Any();
        }
    }
}

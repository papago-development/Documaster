using Microsoft.VisualStudio.TestTools.UnitTesting;
using Documaster.Business.Services;
using Documaster.Model.Entities;
using Rhino.Mocks;
using System.Collections.Generic;
using System.Linq;

namespace Documaster.Business.Tests
{
    [TestClass]
    public class CategoryServiceTests
    {
        private static  IUnitOfWork _unitOfWork;
        private static IGenericRepository<Category> _categoryRepository;
        private const string categoryName = "Casa";
        private IEnumerable<Category> categories;
        private static Category category = new Category
        {
            Id = 1,
            Name = categoryName,
            Requirements = new List<Requirement>
                    {
                        new Requirement
                        {
                            Id = 1
                        }
                    }
        };
       

        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {
            _categoryRepository = MockRepository.GenerateMock<IGenericRepository<Category>>();
            _unitOfWork = MockRepository.GenerateMock<IUnitOfWork>();
        }

        [TestInitialize]
        public void TestInit()
        {
            _categoryRepository.Stub(m => m.Update(category, new List<string>())).Return(true);
            _categoryRepository.Stub(m => m.Create(category)).Return(category);

            categories = new List<Category>
            {
                category,
                new Category
                {
                    Id = 2,
                    Name = "Bloc",
                    Requirements = new List<Requirement>
                    {
                        new Requirement
                        {
                            Id = 1,
                            ProjectRequirements = new List<ProjectRequirement>
                            {
                                new ProjectRequirement
                                {
                                    ProjectId = 100
                                }
                            }
                        }
                    }
                }   
            };

            _categoryRepository.Stub(m => m.GetAll()).Return(categories.AsQueryable());
        }

        [TestMethod]
        public void WhenEditingCategoryExpectChangesToBeSave()
        {
            //Arrange
            var categoryService = new CategoryService(_categoryRepository, _unitOfWork);

            //Act
            categoryService.EditCategory(category);

            //Assert
            _unitOfWork.AssertWasCalled(m => m.SaveChanges());
        }

        //[TestMethod]
        //public void WhenEditingCategoryExpectSuccess()
        //{
        //    //Arrange
        //    var categoryService = new CategoryService(_categoryRepository, _unitOfWork);

        //    //Act
        //  var result = categoryService.EditCategory(category);

        //    //Assert
        //   Assert.IsTrue(result);
      
        //}

        [TestMethod]
        public void WhenCreatingCategoryExpectSuccess()
        {
            //Arrange 
            var categoryService = new CategoryService(_categoryRepository, _unitOfWork);

            //Act
            var result = categoryService.CreateCategory(category);

            //Assert
            _unitOfWork.AssertWasCalled(x => x.SaveChanges());
            var createdCategory = _categoryRepository.Create(result);
            Assert.IsTrue(string.Equals(category.Name, createdCategory.Name));
        }

        [TestMethod]
        public void WhenDeletingCategoryExpectSuccess()
        {
            //Arrange
            var categoryService = new CategoryService(_categoryRepository, _unitOfWork);

            //Act
            var result = categoryService.DeleteById(category.Id);

            //Assert
            _categoryRepository.Delete(category.Id);
           
        }

        [TestMethod]
        public void WhenGettingCategoryExpect()
        {
            //Arrange
            var categoryService = new CategoryService(_categoryRepository, _unitOfWork);

            //Act
            var assignedCategories = categoryService.GetCategoriesByAssignedCategory(100);

            //Assert
            var assigned = assignedCategories.First(x => x.Id == 2);
            var test = assigned.AssignedRequirements.First().Assigned;
            Assert.IsTrue(test);
        }
    }
}

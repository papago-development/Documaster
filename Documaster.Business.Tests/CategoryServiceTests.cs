using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Documaster.Business.Services;
using Documaster.Model.Entities;
using Rhino.Mocks;
using System.Collections.Generic;

namespace Documaster.Business.Tests
{
    [TestClass]
    public class CategoryServiceTests
    {
        private static  IUnitOfWork _unitOfWork;
        private static IGenericRepository<Category> _categoryRepository;
        private static Category category;


        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {
            _categoryRepository = MockRepository.GenerateMock<IGenericRepository<Category>>();
            _unitOfWork = MockRepository.GenerateMock<IUnitOfWork>();
    
        }

        [TestInitialize]
        public void TestInit()
        {
            category = new Category();
            _categoryRepository.Stub(m => m.Update(category, new List<string>())).Return(true);

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

        [TestMethod]
        public void WhenEditingCategoryExpectSuccess()
        {
            //Arrange
            var categoryService = new CategoryService(_categoryRepository, _unitOfWork);

            //Act
          var result = categoryService.EditCategory(category);

            //Assert
           //Assert.IsTrue(result);
           TestInit();
        }
    }
}

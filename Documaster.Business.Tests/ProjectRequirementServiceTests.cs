using System;
using System.Collections.Generic;
using Documaster.Business.Services;
using Documaster.Model.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Documaster.Business.Tests
{
    [TestClass]
    public class ProjectRequirementServiceTests
    {
        private static IUnitOfWork _unitOfWork;
        private static IGenericRepository<ProjectRequirement> _projectRequirementRepository;
        private static ProjectRequirement projectRequirement;

        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {
            _projectRequirementRepository = MockRepository.GenerateMock<IGenericRepository<ProjectRequirement>>();
            _unitOfWork = MockRepository.GenerateMock<IUnitOfWork>();
        }

        [TestInitialize]
        public void TestInit()
        {
            _projectRequirementRepository.Stub(m => m.Update(projectRequirement, new List<string>())).Return(true);
            _projectRequirementRepository.Stub(m => m.Create(projectRequirement)).Return(projectRequirement);
         }

        //[TestMethod]
        //public void WhenCreatingProjectRequirementExpectSuccess()
        //{
         //   //Arrange
         //   var projectRequirementService = new ProjectRequirementService(_projectRequirementRepository, _unitOfWork);
            
         //   //Act
         //   var result = projectRequirementService.CreateProjectRequirement(projectRequirement);

         //   //Assert
         //   _unitOfWork.AssertWasCalled(x => x.SaveChanges());
         //   var createdProjectRequirement = _projectRequirementRepository.Create(result);
         //}
    }
}

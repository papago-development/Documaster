using System;
using System.Collections.Generic;
using Documaster.Business.Services;
using Documaster.Model.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Documaster.Business.Tests
{
    [TestClass]
    public class RequirementServiceTests
    {
        private static IUnitOfWork _unitOfWork;
        private static IGenericRepository<Requirement> _requirementRepository;
        private static Requirement requirement = new Requirement
        {
            Id = 1, 
            Name = requirementName
        };
        private const string requirementName = "aviz";

        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {
            _requirementRepository = MockRepository.GenerateMock<IGenericRepository<Requirement>>();
            _unitOfWork = MockRepository.GenerateMock<IUnitOfWork>();
        }

        [TestInitialize]
        public void TestInit()
        {
            _requirementRepository.Stub(m => m.Create(requirement)).Return(requirement);
            _requirementRepository.Stub(m => m.Update(requirement, new List<string>())).Return(true);
        }

        [TestMethod]
        public void WhenCreatingRequirementExpectSuccess()
        {
            //Arrange
            var requirementService = new RequirementService(_requirementRepository, _unitOfWork);

            //Act
            var result = requirementService.CreateRequirement(requirement);

            //Assert
            _unitOfWork.AssertWasCalled(x => x.SaveChanges());
            var createdRequirement = _requirementRepository.Create(result);
            Assert.IsTrue(string.Equals(requirement.Name, createdRequirement.Name));
        }

        [TestMethod]
        public void WhenUpdatingRequirementExpectSuccess()
        {
            //Arrange
            var requirementService = new RequirementService(_requirementRepository, _unitOfWork);

            //Act
            var result = requirementService.UpdateRequirement(requirement);

            //Assert
            _unitOfWork.AssertWasCalled(x => x.SaveChanges());
        }

        [TestMethod]
        public void WhenDeletingRequirementExpectSuccess()
        {
            //Arrange
            var requirementService = new RequirementService(_requirementRepository, _unitOfWork);

            //Act
            var result = requirementService.DeleteRequirement(requirement);

            //Assert
            _unitOfWork.AssertWasCalled(x => x.SaveChanges());
        }
    }
}

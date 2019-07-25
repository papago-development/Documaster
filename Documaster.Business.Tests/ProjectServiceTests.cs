using Documaster.Business.Services;
using Documaster.Model.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Documaster.Business.Tests
{
    [TestClass]
    public class ProjectServiceTests
    {
        private static IUnitOfWork _unitOfWork;
        private static IGenericRepository<Project> _projectRepository;
        private  static Project project;
 
        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {
            _projectRepository = MockRepository.GenerateMock<IGenericRepository<Project>>();
            _unitOfWork = MockRepository.GenerateMock<IUnitOfWork>();
        }


        [TestInitialize]
        public void TestInit()
        {
            project = new Project
            {
                Id = 1
            };

            _projectRepository.Stub(m => m.Delete(project.Id)).Return(true);
        }

        //Test for UpdateProject method
        [TestMethod]
        public void WhenUpdatingProjectExpectChangesToBeSave()
        {
            //Arrange
            var projectService = new ProjectService(_projectRepository, _unitOfWork);

            //Act
            projectService.Update(project);

            //Assert
            _unitOfWork.AssertWasCalled(p => p.SaveChanges());
        }

        //Test for CreateProject method
        [TestMethod]
        public void WhenCreatingProjectExpectSuccess()
        {
            //Arrange
            var projectService = new ProjectService(_projectRepository, _unitOfWork);

            //Act
            var result = projectService.Create(project);

            //Assert
            _unitOfWork.AssertWasCalled(p => p.SaveChanges());
           // _projectRepository.Create(result);
        }

        //Test for DeleteProject method
        [TestMethod]
        public void WhenDeletingProjectExpectSuccess()
        {
            //Arrange
            var projectService = new ProjectService(_projectRepository, _unitOfWork);

            //Act
            var result = projectService.Delete(project);

            //Assert
            _unitOfWork.AssertWasCalled(p => p.SaveChanges());
            Assert.IsTrue(result);
        }
    }
}

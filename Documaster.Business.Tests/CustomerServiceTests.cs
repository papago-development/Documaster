using System;
using Documaster.Business.Services;
using Documaster.Model.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Documaster.Business.Tests
{
    [TestClass]
    public class CustomerServiceTests
    {
        private static IUnitOfWork _unitOfWork;
        private static IGenericRepository<Customer> _customerRepository;
        private static readonly Customer customer;
        private static readonly Project project;

        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {
            _customerRepository = MockRepository.GenerateMock<IGenericRepository<Customer>>();
            _unitOfWork = MockRepository.GenerateMock<IUnitOfWork>();
        }


        //[TestMethod]
        //public void WhenCreatingCustomerExpectSuccess()
        //{
        //    //Arrange
        //    var customerService = new CustomerService(_customerRepository, _unitOfWork);

        //    //Act
        //    var result = customerService.CreateCustomer(customer);

        //    //Assert
        //    _customerRepository.Create(result);
        //}
    }
}

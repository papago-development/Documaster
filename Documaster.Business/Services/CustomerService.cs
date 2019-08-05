using Documaster.Model.Entities;
using System.Collections.Generic;

namespace Documaster.Business.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IGenericRepository<Customer> _customerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _customerRepository = _unitOfWork.Repository<Customer>();
        }

        public Customer CreateCustomer(Customer customer)
        {
            return _customerRepository.Create(customer);
        }

        public bool Delete(Customer customer)
        {
            var deleteCustomer = _customerRepository.Delete(customer.Id);
            _unitOfWork.SaveChanges();
            return deleteCustomer; 
        }

        public IEnumerable<Customer> GetCustomersByProject(Project project)
        {
            return _customerRepository.Get(x => x.Id == project.Id);
        }

        public bool UpdateCustomer(Customer customer)
        {
            return _customerRepository.Update(customer, new List<string> { "Name", "Telephone", "Email", "Address", "AdditionalInfo1", "AdditionalInfo2" });
        }
    }
}

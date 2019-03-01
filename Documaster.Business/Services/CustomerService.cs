using Documaster.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documaster.Business.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IGenericRepository<Customer> _customerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IGenericRepository<Customer> customerRepository,
                               IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
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

    }
}

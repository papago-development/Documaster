using Documaster.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documaster.Business.Services
{
    public interface ICustomerService
    {
        IEnumerable<Customer> GetCustomersByProject(Project project);
        Customer CreateCustomer(Customer customer);
        bool Delete(Customer customer);

    }
}

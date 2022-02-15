using NotesControlAPI.V1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesControlAPI.V1.Repositories.Contracts
{
    public interface ICustomerRepository
    {
        int InsertCustomer(CustomerModel customer);
        CustomerModel GetCustomerById(int id , int userId);
        List<CustomerModel> GetCustomerByNameCnpj(string name, string cnpj, int userId);
        void UpdateCustomerArchives();
        int UpdateCustomer(CustomerModel customer);
    }
}

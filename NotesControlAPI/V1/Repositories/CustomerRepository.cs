using Microsoft.EntityFrameworkCore;
using NotesControlAPI.Database;
using NotesControlAPI.V1.Helpers;
using NotesControlAPI.V1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesControlAPI.V1.Repositories.Contracts
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly NotesControlContext _context;

        public CustomerRepository(NotesControlContext context)
        {
            _context = context;
        }

        public int InsertCustomer(CustomerModel customer)
        {
            var cnpj = customer.Cnpj;
            var cnpjReplace = cnpj.Replace(".", "").Replace("/", "").Replace("-", "").Trim();
            var isValidCnpj = CheckCNPJ.IsCnpj(cnpj);
            customer.Cnpj = cnpjReplace;

            if (!isValidCnpj)
            {
                return -1;
            }
            if (_context.Customers.Where(x => x.Cnpj == cnpjReplace).Any())
            {
                return -2;
            }


            _context.Customers.Add(customer);
            _context.SaveChanges();

            return customer.Customer_Id;
        }

        public int UpdateCustomer(CustomerModel customer)
        {
            var cnpj = customer.Cnpj;
            var cnpjReplace = cnpj.Replace(".", "").Replace("/", "").Replace("-", "").Trim();
            var isValidCnpj = CheckCNPJ.IsCnpj(cnpj);
            customer.Cnpj = cnpjReplace;

            if (!isValidCnpj)
            {
                return -1;
            }
            if (_context.Customers.Where(x => x.Cnpj == cnpjReplace && x.Customer_Id != customer.Customer_Id).Any())
            {
                return -2;
            }

            _context.Customers.Update(customer);
            _context.SaveChanges();

            return 1;
        }

        public void UpdateCustomerArchives()
        {
        }

        public CustomerModel GetCustomerById(int id, int userId)
        {
            var customer = _context.Customers.AsNoTracking().Where(x => x.UserId == userId && x.Customer_Id == id).FirstOrDefault();
            return customer;
        }

        public List<CustomerModel> GetCustomerByNameCnpj(string name, string cnpj, int userId)
        {
            var customers = _context.Customers.Where(x => x.UserId == userId && (x.Commercial_Name.Contains(name) || x.Cnpj == cnpj)).ToList();
            return customers;
        }

    }
}

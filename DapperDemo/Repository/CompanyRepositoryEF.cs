using DapperDemo.Data;
using DapperDemo.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DapperDemo.Repository
{
    public class CompanyRepositoryEF : ICompanyRepository

    {
        //private  readonly IDbConnection db;
        private readonly ApplicationDbContext _db;

        //public CompanyRepository(IConfiguration configuration )
        //{
        //    this.db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));

        //}

        public CompanyRepositoryEF(ApplicationDbContext db)
        {
            _db = db;
        }
        public Company Add(Company company)
        {
            //var sql = "INSERT INTO Companies (Name, Address, City, State, PostalCode) VALUES(@Name, @Address, @City, @State, @PostalCode);"
            //                + "SELECT CAST(SCOPE_IDENTITY() as int); ";


            //var id = db.Query<int>(sql, company).Single();
            _db.Companies.Add(company);
            _db.SaveChanges();
            return company;
        }

        public Company find(int id)
        {
            return _db.Companies.FirstOrDefault(u => u.CompanyId == id);

        }

        public List<Company> GetAll()
        {
            return _db.Companies.ToList();
        }

        public void Remove(int id)
        {
            Company company = _db.Companies.FirstOrDefault(u => u.CompanyId == id);
            _db.Companies.Remove(company);
                _db.SaveChanges();
            return;

        }

        public Company Update(Company company)
        {
            _db.Companies.Update(company);
            _db.SaveChanges();
            return company;
        }
    }
}

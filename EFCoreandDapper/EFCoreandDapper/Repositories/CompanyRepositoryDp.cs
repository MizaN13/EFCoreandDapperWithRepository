using Dapper;
using EFCoreandDapper.Data;
using EFCoreandDapper.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreandDapper.Repositories
{
    public class CompanyRepositoryDp : ICompanyRepository
    {
        private IDbConnection db;

        public CompanyRepositoryDp(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
        public Company Add(Company company)
        {
            var sql = "INSERT INTO Companies (Name, Address, City, State, PostCode) VALUES(@Name, @Address, @City, @State, @PostCode);"
                        + "SELECT CAST(SCOPE_IDENTITY() as int); ";
            var id = db.Query<int>(sql, new 
            { 
                @Name = company.Name,
                @Address = company.Address,
                @City = company.City,
                @State = company.State,
                @PostCode = company.PostCode
            }).Single();
            company.CompanyId = id;
            return company;
        }

        public Company Find(int id)
        {
            var sql = "SELECT * FROM Companies WHERE CompanyId = @CompanyId";
            return db.Query<Company>(sql, new { @CompanyId = id }).Single();
        }

        public List<Company> GetAll()
        {
            var sql = "SELECT * FROM Companies";
            return db.Query<Company>(sql).ToList();
        }

        public void Remove(int id)
        {
            
        }

        public Company Update(Company company)
        {
            return null;
        }
    }
}

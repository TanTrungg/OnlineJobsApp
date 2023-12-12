using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class EmployerDAO
    {
        private static EmployerDAO instance = null;
        private static readonly object instanceLock = new object();

        public static EmployerDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new EmployerDAO();
                    }
                    return instance;
                }
            }
        }

        public async Task<Company> GetCompanyByEmail(string email)
        {
            Company c = new Company();
            try
            { 
                using var context = new OnlineJobSeekingManagementContext();
                c = await context.Companies
                    .Where(c => c.Email == email)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return c;
        }

        public Company GetCompanyByEmailAndPassword(string email, string password)
        {
            Company c = new Company();
            try
            {
                using var context = new OnlineJobSeekingManagementContext();
                c =  context.Companies
                    .Where(c => c.Email == email && c.Password == password && c.IsDeleted == false)
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return c;
        }

        public async Task<Company> GetCompanyById(int id)
        {
            Company c = new Company();
            try
            {
                using var context = new OnlineJobSeekingManagementContext();
                c = await context.Companies
                    .Where(c => c.Id == id && c.IsDeleted == false)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return c;
        }

        public async Task AddCompany(Company company)
        {
            try
            {
                using var context = new OnlineJobSeekingManagementContext();
                company.IsDeleted = false;
                company.CreatedAt = DateTime.Now;
                context.Companies.Add(company);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateCompany(Company company)
        {
            try
            {
                using var context = new OnlineJobSeekingManagementContext();
                company.UpdatedAt = DateTime.Now;
                context.Companies.Update(company);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

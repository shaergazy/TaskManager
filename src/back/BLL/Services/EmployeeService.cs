using BLL.Services.External;
using Common.Enums;
using Common.Exceptions;
using Common.Extensions;
using Common.Helpers;
using Common.Resources;
using DAL.EF;
using DAL.Entities;
using DAL.Entities.Users;
using DTO;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class EmployeeService
    {
        private AppDbContext _appDbContext;
        private UserManager<User> _userManager;
        public EmployeeService(AppDbContext appDbContext, UserManager<User> userManager)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
        }

        public async Task<string> AddAsync(EmployeeDto.Add employeeDto)
                {
            if (_appDbContext.Employees.Any(x => x.Email == employeeDto.Email))
                throw new InnerException("Employee with the same email already exist");

            var employee = new Employee
            {
                Id = Guid.NewGuid().ToString(),
                BirthDate = employeeDto.BirthDate,
                UserName = employeeDto.Email,
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                Email = employeeDto.Email,
                PhoneNumber = employeeDto.PhoneNumber,
            };
            var role = await _appDbContext.Set<Role>()
                .FirstOrDefaultAsync(x => x.Name == RoleType.Employee.ToString());

            employee.Roles = new[]{new UserRole
            {
                RoleId = role.Id,
            }};

            var password = RandomString.AlphaNumeric(10);
            await _userManager.CreateAsync(employee);
            await _userManager.AddPasswordAsync(employee, password);

            await _appDbContext.SaveChangesAsync();

            return employee.Id;
        }

        public async Task<EmployeeDto.Get> GetByIdAsync(string employeeId)
        {
            var employee = await _appDbContext.Employees.FirstOrDefaultAsync(x => x.Id == employeeId);
            if (employee == null)
                throw new InnerException($"Employee with identifier {employeeId} doesn't exist");

            return employee.Adapt<EmployeeDto.Get>();
        }

        public async Task UpdateAsync(EmployeeDto.Edit employeeDto)
        {
            var employee = await _appDbContext.Employees.FindAsync(employeeDto.Id);
            if (employee == null)
                throw new InnerException("Employee does not exist.");
            if (_appDbContext.Employees.Any(x => x.Email == employeeDto.Email && x.Id != employeeDto.Id))
                throw new InnerException("Employee with the same email already exist on database");

            employee.FirstName = employeeDto.FirstName;
            employee.LastName = employeeDto.LastName;
            employee.Email = employeeDto.Email;
            employee.PhoneNumber = employeeDto.PhoneNumber;

            _appDbContext.Employees.Update(employee);

            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string employeeId)
        {
            var employee = await _appDbContext.Employees
                .Include(x => x.Roles)
                .Include(x => x.Projects)
                .FirstOrDefaultAsync(x => x.Id == employeeId);
            if (employee == null)
                throw new InnerException("Employee does not exist.");

            _appDbContext.Employees.Remove(employee);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<List<EmployeeDto.List>> ListAsync(int? projectId)
        {
            return _appDbContext.Employees
                                   .Where(x => !projectId.HasValue || x.Projects.Any(x => x.ProjectId == projectId))
                                   .Adapt<List<EmployeeDto.List>>();
        }
    }
}
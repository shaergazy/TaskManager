using Common.Exceptions;
using DAL.EF;
using DAL.Entities;
using DTO;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ProjectService
    {
        AppDbContext _appDbContext;

        public ProjectService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<int> AddAsync(ProjectDto.Add projectDto)
        {
            if (await _appDbContext.Projects.AnyAsync(x => x.Name == projectDto.Name))
                throw new InnerException("Project with the same name already exist in database");
            if (projectDto.StartDateTime > projectDto.EndDateTime)
                throw new InnerException("Project can't end before start");
            var director = await _appDbContext.Employees.FirstOrDefaultAsync(x => x.Id == projectDto.DirectorId);
            //if (director == null)
            //    throw new InnerException($"Employee with id {projectDto.DirectorId} does not exist");

            var project = projectDto.Adapt<Project>();
            project.Director = director;

            _appDbContext.Projects.Add(project);
            await _appDbContext.SaveChangesAsync();

            return project.Id;
        }

        public async Task<ProjectDto.Get> GetByIdAsync(int projectId)
        {
            var project = await _appDbContext.Projects.FirstOrDefaultAsync(x => x.Id == projectId);
            if (project == null)
                throw new InnerException($"Project with identifier {projectId} doesn't exist");

            TypeAdapterConfig<Project, ProjectDto.Get>
                                        .NewConfig()
                                        .Map(dest => dest.DirectorName, src => src.Director.FirstName);

            return project.Adapt<ProjectDto.Get>();
        }

        public async Task UpdateAsync(ProjectDto.Edit projectDto)
        {
            var project = await _appDbContext.Projects.FirstOrDefaultAsync(x => x.Id == projectDto.Id);
            if (project == null)
                throw new InvalidOperationException($"Project with identifier {projectDto.Id} doesn't exist");
            if (await _appDbContext.Projects.AnyAsync(x => x.Name == projectDto.Name && x.Id != projectDto.Id))
                throw new InnerException("Project with the same name already exist in database");
            if (projectDto.StartDateTime > projectDto.EndDateTime)
                throw new InnerException("Project can't end before start");

            project.Name = projectDto.Name;
            project.ContractorCompanyName = projectDto.ContractorCompanyName;
            project.CustomerCompanyName = projectDto.CustomerCompanyName;
            project.DirecrorId = projectDto.DirectorId;
            project.StartDateTime = projectDto.StartDateTime;
            project.EndDateTime = projectDto.EndDateTime;
            project.Priority = projectDto.Priority;

            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int projectId)
        {
            var project = await _appDbContext.Projects
                                .Include(x => x.Employees)
                                .FirstOrDefaultAsync(x => x.Id == projectId);
            if (project == null)
                throw new InvalidOperationException($"Project with identifier {projectId} doesn't exist");
            var tasks =_appDbContext.Jobs.Where(x => x.ProjectId == projectId);

            _appDbContext.Jobs.RemoveRange(tasks);
            _appDbContext.Projects.Remove(project);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<List<ProjectDto.List>> ListAsync(FiltrationDto.ProjectFilter? filter)
        {
            TypeAdapterConfig<Project, ProjectDto.List>
                                        .NewConfig()
                                        .Map(dest => dest.DirectorName, src => src.Director.FirstName);

            return _appDbContext.Projects
                .Include(x => x.Director)
                .Where(x => !filter.FirstDateTime.HasValue || x.StartDateTime >= filter.FirstDateTime)
                .Where(x => !filter.SecondDateTime.HasValue || x.StartDateTime <= filter.SecondDateTime)
                .Where(x => !filter.MaxValueOfPriority.HasValue || x.Priority <= filter.MaxValueOfPriority)
                .Where(x => !filter.MinValueOfPriority.HasValue || x.Priority >= filter.MinValueOfPriority)
                .ProjectToType<ProjectDto.List>().ToList();
        }

        public async Task AddEmployeeToProjectAsync(ProjectEmployeeDto dto)
        {
            var project = await _appDbContext.Projects.FirstOrDefaultAsync(x => x.Id == dto.ProjectId);
            if (project == null)
                throw new InnerException($"Project with id {dto.ProjectId} does not exist");
            if (project.Employees != null && project.Employees.Any(x => x.EmployeeId == dto.EmployeeId))
                throw new InnerException($"Employee with id {dto.EmployeeId} already in project");
            var employee = _appDbContext.Employees.FirstOrDefaultAsync(x => x.Id == dto.EmployeeId);
            if (project == null)
                throw new InnerException($"Employee with id {dto.EmployeeId} does not exist");

            var projectEmployee = new ProjectEmployee()
            {
                EmployeeId = dto.EmployeeId,
                ProjectId = dto.ProjectId,
            };

            _appDbContext.Set<ProjectEmployee>().Add(projectEmployee);

            await _appDbContext.SaveChangesAsync();
        }

        public async Task RemoveEmployeeFromProjectAsync(ProjectEmployeeDto dto)
        {
            var projectEmployee = await _appDbContext.Set<ProjectEmployee>().FirstOrDefaultAsync(x => x.ProjectId == dto.ProjectId
                                                                                                 && x.EmployeeId == dto.EmployeeId);
            if (projectEmployee == null)
                throw new InnerException("The employee does not exist in project");

            _appDbContext.Set<ProjectEmployee>().Remove(projectEmployee);
            await _appDbContext.SaveChangesAsync();
        }
    }
}


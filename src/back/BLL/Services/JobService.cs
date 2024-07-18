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
    public class JobService
    {
        AppDbContext _appDbContext;

        public JobService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<int> AddAsync(JobDto.Add jobDto)
        {
            var project = await _appDbContext.Projects.FirstOrDefaultAsync(x => x.Id == jobDto.ProjectId);
            if (project == null)
                throw new InnerException("Project does not exist");
            var author = await _appDbContext.Employees.FirstOrDefaultAsync(x => x.Id == jobDto.AuthorId);
            if (author == null)
                throw new InnerException($"Employee with id {jobDto.AuthorId} does not exist");
            var executor = await _appDbContext.Employees.FirstOrDefaultAsync(x => x.Id == jobDto.ExecutorId);
            if (executor == null)
                throw new InnerException($"Employee with id {jobDto.ExecutorId} does not exist");

            var job = jobDto.Adapt<Job>();
            job.Project = project;
            job.Author = author;
            job.Executor = executor;

            _appDbContext.Jobs.Add(job);
            await _appDbContext.SaveChangesAsync();

            return job.Id;
        }

        public async Task UpdateAsync(JobDto.Edit jobDto)
        {
            var job = await _appDbContext.Jobs.FirstOrDefaultAsync(x => x.Id == jobDto.Id);
            if (job == null)
                throw new InnerException($"Job with id {jobDto.Id}");
            var author = await _appDbContext.Employees.FirstOrDefaultAsync(x => x.Id == jobDto.AuthorId);
            if (author == null)
                throw new InnerException($"Employee with id {jobDto.AuthorId} does not exist");
            var executor = await _appDbContext.Employees.FirstOrDefaultAsync(x => x.Id == jobDto.ExecutorId);
            if (executor == null)
                throw new InnerException($"Employee with id {jobDto.ExecutorId} does not exist");

            job.Status = jobDto.Status;
            job.Name = jobDto.Name;
            job.Comment = jobDto.Comment;
            job.Priority = jobDto.Priority;
            job.Author = author;
            job.Executor = executor;

            _appDbContext.Jobs.Update(job);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int jobId)
        {
            var job = await _appDbContext.Jobs.FindAsync(jobId);
            if (job == null)
                throw new InvalidOperationException($"Job with identifier {jobId} doesn't exist");

            _appDbContext.Jobs.Remove(job);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<List<JobDto.List>> ListAsync(int? projectId)
        {
            TypeAdapterConfig<Job, JobDto.List>
                                        .NewConfig()
                                        .Map(dest => dest.Author, src => src.Author.Email)
                                        .Map(dest => dest.Executor, src => src.Executor.Email);

            return _appDbContext.Jobs
                .Include(x => x.Author)
                .Include(x => x.Executor)
                .Where(x => !projectId.HasValue || x.ProjectId == projectId)
                .ProjectToType<JobDto.List>().ToList();
        }

    }
}

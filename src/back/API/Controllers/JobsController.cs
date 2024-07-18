using DTO;
using DTO.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using BLL.Services;

namespace API.Controllers
{
    ///<summary>
    /// Jobs controller
    ///</summary>
    /// <response code="400">Error in model data</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="500">Uncatched, unknown error</response>
    /// <response code="403">Access denied</response>
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(typeof(BadRequestDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(BadRequestDto), StatusCodes.Status500InternalServerError)]
    public class JobsController : ControllerBase
    {
        private JobService _jobService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="jobService">Main service</param>
        public JobsController(JobService jobService)
        {
            _jobService = jobService;
        }

        /// <summary> 
        /// Add Job
        /// </summary>
        /// <param name="dto">model of Job</param>
        /// <response code="200">Success</response>
        /// <returns>Job Id</returns>
        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<int> Add([Required] JobDto.Add dto)
        {
            return await _jobService.AddAsync(dto);
        }

        /// <summary>
        /// Get Jobs
        /// </summary>
        /// <response code="200">Success</response>
        /// <returns>Job models</returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(List<JobDto.Get>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestDto), StatusCodes.Status404NotFound)]
        public async Task<List<JobDto.List>> List(int? projectId)
        {
            return await _jobService.ListAsync(projectId);
        }

        /// <summary>
        /// Edit Job
        /// </summary>
        /// <param name="dto">Edit Jobs</param>
        /// <response code="204">No content</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task Edit(JobDto.Edit dto)
        {
            await _jobService.UpdateAsync(dto);
        }

        /// <summary>
        /// Delete Job by id
        /// </summary>
        /// <param name="id">id of Job</param>
        /// <response code="204">No content</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task Delete(int id)
        {
            await _jobService.DeleteByIdAsync(id);
        }
    }
}

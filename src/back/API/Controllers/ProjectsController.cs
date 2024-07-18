using API.Infrastructure;
using BLL.Services;
using Common.Enums;
using DTO;
using DTO.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace API.Controllers
{
    ///<summary>
    /// Projects controller
    ///</summary>
    /// <response code="400">Error in model data</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="500">Uncatched, unknown error</response>
    /// <response code="403">Access denied</response>
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(typeof(BadRequestDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(BadRequestDto), StatusCodes.Status500InternalServerError)]
    public class ProjectsController : ControllerBase
    {
        private ProjectService _projectService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="projectService">Main service</param>
        public ProjectsController(ProjectService projectService)
        {
            _projectService = projectService;
        }

        /// <summary> 
        /// Add Project
        /// </summary>
        /// <param name="dto">model of Project</param>
        /// <response code="200">Success</response>
        /// <returns>Project Id</returns>
        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<int> Add([Required] ProjectDto.Add dto)
        {
            return await _projectService.AddAsync(dto);
        }

        /// <summary>
        /// Get Project
        /// <param name="id">model of Project</param>
        /// </summary>
        /// <response code="200">Success</response>
        /// <returns>Project models</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ProjectDto.Get), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestDto), StatusCodes.Status404NotFound)]
        public async Task<ProjectDto.Get> GetById(int id)
        {
            return await _projectService.GetByIdAsync(id);
        }

        /// <summary>
        /// Get Projects
        /// </summary>
        /// <response code="200">Success</response>
        /// <returns>Project models</returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(List<ProjectDto.Get>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestDto), StatusCodes.Status404NotFound)]
        public async Task<List<ProjectDto.List>> List([FromQuery] FiltrationDto.ProjectFilter? filter)
        {
            return await _projectService.ListAsync(filter);
        }

        /// <summary>
        /// Edit Project
        /// </summary>
        /// <param name="dto">Edit Projects</param>
        /// <response code="204">No content</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task Edit(ProjectDto.Edit dto)
        {
            await _projectService.UpdateAsync(dto);
        }

        /// <summary>
        /// Delete Project by id
        /// </summary>
        /// <param name="id">id of Project</param>
        /// <response code="204">No content</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task Delete(int id)
        {
            await _projectService.DeleteByIdAsync(id);
        }

        /// <summary> 
        /// Add Employee to Project
        /// </summary>
        /// <param name="dto">id of project</param>
        /// <response code="204">Success</response>
        /// <returns>Project Id</returns>
        [HttpPost("Add-Employee-To-Project")]
        [ProducesResponseType(typeof(int), StatusCodes.Status204NoContent)]
        public async Task AddEmployeeToProject(ProjectEmployeeDto dto)
        {
            await _projectService.AddEmployeeToProjectAsync(dto);
        }

        /// <summary> 
        /// Add Employee to Project
        /// </summary>
        /// <param name="dto">id of project</param>
        /// <response code="204">Success</response>
        /// <returns>Project Id</returns>
        [HttpDelete("Delete-Employee-From-Project")]
        [ProducesResponseType(typeof(int), StatusCodes.Status204NoContent)]
        public async Task RemoveEmployeeFromProject(ProjectEmployeeDto dto)
        {
            await _projectService.RemoveEmployeeFromProjectAsync(dto);
        }
    }
}

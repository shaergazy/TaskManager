using API.Infrastructure;
using BLL.Services;
using Common.Enums;
using DTO.Base;
using DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace API.Controllers
{
    //<summary>
    /// Employees controller
    ///</summary>
    /// <response code="400">Error in model data</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="500">Uncatched, unknown error</response>
    /// <response code="403">Access denied</response>
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(typeof(BadRequestDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(BadRequestDto), StatusCodes.Status500InternalServerError)]
    public class EmployeesController : ControllerBase
    {
        private EmployeeService _employeeService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="employeeService">Main service</param>
        public EmployeesController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        /// <summary> 
        /// Add Employee
        /// </summary>
        /// <param name="dto">model of Employee</param>
        /// <response code="200">Success</response>
        /// <returns>Employee Id</returns>
        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<string> Add([Required] EmployeeDto.Add dto)
            {
            return await _employeeService.AddAsync(dto);
        }

        /// <summary>
        /// Get Employee
        /// <param name="id">model of Employee</param>
        /// </summary>
        /// <response code="200">Success</response>
        /// <returns>Employee models</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(EmployeeDto.Get), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestDto), StatusCodes.Status404NotFound)]
        public async Task<EmployeeDto.Get> GetById(string id)
        {
            return await _employeeService.GetByIdAsync(id);
        }

        /// <summary>
        /// Get Employees
        /// </summary>
        /// <response code="200">Success</response>
        /// <returns>Employee models</returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(List<EmployeeDto.Get>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestDto), StatusCodes.Status404NotFound)]
        public async Task<List<EmployeeDto.List>> List(int? projectId)
        {
            return await _employeeService.ListAsync(projectId);
        }

        /// <summary>
        /// Edit Employee
        /// </summary>
        /// <param name="dto">Edit Employees</param>
        /// <response code="204">No content</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task Edit(EmployeeDto.Edit dto)
        {
            await _employeeService.UpdateAsync(dto);
        }

        /// <summary>
        /// Delete Employee by id
        /// </summary>
        /// <param name="id">id of Employee</param>
        /// <response code="204">No content</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task Delete(string id)
        {
            await _employeeService.DeleteByIdAsync(id);
        }
    }
}

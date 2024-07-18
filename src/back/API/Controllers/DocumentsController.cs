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
    /// Documents controller
    ///</summary>
    /// <response code="400">Error in model data</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="500">Uncatched, unknown error</response>
    /// <response code="403">Access denied</response>
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(typeof(BadRequestDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(BadRequestDto), StatusCodes.Status500InternalServerError)]
    public class DocumentsController : ControllerBase
    {
        private DocumentService _documentService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="documentService">Main service</param>
        public DocumentsController(DocumentService documentService)
        {
            _documentService = documentService;
        }

        /// <summary> 
        /// Add Document
        /// </summary>
        /// <param name="dto">model of Document</param>
        /// <response code="200">Success</response>
        /// <returns>Document Id</returns>
        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<int> Add([FromForm][Required] DocumentDto.Add dto)
        {
            return await _documentService.AddAsync(dto, await dto.File.ToStream());
        }

        /// <summary>
        /// Get all Documents
        /// </summary>
        /// <response code="200">Success</response>
        /// <returns>Document models</returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(List<DocumentDto.Get>), StatusCodes.Status200OK)]
        public async Task<List<DocumentDto.Get>> List()
        {
            return await _documentService.ListAsync();
        }
        /// <summary>
        /// Delete Document by id
        /// </summary>
        /// <param name="id">id of Document</param>
        /// <response code="204">No content</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task Delete(int id)
        {
            await _documentService.DeleteByIdAsync(id);
        }
    }
}

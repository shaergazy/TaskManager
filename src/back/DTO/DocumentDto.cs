using Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class DocumentDto
    {

        /// <summary>
        /// base object
        /// </summary>
        public abstract class Base
        {
            /// <summary>
            /// Name of document
            /// </summary>
            [Required]
            [MaxLength(64)]
            public string Name { get; set; }
        }

        /// <summary>
        /// Abstract class for id
        /// </summary>
        public abstract class IdHasBase : Base, IIdHas<int>
        {
            /// <summary>
            /// Id
            /// </summary>
            [Required]
            public int Id { get; set; }
        }

        /// <summary>
        /// Get object
        /// </summary>
        public class Get : IdHasBase
        {
            [Required]
            public string Path { get; set; }
        }

        /// <summary>
        /// List objects
        /// </summary>
        public class List : Get { }

        /// <summary>
        /// Add object
        /// </summary>
        public class Add
        {
            /// <summary>
            /// Id
            /// </summary>
            [Required]
            public IFormFile File { get; set; }
        }
    }
}

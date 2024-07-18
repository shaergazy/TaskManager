using Common.Interfaces;
using System.ComponentModel.DataAnnotations;
using System;
using Common.Enums;

namespace DTO
{
    public class ProjectDto
    {
        /// <summary>
        /// base object
        /// </summary>
        public class Base
        {
            /// <summary>
            /// Name
            /// </summary>
            [Required]
            [MaxLength(128)]
            public string Name { get; set; }

            /// <summary>
            /// Customer-Company name
            /// </summary>
            [Required, MaxLength(256)]
            public string CustomerCompanyName { get; set; }

            /// <summary>
            /// Contractor-Company name
            /// </summary>
            [Required, MaxLength(256)]
            public string ContractorCompanyName { get; set; }

            /// <summary>
            /// Start date of project
            /// </summary>
            [Required]
            public DateTime StartDateTime { get; set; }

            /// <summary>
            /// End date of project
            /// </summary>
            [Required]
            public DateTime EndDateTime { get; set; }

            /// <summary>
            /// Priority of project
            /// </summary>
            [Required, Range(0, 100)]
            public int Priority { get; set; }
        }

        /// <summary>
        /// Abstract class for id
        /// </summary>
        public class IdHasBase : Base, IIdHas<int>
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
            /// <summary>
            /// Name of director
            /// </summary>
            public string DirectorName { get; set; }
        }

        /// <summary>
        /// List objects
        /// </summary>
        public class List : Get { }

        /// <summary>
        /// Add object
        /// </summary>
        public class Add : Base
        {
            /// <summary>
            /// Id of direcror
            /// </summary>
            public string? DirectorId { get; set; }
        }

        /// <summary>
        /// Edit object
        /// </summary>
        public class Edit : IdHasBase
        {
            /// <summary>
            /// Id of direcror
            /// </summary>
            public string DirectorId { get; set; }
        }
    }
}

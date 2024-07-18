using Common.Interfaces;
using System.ComponentModel.DataAnnotations;
using System;
using Common.Enums;

namespace DTO
{
    public class JobDto
    {
        /// <summary>
        /// base object
        /// </summary>
        public class Base
        {
            /// <summary>
            /// Name
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Comment
            /// </summary>
            public string Comment { get; set; }

            /// <summary>
            /// Job status
            /// </summary>
            public JobStatus Status { get; set; }

            /// <summary>
            /// Priority
            /// </summary>
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
            /// Name of Author
            /// </summary>
            public string Author { get; set; }

            /// <summary>
            /// Name of Executor
            /// </summary>
            public string Executor { get; set; }
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
            /// Project Id
            /// </summary>
            public int ProjectId { get; set; }

            /// <summary>
            /// Id of Author
            /// </summary>
            public string? AuthorId { get; set; }

            /// <summary>
            /// Id of Executor
            /// </summary>
            public string? ExecutorId { get; set; }
        }

        /// <summary>
        /// Edit object
        /// </summary>
        public class Edit : IdHasBase
        {
            /// <summary>
            /// Name of Author
            /// </summary>
            public string? AuthorId { get; set; }

            /// <summary>
            /// Name of Executor
            /// </summary>
            public string? ExecutorId { get; set; }
        }
    }
}

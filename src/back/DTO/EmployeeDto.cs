using Common.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class EmployeeDto
    {
        /// <summary>
        /// base object
        /// </summary>
        public abstract class Base
        {
            /// <summary>
            /// Firstname
            /// </summary>
            [Required]
            [MaxLength(64)]
            public string FirstName { get; set; }

            /// <summary>
            /// Lastname
            /// </summary>
            [Required]
            [MaxLength(64)]
            public string LastName { get; set; }

            /// <summary>
            /// Email
            /// </summary>
            [Required]
            [MaxLength(256)]
            [EmailAddress]
            public string Email { get; set; }
            /// <summary>
            /// Phone number
            /// </summary>
            [Required]
            [Phone]
            public string PhoneNumber { get; set; }

            /// <summary>
            /// Birth date of employee
            /// </summary>
            public DateTime BirthDate { get; set; }
        }

        /// <summary>
        /// Abstract class for id
        /// </summary>
        public abstract class IdHasBase : Base, IIdHas<string>
        {
            /// <summary>
            /// Id
            /// </summary>
            [Required]
            public string Id { get; set; }
        }

        /// <summary>
        /// Get object
        /// </summary>
        public class Get : IdHasBase { }

        /// <summary>
        /// List objects
        /// </summary>
        public class List : Get { }

        /// <summary>
        /// Add object
        /// </summary>
        public class Add : Base { }

        /// <summary>
        /// Edit object
        /// </summary>
        public class Edit : IdHasBase { }
    }
}
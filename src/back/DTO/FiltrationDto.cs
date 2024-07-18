using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class FiltrationDto
    {
        /// <summary>
        /// Filter for Project
        /// </summary>
        public class ProjectFilter
        {
            /// <summary>
            /// Min value of priority
            /// </summary>
            public int? MinValueOfPriority { get; set; }

            /// <summary>
            /// Max value of priority
            /// </summary>
            public int? MaxValueOfPriority { get; set; }

            /// <summary>
            /// First Date Time for filtration
            /// </summary>
            public DateTime? FirstDateTime { get; set; }

            /// <summary>
            /// Second Date Time for filtration
            /// </summary>
            public DateTime? SecondDateTime { get; set; }
        } 
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MasAcademyLab.Service.Models
{
    public class TrainingModel
    {
        /// <summary>
        /// Training name
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        /// <summary>
        /// Training code
        /// </summary>
        [Required]
        public string Code { get; set; }
        /// <summary>
        /// Training date
        /// </summary>
        public DateTime EventDate { get; set; } = DateTime.MinValue;
        /// <summary>
        /// Training Length
        /// </summary>
        [Range(1,100)]
        public int Length { get; set; } = 1;
        /// <summary>
        /// Training location object of the type <see cref="LocationModel"/>
        /// </summary>
        public LocationModel Location { get; set; }
        /// <summary>
        /// A list of Training talks object of the type <see cref="TalkModel"/>
        /// </summary>
        public IEnumerable<TalkModel> Talks { get; set; }
    }
}

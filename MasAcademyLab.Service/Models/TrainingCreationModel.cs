using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MasAcademyLab.Service.Models
{
    public class TrainingCreationModel
    {
        /// <summary>
        /// Name of the Training
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        /// <summary>
        /// Code of the Training
        /// </summary>
        [Required]
        public string Code { get; set; }
        /// <summary>
        /// Training Date
        /// </summary>
        public DateTime EventDate { get; set; } = DateTime.MinValue;
        /// <summary>
        /// Length of the Training
        /// </summary>
        [Range(1, 100)]
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

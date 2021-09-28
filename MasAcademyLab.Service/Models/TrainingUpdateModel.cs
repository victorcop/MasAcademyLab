using System;
using System.ComponentModel.DataAnnotations;

namespace MasAcademyLab.Service.Models
{
    public class TrainingUpdateModel
    {
        /// <summary>
        /// Training name
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        /// <summary>
        /// Training date
        /// </summary>
        public DateTime EventDate { get; set; } = DateTime.MinValue;
        /// <summary>
        /// Training Length
        /// </summary>
        [Range(1, 100)]
        public int Length { get; set; } = 1;
        /// <summary>
        /// Talk location object of the type <see cref="LocationModel"/>
        /// </summary>
        public LocationModel Location { get; set; }
    }
}

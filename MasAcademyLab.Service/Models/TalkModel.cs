using System.ComponentModel.DataAnnotations;

namespace MasAcademyLab.Service.Models
{
    public class TalkModel
    {
        /// <summary>
        /// Talk Id
        /// </summary>
        public int TalkId { get; set; }
        /// <summary>
        /// Talk title
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        /// <summary>
        /// Talk Abstract
        /// </summary>
        public string Abstract { get; set; }
        /// <summary>
        /// Talk Level
        /// </summary>
        [Range(100,800)]
        public int Level { get; set; }
        /// <summary>
        /// Talk speaker object of the type <see cref="SpeakerModel"/>
        /// </summary>
        public SpeakerModel Speaker { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace daebak_subdivision_website.Models
{
    [Table("FEEDBACKS")]
    public class Feedback
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("FEEDBACK_ID")]
        public int Id { get; set; }

        [Required]
        [ForeignKey("User")]
        [Column("USER_ID")]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [Required, StringLength(255)]
        [Column("COMMENTS")]
        public string Comments { get; set; }

        [Required]
        [Range(1, 5)]
        [Column("RATING")]
        public int Rating { get; set; } // Rating: 1 to 5 stars

        [Column("SUBMITTED_AT")]
        public DateTime SubmittedAt { get; set; } = DateTime.Now;
    }
}

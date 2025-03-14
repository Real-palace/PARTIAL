using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace daebak_subdivision_website.Models
{
    [Table("FACILITIES")]
    public class Facility
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("FACILITY_ID")]
        public int Id { get; set; }

        [Required, StringLength(100)]
        [Column("NAME")]
        public string Name { get; set; }

        [Required]
        [Column("DESCRIPTION", TypeName = "TEXT")]
        public string Description { get; set; }

        [Required, StringLength(100)]
        [Column("LOCATION")]
        public string Location { get; set; }

        // Navigation property for Facility Reservations
        public virtual ICollection<FacilityReservation> FacilityReservations { get; set; }
    }
}

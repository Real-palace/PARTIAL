using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace daebak_subdivision_website.Models
{
    [Table("SERVICE_REQUESTS")] // Maps to actual database table
    public class ServiceRequest
    {
        [Key]
        [Column("REQUEST_ID")]
        public int Id { get; set; }  // Maps to REQUEST_ID

        [Column("USER_ID")]
        public int UserId { get; set; }  // Maps to USER_ID

        [Column("HOUSE_NUMBER")]
        public string? HouseNumber { get; set; }

        [Column("REQUEST_TYPE")]
        [Required]
        public string RequestType { get; set; }  // ('Maintenance', 'Security', 'Other')

        [Column("DESCRIPTION")]
        [Required]
        public string Description { get; set; }

        [Column("STATUS")]
        [Required]
        public string Status { get; set; }  // ('Open', 'In Progress', 'Closed')

        [Column("CREATED_AT")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("UPDATED_AT")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        [Column("ASSIGNED_TO")]
        public int? AssignedTo { get; set; }  // Nullable
    }
}

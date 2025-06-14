using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace travelsAPI.Models
{
    public class Place
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(128)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(32)]
        public string Country { get; set; } = string.Empty;

        [Required]
        [MaxLength(32)]
        public string State { get; set; } = string.Empty;

        [Required]
        [MaxLength(32)]
        public string City { get; set; } = string.Empty;

        [Required]
        [MaxLength(128)]
        public string Address { get; set; } = string.Empty;

        [Required]
        [MaxLength(16)]
        public string ZipCode { get; set; } = string.Empty;

        public Boolean IsActive { get; set; } = true;

        //EF Core asigna la fecha automáticamente al crear
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace travelsAPI.Models
{
    public class TravelStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(32)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(32)]
        public string Color { get; set; } = string.Empty;

        public Boolean IsActive { get; set; } = true;

        //EF Core asigna la fecha automáticamente al crear
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}


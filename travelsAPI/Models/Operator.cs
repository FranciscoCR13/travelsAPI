using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace travelsAPI.Models
{
    public class Operator
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(32)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(32)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [MaxLength(64)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(64)]
        public string Phone { get; set; } = string.Empty;

        public Boolean IsActive { get; set; } = true;

        //EF Core asigna la fecha automáticamente al crear
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

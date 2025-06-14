using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;

namespace travelsAPI.Models
{
    public class Travel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(32)]
        public string Name { get; set; } = string.Empty;

        // Llave Foranea para el origen
        [Required]
        public int OriginId { get; set; }

        [ForeignKey(nameof(OriginId))]
        public virtual Place? Origin { get; set; }

        // Llave Foranea para el Destino
        [Required]
        public int DestinationId { get; set; }

        [ForeignKey(nameof(DestinationId))]
        public virtual Place? Destination { get; set; }

        // Llave Foranea para el Operador
        [Required]
        public int OperatorId { get; set; }

        [ForeignKey(nameof(OperatorId))]
        public virtual Operator? Operator { get; set; }

        [Required]
        public int StatusId { get; set; }

        [ForeignKey(nameof(StatusId))]
        public virtual TravelStatus? Status { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }

        [Required]
        [Column(TypeName = "time")]
        public TimeSpan StartTime { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime EndDate { get; set; }

        [Required]
        [Column(TypeName = "time")]
        public TimeSpan EndTime { get; set; }

        [MaxLength(500)]
        public string? Notes { get; set; }

        //EF Core asigna la fecha automáticamente al crear
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
    }
}

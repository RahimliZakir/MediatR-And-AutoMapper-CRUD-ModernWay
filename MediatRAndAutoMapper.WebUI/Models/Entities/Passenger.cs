using MediatRAndAutoMapper.WebUI.Models.Entities.Membership;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediatRAndAutoMapper.WebUI.Models.Entities
{
    public class Passenger
    {
        public int Id { get; set; }

        public int? CreatedByUserId { get; set; }

        public virtual AppUser CreatedByUser { get; set; }

        [Required(ErrorMessage = "Bu xana boş qoyula bilməz!")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Bu xana boş qoyula bilməz!")]
        public string Surname { get; set; } = null!;

        public string? GeneratedSecretKey { get; set; }

        [Required(ErrorMessage = "Bu xana boş qoyula bilməz!")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Bu xana boş qoyula bilməz!")]
        public string TicketNumber { get; set; } = null!;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; }

        // Other Way
        //public DateTime CreatedDate { get; set; } = DateTime.UtcNow.AddHours(4);
    }
}

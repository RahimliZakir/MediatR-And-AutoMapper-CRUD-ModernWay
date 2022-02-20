using MediatRAndAutoMapper.WebUI.Models.Entities.Membership;
using System.ComponentModel.DataAnnotations;

namespace MediatRAndAutoMapper.WebUI.AppCode.Modules.PassengersModule
{
    public class PassengerViewModel
    {
        public int Id { get; set; }

        public int? CreatedByUserId { get; set; }

        public AppUser? CreatedByUser { get; set; }

        [Required(ErrorMessage = "Bu xana boş qoyula bilməz!")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Bu xana boş qoyula bilməz!")]
        public string Surname { get; set; } = null!;

        [Required(ErrorMessage = "Bu xana boş qoyula bilməz!")]
        public int Age { get; set; }

        public string? GeneratedSecretKey { get; set; }

        [Required(ErrorMessage = "Bu xana boş qoyula bilməz!")]
        public string TicketNumber { get; set; } = null!;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow.AddHours(4);
    }
}

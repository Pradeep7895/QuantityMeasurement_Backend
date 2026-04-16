using System.ComponentModel.DataAnnotations;

namespace QuantityMeasurementApp.Model.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }
    }
}
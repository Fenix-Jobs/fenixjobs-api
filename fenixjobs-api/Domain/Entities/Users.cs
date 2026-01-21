using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fenixjobs_api.Domain.Entities
{
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [MaxLength(15)]
        public string? PhoneNumber { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [MaxLength(50)]
        public string? IdProfession { get; set; }
    }
}

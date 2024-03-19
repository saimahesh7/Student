using System.ComponentModel.DataAnnotations;

namespace Student.Models.DTOs
{
    public class AddStudentDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string Email { get; set; }
        [Required]
        [MinLength(13)]
        [MaxLength(15)]
        public string Phone { get; set; }
        [MaxLength(3000,ErrorMessage ="Address should be below 3000 characters")]
        public string? Address { get; set; }
    }
}

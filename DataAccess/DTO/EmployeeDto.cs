using System.ComponentModel.DataAnnotations;

namespace DataAccess.DTO
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 50 characters.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 50 characters.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Date of birth is required.")]
        public string DOB { get; set; }
        [Required(ErrorMessage = "Date of joining is required.")]
        public string DateOfJoin { get; set; }
        [Required(ErrorMessage = "Department is required.")]
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }

    }
}

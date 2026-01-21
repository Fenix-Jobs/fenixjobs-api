namespace fenixjobs_api.Application.DTOs
{
    public class RegisterDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string Password { get; set; }


        public string? ProfessionTitle { get; set; }

        public string? ProfessionDescription { get; set; }

        public string? ProfessionExperience { get; set; }

        public string? ProfessionEvidence { get; set; }
    }
}

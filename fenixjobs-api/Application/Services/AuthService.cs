using fenixjobs_api.Application.DTOs;
using fenixjobs_api.Application.Interfaces;
using fenixjobs_api.Domain.Documents;
using fenixjobs_api.Domain.Entities;
using fenixjobs_api.Infrastructure.Persistence.MongoDB;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace fenixjobs_api.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _repository;
        private readonly IConfiguration _configuration;
        private readonly FenixMongoContext _mongoContext;

        public AuthService(IUserRepository repository, IConfiguration configuration, FenixMongoContext mongoContext)
        {
            _repository = repository;
            _configuration = configuration;
            _mongoContext = mongoContext;
        }

        public async Task<ServiceResponseDto<Users>> RegisterAsync(RegisterDto dto)
        {
            var response = new ServiceResponseDto<Users>();

            var existingUser = await _repository.GetByEmailAsync(dto.Email);

            if (existingUser != null)
            {
                response.Status = false;
                response.Message = "El email ya está registrado.";
                return response;
            }

            string? professionIdToSave = null;

            bool hasProfessionData = !string.IsNullOrWhiteSpace(dto.ProfessionTitle) ||
                             !string.IsNullOrWhiteSpace(dto.ProfessionDescription) ||
                             !string.IsNullOrWhiteSpace(dto.ProfessionExperience);

            if (hasProfessionData)
            {
                var newProfession = new Profession
                {
                    Title = dto.ProfessionTitle ?? "",
                    Description = dto.ProfessionDescription ?? "",
                    Experience = dto.ProfessionExperience ?? "",
                    Evidence = dto.ProfessionEvidence ?? "",
                    CreatedAt = DateTime.UtcNow
                };

                try
                {
                    await _mongoContext.Profession.InsertOneAsync(newProfession);

                    professionIdToSave = newProfession.Id;
                }
                catch (Exception ex)
                {
                    response.Status = false;
                    response.Message = "Error al guardar profesión: " + ex.Message;
                    return response;
                }
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var newEmployee = new Users
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                PasswordHash = passwordHash,
                IdProfession = professionIdToSave
            };

            try
            {
                await _repository.AddAsync(newEmployee);
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = "Error al guardar usuario: " + ex.Message;
                return response;
            }

            response.Status = true;
            response.Message = "Usuario registrado exitosamente";
            response.Data = newEmployee;

            return response;
        }
    }
}

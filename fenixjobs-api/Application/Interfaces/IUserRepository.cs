using fenixjobs_api.Domain.Entities;

namespace fenixjobs_api.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<Users?> GetByEmailAsync(string email);

        Task<Users?> GetByIdAsync(int id);

        Task AddAsync(Users user);

        Task UpdateAsync(Users user);
    }
}

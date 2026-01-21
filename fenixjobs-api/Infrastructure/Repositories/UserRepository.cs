// using fenixjobs_api.Application.Interfaces;
using fenixjobs_api.Domain.Entities;
using fenixjobs_api.Infrastructure.Persistence.MySQL;
using Microsoft.EntityFrameworkCore;

namespace fenixjobs_api.Infrastructure.Repositories
{
    public class UserRepository
    {
        private readonly FenixDbContext _context;

        public UserRepository(FenixDbContext context)
        {
            _context = context;
        }
    }
}

using fenixjobs_api.Domain.Documents;
using MongoDB.Driver;

namespace fenixjobs_api.Infrastructure.Persistence.MongoDB
{
    public class FenixMongoContext
    {
        private readonly IMongoDatabase _database;

        public FenixMongoContext(IMongoDatabase database)
        {
            _database = database;
        }

        public IMongoCollection<Profession> Profession => _database.GetCollection<Profession>("Professions");
    }
}

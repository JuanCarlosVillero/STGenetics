
namespace STGenetics.Infrastructure.Queries
{
    using Dapper;
    using STGenetics.Application.Abstraction.Queries;
    using STGenetics.Application.Response;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AnimalQueries : IAnimalQueries
    {
        private IDbConnection db;

        public AnimalQueries(string connString)
        {
            this.db = new SqlConnection(connString);
        }

        public async Task<IList<AnimalResponse>> GetAllAnimalListAsync()
        {
            var result = await this.db.QueryAsync<AnimalResponse>("SELECT * FROM Animal");
            return result.ToList();
        }

        public async Task<AnimalResponse?> GetAnimalByIdAsync(int animalId)
        {
            var result = await this.db.QueryAsync<AnimalResponse>("SELECT * FROM Animal WHERE AnimalId = @AnimalId", new {AnimalId = animalId});
            return result.SingleOrDefault();
        }
    }
}

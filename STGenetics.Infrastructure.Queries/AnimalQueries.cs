
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
    using System.Security.AccessControl;
    using System.Text;
    using System.Threading.Tasks;

    public class AnimalQueries : IAnimalQueries
    {
        private IDbConnection db;

        public AnimalQueries(string connString)
        {
            this.db = new SqlConnection(connString);
        }

        public async Task<IList<AnimalResponse>> GetFilterAnimalsAsync(
            int animalId, string? name, string? sex, string? status)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            StringBuilder whereClause = new StringBuilder($@" WHERE 1 = 1");

            if (animalId != default)
            {
                whereClause.Append(new StringBuilder($@" AND AnimalId = @AnimalId "));
                parameters.Add("AnimalId", animalId);
            }

            if (!string.IsNullOrEmpty(name))
            {
                whereClause.Append(new StringBuilder($@" AND (UPPER(Name) LIKE '%'+UPPER(@Name)+'%') "));
                parameters.Add("Name", name);
            }

            if (!string.IsNullOrEmpty(sex))
            {
                whereClause.Append(new StringBuilder($@" AND (UPPER(Sex) LIKE '%'+UPPER(@Sex)+'%') "));
                parameters.Add("Sex", sex);
            }

            if (!string.IsNullOrEmpty(status))
            {
                whereClause.Append(new StringBuilder($@" AND (UPPER(Status) LIKE '%'+UPPER(@Status)+'%') "));
                parameters.Add("Status", status);
            }

            var sql = "SELECT * FROM Animal " + whereClause.ToString();
            var result = await this.db.QueryAsync<AnimalResponse>(sql, parameters);
            return result.ToList();
        }

        public async Task<AnimalResponse?> GetAnimalByIdAsync(int animalId)
        {
            var result = await this.db.QueryAsync<AnimalResponse>("SELECT * FROM Animal WHERE AnimalId = @AnimalId", new {AnimalId = animalId});
            return result.SingleOrDefault();
        }
    }
}

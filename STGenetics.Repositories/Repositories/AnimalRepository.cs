

namespace STGenetics.Repositories.Repositories
{
    using Dapper;
    using STGenetics.Domain.Model;
    using STGenetics.DomainServices.Abstraction.Repository;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class AnimalRepository:IAnimalRepository
    {
        private IDbConnection db;

        public AnimalRepository(string connString)
        {
            this.db = new SqlConnection(connString);
        }

        public async Task<AnimalModel> Add(AnimalModel animalModel)
        {
            var sql =
                "INSERT INTO [dbo].[Animal] ([Name] ,[Breed] ,[BirthDate], [Sex], [Price], [Status]) VALUES(@Name, @Breed, @BirthDate, @Sex, @Price, @Status); " +
                "SELECT CAST(SCOPE_IDENTITY() AS INT)";

            var id = await this.db.QueryAsync<int>(sql, animalModel);
            animalModel.AnimalId = id.Single();
            return animalModel;
        }
    }
}

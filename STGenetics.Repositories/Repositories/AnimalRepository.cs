

namespace STGenetics.Repositories.Repositories
{
    using Dapper;
    using STGenetics.Domain.Model;
    using STGenetics.DomainServices.Abstraction.Repository;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Threading.Tasks;
    public class AnimalRepository : IAnimalRepository
    {
        private IDbConnection db;

        public AnimalRepository(string connString)
        {
            this.db = new SqlConnection(connString);
        }

        public async Task<AnimalModel> AddAsync(AnimalModel animalModel)
        {
            var sql =
                "INSERT INTO [dbo].[Animal] ([Name] ,[Breed] ,[BirthDate], [Sex], [Price], [Status]) VALUES(@Name, @Breed, @BirthDate, @Sex, @Price, @Status); " +
                "SELECT CAST(SCOPE_IDENTITY() AS INT)";

            var id = await this.db.QueryAsync<int>(sql, animalModel);
            animalModel.AnimalId = id.Single();
            return animalModel;
        }

        public async Task<AnimalModel> UpdateAsync(AnimalModel animalModel)
        {
            var sql =
                "UPDATE [dbo].[Animal] " +
                "SET    Name = @Name, " +
                "       Breed = @Breed, " +
                "       BirthDate = @BirthDate, " +
                "       Sex = @Sex," +
                "       Price = @Price," +
                "       Status = @Status " +
                "WHERE AnimalId = @AnimalId";

            await this.db.ExecuteAsync(sql, animalModel);
            return animalModel;
        }

        public async Task DeleteAsync(long animalId)
        {
            await this.db.ExecuteAsync("DELETE FROM Animal WHERE AnimalId = @AnimalId", new { AnimalId = animalId });
        }
    }
}

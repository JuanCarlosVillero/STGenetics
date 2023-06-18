using Dapper;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;

namespace DataLayer
{
    public class GeneticsRepository : IGeneticsRepository
    {
        //private readonly AppConfiguration _appConfiguration;
        private IDbConnection db;

        public GeneticsRepository(string connString)
        //public GeneticsRepository(IOptionsSnapshot<AppConfiguration> options)
        {
            //this._appConfiguration = options.Value;
            //var conn = "server=.\\sqlexpress;database=STGenetics;Trusted_Connection=Yes;";
            //this.db = new SqlConnection(this._appConfiguration.DefaultConnection);
            this.db = new SqlConnection(connString);
        }
        public Animal Add(Animal animal)
        {
            throw new NotImplementedException();
        }

        public Animal Find(int animalId)
        {
            throw new NotImplementedException();
        }

        public List<Animal> GetAll()
        {
            return this.db.Query<Animal>("SELECT * FROM Animal").ToList();
        }

        public void Remove(int animalId)
        {
            throw new NotImplementedException();
        }

        public Animal Update(Animal animal)
        {
            throw new NotImplementedException();
        }
    }
}
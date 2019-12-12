using Domain.Pessoas.Entities;
using MongoDB.Bson.IO;
using MongoDB.Driver;

namespace Infra.Pessoas.DataAccess.Mongo
{
    public class MongoContext
    {
        private string _strConnection { get; set; }
        private string _DatabaseName { get; set; }
        public bool IsSSL { get; set; }
        public IMongoDatabase _dbMongo { get; set; }
        private MongoClient MongoClient { get; set; }
        private JsonWriterSettings _settings { get; set; }

        public MongoContext(string strConnection, string DataBasename)
        {
            _strConnection = strConnection;
            _DatabaseName = DataBasename;
			
			Conectar();
        }

        private void Conectar()
        {
            MongoClient = new MongoClient(_strConnection);
            _dbMongo = MongoClient.GetDatabase(_DatabaseName);
            if (!new ManagerCollection().CollectionExists(MongoClient, _DatabaseName, "Pessoas"))
            {
                _dbMongo.CreateCollection("Pessoas");
				
			}
			

		}
        //é como se fosse o DbSet<> do entity
        public IMongoCollection<Pessoa> Pessoas
        {
            get
            {

                return _dbMongo.GetCollection<Pessoa>("Pessoas");
            }
        }


    }
}

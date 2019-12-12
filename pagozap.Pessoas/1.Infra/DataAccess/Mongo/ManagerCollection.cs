using MongoDB.Bson;
using MongoDB.Driver;

namespace Infra.Pessoas.DataAccess.Mongo
{
    public class ManagerCollection
    {
        public bool CollectionExists(MongoClient client,string _DatabaseName, string collectionName)
        {
			try
			{
				var listCollectionNames = client.GetDatabase(_DatabaseName).ListCollectionNames().ToList();
				foreach (var item in listCollectionNames)
				{
					if (item.Equals(collectionName))
					{
						return true;
					}
				}
				//check for existence
				return false;
			}
			catch (System.Exception ex)
			{

				return false;
			}
			
		}
    }
}

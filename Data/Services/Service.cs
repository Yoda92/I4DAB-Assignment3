using MongoDB.Driver;
namespace Data {
    public class Service<TEntity> {

        protected readonly IMongoCollection<TEntity> _collection;

        public Service () {
            var client = new MongoClient ("mongodb://127.0.0.1:27017/");
            var database = client.GetDatabase ("FeetBook2000DB");

            _collection = database.GetCollection<TEntity> ((typeof (TEntity)).Name + 's');
        }

    }

}
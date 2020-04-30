using System;
using System.Collections.Generic;
using MongoDB.Driver;
namespace Data {
    public class Service<TEntity> where TEntity : Entity {

        protected readonly IMongoCollection<TEntity> _collection;

        public Service () {
            var client = new MongoClient ("mongodb://127.0.0.1:27017/");
            var database = client.GetDatabase ("FeetBook2000DB");

            _collection = database.GetCollection<TEntity> ((typeof (TEntity)).Name + 's');
        }
        public List<TEntity> GetAll () => _collection.Find (_ => true).ToList ();
        public TEntity Create (TEntity entity) {
            _collection.InsertOne (entity);
            return entity;
        }

        public List<TEntity> Find (Func<TEntity, bool> predicate) => _collection.Find (entity => predicate (entity)).ToList ();

        public TEntity Update (TEntity entity) {
            _collection.ReplaceOne (t => t.Id == entity.Id, entity);
            return entity;
        }

        public TEntity Delete (TEntity entity) {
            _collection.DeleteOne (t => t.Id == entity.Id);
            return entity;
        }
    }

}
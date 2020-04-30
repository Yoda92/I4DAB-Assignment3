using System;
using System.Collections.Generic;
using Data.Entities;
using MongoDB.Driver;
namespace Data.Services {
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

        public TEntity Update (TEntity entity) {
            _collection.ReplaceOne (t => t.Id == entity.Id, entity);
            return entity;
        }

        public TEntity GetById (string Id) {
            var users = _collection.Find (t => t.Id == Id).ToList ();
            if (users.Count < 1) return null;
            return users[0];
        }

        public TEntity Delete (TEntity entity) {
            _collection.DeleteOne (t => t.Id == entity.Id);
            return entity;
        }
    }

}
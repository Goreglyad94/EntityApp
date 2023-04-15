using EntityApp.Dal.Models;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityApp.Dal.Core
{

    public interface IEntityRepository 
    {
        public bool Add(Entity entity);
        public Entity? Get(Guid id);
        public bool Remove(Guid id);
        public void Clear();

        Entity? this[Guid id]
        {
            get;
        }
    }

    public class EntityRepository : IEntityRepository, IEnumerable<ConcurrentDictionary<Guid, Entity>>
    {
        ConcurrentDictionary<Guid, Entity> _entities;

        public Entity? this[Guid id]
        {
            get
            {
                _entities.TryGetValue(id, out Entity? entity);
                return entity;
            }
        }

        public EntityRepository()
        {
            _entities = new ConcurrentDictionary<Guid, Entity>();
        }

        public bool Add(Entity entity)
        {
           return _entities.TryAdd(entity.Id, entity);
        }

        public Entity? Get(Guid id)
        {
            _entities.TryGetValue(id, out var entity);
            return entity;
        }

        public bool Remove(Guid id) 
        {
            _entities.Remove(id, out Entity? entity);
            return entity != null;
        }

        public void Clear()
        {
            _entities.Clear();
        }

        public IEnumerator<ConcurrentDictionary<Guid, Entity>> GetEnumerator()
        {
            return (IEnumerator<ConcurrentDictionary<Guid, Entity>>)_entities.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _entities.GetEnumerator();
        }
    }
}

using EntityApp.Dal.Models;
using System.Collections;

namespace EntityApp.Dal.Core
{
    /// <summary>
    /// Коллекцтя  объектов Entity
    /// </summary>
    public class Entities : ICollection<Entity>
    {
        private List<Entity> _entities;
        private readonly object _lock = new object();

        public int Count => _entities.Count;

        public bool IsReadOnly => false;

        public Entities(List<Entity> entities)
        {
            _entities = entities;
        }

        public Entities()
        {
            _entities = new List<Entity>();
        }

        public Entity? this[Guid id]
        {
            get 
            {
                lock (_lock)
                {
                    return _entities.FirstOrDefault(x => x.Id == id);
                }
            } 
        }

        public void Add(Entity item)
        {
            lock (_lock)
            {
                if (_entities.FirstOrDefault(x => x.Id == item.Id) == null)
                    _entities.Add(item);
                else
                    throw new InvalidOperationException($"В коллекции уже есть объект с Id = {item.Id}");
            }
        }

        public void Clear()
        {
            lock (_lock)
            {
                if (_entities != null)
                    _entities.Clear();
            }
        }

        public bool Contains(Entity item)
        {
            if (item == null)
                return false;

            return _entities.Contains(item);
        }

        public void CopyTo(Entity[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException("Передан пустой массив");

            ((ICollection<Entity>)this).CopyTo(array, arrayIndex);
        }

        public bool Remove(Entity item)
        {
            lock (_lock)
            {
                if (item == null)
                    throw new ArgumentNullException();

                return _entities.Remove(item);
            }
        }

        public IEnumerator<Entity> GetEnumerator()
            => _entities.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => _entities.GetEnumerator();
    }
}

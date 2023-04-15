using EntityApp.Dal.Models;

namespace EntityApp.Dal.Core
{
    public interface IEntityService
    {
        Task Insert(Entity entity);
        Task<Entity?> Get(Guid id);
        void Clear();
    }

    public class EntityService : IEntityService
    {
        private readonly Entities _entities;
        public EntityService(Entities entities)
        {
            _entities = entities;
        }

        /// <summary>
        /// Добавить новый объект Entity в коллекцию Entities
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task Insert(Entity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Объект не найден");

            try
            {
                await Task.Run(() => _entities.Add(entity));
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Не удалось добавить объект в коллекцию", ex);
            }
        }

        /// <summary>
        /// Возвращает объект из коллекции Entities по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор объекта</param>
        /// <returns></returns>
        public async Task<Entity?> Get(Guid id)
        {
            return await Task.Run(() => _entities[id]);
        }

        /// <summary>
        /// Очистка коллекции Entities
        /// </summary>
        public void Clear()
        {
            _entities.Clear();
        }
    }
}

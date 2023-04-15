using EntityApp.Dal.Models;

namespace EntityApp.Dal.Core
{
    public class EntityRepositoryService : IEntityService
    {
        private readonly IEntityRepository _entityRepository;

        public EntityRepositoryService(IEntityRepository entityRepository)
        {
            _entityRepository = entityRepository;
        }

        /// <summary>
        /// Очистить коллекцию
        /// </summary>
        public void Clear()
        {
            _entityRepository.Clear();
        }

        /// <summary>
        /// Получить объект по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Entity?> Get(Guid id)
        {
            return await Task.Run(() => _entityRepository.Get(id));
        }

        /// <summary>
        /// Добавление нового объекта
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task Insert(Entity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Объект не найден");

            await Task.Run(() =>
            {
                if (!_entityRepository.Add(entity))
                {
                    throw new InvalidOperationException($"Объект с Id {entity.Id} уже добавлен в коллекцию");
                }
            });
        }
    }
}

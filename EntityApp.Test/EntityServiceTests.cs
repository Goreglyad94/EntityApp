using EntityApp.Dal.Core;
using EntityApp.Dal.Models;
using NUnit.Framework.Constraints;

namespace EntityApp.Test
{
    public class EntityServiceTests
    {
        IEntityService _entityService;

        [SetUp]
        public void Setup()
        {
            var entities = new EntityRepository();
            _entityService = new EntityRepositoryService(entities);

            //var entities = new Entities();
            //_entityService = new EntityService(entities);
        }

        [Test]
        public void Add_New_Entity_to_Entities_Collection()
        {
            _entityService.Insert(new Entity());
            Assert.Pass();
            _entityService.Clear();
        }

        [Test]
        public void Can_THROW_Exeption_WHEN_Add_new_Entity_to_Entities_Collection_WHEN_Entity_already_exist_WITH_same_GUID()
        {
            var guid = Guid.NewGuid();
            var entity1 = new Entity() { Id = guid };
            var entity2 = new Entity() { Id = guid };

            Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await _entityService.Insert(entity1);
                await _entityService.Insert(entity2);
            });

            _entityService.Clear();
        }

        [Test]
        public async Task Can_Get_Exist_Entity()
        {
            var guid = Guid.NewGuid();
            await _entityService.Insert(new Entity() { Id = guid });
            var entity = await _entityService.Get(guid);
            Assert.IsNotNull(entity);
        }

        [Test]
        public async Task Can_Get_Null_Entity()
        {
            await _entityService.Insert(new Entity()
            {
                Id = Guid.NewGuid()
            });

            var guid = Guid.NewGuid();
            var entity = await _entityService.Get(guid);
            Assert.IsNull(entity);
        }

        [Test]
        public void INSERT_multithreading()
        {
            var guid = Guid.NewGuid();
            var entity = new Entity() { Id = guid };

            var guidList = new List<Guid>();

            for (int i = 0; i < 100; i++)
            {
                guidList.Add(Guid.NewGuid());
            }

            Assert.ThrowsAsync<AggregateException>(async () =>
            {
                var task1 = Task.Run(async () =>
                {
                    for (int i = 0; i < 100; i++)
                    {
                        await _entityService.Insert(new Entity() { Id = guidList[i], Amount = i });
                        Task.Delay(new Random().Next(0, 100));
                    }
                });

                var task2 = Task.Run(async () =>
                {
                    for (int i = 99; i != 0; i--)
                    {
                        await _entityService.Insert(new Entity() { Id = guidList[i], Amount = i });
                        Task.Delay(new Random().Next(0, 100));
                    }
                });

                task1.Wait();
                task2.Wait();
            });

            _entityService.Clear();
        }
    }
}
using EntityApp.Dal.Core;

namespace EntityApp.Test
{
    public class EntityServiceTests
    {
        IEntityService _entityService;

        [SetUp]
        public void Setup()
        {
            var entities = new Entities();
            _entityService = new EntityService(entities);
        }

        [Test]
        public void Add_New_Entity_to_Entities_Collection()
        {
            _entityService.Insert(new Dal.Models.Entity());
            Assert.Pass();
            _entityService.Clear();
        }

        [Test]
        public void Can_THROW_Exeption_WHEN_Add_new_Entity_to_Entities_Collection_WHEN_Entity_already_exist_WITH_same_GUID()
        {
            var guid = Guid.NewGuid();
            var entity1 = new Dal.Models.Entity() { Id = guid };
            var entity2 = new Dal.Models.Entity() { Id = guid };

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
            await _entityService.Insert(new Dal.Models.Entity() { Id = guid });
            var entity = await _entityService.Get(guid);
            Assert.IsNotNull(entity);
        }

        [Test]
        public async Task Can_Get_Null_Entity()
        {
            await _entityService.Insert(new Dal.Models.Entity() 
            { 
                Id = Guid.NewGuid()
            });
            
            var guid = Guid.NewGuid();
            var entity = await _entityService.Get(guid);
            Assert.IsNull(entity);
        }
    }
}
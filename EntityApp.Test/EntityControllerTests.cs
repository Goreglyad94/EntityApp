using EntityApp.Controllers;
using EntityApp.Dal.Core;
using EntityApp.Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityApp.Test
{
    public class EntityControllerTests
    {
        EntityController _entityController;
        IEntityService _entitiService;

        [SetUp]
        public void Setup()
        {
            var entities = new Entities();
            var entitiService = new EntityService(entities);

            _entitiService = entitiService;
            _entityController = new EntityController(entitiService);
        }

        [Test]
        public void INSERT_new_entity_WITH_Valid_JSON()
        {
            _entityController.Insert("{\"id\":\"cfaa0d3f-7fea-4423-9f69-ebff826e2111\",\"operationDate\":\"2019-04-02\",\"amount\":23.05}");
            Assert.Pass();
            _entitiService.Clear();
        }

        [Test]
        public void INSERT_empty_request()
        {
            Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await _entityController.Insert(null);
            });
        }

        [Test]
        public void INSERT_Invalid_Guid()
        {
            Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await _entityController.Insert("{\"id\":\"aaa\",\"operationDate\":\"2019-04-02\",\"amount\":23.05}");
            });
        }
    }
}

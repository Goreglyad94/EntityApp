using EntityApp.Dal.Core;
using EntityApp.Dal.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

namespace EntityApp.Controllers
{
    [ApiController]
    public class EntityController : ControllerBase
    {
        private readonly IEntityService _entityService;
        public EntityController(IEntityService entityService)
        {
            _entityService = entityService;
        }

        /// <summary>
        /// Добавить новый объект Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("insert")]
        public async Task<IActionResult> Insert(string request)
        {
            try
            {
                var entity = JsonConvert.DeserializeObject<Entity>(request);

                if (_entityService.Get(entity.Id) != null)
                {
                    return BadRequest($"Объект с идентификатором {entity.Id} уже добавлен");
                }

                await _entityService.Insert(entity);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Ошибка вызова метода {nameof(Insert)}", ex);
            }

            return Ok();
        }

        /// <summary>
        /// Получить объект Entity по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> Get(string id)
        {
            Guid guid;
            string result;

            if (Guid.TryParse(id, out guid))
            {
                var entity = await _entityService.Get(guid);
                result = JsonConvert.SerializeObject(entity);
            }
            else
            {
                return BadRequest("Идентификатор объекта должен быть в формате GUID");
            }

            return Ok(result);
        }
    }
}

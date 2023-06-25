using Autofac.Core;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Midas.Net.Domain.Crud;
using Midas.Net.ResponseHandling;
using Newtonsoft.Json;
using System.Net;
using System.Text.Json;

namespace Midas.Net.Crud
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [ServiceFilter(typeof(CrudSupportFilter))]
    public class CrudController : ControllerBase
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMapper _mapper;
        public Type EntityType { get; set; }
        public CrudController(IServiceProvider serviceProvider, IMapper mapper)
        {
            _serviceProvider = serviceProvider;
            _mapper = mapper;
        }

        [HttpGet("{entityType}")]
        public async Task<IActionResult> GetAll(string entityType)
        {
            dynamic service = getDynamicService();

            var entities = await service.GetAllAsync();

            if (entities == null)
                throw new HttpException(HttpStatusCode.NotFound);

            return Ok(entities);
        }       

        [HttpGet("{entityType}/{id}")]
        public async Task<IActionResult> GetById(string entityType, long id)
        {
            dynamic service = getDynamicService();

            var entity = await service.GetByIdAsync(id);
            if (entity == null)
                throw new HttpException(HttpStatusCode.NotFound);

            return Ok(entity);
        }

        [HttpPost("{entityType}")]
        public async Task<IActionResult> Create(string entityType, [FromBody] JsonElement jsonData)
        {

            var body = JsonConvert.DeserializeObject(jsonData.GetRawText(), EntityType);

            dynamic service = getDynamicService();

            var entity = await service.CreateAsync(body);

            return Ok(entity);
        }

        [HttpPut("{entityType}")]
        public async Task<IActionResult> Update(string entityType, [FromBody] JsonElement jsonData)
        {
            var body = JsonConvert.DeserializeObject(jsonData.GetRawText(), EntityType);

            dynamic service = getDynamicService();

            var entity = await service.UpdateAsync(body);

            return Ok(entity);
        }

        [HttpDelete("{entityType}/{id}")]
        public async Task<IActionResult> Delete(string entityType, long id)
        {
            dynamic service = getDynamicService();

            await service.DeleteAsync(id);
            return Ok();
        }

        private dynamic getDynamicService()
        {
            var serviceType = typeof(ICrudService<>).MakeGenericType(EntityType);
            var service = (dynamic)_serviceProvider.GetService(serviceType);
            return service;
        }

    }

}

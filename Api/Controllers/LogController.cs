using Business.Dto;
using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogController : ControllerBase
    {
        public readonly ILogStoreService _logStoreService;

        public LogController(ILogStoreService logStoreService)
        {
            _logStoreService = logStoreService;
        }

        [HttpPost]
        public ActionResult<LogRequest> Create([FromBody]LogRequest request)
        {
            _logStoreService.Create(request);
            return Created(nameof(Create), request);
        }

        [HttpGet]
        public ActionResult<LogResponseDtoArray> All()
        {
            if (!_logStoreService.LocationIsReadable())
                return UnprocessableEntity("Can not read from source.");
            
            return _logStoreService.All();
        }

        [HttpGet("{key:int}")]
        public ActionResult<LogResponseDto> Get([FromRoute]int key)
        {
            if (!_logStoreService.LocationIsReadable())
                return UnprocessableEntity("Can not read from source.");

            if (!_logStoreService.Exists(key))
                return NotFound(key);

            return _logStoreService.Get(key);
        }
    }
}

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
        public ActionResult<LogRequest> Create(LogRequest request)
        {
            _logStoreService.Create(request);
            return Created(nameof(Create), request);
        }

        [HttpGet]
        public ActionResult<LogRequest> All()
        {
            if (!_logStoreService.LocationIsReadable())
                return UnprocessableEntity("Can not read from source.");
            // Unable to process request

            return _logStoreService.All();
        }

        [HttpGet("{key}")]
        public ActionResult<LogDto> Get([FromRoute]string key)
        {
            if (!_logStoreService.LocationIsReadable())
                return UnprocessableEntity("Can not read from source.");
            // Unable to process request

            if (!_logStoreService.Exists(key))
                return NotFound(key);

            return _logStoreService.Get(key);
        }
    }
}

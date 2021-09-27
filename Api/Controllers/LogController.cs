using Api.Models;
using Api.Services;
using Business.Dto;
using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogController : ControllerBase
    {
        public readonly ILogStoreService _logStoreService;
        private readonly MailService _mailService;

        public LogController(ILogStoreService logStoreService, MailService mailService)
        {
            _logStoreService = logStoreService;
            _mailService = mailService;
        }

        [HttpPost]
        public ActionResult<LogRequest> Create([FromBody]LogRequest request)
        {
            _logStoreService.Create(request);
            return Created(nameof(Create), request);
        }

        [HttpPost("SendEmail")]
        public async Task<IActionResult> Send([FromForm] MailRequest mailrequest)
        {

            await _mailService.SendEmailAsync(mailrequest,_logStoreService.All());
            return Ok();



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

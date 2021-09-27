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
        public ActionResult<LogRequest> Create(LogRequest request)
        {
            _logStoreService.Create(request);
            return Created(nameof(Create), request);
        }
        [HttpPost("SendEmail")]
        public async Task<IActionResult> Send([FromForm] MailRequest mailrequest)
        {

            await _mailService.SendEmailAsync(mailrequest);
            return Ok();



        }

        [HttpGet]
        public ActionResult<LogRequest> All()
        {
            if (!_logStoreService.LocationIsReadable())
                return NotFound();
            // Unable to process request

            return _logStoreService.All();
        }

        [HttpGet("{key}")]
        public ActionResult<LogDto> Get([FromRoute]string key)
        {
            if (!_logStoreService.LocationIsReadable())
                return NotFound();
            // Unable to process request

            if (!_logStoreService.Exists(key))
                return NotFound();

            return _logStoreService.Get(key);
        }
    }
}

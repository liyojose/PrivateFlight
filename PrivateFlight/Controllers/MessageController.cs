using Azure.Core.Pipeline;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrivateFlight.Dto;
using PrivateFlight.Repo;

namespace PrivateFlight.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly ILogger<MessageController> _logger;
        private readonly IMessageRepository _repo;

        public MessageController(ILogger<MessageController> logger, IMessageRepository repo)
        {
            _logger = logger;
            _repo=repo;
        }

        [HttpGet(Name = "GetMessageByCode")]
        public async Task<MessageDto> GetMessageByCode([FromQuery]string countrycode, DateTime departuredate)
        {
            return await _repo.GetMessage(countrycode, departuredate);
        }
        [HttpGet]
        [Route("GetAllMessage")]
        public async Task<IEnumerable<MessageDto>> GetAllMessage([FromQuery] string countrycode)
        {
            return await _repo.GetAllMessage(countrycode);
        }
    }
}

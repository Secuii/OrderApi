using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using OrderAPI.DataBase;
using OrderAPI.Hub;
using OrderAPI.Requests;
using OrderAPI.Responses;

namespace OrderAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly ICommandResolver _commandResolver;
        private readonly IHubContext<MessageHub, IMessageHubClient> _hubContext;

        public OrderController(ILogger<OrderController> logger, ICommandResolver commandResolver, IHubContext<MessageHub, IMessageHubClient> hubContext)
        {
            _logger = logger;
            _commandResolver = commandResolver;
            _hubContext = hubContext;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public GetAllItemsResponse Get()
        {
            var data = _commandResolver.Resolve<GetAllItemsRequest, GetAllItemsResponse>(new GetAllItemsRequest());
            return data;
        }

        [HttpPost]
        [Route("orderPost")]
        public string Post()
        {
            List<string> offers = new List<string>();
            _hubContext.Clients.All.SendOffersToUser(offers);
            return "Offers sent successfully to all users!";
        }
    }
}

using Grpc.Net.Client;
using GrpcService;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GrpcController : ControllerBase
    {
        private readonly GrpcChannel _channel;
        private readonly Greeter.GreeterClient _greeterClient;

        public GrpcController()
        {
            _channel = GrpcChannel.ForAddress("https://localhost:7168"); ;
            _greeterClient = new Greeter.GreeterClient(_channel);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var reply = await _greeterClient.GetCustomersAsync(new Request());
            
            return Ok(reply);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] PostRequest request, CancellationToken token)
        {
            var reply = await _greeterClient.PostCustomerAsync(request);
            
            return Ok(reply);
        }
    }
}


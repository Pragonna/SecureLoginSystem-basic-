using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected ISender _sender => HttpContext.RequestServices.GetService<ISender>();

        protected string? GetIpAddress()
        {
            // Check if the request contains the "X-Forwarded-For" header.
            // This header is used when the request goes through a proxy or load balancer.
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];

            // If "X-Forwarded-For" is not found, get the remote IP address of the client.
            // Convert IPv6 to IPv4 if needed and return it as a string.
            return HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString();
        }

    }
}

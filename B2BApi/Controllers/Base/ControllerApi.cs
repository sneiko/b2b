using Microsoft.AspNetCore.Mvc;

namespace B2BApi.Controllers.Base
{
    [Produces("application/json")]
    [ProducesResponseType(typeof(string), 500)]
    [ApiController]
    public abstract class ControllerApi : ControllerBase
    {
        
    }
}
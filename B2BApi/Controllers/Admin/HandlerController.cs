using B2BApi.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace B2BApi.Controllers.Admin
{
    [Area("Admin")]
    [Route("api/v1/[area]/[controller]")]
    [Authorize(Roles = "Admin")]
    public class HandlerController : ControllerAuthorizeApi
    {
        
    }
}
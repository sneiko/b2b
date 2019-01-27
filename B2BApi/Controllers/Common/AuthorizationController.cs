using System.Threading.Tasks;
using B2BApi.Controllers.Base;
using B2BApi.Enums;
using B2BApi.FormModels;
using B2BApi.Helpers;
using B2BApi.Intrefaces;
using B2BApi.Models;
using B2BApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace B2BApi.Controllers
{
    [Area("Common")]
    [Route("api/v1/[area]/[controller]")]
    public class AuthorizationController : ControllerApi
    {
        private readonly IAuthorizService _authorizeService;
        private readonly IDateTimeProvider _date;
        private readonly JwtConfiguration _configuration;
        
        public AuthorizationController(IAuthorizService authorizeService,  IDateTimeProvider date, 
            IOptions<JwtConfiguration> configuration)
        {
            _authorizeService = authorizeService;
            _date = date;
            _configuration = configuration.Value;
        }
        
        /// <summary>
        ///     Авторизует пользователя
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ServiceResult<CompleteToken>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> Post([FromBody] AuthorizeFormModel formModel)
        {
            var authorizeResult = await _authorizeService.AuthorizeAsync(formModel);
            if (authorizeResult.Status == ResultStatus.Fail)
            {
                return StatusCode(403, authorizeResult.Message);
            }
            
            var newToken = JwtHelper.BuildCompleteToken(authorizeResult.ResultObject, _configuration, _date.Now);
            return Ok(await _authorizeService.SaveRefreshTokenCommonAsync(authorizeResult.ResultObject, newToken));
        }
    }
}
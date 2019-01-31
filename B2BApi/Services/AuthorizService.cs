using System;
using System.Threading.Tasks;
using B2BApi.Enums;
using B2BApi.FormModels;
using B2BApi.Intrefaces;
using B2BApi.Models;
using B2BApi.Models.Enum;
using B2BApi.Repositories;
using B2BApi.ViewModels;

namespace B2BApi.Services
{
    public class AuthorizService : IAuthorizService
    {
        private readonly IHashProvider _hashProvider;
        private readonly IUsersRepository _usersRepository;

        public AuthorizService(IHashProvider hashProvider, IUsersRepository usersRepository)
        {
            _hashProvider = hashProvider;
            _usersRepository = usersRepository;
        }

        public async Task<ServiceResult<User>> AuthorizeAsync(AuthorizeFormModel formModel)
        {
            try
            {
                var salt = await _usersRepository.GetUserSaltAsync(formModel.UserName);
                if (!string.IsNullOrWhiteSpace(salt))
                {
                    var hashedPassword = _hashProvider.GetHash(formModel.Password + salt);
                    var user = await _usersRepository.GetUserAsync(formModel.UserName, hashedPassword);
                    if (user != null && user.Status != UserStatus.Blocked)
                    {
                        return new ServiceResult<User>(user, ResultStatus.Success);
                    }
                }

                return new ServiceResult<User>(null, ResultStatus.Fail,
                    $"Пользователь {formModel.UserName} не найден или пароль не действителен.");
            }
            catch (Exception e)
            {
                return new ServiceResult<User>(null, ResultStatus.Fail, "Сервис недоступен");
            }
        }
        
        public async Task<ServiceResult<CompleteToken>> SaveRefreshTokenCommonAsync(User user, CompleteToken token)
        {
            try
            {
                await _usersRepository.SaveCompleteToken(user.Id, token);
                return new ServiceResult<CompleteToken>(token, ResultStatus.Success);
            }
            catch (Exception e)
            {
                return new ServiceResult<CompleteToken>(null, ResultStatus.Fail, "Сервис недоступен");
            }
        }
        
        public async Task<ServiceResult<User>> ValidateRefreshTokenCommonAsync(string token, int userId)
        {
            try
            {
                if (!await _usersRepository.IsRefreshTokenValid(userId, token))
                {
                    return new ServiceResult<User>(null, ResultStatus.Fail, "Токен не действителен");
                }
                
                var user = await _usersRepository.GetUserAsync(userId);
                return new ServiceResult<User>(user, ResultStatus.Success);
            }
            catch (Exception e)
            {
                return new ServiceResult<User>(null, ResultStatus.Fail, "Сервис недоступен");
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using B2BApi.DbContext;
using B2BApi.Interfaces;
using B2BApi.Models;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.EntityFrameworkCore;

namespace B2BApi.Repositories
{
    public class UsersRepository : BaseRepository, IUsersRepository
    {
        public UsersRepository(B2BDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<string> GetUserSaltAsync(string username)
        {
            var entry = await Context.Users
                .Include(x => x.Credentials)
                .FirstOrDefaultAsync(x => x.UserName.Equals(username, StringComparison.InvariantCultureIgnoreCase));
            return entry?.Credentials.Salt;
        }
        
        public async Task<User> GetUserAsync(string username, string password)
            => await Context.Users
                .Include(x => x.Credentials)
                .Include(x => x.Token)
                .FirstOrDefaultAsync(x => x.UserName.Equals(username,
                                              StringComparison.InvariantCultureIgnoreCase) &&
                                          x.Credentials.Password.Equals(password));
        
        public async Task<User> GetUserAsync(int userId)
            => await Context.Users
                .Include(x => x.Credentials)
                .Include(x => x.Token)
                .FirstOrDefaultAsync(x => x.Id == userId);
        
        public async Task SaveCompleteToken(int userId, CompleteToken token)
        {
            var updated = await Context.Users
                              .Include(x => x.Token)
                              .FirstAsync(x => x.Id == userId) ?? throw new KeyNotFoundException();
            updated.Token = token;
            await Context.SaveChangesAsync();
        }
        
        
        public async Task<bool> IsRefreshTokenValid(int userId, string refreshToken)
        {
            var entry = await Context.Users
                .Include(x => x.Token)
                .FirstOrDefaultAsync(x => x.Id == userId && x.Token.RefreshToken == refreshToken);
            return entry != null;
        }
    }
}
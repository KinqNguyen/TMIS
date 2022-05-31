
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TH_Project.Data;
using TH_Project.Data.Tables;

namespace Stump.Api.Data.Services
{
    public class TokenService
    {
        private readonly TH_DbConotext context;
        public TokenService(TH_DbConotext context)
        {
            this.context = context;
        }

        public async Task AddTokenAsync(RefreshToken refreshToken)
        {
            context.RefreshTokens.Add(refreshToken);
            await context.SaveChangesAsync();
        }

        public async Task RefreshTokenAsync(long tokenStoredId, string newJwtId)
        {
            var token = await context.RefreshTokens.FirstOrDefaultAsync(x => x.Id == tokenStoredId);
            if (token == null)
                return;
            token.JwtId = newJwtId;
            await context.SaveChangesAsync();
        }

        public async Task<RefreshToken> GetAsync(string refreshToken)
        {
            return await context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == refreshToken);
        }
        
        public async Task<RefreshToken> GetByJwtTokenAsync(string JwtToken)
        {
            return await context.RefreshTokens.FirstOrDefaultAsync(x => x.JwtId == JwtToken);
        }

        public async Task EditAsync(RefreshToken refreshToken)
        {
            var entity = context.RefreshTokens.Find(refreshToken.Id);

            entity.IsUsed = refreshToken.IsUsed;

            await context.SaveChangesAsync();

        }
    }
}

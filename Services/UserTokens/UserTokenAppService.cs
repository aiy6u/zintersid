using zintersid.Common.IoC;
using zintersid.Data.Entities;
using zintersid.Repositories;

namespace zintersid.Services.UserTokens
{
    public class UserTokenAppService : AppServiceBase, IUserTokenAppService
    {
        private readonly IUserTokenRepository _userTokenRepository;

        public UserTokenAppService(IUserTokenRepository userTokenRepository)
        {
            _userTokenRepository = userTokenRepository;
        }

        public async Task<IEnumerable<AppUserToken>> GetUserTokensAsync(string userId)
        {
            // Retrieve all tokens associated with a specific userId
            return await _userTokenRepository.FindAsync(t => t.UserId == userId);
        }

        public async Task<AppUserToken?> GetUserTokenByIdAsync(string tokenId)
        {
            // Retrieve a specific user token by its token ID
            return await _userTokenRepository.GetByIdAsync(tokenId);
        }

        public async Task AddUserTokenAsync(AppUserToken token)
        {
            // Add a new user token to the repository
            await _userTokenRepository.AddAsync(token);
        }

        public async Task UpdateUserTokenAsync(AppUserToken token)
        {
            // Update an existing user token in the repository
            await _userTokenRepository.UpdateAsync(token);
        }

        public async Task DeleteUserTokenAsync(string userId)
        {
            // Delete a user token from the repository by its token ID
            await _userTokenRepository.DeleteAllUserToken(userId);
        }
       
        public async Task DeleteRefreshTokenAsync(string userId, string refreshToken)
        {
            // Delete a user token from the repository by its token ID
            await _userTokenRepository.DeleteRefreshToken(userId, refreshToken);
        }

        public async Task<AppUserToken?> GetByTokenAsync(string token)
        {
            // Retrieve a user token by the token string
            return await _userTokenRepository.FindSingleAsync(t => t.Value == token);
        }

        public async Task SaveRefreshTokenAsync(string userId, string refreshToken)
        {
            // Save a new refresh token for a user
            var userToken = new AppUserToken
            {
                UserId = userId,
                LoginProvider = "InApp",
                Name = $"RefreshToken-{DateTime.UtcNow:yyyyMMddHHmmss}",
                Value = refreshToken,
                Expiration = DateTime.UtcNow.AddDays(7) // TODO: Update expiration as needed
            };
            await AddUserTokenAsync(userToken);
        }
    }
}
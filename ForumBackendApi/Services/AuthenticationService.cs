using System.Security.Claims;
using System.Security.Cryptography;
using ForumBackend.Core.DataTransfer;
using ForumBackend.Core.Model;
using ForumBackend.Core.Services;
using ForumBackend.Data.Context;
using ForumBackendApi.Util;
using Microsoft.EntityFrameworkCore;

namespace ForumBackendApi.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly ForumContext _context;
    private readonly IAccessTokenGenerator _accessTokenGenerator;
    private readonly IAccessTokenValidator _accessTokenValidator;

    public AuthenticationService(ForumContext context, IAccessTokenGenerator accessTokenGenerator,
        IAccessTokenValidator accessTokenValidator)
    {
        _context = context;
        _accessTokenGenerator = accessTokenGenerator;
        _accessTokenValidator = accessTokenValidator;
    }

    public async Task<ResultBase<string, string>> Login(string username, string password)
    {
        ForumUser? user = await _context.Users!
            .Include(u => u.UserAuth)
            .FirstOrDefaultAsync(u => u.Nickname == username);

        if (user == null)
        {
            return new ErrorResult<string, string>("Invalid username or password");
        }

        using SHA512 hasher = SHA512.Create();
        var hash = user.UserAuth.Password;

        if (!hasher.VerifyHash(password, hash))
        {
            return new ErrorResult<string, string>("Invalid username or password");
        }

        var token = _accessTokenGenerator.GetToken(user);
        return new SuccessResult<string, string>(token);
    }

    public async Task<ResultBase<string, string>> Regenerate(string token)
    {
        ClaimsIdentity? identity = _accessTokenValidator.ValidateToken(token);

        if (identity == null)
        {
            return new ErrorResult<string, string>("Bad Token");
        }

        Claim? claim =  identity.FindFirst("id");

        if (claim == null)
        {
            return new ErrorResult<string, string>("Bad Token");
        }

        var id = int.Parse(claim.Value);
        ForumUser? user = await _context.Users!
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
        {
            return new ErrorResult<string, string>("User not found");
        }

        return new SuccessResult<string, string>(_accessTokenGenerator.GetToken(user));
    }
}
using System.Security.Cryptography;
using ForumBackend.Core.DataTransfer;
using ForumBackend.Core.Exceptions;
using ForumBackend.Core.Model;
using ForumBackend.Core.Services;
using ForumBackend.Data.Context;
using ForumBackendApi.Util;
using Microsoft.EntityFrameworkCore;

namespace ForumBackendApi.Services;

public class UserService : IUserService
{
    private readonly ForumContext _context;

    public UserService(ForumContext context)
    {
        _context = context;
    }

    public async Task<ResultBase<ForumUser, string>> RegisterUser(string nickname, string email, string password)
    {
        ForumUser? user = await _context.Users!
            .FirstOrDefaultAsync(u => u.Email == email || u.Nickname == nickname);

        if (user != null)
        {
            if (user.Nickname == nickname)
            {
                return new ErrorResult<ForumUser, string>("User with this nickname already exists");
            }

            if (user.Email == email)
            {
                return new ErrorResult<ForumUser, string>("User with this email already exists");
            }

            throw new IllegalStateException("Found user by email or nickname, but it has not matched either");
        }

        user = new()
        {
            Email = email,
            Nickname = nickname
        };

        UserAuth auth = new();

        using (SHA512 hash = SHA512.Create())
        {
            var sha = hash.GetHash(password);
            auth.Password = sha;
        }

        try
        {
            auth.User = user;
            await _context.Authentication!.AddAsync(auth);
            await _context.SaveChangesAsync();

            return new SuccessResult<ForumUser, string>(user);
        }
        catch (DbUpdateException)
        {
            return new ErrorResult<ForumUser, string>(
                "An unexpected error occured while registering new user, please try again");
        }
    }

    public async Task<ForumUser?> GetUserById(int id)
    {
        ForumUser? user = await _context.Users!
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);

        return user;
    }
}
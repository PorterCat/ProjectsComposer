using Microsoft.AspNetCore.Identity;
using ProjectsComposer.Core.Models;
using ProjectsComposer.DataAccess.Repository;

namespace ProjectsComposer.API.Services;

public class AccountsService(AccountsRepository accountsRepository, JwtService jwtService)
{
    public void Register(string username, string password)
    {
        var account = new Account()
        {
            Id = Guid.NewGuid(),
            UserName = username
        };
        
        var passwordHash = new PasswordHasher<Account>().HashPassword(account, password);
        account.PasswordHash = passwordHash;
        accountsRepository.Add(account);
    }

    public string Login(string username, string password)
    {
        var account = accountsRepository.GetByUsername(username);
        var result = new PasswordHasher<Account>()
            .VerifyHashedPassword(
                account, account.PasswordHash, password);

        if (result == PasswordVerificationResult.Success)
        {
            return jwtService.GenerateJwtToken(account);
        }
        else
        {
            throw new Exception("Unauthorized");
        }
    }
}
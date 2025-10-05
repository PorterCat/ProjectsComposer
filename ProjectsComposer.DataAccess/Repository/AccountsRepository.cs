using ProjectsComposer.Core.Models;

namespace ProjectsComposer.DataAccess.Repository;

public class AccountsRepository
{
    private static IDictionary<string, Account> _accounts = new Dictionary<string, Account>();

    public void Add(Account account) => 
        _accounts[account.UserName] = account;
    
    public Account? GetByUsername(string username) => 
        _accounts.TryGetValue(username, out var account) ? account : null;
}
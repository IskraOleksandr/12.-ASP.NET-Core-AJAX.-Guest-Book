using ASP.NET_Core_и_AJAX._Guest_Book.Models;

namespace ASP.NET_Core_и_AJAX._Guest_Book.Repository
{
    public interface IRepository
    {
        Task<List<Message>> GetMessages();

        Task<User> GetUser(string login);

        Task AddMessage(Message message);

        Task Save();

        Task<List<User>> GetUsers();

        Task AddUser(User user);
    }
}

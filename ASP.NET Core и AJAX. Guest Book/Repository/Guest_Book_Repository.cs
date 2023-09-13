using Microsoft.EntityFrameworkCore;
using ASP.NET_Core_и_AJAX._Guest_Book.Models;

namespace ASP.NET_Core_и_AJAX._Guest_Book.Repository
{
    public class Guest_Book_Repository : IRepository
    {
        private readonly Guest_BookContext _context;

        public Guest_Book_Repository(Guest_BookContext context)
        {
            _context = context;
        }

        public async Task<List<Message>> GetMessages()
        {
            var guest_book_context = _context.Messages.Include(u => u.User);
            return await guest_book_context.ToListAsync();
        }

        public async Task<User> GetUser(string login)
        {
            var user = _context.Users.FirstOrDefault(u => u.Login == login);
            return user;
        }

        public async Task AddMessage(Message message)
        {
            await _context.Messages.AddAsync(message);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        public async Task AddUser(User user)
        {
            await _context.Users.AddAsync(user);
        }
    }
}

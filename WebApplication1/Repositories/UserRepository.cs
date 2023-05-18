using Microsoft.EntityFrameworkCore;
using TeamProject.Data;
using TeamProject.Models.Domain;

namespace TeamProject.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MovieDbContext movieDbContext;
        public UserRepository(MovieDbContext movieDbContext) 
        {
            this.movieDbContext = movieDbContext;
        }

        public async Task<User> AddAsync(User user)
        {
            await movieDbContext.Users.AddAsync(user);
            await movieDbContext.SaveChangesAsync();
            return user;
        }

      

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var user = await movieDbContext.Users.ToListAsync();
            return user.OrderByDescending(x => x.Id).ToList();
            
        }

        public async Task<User?> GetAsync(long id)
        {
            return await movieDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

         public async Task<User?> DeleteAsync(long id)
        {
            var existingUser = await movieDbContext.Users.FindAsync(id);
            if (existingUser != null) 
            {
                movieDbContext.Users.Remove(existingUser);
                await movieDbContext.SaveChangesAsync();
                return existingUser;
            }
            return null;
        }

        public async Task<User?> UpdateAsync(User user)
        {
            var existingUser = await movieDbContext.Users.FindAsync(user.Id);
            if (existingUser == null) return null;

            existingUser.Name = user.Name;
            existingUser.PassWord = user.PassWord;
            existingUser.UserName = user.UserName;
            existingUser.PhoneNum = user.PhoneNum;
            existingUser.NickName = user.NickName;
            existingUser.Email = user.Email;

            await movieDbContext.SaveChangesAsync();
            return existingUser;
        }
    }
}

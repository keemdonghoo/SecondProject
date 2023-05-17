using TeamProject.Models.Domain;

namespace TeamProject.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetAsync(long id);
        Task<User> AddAsync(User user); // <-User (<Uid>,<u_Name>,u_PassWord,<u_userName>,u_phoneNum,u_nickName,u_isAdmin)
        Task<User?> UpdateAsync(User user);//(u_PassWord,u_phoneNum,u_nickName)
        Task<User?> DeleteAsync(long id);
    }
}

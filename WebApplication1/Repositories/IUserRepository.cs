using TeamProject.Models.Domain;

namespace TeamProject.Repositories
{
    public interface IUserRepository
    {
        //모든 유저 읽어오기
        Task<IEnumerable<User>> GetAllAsync();

        // 특정 아이디의 유저 읽어오기
        Task<User?> GetAsync(long id);

        Task<User?> GetByNameAsync(string name);

        // 유저 등록
        Task<User> AddAsync(User user); // <-User (<Uid>,<u_Name>,u_PassWord,<u_userName>,u_phoneNum,u_nickName,u_isAdmin)

        // 유저 데이터 수정
        Task<User?> UpdateAsync(User user);//(u_PassWord,u_phoneNum,u_nickName)

        // 유저 삭제
        Task<User?> DeleteAsync(long id);

    }
}

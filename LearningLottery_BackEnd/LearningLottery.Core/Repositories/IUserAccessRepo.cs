using LearningLottery.Core.Repositories.Models;
using LearningLottery.Core.Services.Models;

namespace LearningLottery.Core.Repositories;

public interface IUserAccessRepo {
    List<T> GetAllUser<T>() where T : class;
    T GetUserById<T>(int id) where T : class;
    bool AddUser(UserAccess user);
    bool DeleteUserById(int id);
    bool UpdateUser(UserAccess transformToRepoModel);
}
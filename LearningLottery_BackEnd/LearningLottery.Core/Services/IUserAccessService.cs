using LearningLottery.Core.Repositories.Models;
using LearningLottery.Core.Services.Models;

namespace LearningLottery.Core.Services;

public interface IUserAccessService {
    List<UserAccess> GetAllUser();
    UserAccess GetUserById(int id);
    bool AddUser(UserAccess user);
    bool DeleteUserById(int id);
    bool UpdateUser(UserAccess user);
}
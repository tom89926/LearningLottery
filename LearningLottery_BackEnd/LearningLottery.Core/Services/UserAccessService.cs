using LearningLottery.Core.InterfaceControl;
using LearningLottery.Core.Repositories;
using LearningLottery.Core.Repositories.Models;
using LearningLottery.Core.Services.Models;

namespace LearningLottery.Core.Services;

[IOC]
public class UserAccessService : IUserAccessService {
    private readonly IUserAccessRepo _userAccessRepo;
    private readonly IDateTimeProvider _dateTimeProvider;

    public UserAccessService(
        IUserAccessRepo userAccessRepo,
        IDateTimeProvider dateTimeProvider) {
        _userAccessRepo = userAccessRepo;
        _dateTimeProvider = dateTimeProvider;
    }

    public List<UserAccess> GetAllUser() {
        return _userAccessRepo.GetAllUser<UserAccess>();
    }

    public UserAccess GetUserById(int id) {
        return _userAccessRepo.GetUserById<UserAccess>(id);
    }

    public bool AddUser(UserAccess userAccess) {
        userAccess.AddTime = _dateTimeProvider.Now();
        return _userAccessRepo.AddUser(userAccess);
    }

    public bool DeleteUserById(int id) {
        return _userAccessRepo.DeleteUserById(id);
    }

    public bool UpdateUser(UserAccess userAccess) {
        return _userAccessRepo.UpdateUser(userAccess);
    }
}
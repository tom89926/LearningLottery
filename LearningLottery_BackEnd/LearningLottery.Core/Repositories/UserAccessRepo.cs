using AutoMapper;
using Dapper;
using iTextSharp.text;
using LearningLottery.Core.InterfaceControl;
using LearningLottery.Core.Repositories.Models;
using LearningLottery.Core.Services;
using LearningLottery.Core.Services.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Caching.Memory;

namespace LearningLottery.Core.Repositories;

[IOC]
public class UserAccessAccessRepo: IUserAccessRepo {
    private readonly IMemoryCache _memoryCache;

    private const string UserAccessCacheKey = "UserAccess";
    
    public UserAccessAccessRepo(
        IMemoryCache memoryCache) {
        _memoryCache = memoryCache;
    }
    
    private static User TransformToRepoModel(
        UserAccess userAccess
    ) {
        return new User() {
            Id = userAccess.Id,
            Name = userAccess.Name,
            AddTime = userAccess.AddTime,
        };
    }

    private static UserAccess OutPutUserAccess(User user) {
        var target = new UserAccess();
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<User, UserAccess>();
        });
        var mapper = config.CreateMapper();

        mapper.Map(user, target);

        return target;
    }
    private static List<UserAccess> OutPutUserAccessList(List<User> userList) {
        return userList.Select(userData => OutPutUserAccess(userData)).ToList();
    }
    
    public List<T> GetAllUser<T>() where T : class {
        var allUser = _memoryCache.Get<List<User>>(UserAccessCacheKey);
        if (allUser == null) return new List<T>();
        if (typeof(T) == typeof(UserAccess)) {
            return OutPutUserAccessList(allUser) as List<T> ?? new List<T>();
        }

        return allUser as List<T> ?? new List<T>();
    }

    public T GetUserById<T>(int id) where T : class {
        var users = GetAllUser<T>();
        var foundUser = users.FirstOrDefault(user => {
            switch (user)
            {
                case User u when u.Id == id:
                case UserAccess userAccess when userAccess.Id == id:
                    return true;
                default:
                    return false;
            }
        });
        
        if (foundUser != null) return foundUser;

        throw new Exception("User not found");
    }

    private int GenerateId() {
        var users = GetAllUser<User>();
        if(users.Count == 0) return 1;
        return users.Last().Id + 1;
    }
    
    public bool AddUser(UserAccess userAccess) {
        try {
            var users = GetAllUser<User>();
            var newUser = TransformToRepoModel(userAccess);
            newUser.Id = GenerateId();
            users.Add(newUser);
            _memoryCache.Set(UserAccessCacheKey, users);
            return true;
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return false;
        }
    }

    public bool DeleteUserById(int id) {
        var user = GetUserById<User>(id);
        var users = GetAllUser<User>();
        var deleteResult = users.Remove(user);
        try {
            _memoryCache.Set(UserAccessCacheKey, users);
            return deleteResult;
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return false;
        }
    }

    public bool UpdateUser(UserAccess userAccess) {
        var user = GetUserById<User>(userAccess.Id);
        user.Name = userAccess.Name;
        var users = GetAllUser<User>();
        var index = users.FindIndex(u => u.Id == user.Id);
        if (index == -1) return false;
        
        users[index] = user;

        try {
            _memoryCache.Set(UserAccessCacheKey, users);
            return true;
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return false;
        }
    }
}
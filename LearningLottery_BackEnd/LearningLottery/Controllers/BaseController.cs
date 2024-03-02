using LearningLottery.Models;
using Microsoft.AspNetCore.Mvc;

namespace LearningLottery.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController : ControllerBase {
        protected static Core.Services.Models.UserAccess OutPutUserAccessServiceModel(Models.UserAccess userAccess) {
            var access = new Core.Services.Models.UserAccess {
                Id = userAccess.Id,
                Name = userAccess.Name
            };
            return access;
        }
    }
}
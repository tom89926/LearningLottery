using LearningLottery.Core.InterfaceControl;

namespace LearningLottery.Services;

public class ModelTransformerService : IModelTransformerService {
    public ModelTransformerService() {
        
    }
    public Core.Services.Models.UserAccess OutPutUserAccessServiceModel(Models.UserAccess userAccess) {
        var access = new Core.Services.Models.UserAccess {
            Id = userAccess.Id,
            Name = userAccess.Name
        };
        return access;
    }
}
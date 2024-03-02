namespace LearningLottery.Services;

public interface IModelTransformerService {
    Core.Services.Models.UserAccess OutPutUserAccessServiceModel(Models.UserAccess userAccess);
}
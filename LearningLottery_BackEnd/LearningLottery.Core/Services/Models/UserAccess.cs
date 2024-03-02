using LearningLottery.Core.Repositories.Models;

namespace LearningLottery.Core.Services.Models; 

public class UserAccess {
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    
    public DateTime AddTime { get; set; }


}
using System.ComponentModel.DataAnnotations;
namespace LearningLottery.Models; 

public class UserAccess {
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime AddTime { get; set; }
}
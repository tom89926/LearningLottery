using System.ComponentModel.DataAnnotations;

namespace LearningLottery.Core.Repositories.Models; 

public class User {
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    
    public DateTime AddTime { get; set; }
}
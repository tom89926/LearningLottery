using LearningLottery.Core.InterfaceControl;
namespace LearningLottery.Core.Services.Models;

[IOC]
public class DateTimeProvider : IDateTimeProvider {
    public DateTime Now() {
        return DateTime.Now;
    }

    public DateTime Today() {
        return DateTime.Today;
    }

    public DateTime GetDateAfterDays(int days) {
        return Now().Date.AddDays(days);
    }
}

public interface IDateTimeProvider {
    DateTime Now();
    DateTime Today();
    DateTime GetDateAfterDays(int days);
}
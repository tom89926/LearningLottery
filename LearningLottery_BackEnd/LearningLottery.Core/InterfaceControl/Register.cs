namespace LearningLottery.Core.InterfaceControl;

public sealed class Register
{
    public void DI(Action<Type, Type, LifeType> register, string assembly = "")
    {
        var helper = new AssemblyHelper(assembly);
        var settingList = helper.GetSetting(helper.GetAssemblies());
        foreach (var setting in settingList)
        {
            register(setting.Item1, setting.Item2, setting.Item3);
        }
    }
}
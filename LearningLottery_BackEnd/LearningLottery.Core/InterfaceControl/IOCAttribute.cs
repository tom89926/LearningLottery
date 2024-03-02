namespace LearningLottery.Core.InterfaceControl;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
public class IOCAttribute : Attribute
{
    public Type DInterface { get; set; }
    public Type DImplementation { get; set; }
    public LifeType Life { get; set; }

    public IOCAttribute()
    {
        Life = LifeType.Transient;
    }

    public IOCAttribute(LifeType lifeType)
    {
        Life = lifeType;
    }
    
    public IOCAttribute(Type dInterfaceType, Type dImplementationType, LifeType lifeType)
    {
        DInterface = dInterfaceType;
        DImplementation = dImplementationType;
        Life = lifeType;
    }
}
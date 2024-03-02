using System.Reflection;
using Microsoft.Extensions.DependencyModel;

namespace LearningLottery.Core.InterfaceControl;

public class AssemblyHelper
{
    private string _assemblyFilter;

    public AssemblyHelper(string assemblyFilter)
    {
        _assemblyFilter = assemblyFilter;
    }

    private static Assembly GetAssembly(string name)
    {
        var libraries = DependencyContext.Default.RuntimeLibraries;
        foreach (var library in libraries)
        {
            if (library.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                var assembly = Assembly.Load(new AssemblyName(library.Name));
                return assembly;
            }
        }

        return null;
    }

    public List<Assembly> GetAssemblies()
    {
        var assemblies = new List<Assembly>();
        
        var libraries = DependencyContext.Default.RuntimeLibraries;
        foreach (var library in libraries)
        {
            if (library.Name.IndexOf(_assemblyFilter, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                var assembly = Assembly.Load(new AssemblyName(library.Name));
                assemblies.Add(assembly);
            }
        }
        
        return assemblies;
    }

    public List<Assembly> GetAssemblies(List<string> assemblyNameList)
    {
        var assemblies = new List<Assembly>();

        foreach (var assemblyName in assemblyNameList)
        {
            var assembly = GetAssembly(assemblyName);
            if (assembly != null)
            {
                assemblies.Add(assembly);
            }
        }

        return assemblies;
    }

    public List<(Type, Type, LifeType)> GetSetting(List<Assembly> assemblies)
    {
        var rtnList = new List<(Type, Type, LifeType)>();

        foreach (var assembly in assemblies)
        {
            foreach (var classType in assembly.GetTypes())
            {
                var attributeType = classType.GetTypeInfo().GetCustomAttribute<IOCAttribute>();
                if (attributeType != null)
                {
                    var implementType = attributeType.DImplementation;
                    if (implementType == null)
                    {
                        implementType = classType;
                    }

                    var interfaceType = attributeType.DInterface;
                    if (interfaceType == null)
                    {
                        var interfaceArray = classType.GetInterfaces();
                        if (interfaceArray.Count().Equals(1))
                        {
                            interfaceType = interfaceArray.Single();
                        }
                        else
                        {
                            interfaceType = classType.GetInterfaces().FirstOrDefault(i =>
                                i.FullName.IndexOf(_assemblyFilter, StringComparison.OrdinalIgnoreCase) >= 0);
                        }

                        if (interfaceType == null)
                        {
                            interfaceType = classType;
                        }
                    }

                    rtnList.Add((interfaceType, implementType, attributeType.Life));
                }
            }
        }

        return rtnList;
    }
}
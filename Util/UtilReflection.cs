using System.Linq.Expressions;
using System.Reflection;

namespace Util;

public static class UtilReflection
{
    public static readonly List<Assembly> assemblies;
    public static IReadOnlyList<Assembly> Assemblies => assemblies;

    static UtilReflection()
    {
        assemblies = new List<Assembly>(AppDomain.CurrentDomain.GetAssemblies());
        AppDomain.CurrentDomain.AssemblyLoad += (sender, args) => assemblies.Add(args.LoadedAssembly);
    }

    public static Type? GetType(string typeName)
    {
        var type = Type.GetType(typeName);
        if (type == null)
        {
            for (int i = assemblies.Count - 1; i >= 0; i--)
            {
                type = assemblies[i].GetType(typeName);
                if (type != null)
                    return type;
            }
        }
        return type;
    }

    public static Assembly[] GetAllAssemblies()
    {
        var assemblyNames = Assembly.GetExecutingAssembly().GetReferencedAssemblies();
        var assemblyRefs = new Assembly[assemblyNames.Length];
        for (int i = 0; i < assemblyNames.Length; i++)
            assemblyRefs[i] = Assembly.Load(assemblyNames[i]);
        return assemblyRefs;
    }

    public static T? GetFieldValue<T>(object obj, string name)
    {
        var bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
        var field = obj.GetType().GetField(name, bindingFlags);
        return (T?)field?.GetValue(obj);
    }

    public static T? GetPropValue<T>(object obj, string name)
    {
        var bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
        var prop = obj.GetType().GetProperty(name, bindingFlags);
        return (T?)prop?.GetValue(obj);
    }

    public delegate object ConstructorFunc(params object[] args);
    public static ConstructorFunc CreateConstructorFunc(Type constructorClassType, params Type[] constructorParams)
    {
        var constructorInfo = constructorClassType.GetConstructor(constructorParams);
        if (constructorInfo == null)
            throw new Exception($"{nameof(constructorInfo)} is null");

        // To feed the constructor with the right parameters, we need to generate an array 
        // of parameters that will be read from the initialize object array argument.
        var len = constructorParams.Length;
        var constructorArgs = new UnaryExpression[len];

        // convert the object[index] to the right constructor parameter type.
        var paramExpr = Expression.Parameter(typeof(object[]));
        for (int i = 0; i < len; i++)
            constructorArgs[i] = Expression.Convert(Expression.ArrayAccess(paramExpr, Expression.Constant(i)), constructorParams[i]);

        var body = Expression.New(constructorInfo, constructorArgs);
        var constructor = Expression.Lambda<ConstructorFunc>(body, paramExpr);
        return constructor.Compile();
    }

}

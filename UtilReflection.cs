using System;
using System.Collections.Generic;
using System.Reflection;

namespace Util
{
    public static class UtilReflection
    {
        public static readonly List<Assembly> assemblies;
        public static IReadOnlyList<Assembly> Assemblies => assemblies;

        static UtilReflection()
        {
            assemblies = new List<Assembly>(AppDomain.CurrentDomain.GetAssemblies());
            AppDomain.CurrentDomain.AssemblyLoad += (sender, args) => assemblies.Add(args.LoadedAssembly);
        }

        public static Type GetType(string typeName)
        {
            Type type = Type.GetType(typeName);
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

        public static T GetFieldValue<T>(object obj, string name)
        {
            var bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            var field = obj.GetType().GetField(name, bindingFlags);
            return (T)field?.GetValue(obj);
        }

        public static T GetPropValue<T>(object obj, string name)
        {
            var bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            var prop = obj.GetType().GetProperty(name, bindingFlags);
            return (T)prop?.GetValue(obj);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace INVex.Common.Common
{
    public static class AssemblyUtils
    {
        public static string ApplicationAssemblyFullName = string.Empty;

        private static List<Assembly> assemblies;
        private static List<Assembly> GetProjectAssemblies()
        {
            if (assemblies == null)
            {
                assemblies = new List<Assembly>();
                foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    if (assembly.FullName.StartsWith("INVex.") || assembly.FullName == ApplicationAssemblyFullName)
                    {
                        assemblies.Add(assembly);
                    }
                }
            }
            return assemblies;
        }

        /// <summary>
        /// Возвращает список зарегистрированных типов производных от 
        /// указанного типа или реализующих указанный интерфейс
        /// </summary>
        /// <param name="baseType">тип (или интерфейс)</param>
        /// <returns>список типов данных</returns>
        public static List<Type> GetTypes(Type baseType)
        {
            List<Type> list = new List<Type>();
            foreach (Assembly assembly in GetProjectAssemblies())
            {
                Type[] types;
                try
                {
                    types = assembly.GetTypes();
                }
                catch (Exception)
                {
                    continue;
                }
                foreach (Type t in types)
                {
                    if (t.BaseType == baseType)
                    {
                        list.Add(t);
                    }
                    else if (t.BaseType != null && t.BaseType.BaseType == baseType)
                    {
                        list.Add(t);
                    }
                    else if (t.GetInterfaces().Contains(baseType))
                    {
                        list.Add(t);
                    }
                }
            }
            return list;
        }
    }
}

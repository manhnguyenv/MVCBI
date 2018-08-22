using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using VASJ.BI.ModuleConnectors;

namespace VASJ.BI.Helpers
{
    public static class ModuleConnectorsHelper
    {
        public static IEnumerable<ModuleConnectors.IModuleConnector> GetModuleConnectors()
        {
            return (from t in Assembly.GetExecutingAssembly().GetTypes()
                    where t.GetInterfaces().Contains(typeof(IModuleConnector))
                       && t.GetConstructor(Type.EmptyTypes) != null
                    select Activator.CreateInstance(t) as IModuleConnector);
        }
    }
}
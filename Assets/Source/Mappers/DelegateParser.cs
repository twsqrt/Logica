using System;
using System.Reflection;

namespace Mappers
{
    public class DelegateParser
    {
        private static bool TemplateFunction(bool p1, bool p2, bool p3)
            => !(p1 && p3) || p2;

        public Delegate Parse(string configString)
        {
            Type type = typeof(DelegateParser);
            MethodInfo methodInfo = type.GetMethod("TemplateFunction", BindingFlags.Static | BindingFlags.NonPublic);
            return Delegate.CreateDelegate(typeof(Func<bool, bool, bool, bool>), methodInfo);
        }
    }
}
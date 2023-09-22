using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Converter
{
    public class ConfigStringToDelegate : IConverter<string, Delegate>
    {
        private static bool TemplateFunction(bool p1, bool p2, bool p3)
            => !(p1 && p3) || p2;

        public Delegate Converter(string configString)
        {
            Type type = typeof(ConfigStringToDelegate);
            MethodInfo methodInfo = type.GetMethod("TemplateFunction", BindingFlags.Static | BindingFlags.NonPublic);
            return Delegate.CreateDelegate(typeof(Func<bool, bool, bool, bool>), methodInfo);
        }
    }
}
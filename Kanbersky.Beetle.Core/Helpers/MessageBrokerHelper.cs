using System;
using System.Text;

namespace Kanbersky.Beetle.Core.Helpers
{
    public static class MessageBrokerHelper
    {
        public static string GetTypeName(Type type)
        {
            var name = type.FullName.ToLower().Replace("+", ".");
            StringBuilder sb = new StringBuilder();
            sb.Append(name);
            sb.Append("_event");
            return sb.ToString();
        }

        public static string GetTypeName<T>()
        {
            return GetTypeName(typeof(T));
        }
    }
}

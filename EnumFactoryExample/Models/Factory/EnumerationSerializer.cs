using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EnumFactoryExample.Models.Factory
{
    public class EnumerationSerializer
    {
        private static IList<JavascriptEnumTypeInfo> _types;

        public static void LoadTypes()
        {
            if (_types != null) return;
            _types =
                Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .Where(x => x.IsEnum)
                    .Select(t => new { t, attributes = t.GetCustomAttributes(typeof(JavaScriptEnumAttribute), false) })
                    .Where(@t1 => @t1.attributes != null && @t1.attributes.Length > 0)
                    .Select(@t1 => new JavascriptEnumTypeInfo { Type = @t1.t })
                    .ToList();
        }

        public static string GetEnumJavaScript(string nameSpace)
        {
            return String.Format(
@"(function () {{
    window.{0} = window.{0} || {{}};

    {0}.enums = {{
{1}
}};
}})();", nameSpace, String.Join(",", GetTypes().Select(ConvertEnumToJavaScript).ToList()));
        }

        public static string GetEnumJson()
        {
            return String.Format("{{{0}}}", String.Join(",", GetTypes().Select(ConvertEnumToJson).ToList()));
        }

        public static Type[] GetTypes()
        {
            LoadTypes();
            return _types.Select(x => x.Type)
                .Distinct()
                .ToArray();
        }

        public static string ConvertEnumToJavaScript(Type e)
        {
            return string.Format(@"{0}:{{{1}}}
", e.Name, string.Join(",", Enum.GetValues(e).Cast<object>().Select(@enum => string.Format(@"{0}:{{ id: {1}, name: ""{0}""}}
", @enum, (int)@enum))));
        }

        public static string ConvertEnumToJson(Type e)
        {
            return string.Format("\"{0}\":{{{1}}}", e.Name, string.Join(",", Enum.GetValues(e).Cast<object>().Select(@enum => string.Format("\"{0}\":{{ \"id\": {1}, \"name\": \"{0}\"}}", @enum, (int)@enum))));
        }
    }
}
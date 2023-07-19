using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BlazorHero.CleanArchitecture.Utilities
{
    public static class KnownValuesList
    {
        public static Dictionary<string, string> GetKnownTagPrefixList()
        {
            var result = new Dictionary<string, string>();
            var values = typeof(KnownValues.KnownTagPrefix).GetFields(BindingFlags.Static | BindingFlags.Public)
                                 .Select(x => x.GetValue(null)).Cast<string>();

            Type type = typeof(KnownValues.KnownTagPrefix); // MyClass is static class with static properties
            foreach (var p in type.GetFields(BindingFlags.Static | BindingFlags.Public))
            {
                var v = p.GetValue(null); // static classes cannot be instanced, so use null...
                                          //do something with v
                result.Add(v.ToString(), v.ToString());
            }

            return result;
        }

        public static Dictionary<string, string> GetKnownHtmlPagesList()
        {
            var result = new Dictionary<string, string>();
            var values = typeof(KnownValues.KnownHtmlPage).GetFields(BindingFlags.Static | BindingFlags.Public)
                                 .Select(x => x.GetValue(null)).Cast<string>();

            Type type = typeof(KnownValues.KnownHtmlPage); // MyClass is static class with static properties
            foreach (var p in type.GetFields(BindingFlags.Static | BindingFlags.Public))
            {
                var v = p.GetValue(null); // static classes cannot be instanced, so use null...
                                          //do something with v
                result.Add(v.ToString(), v.ToString());
            }

            return result;
        }

        public static Dictionary<string, string> GetKnownContentPlacementList()
        {
            var result = new Dictionary<string, string>();
            var values = typeof(KnownValues.KnownContentPlacement).GetFields(BindingFlags.Static | BindingFlags.Public)
                                 .Select(x => x.GetValue(null)).Cast<string>();

            Type type = typeof(KnownValues.KnownContentPlacement); // MyClass is static class with static properties
            foreach (var p in type.GetFields(BindingFlags.Static | BindingFlags.Public))
            {
                var v = p.GetValue(null);
                result.Add(v.ToString(), v.ToString());
            }

            return result;
        }
    }
}

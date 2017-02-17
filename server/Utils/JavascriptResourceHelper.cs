using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;

namespace TAI.Utils
{
    public class JavascriptResourceHelper
    {
        private static Dictionary<string, string> GenerateResourceDictionary(ResourceManager manager, CultureInfo cultureInfo)
        {
            var result = new Dictionary<string, string>();
            var resourceSet = manager.GetResourceSet(cultureInfo, true, true);
            foreach (DictionaryEntry entry in resourceSet)
            {
                result.Add(entry.Key.ToString(), entry.Value.ToString());
            }
            return result;
        }

        public class JavaScriptResources
        {
            private CultureInfo _cultureInfo;

            public JavaScriptResources(CultureInfo cultureInfo)
            {
                _cultureInfo = cultureInfo;
            }
        }
    }
}

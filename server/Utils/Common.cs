using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TAI.Utils
{
    public class Common
    {
        public static dynamic DictionaryToObject(Dictionary<string, object> dict, string text = null)
        {
            IDictionary<string, object> eo = new ExpandoObject() as IDictionary<string, object>;
            if (dict == null) return eo;
            foreach (KeyValuePair<string, object> kvp in dict)
            {
                if (kvp.Value == null) eo.Add(new KeyValuePair<string, object>(kvp.Key, null));
                else eo.Add(kvp);
            }
            if(text != null)
                eo.Add("ValueString", text);
            return eo;
        }

        public static string Sha256(string text)
        {
            var bytes = Encoding.UTF8.GetBytes(text);
            var hashstring = new SHA256Managed();
            var hash = hashstring.ComputeHash(bytes);
            var hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }
    }
}

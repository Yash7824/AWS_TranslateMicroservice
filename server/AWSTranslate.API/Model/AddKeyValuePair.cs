using System.Collections.Generic;

namespace AWSTranslate.API.Model
{
    public class AddKeyValuePair
    {
        public string pathLanguage { get; set; }
        public Dictionary<string, string> toAddDictionary { get; set; }
    }
}

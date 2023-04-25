using System.Collections.Generic;
using System.Linq;

namespace SimpleTranslationSystem
{
    public class Language
    {
        public string code;
        public List<Translation> translations;

        public Language(string code)
        {
            this.code = code;
        }

        public Language(string code, List<Translation> translations)
        {
            this.code = code;
            this.translations = translations;
        }

        public string GetText(string identifier, bool caseSensitive = false)
        {
            Translation translation;
            if (caseSensitive)
            {
                translation = translations.FirstOrDefault(t => t.identifier == identifier);
            }
            else
            {
                translation = translations.FirstOrDefault(t => t.identifier.ToLower() == identifier.ToLower());
            }

            return translation.translation;
        }
    }
}
using System.Linq;
using System.Text;

namespace SimpleTranslationSystem
{
    public static class TranslationSystem
    {
        private static char columnSeparator = ';';
        private static Language[] languages;
        private static Language language;

        public static void SetLanguage(Language language)
        {
            TranslationSystem.language = language;
        }

        public static void SetLanguage(string code)
        {
            Language language = GetLanguage(code);
            if (language != null)
            {
                TranslationSystem.language = language;
            }
            else
            {
                throw new System.Exception($"No language found with identifier \"{code}\"");
            }
        }

        public static Language GetLanguage()
        {
            return language;
        }

        public static Language GetLanguage(string code)
        {
            return languages.FirstOrDefault(lang => lang.code == code);
        }

        public static string GetText(string identifier, bool caseSensitive = false)
        {
            return GetText(identifier, language, caseSensitive);
        }

        public static string GetText(string identifier, Language language, bool caseSensitive = false)
        {
            return language.GetText(identifier, caseSensitive);
        }

        public static void SetLanguagesFromCSVFile(string pathToCsvFile)
        {
            SetLanguagesFromCSVFile(pathToCsvFile, Encoding.UTF8);
        }

        public static void SetLanguagesFromCSVFile(string pathToCsvFile, System.Text.Encoding encoding)
        {
            Language[] languages =
                TranslationFileReader.GetLanguagesFromCsvFile(pathToCsvFile, columnSeparator, encoding);

            SetLanguages(languages);
        }

        public static void SetLanguages(Language[] languages)
        {
            TranslationSystem.languages = languages;
        }

        public static void SetColumnSeparator(char columnSeparator){
            TranslationSystem.columnSeparator = columnSeparator;
        }

        public static char GetColumnSeparator(){
            return TranslationSystem.columnSeparator;
        }
    }
}

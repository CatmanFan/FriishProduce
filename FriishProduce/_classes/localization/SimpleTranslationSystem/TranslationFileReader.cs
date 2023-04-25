using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SimpleTranslationSystem
{
    public static class TranslationFileReader
    {
        public static Language[] GetLanguagesFromCSVFile(string pathToCsvFile, char columnSeparator)
        {
            return GetLanguagesFromCsvFile(pathToCsvFile, columnSeparator, Encoding.UTF8);
        }

        public static Language[] GetLanguagesFromCsvFile(string pathToCsvFile, char columnSeparator, Encoding encoding)
        {
            if (!File.Exists(pathToCsvFile))
                throw new FileNotFoundException($"No CSV file found at \"{pathToCsvFile}\"");

            string[] lines = File.ReadAllLines(pathToCsvFile, encoding);

            // Header has the language codes (E.g. en, es, fr)
            string header = lines[0];

            // Skip the first row where the translation identifiers are
            string[] languageCodes = header.Split(columnSeparator).Skip(1).ToArray();

            Language[] languages = new Language[languageCodes.Length];

            // Set language codes
            for (int i = 0; i < languageCodes.Length; i++)
            {
                string languageIdentifier = languageCodes[i];
                languages[i] = new Language(languageIdentifier, new List<Translation>());
            }

            // Loop through all of the rows, but skip first row because that's where the language codes are
            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] columns = line.Split(columnSeparator);
                string translationIdentifier = columns[0];

                // Loop through the columns in that line, but skip the first column because that's where the translation identifier is
                for (int j = 1; j < columns.Length; j++)
                {
                    string columnData = columns[j];

                    if (string.IsNullOrWhiteSpace(columnData))
                        continue;

                    Translation translation = new Translation(translationIdentifier, columns[j]);

                    // Remove one from the index because we started at 1 and language codes are offset by one
                    Language columnLanguage = languages[j - 1];
                    columnLanguage.translations.Add(translation);
                }
            }

            return languages;
        }
    }
}

namespace SimpleTranslationSystem
{
    [System.Serializable]
    public struct Translation
    {
        public string identifier;
        public string translation;

        public Translation(string identifier, string translation)
        {
            this.identifier = identifier;
            this.translation = translation;
        }
    }
}
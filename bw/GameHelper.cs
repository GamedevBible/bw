namespace bw
{
    public enum Locales
    {
        English,
        Russian,
        Spain
    }
    public static class GameHelper
    {
        public static string[] GetAlphabet(Locales locale)
        {
            switch (locale)
            {
                case Locales.English:
                    return new[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L",
                                   "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"};
                case Locales.Russian:
                    return new[] { "А", "Б", "В", "Г", "Д", "Е", "Ж", "З", "И", "Й", "К", "Л",
                                   "М", "Н", "О", "П", "Р", "С", "Т", "У", "Ф", "Х", "Ц", "Ч", "Ш", "Щ",
                                   "Ъ", "Ы", "Ь", "Э", "Ю", "Я"};
                case Locales.Spain:
                    return new[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "Á", "É", "Í", "Ó", "Ú", "Ü", "¡",
                                   "M", "N", "Ñ", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"};
                default:
                    return new[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L",
                                   "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"};
            }
        }
    }
}
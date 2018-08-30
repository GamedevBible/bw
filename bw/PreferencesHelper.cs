using Android.Content;
using Android.Preferences;

namespace bw
{
    class PreferencesHelper
    {
        private ISharedPreferences _prefs;
        private ISharedPreferencesEditor _editor;
        private bool _firstStarted;
        private int _selectedLanguage;
        private string _lastVersion;
        private int _guessedWords;
        private int _guessedHardWords;

        public PreferencesHelper()
        {

        }

        public void InitHepler(Context context)
        {
            if (_prefs == null)
                _prefs = PreferenceManager.GetDefaultSharedPreferences(context);

            if (_editor == null)
                _editor = _prefs.Edit();

            _lastVersion = _prefs.GetString("lastVersion", string.Empty);
            _selectedLanguage = _prefs.GetInt("selectedLanguage", 0);
            _firstStarted = _prefs.GetBoolean("firstStarted", true);
            _guessedWords = _prefs.GetInt("guessedWords", 0);
            _guessedHardWords = _prefs.GetInt("guessedHardWords", 0);
        }

        public string GetLastVersion()
        {
            return _lastVersion;
        }

        public void PutLastVersion(Context context, string version)
        {
            if (_prefs == null)
                _prefs = PreferenceManager.GetDefaultSharedPreferences(context);

            if (_editor == null)
                _editor = _prefs.Edit();

            _editor.PutString("lastVersion", version);
            _editor.Commit();
        }

        public int GetSelectedLanguage()
        {
            return _selectedLanguage;
        }

        public void PutSelectedLanguage(Context context, int language)
        {
            if (_prefs == null)
                _prefs = PreferenceManager.GetDefaultSharedPreferences(context);

            if (_editor == null)
                _editor = _prefs.Edit();

            _selectedLanguage = language;
            _editor.PutInt("selectedLanguage", language);
            _editor.Commit();
        }

        public bool GetFirstStarted()
        {
            return _firstStarted;
        }

        public void PutFirstStarted(Context context, bool started)
        {
            if (_prefs == null)
                _prefs = PreferenceManager.GetDefaultSharedPreferences(context);

            if (_editor == null)
                _editor = _prefs.Edit();

            _editor.PutBoolean("firstStarted", started);
            _editor.Commit();
        }

        public int GetGuessedWords()
        {
            return _guessedWords;
        }

        public void PutGuessWord(Context context)
        {
            if (_prefs == null)
                _prefs = PreferenceManager.GetDefaultSharedPreferences(context);

            if (_editor == null)
                _editor = _prefs.Edit();

            _guessedWords = _prefs.GetInt("guessedWords", 0);
            _guessedWords++;
            _editor.PutInt("guessedWords", _guessedWords);
            _editor.Commit();
        }

        public int GetGuessedHardWords()
        {
            return _guessedHardWords;
        }

        public void PutGuessHardWord(Context context)
        {
            if (_prefs == null)
                _prefs = PreferenceManager.GetDefaultSharedPreferences(context);

            if (_editor == null)
                _editor = _prefs.Edit();

            _guessedHardWords = _prefs.GetInt("guessedHardWords", 0);
            _guessedHardWords++;
            _editor.PutInt("guessedWords", _guessedHardWords);
            _editor.Commit();
        }
    }
}
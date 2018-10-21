using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using Java.Util;

namespace bw
{
    public class WordsDatabase : SQLiteConnection
    {
        private const string ru_db = "bwords_ru.db";
        private const string en_db = "bwords_en.db";
        private const string es_db = "bwords_es.db";

        public string DATABASE_NAME = Locale.Default.Language == "ru" 
            ? ru_db
            : Locale.Default.Language == "es"
            ? es_db : en_db;

        private List<words> _words = new List<words>();

        public WordsDatabase(string path) : base($"{path}/{(Locale.Default.Language == "ru" ? ru_db : Locale.Default.Language == "es" ? es_db : en_db)}")
        {
            BeginTransaction();

            _words = Table<words>().ToList();
        }

        public WordInfo GetItem(int category, int level, int[] except)
        {
            var words = new List<words>();
            
            /*if (level > 0)
                if (_words.Where(t => t.category == category && t.level == level - 1).ToList().Count == 0)
                    level = 0;*/

            if (category == 0)
                if (level == 0)
                    words = _words;
                else
                    words = _words.Where(t => t.level == level - 1).ToList();
            else
                if (level == 0)
                    words = _words.Where(t => t.category == category).ToList();
                else
                    words = _words.Where(t => t.category == category && t.level == level - 1).ToList();

            System.Random rand = new System.Random();
            int temp;

            foreach (var id in except)
            {
                if (words.Count > 0)
                    words.RemoveAll(t => t._id == id);
            }

            temp = rand.Next(0, words.Count);

            if (words.Count > 0)
                return new WordInfo { Word = words[temp].word, Id = words[temp]._id };
            else
                return new WordInfo { Word = string.Empty, Id = -1 };
        }

        public class WordInfo
        {
            public string Word { get; set; }
            public int Id { get; set; }
        }
    }
}
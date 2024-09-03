using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quesu.Parser.Films
{
    public class FilmsParser : MonoBehaviour
    {
        [SerializeField] TextAsset[] data;
        public List<MovieResponse> all;

        [Serializable]
        public class MovieResponse
        {
            public int page;
            public List<FilmParserData> results;
        }
        void Start()
        {
            foreach (TextAsset ta in data)
            {
                MovieResponse mr = JsonUtility.FromJson<MovieResponse>(ta.text);
                foreach (FilmParserData fdp in mr.results)
                    fdp.ParseImage();

                all.Add(mr);
            }
            SetTriviaData();
        }
        public void SetTriviaData()
        {

            TriviaData.TriviaContent c = new TriviaData.TriviaContent();
            c.all = new List<ItemData>();
            foreach (MovieResponse m in all)
            {
                foreach (FilmParserData f in m.results)
                {                  
                    ItemData i = new ItemData();
                    i.text = f.title;
                    i.year = int.Parse(f.GetYear());
                    i.image = f.poster_path;
                    c.all.Add(i);
                    Data.Instance.triviaData.SetData(c, 0);
                }
            }
        }
    }

}
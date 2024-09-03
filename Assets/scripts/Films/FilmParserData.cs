using System;
using System.Collections.Generic;

namespace Quesu.Parser.Films
{
    [Serializable]
    public class FilmParserData
    {
       // public bool adult;
       // public string backdrop_path;
        public List<int> genre_ids;
        public int id;
       // public string original_language;
        public string original_title;
        public string overview;
      //  public double popularity;
        public string poster_path;
        public string release_date;
        public string title;
        public bool video;
       // public float vote_average;
        public int vote_count;
        public void ParseImage()
        {
            poster_path = "http://image.tmdb.org/t/p/w300" + poster_path;
        }
        public string GetYear()
        {
            string[] arr = release_date.Split("-");
            if (arr.Length > 1)
                return arr[0];
            else return "";
        }
    }
}


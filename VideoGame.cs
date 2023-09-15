using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _2ndLab
{
    public class VideoGame : IComparable<VideoGame>
    {
        public string name;

        public string platform;

        public int year;

        public string genre;

        public string publisher;

        public string NA_sales;

        public string EU_sales;

        public string JP_sales;

        public string other_sales;

        public string global_sales;

        public static VideoGame FromFile(string line)
        {
            string[] info = line.Split(',');
            VideoGame reader = new VideoGame();
            reader.name = Convert.ToString(info[0]);
            reader.platform = Convert.ToString(info[1]);
            reader.year = Convert.ToInt32(info[2]);
            reader.genre = Convert.ToString(info[3]);
            reader.publisher = Convert.ToString(info[4]);
            reader.NA_sales = Convert.ToString(info[5]);
            reader.EU_sales = Convert.ToString(info[6]);
            reader.JP_sales = Convert.ToString(info[7]);
            reader.other_sales = Convert.ToString(info[8]);
            reader.global_sales = Convert.ToString(info[9]);
            return reader;
        }

        public static string ToString(VideoGame videogame)
        {
            return videogame.name;
        }

        public int CompareTo(VideoGame other)
        {
            if (other == null)
            {
                return 1;
            }
            return string.Compare(global_sales, other.global_sales, StringComparison.OrdinalIgnoreCase);
        }

        public static void PublisherStatistics(List<VideoGame> list, string publisher)
        {
            string publisher2 = publisher;
            int total = list.Count;
            int publisherGameCount = list.Count((VideoGame game) => game.publisher == publisher2);
            double percentage = (double)publisherGameCount / (double)total * 100.0;
            percentage = Math.Round(percentage, 2);
            Console.WriteLine("Out of " + total + " games, " + publisherGameCount + " are made by " + publisher2 + ", which is " + percentage + "%.");
        }

        public static void GenreStatistics(List<VideoGame> list, string genre)
        {
            string genre2 = genre;
            int total = list.Count;
            int genreGameCount = list.Count((VideoGame game) => game.genre == genre2);
            double percentage = (double)genreGameCount / (double)total * 100.0;
            percentage = Math.Round(percentage, 2);
            Console.WriteLine("Out of " + total + " games, " + genreGameCount + " are of the genre " + genre2 + ", which is " + percentage + "%.");
        }

        public static void PublisherData(List<VideoGame> list, string publisher)
        {
            string publisher2 = publisher;
            List<string> pubGames = (from videogame in list
                                     where videogame.publisher.Contains(publisher2)
                                     select videogame.name).ToList();
            pubGames.Sort();
            foreach (string name in pubGames)
            {
                Console.WriteLine(name);
            }
        }

        public static void GenreData(List<VideoGame> list, string genre)
        {
            string genre2 = genre;
            List<string> genreGames = (from videogame in list
                                       where videogame.genre.Contains(genre2)
                                       select videogame.name).ToList();
            genreGames.Sort();
            foreach (string name in genreGames)
            {
                Console.WriteLine(name);
            }
        }

        public static void Scope()
        {
            List<VideoGame> videogame = (from v in File.ReadAllLines("F:\\ETSU_Fall_23\\Server_Side\\Labs\\2ndLab\\videogames.csv").Skip(1)
                                         select FromFile(v)).ToList();
            //using IComparable sorts the list
            videogame.Sort();
            //reverses the order to get the top global_sales first
            videogame.Reverse();

            //converting the list to dictionary
            Dictionary<string, List<string>> gamesByPlatform = (from game in videogame
                                                                
                                                                group game by game.platform).ToDictionary((IGrouping<string, VideoGame> g) => g.Key, (IGrouping<string, VideoGame> g) => g.Select((VideoGame game) => game.name + " : " + game.global_sales).ToList());
            
            //now print the top 5 games from each platform
            foreach(var platform in gamesByPlatform)
            {
                Console.WriteLine($"Platform: {platform.Key}");
                //takes the top 5 games from each platform
                var topGames = platform.Value.Take(5);
                foreach(var game in topGames)
                {
                    Console.WriteLine($"{game}");
                }
                Console.WriteLine("\n");
            }
        }
    }
}

using System.IO;
using System.Collections.Generic;
using System;
using System.Linq;


namespace _2ndLab
{

    public class Program
    {

        static void Main(string[] args)
        {
            //linq statment to convert csv to list
            /* List<VideoGame> videogame = File.ReadAllLines("F:\\ETSU_Fall_23\\Server_Side\\Labs\\LabOne\\LabOne\\videogames.csv")
             .Skip(1).Select(v => VideoGame.FromFile(v)).ToList(); */

            //store store lists of games by platform in dictionary where
            // key: platform
            // value: list of games

            try
            {
                //all my calculations are in this method
                VideoGame.Scope();
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("File not Found!");
            }
            catch(StackOverflowException ex)
            {
                Console.WriteLine("Stack Overflowed");
            }





        }
    }
}

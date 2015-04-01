using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using Dapper;
using System.Text.RegularExpressions;
using System.IO;


namespace YahooNameConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<YahooPlayer> results;
            Regex rgx = new Regex("[^a-zA-Z0-9 -]");

            var connStr = ConfigurationManager.ConnectionStrings["fantasy-baseball"].ConnectionString;
            using (var connection = new SqlConnection(connStr))
            {
                connection.Open();
                const string query = @"SELECT [Name],[PlayerId] FROM [fantasy.baseball].[dbo].[YahooData] ypm";
                results = connection.Query<YahooPlayer>(query).ToList<YahooPlayer>();
            }

            if (results != null) {
                foreach (var player in results) {                    
                    player.ScrubbedName = rgx.Replace(player.Name.Normalize(NormalizationForm.FormKD), ""); ;
                    Console.WriteLine(player);
                }

                try
                {
                    using (var connection = new SqlConnection(connStr))
                    {
                        connection.Open();
                        connection.Execute(@"insert YahooDataScrubbed(Name, PlayerId, ScrubbedName) values (@Name, @PlayerId, @ScrubbedName)", results);
                    }

                }
                catch (Exception ex) {
                    Console.WriteLine("Oh snap!");
                }                
            }
            Console.WriteLine("Done....");
            Console.ReadLine();
        }
    }


    public class YahooPlayer
    {
        public string Name { get; set; }
        public int  PlayerId { get; set; }
        public string ScrubbedName { get; set; }

        public override string ToString()
        {
            return string.Format("{0},{1},{2}", Name, PlayerId, ScrubbedName);
        }
    } 
}

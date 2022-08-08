using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace CSVSearchWebApp.Pages
{
    public class IndexModel : PageModel
    {
        public List<string> myData = IndexModel.ReadCsv();
        public List<Rank> Ranks = new List<Rank>();
        public class Rank
        {
            public int RankId { get; set; }
            public string? TeamName { get; set; }
            public string? MascotName { get; set; }
            public string? DateLastWin { get; set; }
            public string? WinRate { get; set; }
            public string? WinsCount { get; set; }
            public string? LossesCount { get; set; }
            public string? TiesCount { get; set; }
            public string? GamesCount { get; set; }
        }

        public void OnGet()
        {

        }

        public void OnPost()
        {
            var searchterm = Request.Form["SearchTerm"];
            var selected = Request.Form["Columns"];            
            if (searchterm == "")
            {
                ViewData["confirmation"] = $"Search Term is required!!";
            }
            else
            {
                if (selected == "")
                {
                    ViewData["confirmation"] = $"Search Term: {searchterm}, Column: All Columns";
                    GeneralSearch(searchterm);
                }
                else {
                    ViewData["confirmation"] = $"Search Term: {searchterm}, Column: {selected}";
                    ColumnSearch(searchterm, selected);
                }                
            }            
        }

        private void GeneralSearch(string searchTerm) {
            for (int i = 1; i < myData.Count; i++)
            {
                if (myData[i].IndexOf(searchTerm) > 0)
                {
                    createRank(myData[i]);
                }
            }
        }

        private void ColumnSearch(string searchterm, string columnSearch)
        {
            List<Rank> myRanks = new List<Rank>();
            for (int i = 1; i < myData.Count; i++)
            {
                var columns = myData[i].Split(',');
                Rank row = new Rank();
                row.RankId = System.Convert.ToInt32(columns[0]);
                row.TeamName = columns[1];
                row.MascotName = columns[2];
                row.DateLastWin = columns[3];
                row.WinRate = columns[4];
                row.WinsCount = columns[5];
                row.LossesCount = columns[6];
                row.TiesCount = columns[7];
                row.GamesCount = columns[8];
                myRanks.Add(row);
            }
            var myLinqQuery = myRanks.Where(m => { return m.GetType().GetProperty(columnSearch).GetValue(m, null).ToString().StartsWith(searchterm); });
            int resultCount = myLinqQuery.Count();  
            foreach (var row in myLinqQuery) {
                Ranks.Add(row);
            }
        }

        public void createRank(string rank) {
            var columns = rank.Split(',');
            Rank row = new Rank();
            row.RankId = System.Convert.ToInt32(columns[0]);
            row.TeamName = columns[1];
            row.MascotName = columns[2];
            row.DateLastWin = columns[3];
            row.WinRate = columns[4];
            row.WinsCount = columns[5];
            row.LossesCount = columns[6];
            row.TiesCount = columns[7];
            row.GamesCount = columns[8];
            Ranks.Add(row);
        }

        internal static List<string> ReadCsv()
        {
            var strLines = System.IO.File.ReadLines("wwwroot/public/MascotsGamesData.csv");
            List<string> myData = strLines.ToList();
            return myData;
        }

    }

}
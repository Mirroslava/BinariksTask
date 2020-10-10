using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Binariks_task.Models;

namespace Binariks_task.Services
{
    public class HandlerService: IHandler
    {
        Liga items1, items2, items3;
        List<Liga> itemsQwery; 
        public HandlerService()
        {
            using (StreamReader fs = new StreamReader("./Content/en.1.json"))
            {
                string json = fs.ReadToEnd();

                items1 = JsonSerializer.Deserialize<Liga>(json);


            }
            using (StreamReader fs = new StreamReader("./Content/en.2.json"))
            {
                string json = fs.ReadToEnd();

                items2 = JsonSerializer.Deserialize<Liga>(json);


            }
            using (StreamReader fs = new StreamReader("./Content/en.3.json"))
            {
                string json = fs.ReadToEnd();

                items3 = JsonSerializer.Deserialize<Liga>(json);


            }
            itemsQwery = new List<Liga> { items1, items2, items3 };
           

        }

        public List<LigaReiting> GetBestAttakingTeam()
        {
            List<LigaReiting> ligaReitings = GetLigaReiting();
            List<LigaReiting> answer = new List<LigaReiting>();

            foreach (var liga in ligaReitings)
            {
                var teams = liga.Teams.OrderByDescending(x => x.TotalScored).FirstOrDefault();
                var result = new LigaReiting
                {
                    Name = liga.Name,
                    Teams = new List<Team> { teams }

                };
                answer.Add(result);
            }

            return answer;
        }
        public List<LigaReiting> GetLigaReiting()
        {
            List<LigaReiting> selects = new List<LigaReiting>();
            List<LigaReiting> answer = new List<LigaReiting>();

            foreach (var liga in itemsQwery)
            {
                var Team1 = liga.matches.Where(x => x.score != null).GroupBy(b => b.team1).Select(g => new Team
                {
                    Name = g.Key,
                    TotalScored = g.Sum(a => a.score.ft[0]),
                    TotalMissed = g.Sum(a => a.score.ft[1]),
                }).ToList();
                var Team2 = liga.matches.Where(x => x.score != null).GroupBy(b => b.team2).Select(g => new Team
                {
                    Name = g.Key,
                    TotalScored = g.Sum(a => a.score.ft[1]),
                    TotalMissed = g.Sum(a => a.score.ft[0]),
                }).ToList();
                Team1.AddRange(Team2);
                var query1 = new LigaReiting
                {
                    Name = liga.name,
                    Teams = Team1

                };
                selects.Add(query1);
               
            }
            foreach (var liga in selects)
            {
                var teams = liga.Teams.GroupBy(x => x.Name).Select(g => new Team
                {
                    Name = g.Key,
                    TotalScored = g.Sum(a => a.TotalScored),
                    TotalMissed = g.Sum(a => a.TotalMissed),
                }).ToList();
                var result = new LigaReiting
                {
                    Name = liga.Name,
                    Teams = teams

                };
                answer.Add(result);
            }
            return answer;
        }

        public List<LigaReiting> GetBestProtectiveTeam()
        {
           
            List<LigaReiting> ligaReitings = GetLigaReiting();
            List<LigaReiting> answer = new List<LigaReiting>();

            foreach (var liga in ligaReitings)
            {
                var teams = liga.Teams.OrderBy(x => x.TotalMissed).FirstOrDefault();
                var result = new LigaReiting
                {
                    Name = liga.Name,
                    Teams = new List<Team> { teams }

                };
                answer.Add(result);
            }

            return answer;
        }

        public Team GetBestScoredMissedTeam()
        {
            List<Matches> matches = new List<Matches>();

            foreach (var liga in itemsQwery)
            {

                matches.AddRange(liga.matches);

            }

            var Team = matches.Where(x => x.score != null).GroupBy(b => b.team1).Select(g => new Team
            {
                Name = g.Key,
                TotalScored = g.Sum(a => a.score.ft[0]),
                TotalMissed = g.Sum(a => a.score.ft[1]),
            }).ToList();
            var Team2 = matches.Where(x => x.score != null).GroupBy(b => b.team2).Select(g => new Team
            {
                Name = g.Key,
                TotalScored = g.Sum(a => a.score.ft[1]),
                TotalMissed = g.Sum(a => a.score.ft[0]),
            }).ToList();
            Team.AddRange(Team2);

            var query = Team.GroupBy(x => x.Name).Select(g => new Team
            {
                Name = g.Key,
                TotalScored = g.Sum(a => a.TotalScored),
                TotalMissed = g.Sum(a => a.TotalMissed)
            }).OrderByDescending(x => x.TotalScored).ThenBy(x => x.TotalMissed).FirstOrDefault();

            return query;
        }

        public ReitingData GetMostEffectiveData()
        {

            List<Matches> matches = new List<Matches>();

            foreach (var liga in itemsQwery)
            {

                matches.AddRange(liga.matches);

            }
            var query = matches.Where(x => x.score != null).GroupBy(x => x.date).Select(g => new ReitingData
            {
                Data = g.Key,
                TotalScoredGoals = g.Sum(a => (a.score.ft[0] + a.score.ft[1]))
            }).OrderByDescending(x => x.TotalScoredGoals).FirstOrDefault();
            return query;
        }
    }
}

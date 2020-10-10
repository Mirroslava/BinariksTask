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
        
        List<League> leagues = new List<League>(); 
        public HandlerService()
        {
            for(int i = 1; i <= 4; i++)
            {
                string path = "./Content/en." + i + ".json";
                using (StreamReader fs = new StreamReader(path))
                {
                    string json = fs.ReadToEnd();

                   League items = JsonSerializer.Deserialize<League>(json);
                    leagues.Add(items);
                }
            }
          
        }

        public List<LeagueReiting> GetBestAttakingTeam()
        {
            List<LeagueReiting> leagueReitings = GetLeagueReiting();
            List<LeagueReiting> bestTeams = new List<LeagueReiting>();

            foreach (var league in leagueReitings)
            {
                var team = league.Teams.OrderByDescending(x => x.TotalScored).FirstOrDefault();
                var result = new LeagueReiting
                {
                    Name = league.Name,
                    Teams = new List<Team> { team }

                };
                bestTeams.Add(result);
            }

            return bestTeams;
        }
        public List<LeagueReiting> GetLeagueReiting()
        {
            List<LeagueReiting> selects = new List<LeagueReiting>();
            List<LeagueReiting> answer = new List<LeagueReiting>();

            foreach (var league in leagues)
            {
                var Team1 = league.matches.Where(x => x.score != null).GroupBy(b => b.team1).Select(g => new Team
                {
                    Name = g.Key,
                    TotalScored = g.Sum(a => a.score.ft[0]),
                    TotalMissed = g.Sum(a => a.score.ft[1]),
                }).ToList();
                var Team2 = league.matches.Where(x => x.score != null).GroupBy(b => b.team2).Select(g => new Team
                {
                    Name = g.Key,
                    TotalScored = g.Sum(a => a.score.ft[1]),
                    TotalMissed = g.Sum(a => a.score.ft[0]),
                }).ToList();
                Team1.AddRange(Team2);
                var query1 = new LeagueReiting
                {
                    Name = league.name,
                    Teams = Team1

                };
                selects.Add(query1);
               
            }
            foreach (var League in selects)
            {
                var teams = League.Teams.GroupBy(x => x.Name).Select(g => new Team
                {
                    Name = g.Key,
                    TotalScored = g.Sum(a => a.TotalScored),
                    TotalMissed = g.Sum(a => a.TotalMissed),
                }).ToList();
                var result = new LeagueReiting
                {
                    Name = League.Name,
                    Teams = teams

                };
                answer.Add(result);
            }
            return answer;
        }

        public List<LeagueReiting> GetBestProtectiveTeam()
        {
           
            List<LeagueReiting> leagueReitings = GetLeagueReiting();
            List<LeagueReiting> bestTeams = new List<LeagueReiting>();

            foreach (var League in leagueReitings)
            {
                var team = League.Teams.OrderBy(x => x.TotalMissed).FirstOrDefault();
                var result = new LeagueReiting
                {
                    Name = League.Name,
                    Teams = new List<Team> { team }

                };
                bestTeams.Add(result);
            }

            return bestTeams;
        }

        public Team GetBestScoredMissedTeam()
        {
            List<Matches> matches = new List<Matches>();

            foreach (var League in leagues)
            {
                matches.AddRange(League.matches);

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

            foreach (var league in leagues)
            {

                matches.AddRange(league.matches);

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

using System;
using System.Collections.Generic;
using Binariks_task.Models;

namespace Binariks_task.Services
{
    public interface IHandler
    {
        List<LeagueReiting> GetBestAttakingTeam();
        List<LeagueReiting> GetBestProtectiveTeam();
        Team GetBestScoredMissedTeam();
        ReitingData GetMostEffectiveData();
    }
}

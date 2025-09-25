using BracketHubDatabase;
using BracketHubDatabase.Entities;
using BracketHubDatabase.Extensions;
using BracketHubShared.CRUD;
using BracketHubShared.Enums;
using BracketHubShared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BracketHubAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BracketHubController : ControllerBase
    {
        //protected BrackethubContext BrackethubContext { get; set; }
        private readonly IDbContextFactory<BrackethubContext> _contextFactory;

        public BracketHubController(IDbContextFactory<BrackethubContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        [HttpGet(nameof(GetGame))]
        public GameModel? GetGame([FromQuery] string type)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                return context.Games.Where(x => x.Type == type)
                    .Select(x => new GameModel(x.Name, x.Type, x.Description))
                    .FirstOrDefault();
            }
        }
        
        [HttpGet(nameof(GetGames))]
        public IEnumerable<GameModel> GetGames()
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                return context.Games.Select(x => new GameModel(x.Name, x.Type, x.Description)).ToList();
            }
        }
        
        
        [HttpGet(nameof(GetTournament))]
        public AdvancedTournamentModel? GetTournament([FromQuery] int id)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                return context.Tournaments.Where(x => x.Id == id).Select(x => new AdvancedTournamentModel()
                {
                    Id = x.Id,
                    GameType = x.Type,
                    Status = x.Status,
                    Name = x.Name,
                    Banner = x.Banner,
                    Date = x.Date,
                    IsPublic = x.IsPublic,
                    Description = x.Description,
                    Matches = x.Matches != null ? x.Matches.Select(m => new MatchModel()
                    {
                        Id = m.Id,
                        Status = m.Status,
                        Round = m.Round,
                        MatchNumber = m.MatchNumber,
                        Winner = m.Winner,
                        Members = m.Members != null ? m.Members.Select(mem => mem.Id).ToArray() : null,
                        ParentMatchess = m.ParentMatches != null ? m.ParentMatches.Select(pm => pm.Id).ToArray() : null,
                        ChildMatch = m.ChildMatch != null ? m.ChildMatch.Id : null
                    }).ToList() : null
                }).FirstOrDefault();
            }
        }
        
        [HttpGet(nameof(GetTournaments))]
        public IEnumerable<TournamentModel> GetTournaments([FromQuery] string? type = null)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                if (!string.IsNullOrEmpty(type))
                {
                    return context.Tournaments.Where(x => x.Type == type).Select(x => new TournamentModel()
                    {
                        Id = x.Id,
                        GameType = x.Type,
                        Status = x.Status,
                        Name = x.Name,
                        Banner = x.Banner,
                        Date = x.Date,
                        IsPublic = x.IsPublic,
                    }).ToList();
                }
                else
                {
                    return context.Tournaments.Select(x => new TournamentModel()
                    {
                        Id = x.Id,
                        GameType = x.Type,
                        Status = x.Status,
                        Name = x.Name,
                        Banner = x.Banner,
                        Date = x.Date,
                        IsPublic = x.IsPublic,
                    }).Take(10).ToList();
                }
            }
        }

        [HttpPost(nameof(PutTournament))]
        public async Task<TournamentModel?> PutTournament(AdvancedTournamentModel model)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var tournament = context.Tournaments.FirstOrDefault(x => x.Id == model.Id);

                tournament ??= context.Tournaments.Add(new Tournament()
                {
                    Type = model.GameType,
                    Name = model.Name,
                }).Entity;

                tournament.Type = model.GameType;
                tournament.Status = model.Status;
                tournament.Name = model.Name;
                tournament.Banner = model.Banner;
                tournament.Date = model.Date;
                tournament.IsPublic = model.IsPublic;
                tournament.Description = model.Description;

                await context.SaveChangesAsync();

                return tournament.Convert();

            }
        }

        #region Member (Signup & Signin)
        [HttpPut(nameof(MemberSignup))]
        public async Task<MemberModel> MemberSignup(MemberCRUDModel model)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var member = context.Members.FirstOrDefault(x => x.Id == model.Id);

                member ??= context.Members.Add(new Member()
                {
                    Name = model.Name,
                    Nickname = model.Nickname,
                }).Entity;

                member.Name = model.Nickname;
                member.Nickname = model.Nickname;

                await context.SaveChangesAsync();

                return member.Convert();
            }
        }
        [HttpPost(nameof(MemberSignin))]
        public MemberModel? MemberSignin(MemberCRUDModel model)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var member = context.Members.FirstOrDefault(x => x.Id == model.Id);

                return member?.Convert();
            }
        }
        #endregion
    }
}

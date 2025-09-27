using BracketHubDatabase;
using BracketHubDatabase.Entities;
using BracketHubDatabase.Extensions;
using BracketHubShared.CRUD;
using BracketHubShared.Extensions;
using BracketHubShared.Models;
using BracketHubShared.Statics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

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

#region Game
        [HttpGet(nameof(GetGame))]
        public async Task<GameModel?> GetGame([FromQuery] string type, CancellationToken cancellationToken = default)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                return await context.Games.Where(x => x.Type == type)
                    .Select(x => new GameModel(x.Name, x.Type, x.Description))
                    .FirstOrDefaultAsync(cancellationToken);
            }
        }
        
        [HttpGet(nameof(GetGames))]
        public async Task<IEnumerable<GameModel>> GetGames(CancellationToken cancellationToken = default)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                return await context.Games.Select(x => new GameModel(x.Name, x.Type, x.Description)).ToListAsync(cancellationToken);
            }
        }
#endregion

#region Tournament
        [HttpGet(nameof(GetTournament))]
        public async Task<AdvancedTournamentModel?> GetTournament([FromQuery] int id, CancellationToken cancellationToken = default)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                return await context.Tournaments.Where(x => x.Id == id).Select(x => new AdvancedTournamentModel()
                {
                    Id = x.Id,
                    GameType = x.Type,
                    Status = x.Status,
                    Name = x.Name,
                    Banner = ImageStatics.GetCustomOrExistingBanner(x.Banner),
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
                        ParentMatches = m.ParentMatches != null ? m.ParentMatches.Select(pm => pm.Id).ToArray() : null,
                        ChildMatch = m.ChildMatch != null ? m.ChildMatch.Id : null
                    }).ToList() : null,
                    Members = x.Members != null ? x.Members.Select(m => new MemberModel(m.Id, x.Name, m.Nickname)).ToList() : null,
                }).FirstOrDefaultAsync(cancellationToken);
            }
        }
        
        [HttpGet(nameof(GetTournaments))]
        public async Task<IEnumerable<TournamentModel>> GetTournaments([FromQuery] string? type = null, CancellationToken cancellationToken = default)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                if (!string.IsNullOrEmpty(type))
                {
                    return await context.Tournaments.Where(x => x.Type == type).Select(x => new TournamentModel()
                    {
                        Id = x.Id,
                        GameType = x.Type,
                        Status = x.Status,
                        Name = x.Name,
                        Banner = ImageStatics.GetCustomOrExistingBanner(x.Banner),
                        Date = x.Date,
                        IsPublic = x.IsPublic,
                    }).ToListAsync(cancellationToken);
                }
                else
                {
                    return await context.Tournaments.Select(x => new TournamentModel()
                    {
                        Id = x.Id,
                        GameType = x.Type,
                        Status = x.Status,
                        Name = x.Name,
                        Banner = ImageStatics.GetCustomOrExistingBanner(x.Banner),
                        Date = x.Date,
                        IsPublic = x.IsPublic,
                    }).Take(10).ToListAsync(cancellationToken);
                }
            }
        }

        [HttpPut(nameof(PutTournament))]
        public async Task<ActionResult<TournamentModel?>> PutTournament([FromBody] AdvancedTournamentModel model, CancellationToken cancellationToken = default)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var tournament = await context.Tournaments.FirstOrDefaultAsync(x => x.Id == model.Id, cancellationToken);

                tournament ??= context.Tournaments.Add(new Tournament()
                {
                    Type = model.GameType,
                    Name = model.Name,
                }).Entity;

                tournament.Type = string.IsNullOrEmpty(model.GameType) ? "OTHER" : model.GameType;
                tournament.Status = model.Status;
                tournament.Name = model.Name;
                tournament.Banner = string.IsNullOrEmpty(model.Banner) ? null : model.Banner;
                tournament.Date = model.Date;
                tournament.IsPublic = model.IsPublic;
                tournament.Description = string.IsNullOrEmpty(model.Description) ? null : model.Description;

                await context.SaveChangesAsync(cancellationToken);

                return tournament.Convert();

            }
        }

        [HttpPut(nameof(AddTournamentMember))]
        public async Task<ActionResult> AddTournamentMember([FromBody] TournamentMemberLink model, CancellationToken cancellationToken = default)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var tournament = await context.Tournaments.Include(x => x.Members).FirstOrDefaultAsync(x => x.Id == model.TournamentId, cancellationToken);
                var member = await context.Members.FirstOrDefaultAsync(x => x.Id == model.MemberId, cancellationToken);

                if (tournament == null || member == null)
                    return NotFound("Member or Tournament was not found!");

                tournament.Members ??= new();

                if (tournament.Members.Contains(member))
                    return Conflict("Member is already a part of this Tournament!");

                tournament.Members.Add(member);

                await context.SaveChangesAsync(cancellationToken);

                return Ok();
            }
        }
        #endregion


        [HttpPut(nameof(PutMatch))]
        public async Task<ActionResult> PutMatch([FromBody] MatchModel model, CancellationToken cancellationToken = default)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                // Include is very slow when a large amount of data needs to be fetched.
                var match = await context.Matches
                    .Include(x => x.Members)
                    .Include(x => x.ParentMatches)
                    .Include(x => x.ChildMatch)
                    .Include(x => x.Tournament)
                    .FirstOrDefaultAsync(x => x.Id == model.Id, cancellationToken);

                match ??= context.Matches.Add(new Match()).Entity;

                match.Status = model.Status;
                match.Round = model.Round;
                match.MatchNumber = model.MatchNumber;
                match.Winner = model.Winner;

                // Members - Bad way to set list, but for this scenario it should be fine
                if (model.Members != null && model.Members.Length > 0)
                    match.Members = await context.Members.Where(x => model.Members.Contains(x.Id)).ToListAsync(cancellationToken);
                else
                    match.Members = null;
                
                // ParentMatches - Bad way to set list, but for this scenario it should be fine
                if (model.ParentMatches != null && model.ParentMatches.Length > 0)
                    match.ParentMatches = await context.Matches.Where(x => model.ParentMatches.Contains(x.Id)).ToListAsync(cancellationToken);
                else
                    match.ParentMatches = null;

                // Child match
                if (model.ChildMatch != null && (!match.ChildMatch.IsNotNull() || match.ChildMatch.Id != model.ChildMatch))
                    match.ChildMatch = await context.Matches.FirstOrDefaultAsync(x => x.Id == model.ChildMatch, cancellationToken);
                else
                    match.ChildMatch = null;
                
                // Tournament
                if (model.Tournament != null && (!match.Tournament.IsNotNull() || match.Tournament.Id != model.Tournament))
                    match.Tournament = await context.Tournaments.FirstOrDefaultAsync(x => x.Id == model.Tournament, cancellationToken);
                else
                    match.Tournament = null;

                await context.SaveChangesAsync(cancellationToken);

                return Ok();
            }
        }


        #region Member (Signup & Signin)
        [HttpPut(nameof(MemberSignup))]
        public async Task<MemberModel> MemberSignup([FromBody] MemberCreateUpdateModel model, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.Nickname))
                throw new FormatException("Incorrect model data");

            using (var context = _contextFactory.CreateDbContext())
            {
                // To check if exists.
                var member = await context.Members.FirstOrDefaultAsync(x => x.Id == model.Id, cancellationToken);

                member ??= context.Members.Add(new Member()
                {
                    Name = model.Name,
                    Nickname = model.Nickname,
                }).Entity;

                member.Name = model.Name;
                member.Nickname = model.Nickname;

                await context.SaveChangesAsync(cancellationToken);

                return member.Convert();
            }
        }
        [HttpPost(nameof(MemberSignin))]
        public async Task<MemberModel?> MemberSignin([FromBody] MemberReadModel model, CancellationToken cancellationToken = default)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var member = await context.Members.FirstOrDefaultAsync(x => x.Id == model.Id, cancellationToken);

                return member?.Convert();
            }
        }
#endregion
    }
}

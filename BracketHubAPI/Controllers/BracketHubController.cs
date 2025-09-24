using BracketHubDatabase;
using BracketHubShared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

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
        public GameModel? GetGame(string type)
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
    }
}

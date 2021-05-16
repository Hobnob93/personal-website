using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.Shared.Enums;
using PersonalWebsite.Shared.Interfaces;
using PersonalWebsite.Shared.Models;

namespace PersonalWebsite.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatisticItemsController : ControllerBase
    {
        private readonly IBoardStatItemFactory statItemFactory;

        public StatisticItemsController(IBoardStatItemFactory statItemFactory)
        {
            this.statItemFactory = statItemFactory;
        }


        public IEnumerable<BoardStatItem> Get(BoardType type)
        {
            return statItemFactory.GetStatItemsForBoardType(type);
        }
    }
}
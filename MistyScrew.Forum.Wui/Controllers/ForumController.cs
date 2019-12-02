using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MistyScrew.Forum.Wui.Controllers
{
    [ApiController]
    [Route("api/forum")]
    public class ForumController : ControllerBase
    {

        public ForumController(ILogger<ForumController> logger)
        {
            _logger = logger;
        }

        private readonly ILogger<ForumController> _logger;

        [HttpGet("areas")]
        public async Task<IEnumerable<Area>> Areas()
        {
            return await ForumClient.Areas();
        }

        [HttpGet("board/{boardName}/threads")]
        public async Task<IEnumerable<Thread>> BoardThreads(string boardName)
        {
            return await ForumClient.Threads(boardName);
        }

    }
}

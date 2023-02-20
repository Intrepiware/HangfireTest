using Hangfire;
using HangfireTest.Services;
using System.Web.Http;

namespace HangfireTest.Controllers
{
    [Route("Process")]
    public class ProcessController : ApiController
    {
        public IHttpActionResult Post()
        {
            var service = new ProcessService();
            var jobId = BackgroundJob.Enqueue(() => service.SlowRunningProcess());
            return Json(new { jobId });
        }
    }
}

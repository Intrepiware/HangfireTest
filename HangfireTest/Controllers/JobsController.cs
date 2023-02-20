using Hangfire;
using System.Linq;
using System.Web.Http;

namespace HangfireTest.Controllers
{
    [Route("Jobs")]
    public class JobsController : ApiController
    {
        public IHttpActionResult Get()
        {
            var jobsApi = JobStorage.Current.GetMonitoringApi();
            return Json(new
            {
                failedJobs = jobsApi.FailedJobs(0, int.MaxValue),
                enqueuedJobs = jobsApi.EnqueuedJobs("default", 0, int.MaxValue),
                successfulJobs = jobsApi.SucceededJobs(0, int.MaxValue)
            });
            
        }

        [Route("Jobs/{id}")]
        public IHttpActionResult Get(string id)
        {
            var jobsApi = JobStorage.Current.GetMonitoringApi();
            return Json(jobsApi.JobDetails(id));
        }

        [HttpPost]
        [Route("Jobs/{id}/Restart")]
        public IHttpActionResult Restart(string id)
        {
            var result = BackgroundJob.Requeue(id);
            return Json(new { restarted = result });
        }
    }
}

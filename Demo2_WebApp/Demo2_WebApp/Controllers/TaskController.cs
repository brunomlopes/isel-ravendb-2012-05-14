using System.Web.Mvc;
using Demo2_WebApp.Infrastructure;
using Raven.Abstractions.Data;

namespace Demo2_WebApp.Controllers
{
    public class TaskController : RavenDbController
    {
        [HttpPost]
        public ActionResult MarkAsDone(string id)
        {
            RavenSession.Advanced.DatabaseCommands.Patch(id, new []
                                                                 {
                                                                     new PatchRequest()
                                                                         {
                                                                             Type = PatchCommandType.Set,
                                                                             Name = "Done",
                                                                             Value = true
                                                                         }, 
                                                                 });
            return Json(new {status="ok"});
        }

        [HttpPost]
        public ActionResult MarkAsNotDone(string id)
        {
            RavenSession.Advanced.DatabaseCommands.Patch(id, new []
                                                                 {
                                                                     new PatchRequest()
                                                                         {
                                                                             Type = PatchCommandType.Set,
                                                                             Name = "Done",
                                                                             Value = false
                                                                         }, 
                                                                 });
            return Json(new {status="ok"});
        }
    }
}
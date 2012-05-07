using System.Linq;
using Demo2_WebApp.Models;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace Demo2_WebApp.Indexes
{
    public class Tasks : AbstractIndexCreationTask<Project, Task>
    {
        public Tasks()
        {
            Map = projects => from project in projects
                              from activity in project.Activities
                              from task in activity.Tasks
                              select new
                                         {
                                             task.Done,
                                             task.Owner,
                                             task.Name,
                                         };

            Reduce = tasks => from task in tasks
                              group task by new {task.Name, task.Done, task.Owner}
                              into task
                              select new
                                         {
                                             task.First().Done,
                                             task.First().Owner,
                                             task.First().Name
                                         };

            Index(t => t.Name, FieldIndexing.Analyzed);
        }
    }
}
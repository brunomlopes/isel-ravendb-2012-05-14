using System.Linq;
using Demo2_WebApp.Models;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace Demo2_WebApp.Indexes
{
    public class Tasks : AbstractIndexCreationTask<Task>
    {
        public Tasks()
        {
            Map = tasks => from task in tasks
                              select new
                                         {
                                             task.Name,
                                         };

            Index(t => t.Name, FieldIndexing.Analyzed);
        }
    }
}
using Demo2_WebApp.Models;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;
using System.Linq;

namespace Demo2_WebApp.Indexes
{
    public class TasksCount_ForPerson : AbstractIndexCreationTask<Task, TasksCount_ForPerson.Result>
    {
        public class Result
        {
            public string OwnerId { get; set; }
            public int Count { get; set; }
        }

        public TasksCount_ForPerson()
        {
            Map = tasks =>
                  from task in tasks
                  where !string.IsNullOrWhiteSpace(task.OwnerId) 
                  select
                      new
                      {
                          task.OwnerId,
                          Count = 1
                      };

            Reduce = 
                results => results
                // não incluir dados sujos
                    .Where(r => !string.IsNullOrWhiteSpace(r.OwnerId))
                // Agrupamos pelo Id do owner, por ser o que identifica univocamente o user
                    .GroupBy(g => g.OwnerId)
                    .Select(p => new
                                     {
                // E aqui selecionamos o primeiro Owner (são todos iguais)
                                         OwnerId = p.Key, 
                // E contamos o número de tarefas
                                         Count = p.Sum(result => result.Count)
                                     });
            // TODO:sort by order name
            //Sort(p => p.OwnerName, SortOptions.String);
        }
    }
}

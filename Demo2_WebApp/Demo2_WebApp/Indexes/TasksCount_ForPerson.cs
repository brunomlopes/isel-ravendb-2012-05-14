using Demo2_WebApp.Models;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;
using System.Linq;

namespace Demo2_WebApp.Indexes
{
    public class TasksCount_ForPerson : AbstractIndexCreationTask<Project, TasksCount_ForPerson.Result>
    {
        public class Result
        {
            public Person Owner { get; set; }
            public string OwnerName { get; set; }
            public int Count { get; set; }
        }

        public TasksCount_ForPerson()
        {
            Map = projects =>
                  from project in projects
                  from task in project.Activities.SelectMany(a => a.Tasks)
                  select
                      new
                      {
                          task.Owner,
                          OwnerName = task.Owner.Name,
                          Count = 1
                      };

            Reduce = 
                results => results
                    // Agrupamos pelo Id do owner, por ser o que identifica univocamente o user
                    .GroupBy(g => g.Owner.Id)
                    .Select(p => new
                                     {
                // E aqui selecionamos o primeiro Owner (são todos iguais)
                                         Owner = p.Select(r => r.Owner).First(), 
                                         OwnerName = p.Select(r => r.OwnerName).First(), 
                // E contamos o número de tarefas
                                         Count = p.Sum(result => result.Count)
                                     });

            Sort(p => p.OwnerName, SortOptions.String);
        }
    }
}

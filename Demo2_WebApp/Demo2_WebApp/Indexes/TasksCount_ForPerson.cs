using Demo2_WebApp.Models;
using Raven.Client.Indexes;
using System.Linq;

namespace Demo2_WebApp.Indexes
{
    public class TasksCount_ForPerson : AbstractIndexCreationTask<Project, TasksCount_ForPerson.Result>
    {
        public class Result
        {
            public Person Owner { get; set; }
            public int Count { get; set; }
        }

        public TasksCount_ForPerson()
        {
            Map = projects =>
                projects
                .SelectMany(p => p.Activities)
                .SelectMany(a => a.Tasks)
                    .Select(task =>
                            new
                                {
                                    task.Owner,
                                    Count = 1
                                }
                    );

            Reduce = 
                results => results
                    // Agrupamos pelo Id do owner, por ser o que identifica univocamente o user
                    .GroupBy(g => g.Owner.Id)
                    .Select(p => new
                                     {
                // E aqui selecionamos o primeiro Owner (são todos iguais)
                                         Owner = p.Select(r => r.Owner).First(), 
                // E contamos o número de tarefas
                                         Count = p.Sum(result => result.Count)
                                     });



        }
    }
}
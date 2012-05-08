using System.Linq;
using Demo2_WebApp.Models;
using Raven.Client.Indexes;

namespace Demo2_WebApp.Indexes
{
    public class ActivitiesCount_PerProject : AbstractMultiMapIndexCreationTask<ActivitiesCount_PerProject.Result>
    {
         public class Result
         {
             public string ProjectName { get; set; }
             public string ProjectId { get; set; }
             public int NumberOfActivities { get; set; }
         }

        public ActivitiesCount_PerProject()
        {
            AddMap<Project>(projects => from project in projects
                                            select new
                                                       {
                                                           ProjectId = project.Id,
                                                           ProjectName = project.Name,
                                                           NumberOfActivities = 0,
                                                       }
                                            );

            AddMap<Activity>(activities => from activity in activities
                                            select new
                                                       {
                                                           ProjectId = activity.ProjectId,
                                                           ProjectName = (string)null,
                                                           NumberOfActivities = 1,
                                                       }
                                            );

            Reduce = results => from result in results
                                group result by result.ProjectId
                                into grouping
                                select new
                                           {
                                               ProjectId = grouping.Key,
                                               ProjectName = grouping.Select(s => s.ProjectName).FirstOrDefault(s => s != null),
                                               NumberOfActivities = grouping.Sum(s => s.NumberOfActivities)
                                           };
        }
    }
}
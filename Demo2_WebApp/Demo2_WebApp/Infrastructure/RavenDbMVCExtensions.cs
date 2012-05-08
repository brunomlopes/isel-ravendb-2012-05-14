using System.Linq;
using Raven.Abstractions.Data;
using Raven.Client;
using Raven.Client.Indexes;

namespace Demo2_WebApp.Infrastructure
{
    public static class RavenDbMVCExtensions
    {
         public static int IntFromId(this string id)
         {
             return int.Parse(id.Split('/')[1]);
         }
         public static void Clear<T>(this IDocumentSession session)
         {
             var indexName = "Demo/Clear/" + typeof (T).Name;
             session.Advanced.DocumentStore.DatabaseCommands.PutIndex(indexName, new IndexDefinitionBuilder<T>
             {
                 Map = documents => documents.Select(entity => new { marker = 1 })
             }, true);
             session.Advanced.LuceneQuery<T>(indexName).Where("marker:0").WaitForNonStaleResults().ToList();

             session.Advanced.DatabaseCommands.DeleteByIndex(indexName, new IndexQuery());

             session.Advanced.DatabaseCommands.DeleteIndex(indexName);
         }
    }
}
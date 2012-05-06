namespace Demo2_WebApp.Infrastructure
{
    public static class RavenDbMVCExtensions
    {
         public static int IntFromId(this string id)
         {
             return int.Parse(id.Split('/')[1]);
         }
    }
}
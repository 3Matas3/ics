using System.Linq;

namespace SimpleTeamApp.App.UWPHelper.Validation
{
    internal static class Strings
    {
        internal static string Agregate(string delimiter, params object[] objects)
        {
            if (objects == null) return "";
            objects = objects.Where(x => x != null).ToArray();
            objects = objects.Where(x => !(x is string) || !string.IsNullOrWhiteSpace(x as string)).ToArray();
            if (objects.Length == 0) return "";

            return objects.Select(x => x.ToString()).Aggregate((c, v) => string.Format("{0}{1}{2}", c, delimiter, v));
        }
    }
}

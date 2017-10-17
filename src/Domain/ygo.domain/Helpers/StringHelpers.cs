using System.IO;

namespace ygo.domain.Helpers
{
    public static class StringHelpers
    {
        public static string MakeValidFileName(this string name)
        {
            return string.Concat(name.Split(Path.GetInvalidFileNameChars()));
        }
    }
}
using System.Collections.Generic;
using System.IO;

namespace Bitdiff.Utils
{
    public class FileUtilities
    {
        public static IEnumerable<string> EnumerateDirectoryRecursive(string root)
        {
            foreach (var file in Directory.GetFiles(root))
                yield return file;
            foreach (var subdir in Directory.GetDirectories(root))
                foreach (var file in EnumerateDirectoryRecursive(subdir))
                    yield return file;
        }
    }
}

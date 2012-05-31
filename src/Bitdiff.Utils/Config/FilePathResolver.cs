using System;
using System.IO;

namespace Bitdiff.Utils.Config
{
    public class FilePathResolver : IFilePathResolver
    {
        private readonly IHttpContextFactory _httpContextFactory;
        private const string RelativePathIndicator = "~";

        public FilePathResolver(IHttpContextFactory httpContextFactory)
        {
            _httpContextFactory = httpContextFactory;
        }

        public string GetPath(string path)
        {
            if (path.StartsWith(RelativePathIndicator))
                return _httpContextFactory.GetContext().Server.MapPath(path);

            return Path.IsPathRooted(path)
                       ? path
                       : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
        }
    }
}
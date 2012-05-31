using System;

namespace Bitdiff.Utils
{
    public interface ICookie
    {
        string Get(string name);
        void Set(string name, string value);
        void Set(string name, string value, DateTime expires);
        void Delete(string name);
    }
}
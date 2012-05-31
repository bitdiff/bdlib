using System;

namespace Bitdiff.Utils
{
    public interface IClock
    {
        DateTime GetNow();
        DateTime GetUtcNow();
    }
}
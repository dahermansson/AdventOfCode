using System;

namespace AoC.Utils
{
    public interface IDay
    {
        int Star1();
        int Star2();
        string Output { get; }
    }
}
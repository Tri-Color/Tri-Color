using System;
using System.Linq;

namespace Git_Analysis.Parsers
{
    public interface Parser
    {
        object parse(String str);
    }
}
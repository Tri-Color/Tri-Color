using System;
using System.Linq;

namespace Git_Analysis.Parsers
{
    public interface Parser
    {
        void parse(String str);
    }
}
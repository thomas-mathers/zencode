using ZenCode.Parser.Grammar;

namespace ZenCode.Parser.Parsers;

public interface IParser
{
    Program Parse(string input);
}
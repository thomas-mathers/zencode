using ZenCode.Grammar;

namespace ZenCode.Parser.Abstractions;

public interface IParser
{
    Program Parse(string input);
}
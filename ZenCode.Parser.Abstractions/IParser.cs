using ZenCode.Parser.Model.Grammar;

namespace ZenCode.Parser.Abstractions;

public interface IParser
{
    Program Parse(string program);
}
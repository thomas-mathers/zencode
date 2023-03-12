using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Model.Grammar;

namespace ZenCode.Parser.Abstractions;

public interface IParameterListParser
{
    ParameterList ParseParameterList(IParser parser, ITokenStream tokenStream);
}
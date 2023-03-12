using Xunit;
using ZenCode.Lexer;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.Parser.Tests.Integration.TestData;

namespace ZenCode.Parser.Tests.Integration.Expressions
{
    public class ExpressionParserTests
    {
        private readonly IParser _sut;

        public ExpressionParserTests()
        {
            _sut = new ParserFactory().Create();
        }

        [Fact]
        public void ParseExpression_AnonFuncNoParamsNoStatements_ReturnsAnonFuncDeclarationExpression()
        {
            // Arrange
            var tokenStream = new TokenStream(new[]
            {
                new Token(TokenType.Function),
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.RightParenthesis),
                new Token(TokenType.RightArrow),
                new Token(TokenType.Void),
                new Token(TokenType.LeftBrace),
                new Token(TokenType.RightBrace)
            });

            var expected = new AnonymousFunctionDeclarationExpression(new VoidType(), new ParameterList(), new Scope());

            // Act
            var actual = _sut.ParseExpression(tokenStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ParseExpression_AnonFuncOneParamOneStatement_ReturnsAnonFuncDeclarationExpression()
        {
            // Arrange
            var tokenStream = new TokenStream(new[]
            {
                new Token(TokenType.Function),
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.Identifier),
                new Token(TokenType.Colon),
                new Token(TokenType.Integer),
                new Token(TokenType.RightParenthesis),
                new Token(TokenType.RightArrow),
                new Token(TokenType.Integer),
                new Token(TokenType.LeftBrace),
                new Token(TokenType.Return),
                new Token(TokenType.Identifier),
                new Token(TokenType.Semicolon),
                new Token(TokenType.RightBrace)
            });

            var returnType = new IntegerType();

            var parameterList = new ParameterList
            {
                Parameters = new[]
                {
                    new Parameter(new Token(TokenType.Identifier), new IntegerType())
                }
            };

            var scope = new Scope
            {
                Statements = new[]
                {
                    new ReturnStatement
                    {
                        Expression = new VariableReferenceExpression(new Token(TokenType.Identifier))
                    }
                }
            };

            var expected = new AnonymousFunctionDeclarationExpression(returnType, parameterList, scope);

            // Act
            var actual = _sut.ParseExpression(tokenStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ParseExpression_AnonFuncMultipleParamMultipleStatement_ReturnsAnonFuncDeclarationExpression()
        {
            // Arrange
            var tokenStream = new TokenStream(new[]
            {
                new Token(TokenType.Function),
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.Identifier),
                new Token(TokenType.Colon),
                new Token(TokenType.Boolean),
                new Token(TokenType.Comma),
                new Token(TokenType.Identifier),
                new Token(TokenType.Colon),
                new Token(TokenType.Integer),
                new Token(TokenType.Comma),
                new Token(TokenType.Identifier),
                new Token(TokenType.Colon),
                new Token(TokenType.Float),
                new Token(TokenType.Comma),
                new Token(TokenType.Identifier),
                new Token(TokenType.Colon),
                new Token(TokenType.String),
                new Token(TokenType.RightParenthesis),
                new Token(TokenType.RightArrow),
                new Token(TokenType.Float),
                new Token(TokenType.LeftBrace),
                new Token(TokenType.Var),
                new Token(TokenType.Identifier),
                new Token(TokenType.Assignment),
                new Token(TokenType.Identifier),
                new Token(TokenType.Return),
                new Token(TokenType.Identifier),
                new Token(TokenType.Semicolon),
                new Token(TokenType.RightBrace)
            });

            var returnType = new FloatType();

            var parameterList = new ParameterList
            {
                Parameters = new[]
                {
                    new Parameter(new Token(TokenType.Identifier), new BooleanType()),
                    new Parameter(new Token(TokenType.Identifier), new IntegerType()),
                    new Parameter(new Token(TokenType.Identifier), new FloatType()),
                    new Parameter(new Token(TokenType.Identifier), new StringType())
                }
            };

            var scope = new Scope
            {
                Statements = new Statement[]
                {
                    new VariableDeclarationStatement(new Token(TokenType.Identifier),
                        new VariableReferenceExpression(new Token(TokenType.Identifier))),
                    new ReturnStatement
                    {
                        Expression = new VariableReferenceExpression(new Token(TokenType.Identifier))
                    }
                }
            };

            var expected = new AnonymousFunctionDeclarationExpression(returnType, parameterList, scope);

            // Act
            var actual = _sut.ParseExpression(tokenStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ParseExpression_NewBooleanArray_ReturnsNewExpression()
        {
            // Arrange
            var tokenStream = new TokenStream(new[]
            {
                new Token(TokenType.New),
                new Token(TokenType.Boolean),
                new Token(TokenType.LeftBracket),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightBracket)
            });

            var expressionList = new ExpressionList
            {
                Expressions = new[]
                {
                    new LiteralExpression(new Token(TokenType.IntegerLiteral))
                }
            };

            var expected = new NewExpression(new BooleanType(), expressionList);

            // Act
            var actual = _sut.ParseExpression(tokenStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ParseExpression_NewIntegerArray_ReturnsNewExpression()
        {
            // Arrange
            var tokenStream = new TokenStream(new[]
            {
                new Token(TokenType.New),
                new Token(TokenType.Integer),
                new Token(TokenType.LeftBracket),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightBracket)
            });

            var expressionList = new ExpressionList
            {
                Expressions = new[]
                {
                    new LiteralExpression(new Token(TokenType.IntegerLiteral))
                }
            };

            var expected = new NewExpression(new IntegerType(), expressionList);

            // Act
            var actual = _sut.ParseExpression(tokenStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ParseExpression_NewFloatArray_ReturnsNewExpression()
        {
            // Arrange
            var tokenStream = new TokenStream(new[]
            {
                new Token(TokenType.New),
                new Token(TokenType.Float),
                new Token(TokenType.LeftBracket),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightBracket)
            });

            var expressionList = new ExpressionList
            {
                Expressions = new[]
                {
                    new LiteralExpression(new Token(TokenType.IntegerLiteral))
                }
            };

            var expected = new NewExpression(new FloatType(), expressionList);

            // Act
            var actual = _sut.ParseExpression(tokenStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ParseExpression_NewStringArray_ReturnsNewExpression()
        {
            // Arrange
            var tokenStream = new TokenStream(new[]
            {
                new Token(TokenType.New),
                new Token(TokenType.String),
                new Token(TokenType.LeftBracket),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightBracket)
            });

            var expressionList = new ExpressionList
            {
                Expressions = new[]
                {
                    new LiteralExpression(new Token(TokenType.IntegerLiteral))
                }
            };

            var expected = new NewExpression(new StringType(), expressionList);

            // Act
            var actual = _sut.ParseExpression(tokenStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ParseExpression_NewBooleanJaggedArray_ReturnsNewExpression()
        {
            // Arrange
            var tokenStream = new TokenStream(new[]
            {
                new Token(TokenType.New),
                new Token(TokenType.Boolean),
                new Token(TokenType.LeftBracket),
                new Token(TokenType.RightBracket),
                new Token(TokenType.LeftBracket),
                new Token(TokenType.RightBracket),
                new Token(TokenType.LeftBracket),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightBracket)
            });

            var expressionList = new ExpressionList
            {
                Expressions = new[]
                {
                    new LiteralExpression(new Token(TokenType.IntegerLiteral))
                }
            };

            var expected = new NewExpression(new ArrayType(new ArrayType(new BooleanType())), expressionList);

            // Act
            var actual = _sut.ParseExpression(tokenStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ParseExpression_NewIntegerJaggedArray_ReturnsNewExpression()
        {
            // Arrange
            var tokenStream = new TokenStream(new[]
            {
                new Token(TokenType.New),
                new Token(TokenType.Integer),
                new Token(TokenType.LeftBracket),
                new Token(TokenType.RightBracket),
                new Token(TokenType.LeftBracket),
                new Token(TokenType.RightBracket),
                new Token(TokenType.LeftBracket),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightBracket)
            });

            var expressionList = new ExpressionList
            {
                Expressions = new[]
                {
                    new LiteralExpression(new Token(TokenType.IntegerLiteral))
                }
            };

            var expected = new NewExpression(new ArrayType(new ArrayType(new IntegerType())), expressionList);

            // Act
            var actual = _sut.ParseExpression(tokenStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ParseExpression_NewFloatJaggedArray_ReturnsNewExpression()
        {
            // Arrange
            var tokenStream = new TokenStream(new[]
            {
                new Token(TokenType.New),
                new Token(TokenType.Float),
                new Token(TokenType.LeftBracket),
                new Token(TokenType.RightBracket),
                new Token(TokenType.LeftBracket),
                new Token(TokenType.RightBracket),
                new Token(TokenType.LeftBracket),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightBracket)
            });

            var expressionList = new ExpressionList
            {
                Expressions = new[]
                {
                    new LiteralExpression(new Token(TokenType.IntegerLiteral))
                }
            };

            var expected = new NewExpression(new ArrayType(new ArrayType(new FloatType())), expressionList);

            // Act
            var actual = _sut.ParseExpression(tokenStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ParseExpression_NewStringJaggedArray_ReturnsNewExpression()
        {
            // Arrange
            var tokenStream = new TokenStream(new[]
            {
                new Token(TokenType.New),
                new Token(TokenType.String),
                new Token(TokenType.LeftBracket),
                new Token(TokenType.RightBracket),
                new Token(TokenType.LeftBracket),
                new Token(TokenType.RightBracket),
                new Token(TokenType.LeftBracket),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightBracket)
            });

            var expressionList = new ExpressionList
            {
                Expressions = new[]
                {
                    new LiteralExpression(new Token(TokenType.IntegerLiteral))
                }
            };

            var expected = new NewExpression(new ArrayType(new ArrayType(new StringType())), expressionList);

            // Act
            var actual = _sut.ParseExpression(tokenStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [ClassData(typeof(LowPrecedenceOperatorHighPrecedenceOperatorPairs))]
        public void ParseExpression_LoPrecedenceOpThenHiPrecedenceOp_ReturnsBinaryExpressionWithLastTwoTermsGroupedFirst(
            TokenType loOp,
            TokenType hiOp)
        {
            // Arrange
            var tokenStream = new TokenStream(new[]
            {
                new Token(TokenType.IntegerLiteral),
                new Token(loOp),
                new Token(TokenType.IntegerLiteral),
                new Token(hiOp),
                new Token(TokenType.IntegerLiteral)
            });

            var expected = new BinaryExpression(
                new LiteralExpression(new Token(TokenType.IntegerLiteral)),
                new Token(loOp),
                new BinaryExpression(
                    new LiteralExpression(new Token(TokenType.IntegerLiteral)),
                    new Token(hiOp),
                    new LiteralExpression(new Token(TokenType.IntegerLiteral))));

            // Act
            var actual = _sut.ParseExpression(tokenStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [ClassData(typeof(LowPrecedenceOperatorHighPrecedenceOperatorPairs))]
        public void ParseExpression_HiPrecedenceOpThenLoPrecedenceOp_ReturnsBinaryExpressionWithFirstTwoTermsGroupedFirst(
            TokenType loOp,
            TokenType hiOp)
        {
            // Arrange
            var tokenStream = new TokenStream(new[]
            {
                new Token(TokenType.IntegerLiteral),
                new Token(hiOp),
                new Token(TokenType.IntegerLiteral),
                new Token(loOp),
                new Token(TokenType.IntegerLiteral)
            });

            var expected = new BinaryExpression(
                new BinaryExpression(
                    new LiteralExpression(new Token(TokenType.IntegerLiteral)),
                    new Token(hiOp),
                    new LiteralExpression(new Token(TokenType.IntegerLiteral))),
                new Token(loOp),
                new LiteralExpression(new Token(TokenType.IntegerLiteral)));

            // Act
            var actual = _sut.ParseExpression(tokenStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [ClassData(typeof(LeftAssociativeBinaryOperators))]
        public void ParseExpression_LeftAssociativeOperator_ReturnsBinaryExpressionWithFirstTwoTermsGroupedFirst(
            TokenType op)
        {
            // Arrange
            var tokenStream = new TokenStream(new[]
            {
                new Token(TokenType.IntegerLiteral),
                new Token(op),
                new Token(TokenType.IntegerLiteral),
                new Token(op),
                new Token(TokenType.IntegerLiteral)
            });

            var expected = new BinaryExpression(
                new BinaryExpression(
                    new LiteralExpression(new Token(TokenType.IntegerLiteral)),
                    new Token(op),
                    new LiteralExpression(new Token(TokenType.IntegerLiteral))),
                new Token(op),
                new LiteralExpression(new Token(TokenType.IntegerLiteral)));

            // Act
            var actual = _sut.ParseExpression(tokenStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(TokenType.Exponentiation)]
        public void ParseExpression_RightAssociativeOperator_ReturnsBinaryExpressionWithLastTwoTermsGroupedFirst(
            TokenType op)
        {
            // Arrange
            var tokenStream = new TokenStream(new[]
            {
                new Token(TokenType.IntegerLiteral),
                new Token(op),
                new Token(TokenType.IntegerLiteral),
                new Token(op),
                new Token(TokenType.IntegerLiteral)
            });

            var expected = new BinaryExpression(
                new LiteralExpression(new Token(TokenType.IntegerLiteral)),
                new Token(op),
                new BinaryExpression(
                    new LiteralExpression(new Token(TokenType.IntegerLiteral)),
                    new Token(op),
                    new LiteralExpression(new Token(TokenType.IntegerLiteral))));

            // Act
            var actual = _sut.ParseExpression(tokenStream);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [ClassData(typeof(LowPrecedenceOperatorHighPrecedenceOperatorPairs))]
        public void
            ParseExpression_HiPrecedenceOpThenParenthesizedLoPrecedenceOp_ReturnsBinaryExpressionWithLastTwoTermsGroupedFirst(
                TokenType hiOp, TokenType loOp)
        {
            // Arrange
            var tokenStream = new TokenStream(new[]
            {
                new Token(TokenType.IntegerLiteral),
                new Token(hiOp),
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.IntegerLiteral),
                new Token(loOp),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightParenthesis)
            });

            var expected = new BinaryExpression(
                new LiteralExpression(new Token(TokenType.IntegerLiteral)),
                new Token(hiOp),
                new BinaryExpression(
                    new LiteralExpression(new Token(TokenType.IntegerLiteral)),
                    new Token(loOp),
                    new LiteralExpression(new Token(TokenType.IntegerLiteral))));

            // Act
            var actual = _sut.ParseExpression(tokenStream);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
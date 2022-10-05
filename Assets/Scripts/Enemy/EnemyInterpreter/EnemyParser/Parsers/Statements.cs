#nullable enable

using System.Collections.Generic;

public partial class EnemyParser
{
    public ParseResult<BreakStASTNodeBase> ParseBreakSt(TokenStreamPointer pointer)
    {
        var stream = pointer.StartStream();
        if (!stream.maybe.Expect("break").IsSatisfied) {
            return ParseResult<BreakStASTNodeBase>.Failed(
                "expected break",
                "BreakStASTNodeBase",
                stream.CurrentPointer);
        }
        return new ParseResult<BreakStASTNodeBase>(
            new BreakStASTNode(),
            stream.CurrentPointer);
    }

    public ParseResult<StatementASTNodeBase> ParseStatement(TokenStreamPointer pointer) {
        var stream = pointer.StartStream();
        var result = stream.match<StatementASTNodeBase>()
            .Try(ParseAssignSt)
            .Try(ParseExpSt)
            .Result;
        if (result == null)
        {
            throw new ParseException("Undefined Exception");
        }
        else {
            return new (
                result,
                stream.CurrentPointer);
        }
    }

    public ParseResult<BlockStASTNodeBase> ParseBlockSt(TokenStreamPointer pointer) {
        var stream = pointer.StartStream();
        if (!stream.maybe
            .Expect("{")
            .ExpectMultiComsumer(ParseStatement,
                out List<StatementASTNodeBase> statements)
            .Expect("}")
            .IsSatisfied) {
            return ParseResult<BlockStASTNodeBase>.Failed(
                "expected {",
                "BlockStASTNodeBase",
                stream.CurrentPointer);
        }
        return new ParseResult<BlockStASTNodeBase>(
            new BlockStASTNode(statements),
            stream.CurrentPointer);
    }

    public ParseResult<DeclarationStASTNodeBase> ParseDeclarationSt(TokenStreamPointer pointer)
    {
        string id;
        var stream = pointer.StartStream();
        if (stream.maybe
            .Expect("int")
            .ExpectSymbolID(out id)
            .IsSatisfied)
        {
            return new ParseResult<DeclarationStASTNodeBase>(
                new DeclarationStASTNode(PrimitiveValue.Type.INT, id),
                stream.CurrentPointer);
        }
        else if (stream.maybe
            .Expect("float")
            .ExpectSymbolID(out id)
            .IsSatisfied)
        {
            return new ParseResult<DeclarationStASTNodeBase>(
                new DeclarationStASTNode(PrimitiveValue.Type.FLOAT, id),
                stream.CurrentPointer);
        }
        else
        {
            return ParseResult<DeclarationStASTNodeBase>.Failed(
                "expected int, float",
                "DeclarationStASTNode",
                stream.CurrentPointer);
        }
    }

    public ParseResult<ExpStASTNodeBase> ParseExpSt(TokenStreamPointer pointer)
    {
        var stream = pointer.StartStream();
        if (!stream.maybe
            .ExpectConsumedBy(ParseExpASTNode,
                out ExpASTNodeBase exp).IsSatisfied)
        {
            return ParseResult<ExpStASTNodeBase>.Failed(
                "expected {",
                "ExpStASTNode",
                stream.CurrentPointer);
        }
        return new ParseResult<ExpStASTNodeBase>(
            new ExpStASTNode(exp),
            stream.CurrentPointer);
    }

    public ParseResult<IfStASTNodeBase> ParseIfSt(TokenStreamPointer pointer)
    {
        var stream = pointer.StartStream();
        if (!stream.maybe
            .Expect("if")
            .Expect("(")
            .ExpectConsumedBy(ParseExpASTNode, out ExpASTNodeBase cond)
            .Expect(")")
            .ExpectConsumedBy(ParseStatement, out StatementASTNodeBase statement)
            .IsSatisfied)
        {
            return ParseResult<IfStASTNodeBase>.Failed(
                "expected {",
                "BlockStASTNodeBase",
                stream.CurrentPointer);
        }
        else if (stream.maybe
                .Expect("else")
                .ExpectConsumedBy(ParseStatement, out StatementASTNodeBase elseStatement)
                .IsSatisfied)
        {
            return new ParseResult<IfStASTNodeBase>(
                new IfStASTNode(cond, statement, elseStatement),
                stream.CurrentPointer);
        }
        else {
            return new ParseResult<IfStASTNodeBase>(
                new IfStASTNode(cond, statement),
                stream.CurrentPointer);
        }
    }

    public ParseResult<AssignStASTNodeBase> ParseAssignSt(TokenStreamPointer pointer)
    {
        var stream = pointer.StartStream();
        if (!stream.maybe.ExpectSymbolID(out string id)
            .Expect("=")
            .ExpectConsumedBy(ParseExpASTNode, out ExpASTNodeBase exp)
            .IsSatisfied)
        {
            return ParseResult<AssignStASTNodeBase>.Failed(
                "expected <ID>",
                "AssignStASTNodeBase",
                stream.CurrentPointer);
        }
        return new (
            new AssignStASTNode(id, exp),
            stream.CurrentPointer);
    }

    public ParseResult<RepeatStASTNodeBase> ParseRepeatSt(TokenStreamPointer pointer) {
        var stream = pointer.StartStream();
        if (!stream.maybe
            .Expect("repeat")
            .Expect("(")
            .ExpectVariable(out ScriptToken token)
            .Expect(")")
            .ExpectConsumedBy(ParseStatement, out StatementASTNodeBase statement)
            .IsSatisfied) {
            return ParseResult<RepeatStASTNodeBase>.Failed(
                "expected repeat",
                "RepeatStASTNodeBase",
                stream.CurrentPointer);
        }
        return new (
            new RepeatStASTNode(
                token.int_val,
                statement),
            stream.CurrentPointer);
    }
}

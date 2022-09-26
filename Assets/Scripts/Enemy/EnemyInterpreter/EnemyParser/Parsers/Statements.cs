#nullable enable

using System.Collections.Generic;

public partial class EnemyParser
{
    public ParseResult<BreakStASTNode> ParseBreakSt(TokenStreamPointer pointer)
    {
        var stream = pointer.StartStream();
        if (stream.maybe.Expect("break").IsSatisfied) {
            return ParseResult<BreakStASTNode>.Failed(
                "expected break",
                "BreakStASTNode",
                stream.CurrentPointer);
        }
        return new ParseResult<BreakStASTNode>(
            new BreakStASTNode(),
            stream.CurrentPointer);
    }

    public ParseResult<StatementASTNode> ParseStatement(TokenStreamPointer pointer) {
        var stream = pointer.StartStream();
        var result = stream.match<StatementASTNode>()
            .Try(ParseBlockSt)
            .Try(ParseCallFuncStASTNode)
            .Try(ParseRepeatSt)
            .Try(ParseBreakSt)
            .Try(ParseIfSt)
            .Result;
        if (result == null)
        {
            throw new ParseException("Undefined Exception");
        }
        else {
            return new ParseResult<StatementASTNode>(
                result,
                stream.CurrentPointer);
        }
    }

    public ParseResult<BlockStASTNode> ParseBlockSt(TokenStreamPointer pointer) {
        var stream = pointer.StartStream();
        if (!TestBlockStASTNode(pointer)) {
            return ParseResult<BlockStASTNode>.Failed(
                "expected {",
                "BlockStASTNode",
                stream.CurrentPointer);
        }
        pointer.StartStream().should
            .Expect("{")
            .ExpectMultiComsumer(ParseStatement,
                out List<StatementASTNode> statements)
            .Expect("}");
        return new ParseResult<BlockStASTNode>(
            new BlockStASTNode(statements),
            stream.CurrentPointer);
    }

    public ParseResult<IfStASTNode> ParseIfSt(TokenStreamPointer pointer)
    {
        var stream = pointer.StartStream();
        if (!TestBlockStASTNode(pointer))
        {
            return ParseResult<IfStASTNode>.Failed(
                "expected {",
                "BlockStASTNode",
                stream.CurrentPointer);
        }
        pointer.StartStream().should
            .Expect("if")
            .Expect("(")
            .ExpectConsumedBy(ParseExpASTNode, out ExpASTNode cond)
            .Expect(")")
            .ExpectConsumedBy(ParseStatement, out StatementASTNode statement);
        if (TestElseStASTNode(pointer))
        {
            pointer.StartStream().should
                .Expect("else")
                .ExpectConsumedBy(ParseStatement, out StatementASTNode elseStatement);
            return new ParseResult<IfStASTNode>(
                new IfStASTNode(cond, statement, elseStatement),
                stream.CurrentPointer);
        }
        else {
            return new ParseResult<IfStASTNode>(
                new IfStASTNode(cond, statement),
                stream.CurrentPointer);
        }
    }

    public ParseResult<AssignStASTNode> ParseAssignSt(TokenStreamPointer pointer)
    {
        var stream = pointer.StartStream();
        if (!TestAssignStASTNode(pointer))
        {
            return ParseResult<AssignStASTNode>.Failed(
                "expected <ID>",
                "AssignStASTNode",
                stream.CurrentPointer);
        }
        stream.should.ExpectSymbolID(out string id)
            .Expect("=")
            .ExpectConsumedBy(ParseExpASTNode, out ExpASTNode exp);
        return new ParseResult<AssignStASTNode>(
            new AssignStASTNode(id, exp),
            stream.CurrentPointer);
    }

    public ParseResult<RepeatStASTNode> ParseRepeatSt(TokenStreamPointer pointer) {
        var stream = pointer.StartStream();
        if (!TestRepeatStASTNode(pointer)) {
            return ParseResult<RepeatStASTNode>.Failed(
                "expected repeat",
                "RepeatStASTNode",
                stream.CurrentPointer);
        }
        pointer.StartStream().should
            .Expect("repeat")
            .Expect("(")
            .ExpectVariable(out ScriptToken token)
            .Expect(")")
            .ExpectConsumedBy(ParseStatement, out StatementASTNode statement);
        return new ParseResult<RepeatStASTNode>(
            new RepeatStASTNode(
                token.int_val,
                statement),
            stream.CurrentPointer);
    }

    public bool TestIfStASTNode(TokenStreamPointer pointer)
    {
        return pointer.StartStream().maybe
            .Expect("if")
            .Expect("(")
            .ExpectConsumedBy(ParseExpASTNode, out ExpASTNode cond)
            .Expect(")")
            .ExpectMultiComsumer(ParseStatement, out List<StatementASTNode> statements)
            .Expect("}")
            .IsSatisfied;

    }

    public bool TestElseStASTNode(TokenStreamPointer pointer)
    {
        return pointer.StartStream().maybe
            .Expect("else")
            .ExpectConsumedBy(ParseStatement, out StatementASTNode statement)
            .IsSatisfied;
    }

    public bool TestBlockStASTNode(TokenStreamPointer pointer)
    {
        return pointer.StartStream().maybe
            .Expect("{")
            .ExpectMultiComsumer(ParseStatement, out List<StatementASTNode> statements)
            .Expect("}")
            .IsSatisfied;

    }

    public bool TestRepeatStASTNode(TokenStreamPointer pointer) {
        return pointer.StartStream().maybe
            .Expect("repeat")
            .Expect("(")
            .ExpectVariable(out ScriptToken token)
            .Expect(")")
            .IsSatisfied;

    }

    public bool TestAssignStASTNode(TokenStreamPointer pointer) {
        return pointer.StartStream().maybe.ExpectSymbolID(out string id)
            .Expect("=")
            .ExpectConsumedBy(ParseExpASTNode, out ExpASTNode exp)
            .IsSatisfied;
    }
}

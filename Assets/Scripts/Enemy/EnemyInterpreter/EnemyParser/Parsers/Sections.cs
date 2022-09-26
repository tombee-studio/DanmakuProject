using System;
public partial class EnemyParser
{
    public ParseResult<BulletASTNode> ParseBulletAST(TokenStreamPointer pointer)
    {
        var stream = pointer.StartStream();
        if (stream.maybe.Expect("bullet").IsSatisfied) return ParseResult<BulletASTNode>.Failed("expected token `bullet` is not found.", "BulletAST", pointer);
        stream.should
            .Expect(">>")
            .ExpectMultiComsumerAtLeast1(ParseBulletSectionAST, out var bulletSections);
        return new(
            new BulletASTNode(bulletSections),
            stream.CurrentPointer
        );
    }
    public ParseResult<BulletSectionASTNode> ParseBulletSectionAST(TokenStreamPointer pointer)
    {
        var stream = pointer.StartStream();
        stream.should
            .Expect("ID")
            .Expect(":")
            .ExpectVariable(out ScriptToken id)
            .ExpectMultiComsumer(ParseCallFuncStASTNode, out var nodes);
        if (id.type != ScriptToken.Type.INT_LITERAL) throw ParseException.Information($"Expected Literal is int one but {id.type} is coming.", stream.CurrentPointer);
        return new(
            new BulletSectionASTNode(id.int_val, nodes),
            stream.CurrentPointer
        );
            
    } 

    public ParseResult<ActionASTNode> ParseActionAST(TokenStreamPointer pointer)
    {
        var stream = pointer.StartStream();

        stream.should
            .Expect("action")
            .Expect(">>")
            .ExpectMultiComsumerAtLeast1(ParseStatement, out var statementASTNodes);
        return new(
            new ActionASTNode(statementASTNodes),
            stream.CurrentPointer
        );
    }

}

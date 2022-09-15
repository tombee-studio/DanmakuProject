#nullable enable

public class EnemyParser {

    public ParseResult<BehaviourASTNode> ParseBehaviour(TokenStreamPointer pointer) {
        var stream = TokenStream.FromPointer(pointer);

        stream.should
            .Expect("behaviour")
            .ExpectSymbolID(out string id)
            .Expect("{")
            .MaybeConsumedBy(ParseBulletAST, out BulletASTNode? bulletASTNode)
            .ExpectConsumedBy(ParseActionAST, out ActionASTNode actionASTNode)
            .Expect("}");

        return new ParseResult<BehaviourASTNode>(
            new BehaviourASTNode(id, bulletASTNode, actionASTNode),
            stream.CurrentPointer
            );
    }

    public ParseResult<BulletASTNode> ParseBulletAST(TokenStreamPointer pointer) {
        
    }

    public ParseResult<ActionASTNode> ParseActionAST(TokenStreamPointer pointer)
    {
        
    }
}
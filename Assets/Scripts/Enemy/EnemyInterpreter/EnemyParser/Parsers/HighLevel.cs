#nullable enable

public partial class EnemyParser {

    public ParseResult<BehaviourASTNodeBase> ParseBehaviour(TokenStreamPointer pointer) {
        var stream = pointer.StartStream();

        stream.should
            .Expect("behaviour")
            .ExpectSymbolID(out string id)
            .Expect("{")
            .MaybeConsumedBy(ParseBulletAST, out BulletASTNode? bulletASTNode)
            .ExpectConsumedBy(ParseActionAST, out ActionASTNodeBase actionASTNode)
            .Expect("}");

        return new ParseResult<BehaviourASTNodeBase>(
            new BehaviourASTNode(id, bulletASTNode, actionASTNode),
            stream.CurrentPointer
        );
    }

}
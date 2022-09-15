using System;
using System.Collections.Generic;

public class EnemyParser {
    
    public ParseResult<BehaviourASTNode> ParseBehaviour(TokenStreamPointer pointer) {
        var stream = TokenStream.FromPointer(pointer);
        stream
            .Expect("behaviour")
            .ExpectSymbolID(out string id)
            .Expect("{");

        var bulletASTResult = ParseBulletAST(stream.CurrentPointer).ApplyIfSucceeded(ref stream);
        var actionASTResult = ParseActionAST(stream.CurrentPointer).ApplyIfSucceeded(ref stream).ShouldSucceed();

        stream.Expect("}");

        var behaviourASTNode = new BehaviourASTNode(
            id,
            bulletASTResult.ParsedNodeNullable,
            actionASTResult.ParsedNode
            );
        return new ParseResult<BehaviourASTNode>(
            behaviourASTNode,
            stream.CurrentPointer
            );
    }

    public ParseResult<BulletASTNode> ParseBulletAST(TokenStreamPointer pointer) {
        
        return null;
    }

    public ParseResult<ActionASTNode> ParseActionAST(TokenStreamPointer pointer)
    {
        pos++;
        return null;
    }
}
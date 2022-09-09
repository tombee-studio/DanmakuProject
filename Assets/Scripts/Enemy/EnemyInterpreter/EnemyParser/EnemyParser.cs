using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParser {
    public BehaviourASTNode ParseBehaviour(List<ScriptToken> tokens, ref int pos) {
        if (tokens[pos].type != ScriptToken.Type.BEHAVIOR)
            throw new Exception("expected token: behaviour");
        pos++;
        if (tokens[pos].type != ScriptToken.Type.SYMBOL_ID)
            throw new Exception("expected token: <ID>");
        string id = tokens[pos].user_defined_symbol;
        pos++;
        if (tokens[pos].type != ScriptToken.Type.CURLY_BRACKET_LEFT)
            throw new Exception("expected token: {");
        pos++;
        var bulletAST = ParseBulletAST(tokens, ref pos);
        var actionAST = ParseActionAST(tokens, ref pos);
        if (tokens[pos].type != ScriptToken.Type.CURLY_BRACKET_RIGHT)
            throw new Exception("expected token: {");
        return new BehaviourASTNode(id);
    }

    public BulletASTNode ParseBulletAST(List<ScriptToken> tokens, ref int pos) {
        pos++;
        return new BulletASTNode();
    }

    public ActionASTNode ParseActionAST(List<ScriptToken> tokens, ref int pos)
    {
        pos++;
        return new ActionASTNode();
    }
}
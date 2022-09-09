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
        return new BehaviourASTNode(id, null, null);
    }
}
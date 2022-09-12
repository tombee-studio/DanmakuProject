using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ロードマップ
 * TokenStream(List<ScriptToken> tokens)
 *  #proceed()         -> 読み進める
 *  #lookahead()    -> トークンを先読み
 * 
 * TokenStreamObserver(TokenStream stream)
 *  #should():TokenStreamValidation
 *  #case():TokenStreamValidation
 *  #tokenStream
 * 
 * TokenStreamValidation
 *  #TokenStreamValidation flow(string token) -> 指定した予約語トークンが流れてくるという条件
 *  #TokenStreamValidation flow_capturedInt(ScriptToken.Type type, out int capturedInt)
 *  #TokenStreamValidation flow_capturedFloat(ScriptToken.Type type, out float capturedFloat)
 *  #TokenStreamValidation flow_capturedSymbolID(ScriptToken.Type type, out string capturedSymbolID)
 *  #N needRecipient<N>(Function<TokenStreamObserver, N> func, out N astNode) where N:
 *  #bool isValidated 
 * 分岐 | | 
 * 文法の場合分け +, -, *, /
 * 
 * 0回以上の繰り返し *
 *
 */

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
        return new BehaviourASTNode(id, null, null);
    }

    public BulletASTNode ParseBulletAST(List<ScriptToken> tokens, ref int pos) {
        pos++;
        return null;
    }

    public ActionASTNode ParseActionAST(List<ScriptToken> tokens, ref int pos)
    {
        pos++;
        return null;
    }
    public class TokenStream
    {
        List<ScriptToken> tokens;
        int tokenPointer;

    }
}
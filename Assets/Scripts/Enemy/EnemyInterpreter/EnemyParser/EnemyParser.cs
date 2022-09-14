using System;
using System.Collections.Generic;


/*
 * ロードマップ
 * 
 * 各非終端記号に対応したパーサーを定義する
 * 
 * 
 * 
 * EnemyParser
 *  
 * NonterminalSymbolParser(インタフェース)
 *  #?check(TokenStream stream) -> TokenStreamの現在の位置から先に該当の非終端記号に該当するものが存在するかを調べる。
 *  #parse(TokenStream stream)  -> 読み進め、トークンから情報を抽出する。
 * 
 * TokenStream(List<ScriptToken> tokens)
 *  private proceed()      -> 読み進める
 *  private lookahead()    -> トークンを先読み
 *  #should():TokenStreamValidation
 *  #maybe():TokenStreamValidation
 *  #tokenStream
 * 
 * TokenStreamValidation
 *  #TokenStreamValidation expectReserved(string token) -> 読み進め、指定されたトークンが流れてくることを条件に設定する。
 *  #TokenStreamValidation expectVariable(out ScriptToken capturedSymbolID)
 *  #TokenStreamValidation expectSymbolID(out string capturedSymbolID)
 *  #TokenStreamValidation confront<N>(
 *      Func<TokenStreamObserver, N> parser,
 *      out N astNode
 *  ) where N:TokenStreamobserver
 *  #TokenStreamValidation case<N>(
 *      NonterminalSymbolParser parser
 *      out N astNode
 *      ) where N:TokenStreamObserver
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
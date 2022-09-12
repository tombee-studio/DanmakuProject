using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ロードマップ
 * TokenStream
 *  #Consume
 *  
 *  非終端
        EXP 
        := EQUALITY_EXP

        EQUALITY_EXP 
        := RELATIONAL_EXP
        | EQUALITY_EXP [==|!=] RELATIONAL_EXP

        RELATIONAL_EXP 
        := TERM_EXP
        | RELATIONAL_EXP [<|>|<=|>=] TERM

        TERM_EXP
        := FACTOR_EXP
        | TERM [+|-] TERM_EXP

        FACTOR_EXP
        := UNARY_EXP
        | FACTOR_EXP [*|/|%] UNARY_EXP

        UNARY_EXP
        := -? PRIMARY_EXP

        PRIMARY_EXP
        := IDENTIFIER
        | PrimitiveValue
        | (EXP)
 * 
 * ParsePrimaryExp()
     * return TokenStream.lookahead()
     * .maybeIdentifier()
     * .maybePrimitiveValue()
     * .maybeNonterminal(ParseExpression);
 * 
 * 
 * 
 * 分岐 | | 
 * 文法の場合分け +, -, *, /
 * 0回以上の繰り返し *
 * 
 * TokenStream
 * .startsWith("behaviour")
 * .then_SymbolID(out string id)
 * .then("{")
 * .then_Multiple("{")
 * .then_Optional("}")
 * .then("}")
 * .then("/")
 * .then(",")
 * [x] Inversed(dictionary)
 * [ ] 
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLexer {
    struct Token {
        public enum Type {
            /**
             * Types of Token
             */
            NONE,
            BEHAVIOR,
        };
        public Type type;
        public string id;
        public int int_val;
        public float float_val;
    };

    List<Token> Lex(string text) {
        List<Token> tokens = new List<Token>();
        int i = 0;
        while (i < text.Length) {
            TokenizeBehavior(text, ref i);
        }
        return tokens;
    }

    Token TokenizeBehavior(string text, ref int pos) {
        int index = pos;
        Token none = new Token();
        Token behavior = new Token();
        none.type = Token.Type.NONE;
        behavior.type = Token.Type.BEHAVIOR;
        foreach (char c in "behavior") {
            if (c != text[index]) return none;
        }
        return behavior;
    }
}
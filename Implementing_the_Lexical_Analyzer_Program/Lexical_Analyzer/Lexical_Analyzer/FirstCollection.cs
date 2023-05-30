using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Lexical_Analyzer.LexicalAnalyzer;

namespace Lexical_Analyzer
{
    internal class First
    {
        public static bool Description(LexicalAnalyzer.Lexeme lex)
        {
            return lex == LexicalAnalyzer.Lexeme.Tint || lex == LexicalAnalyzer.Lexeme.Tbool;
        }

        public static bool Data(LexicalAnalyzer.Lexeme lex)
        {
            return lex == LexicalAnalyzer.Lexeme.Tint || lex == LexicalAnalyzer.Lexeme.Tbool;
        }

        public static bool Type(LexicalAnalyzer.Lexeme lex)
        {
            return lex == LexicalAnalyzer.Lexeme.Tint || lex == LexicalAnalyzer.Lexeme.Tbool;
        }

        public static bool VariableList(LexicalAnalyzer.Lexeme lex)
        {
            return lex == LexicalAnalyzer.Lexeme.Tiden;
        }

        public static bool Variable(LexicalAnalyzer.Lexeme lex)
        {
            return lex == LexicalAnalyzer.Lexeme.Tiden;
        }

        public static bool Expression(LexicalAnalyzer.Lexeme lex)
        {
            return lex == LexicalAnalyzer.Lexeme.Tplus ||
                lex == LexicalAnalyzer.Lexeme.Tminus ||
                lex == LexicalAnalyzer.Lexeme.Tnot ||
                lex == LexicalAnalyzer.Lexeme.TbrackOpen ||
                lex == LexicalAnalyzer.Lexeme.TconstInt ||
                lex == LexicalAnalyzer.Lexeme.TconstIntHex ||
                lex == LexicalAnalyzer.Lexeme.Tiden ||
                lex == LexicalAnalyzer.Lexeme.Ttrue ||
                lex == LexicalAnalyzer.Lexeme.Tfalse;
        }

        public static bool And(LexicalAnalyzer.Lexeme lex)
        {
            return lex == LexicalAnalyzer.Lexeme.Tplus ||
                lex == LexicalAnalyzer.Lexeme.Tminus ||
                lex == LexicalAnalyzer.Lexeme.Tnot ||
                lex == LexicalAnalyzer.Lexeme.TbrackOpen ||
                lex == LexicalAnalyzer.Lexeme.TconstInt ||
                lex == LexicalAnalyzer.Lexeme.TconstIntHex ||
                lex == LexicalAnalyzer.Lexeme.Tiden ||
                lex == LexicalAnalyzer.Lexeme.Ttrue ||
                lex == LexicalAnalyzer.Lexeme.Tfalse;
        }

        public static bool Equality(LexicalAnalyzer.Lexeme lex)
        {
            return lex == LexicalAnalyzer.Lexeme.Tplus ||
                lex == LexicalAnalyzer.Lexeme.Tminus ||
                lex == LexicalAnalyzer.Lexeme.Tnot ||
                lex == LexicalAnalyzer.Lexeme.TbrackOpen ||
                lex == LexicalAnalyzer.Lexeme.TconstInt ||
                lex == LexicalAnalyzer.Lexeme.TconstIntHex ||
                lex == LexicalAnalyzer.Lexeme.Tiden ||
                lex == LexicalAnalyzer.Lexeme.Ttrue ||
                lex == LexicalAnalyzer.Lexeme.Tfalse;
        }

        public static bool Сomparison(LexicalAnalyzer.Lexeme lex)
        {
            return lex == LexicalAnalyzer.Lexeme.Tplus ||
                lex == LexicalAnalyzer.Lexeme.Tminus ||
                lex == LexicalAnalyzer.Lexeme.Tnot ||
                lex == LexicalAnalyzer.Lexeme.TbrackOpen ||
                lex == LexicalAnalyzer.Lexeme.TconstInt ||
                lex == LexicalAnalyzer.Lexeme.TconstIntHex ||
                lex == LexicalAnalyzer.Lexeme.Tiden ||
                lex == LexicalAnalyzer.Lexeme.Ttrue ||
                lex == LexicalAnalyzer.Lexeme.Tfalse;
        }

        public static bool AdditionSubtraction(LexicalAnalyzer.Lexeme lex)
        {
            return lex == LexicalAnalyzer.Lexeme.Tplus ||
                lex == LexicalAnalyzer.Lexeme.Tminus ||
                lex == LexicalAnalyzer.Lexeme.Tnot ||
                lex == LexicalAnalyzer.Lexeme.TbrackOpen ||
                lex == LexicalAnalyzer.Lexeme.TconstInt ||
                lex == LexicalAnalyzer.Lexeme.TconstIntHex ||
                lex == LexicalAnalyzer.Lexeme.Tiden ||
                lex == LexicalAnalyzer.Lexeme.Ttrue ||
                lex == LexicalAnalyzer.Lexeme.Tfalse;
        }
        
        public static bool MultiplicationDivision(LexicalAnalyzer.Lexeme lex)
        {
            return lex == LexicalAnalyzer.Lexeme.Tplus ||
                lex == LexicalAnalyzer.Lexeme.Tminus ||
                lex == LexicalAnalyzer.Lexeme.Tnot ||
                lex == LexicalAnalyzer.Lexeme.TbrackOpen ||
                lex == LexicalAnalyzer.Lexeme.TconstInt ||
                lex == LexicalAnalyzer.Lexeme.TconstIntHex ||
                lex == LexicalAnalyzer.Lexeme.Tiden ||
                lex == LexicalAnalyzer.Lexeme.Ttrue ||
                lex == LexicalAnalyzer.Lexeme.Tfalse;
        }

        public static bool UnarySigns(LexicalAnalyzer.Lexeme lex)
        {
            return lex == LexicalAnalyzer.Lexeme.Tplus ||
                lex == LexicalAnalyzer.Lexeme.Tminus ||
                lex == LexicalAnalyzer.Lexeme.Tnot ||
                lex == LexicalAnalyzer.Lexeme.TbrackOpen ||
                lex == LexicalAnalyzer.Lexeme.TconstInt ||
                lex == LexicalAnalyzer.Lexeme.TconstIntHex ||
                lex == LexicalAnalyzer.Lexeme.Tiden ||
                lex == LexicalAnalyzer.Lexeme.Ttrue ||
                lex == LexicalAnalyzer.Lexeme.Tfalse;
        }

        public static bool Operand(LexicalAnalyzer.Lexeme lex)
        {
            return lex == LexicalAnalyzer.Lexeme.TbrackOpen ||
                lex == LexicalAnalyzer.Lexeme.TconstInt ||
                lex == LexicalAnalyzer.Lexeme.TconstIntHex ||
                lex == LexicalAnalyzer.Lexeme.Tiden ||
                lex == LexicalAnalyzer.Lexeme.Ttrue ||
                lex == LexicalAnalyzer.Lexeme.Tfalse;
        }

        public static bool Const(LexicalAnalyzer.Lexeme lex)
        {
            return lex == LexicalAnalyzer.Lexeme.TconstInt ||
                lex == LexicalAnalyzer.Lexeme.TconstIntHex ||
                lex == LexicalAnalyzer.Lexeme.Ttrue ||
                lex == LexicalAnalyzer.Lexeme.Tfalse;
        }

        public static bool Function(LexicalAnalyzer.Lexeme lex)
        {
            return lex == LexicalAnalyzer.Lexeme.Tint || lex == LexicalAnalyzer.Lexeme.Tbool;
        }

        public static bool VariableDeclarationList(LexicalAnalyzer.Lexeme lex)
        {
            return lex == LexicalAnalyzer.Lexeme.Tint || lex == LexicalAnalyzer.Lexeme.Tbool;
        }

        public static bool CompoundOperator(LexicalAnalyzer.Lexeme lex)
        {
            return lex == LexicalAnalyzer.Lexeme.TbraceOpen;
        }

        public static bool OperatorData(LexicalAnalyzer.Lexeme lex)
        {
            return lex == LexicalAnalyzer.Lexeme.TbraceOpen ||
                lex == LexicalAnalyzer.Lexeme.Tsem ||
                lex == LexicalAnalyzer.Lexeme.Twhile ||
                lex == LexicalAnalyzer.Lexeme.Tint ||
                lex == LexicalAnalyzer.Lexeme.Tbool ||
                lex == LexicalAnalyzer.Lexeme.Tiden;
        }

        public static bool Operator(LexicalAnalyzer.Lexeme lex)
        {
            return lex == LexicalAnalyzer.Lexeme.TbraceOpen ||
                lex == LexicalAnalyzer.Lexeme.Tsem ||
                lex == LexicalAnalyzer.Lexeme.Twhile ||
                lex == LexicalAnalyzer.Lexeme.Tiden;
        }
        
        public static bool Assignment(LexicalAnalyzer.Lexeme lex)
        {
            return lex == LexicalAnalyzer.Lexeme.Tiden;
        }

        public static bool While(LexicalAnalyzer.Lexeme lex)
        {
            return lex == LexicalAnalyzer.Lexeme.Twhile;
        }

        public static bool CallFunction(LexicalAnalyzer.Lexeme lex)
        {
            return lex == LexicalAnalyzer.Lexeme.Tiden;
        }

        public static bool ExpressionList(LexicalAnalyzer.Lexeme lex)
        {
            return lex == LexicalAnalyzer.Lexeme.Tplus ||
                lex == LexicalAnalyzer.Lexeme.Tminus ||
                lex == LexicalAnalyzer.Lexeme.Tnot ||
                lex == LexicalAnalyzer.Lexeme.TbrackOpen ||
                lex == LexicalAnalyzer.Lexeme.TconstInt ||
                lex == LexicalAnalyzer.Lexeme.TconstIntHex ||
                lex == LexicalAnalyzer.Lexeme.Tiden ||
                lex == LexicalAnalyzer.Lexeme.Ttrue ||
                lex == LexicalAnalyzer.Lexeme.Tfalse;
        }
    }
}

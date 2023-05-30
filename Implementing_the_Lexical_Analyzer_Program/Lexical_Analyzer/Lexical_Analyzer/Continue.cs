using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Lexical_Analyzer.LexicalAnalyzer;

namespace Lexical_Analyzer
{
    internal class Continue
    {
        public static bool Data(LexicalAnalyzer lexical_analyzer)
        {
            LexicalAnalyzer new_lexical_analyzer = new LexicalAnalyzer(lexical_analyzer.t.im);
            new_lexical_analyzer.p = lexical_analyzer.p;
            new_lexical_analyzer.abs_p = lexical_analyzer.abs_p;
            new_lexical_analyzer.le = lexical_analyzer.le;
            Lexeme typ;

            typ = new_lexical_analyzer.Scaner(new_lexical_analyzer.le);
            if (typ == Lexeme.Tiden)
            {
                typ = new_lexical_analyzer.Scaner(new_lexical_analyzer.le);
                if (typ == Lexeme.Tassign || typ == Lexeme.Tsem || typ == Lexeme.Tcom) return true;
            }
            return false;
        }

        public static bool Function(LexicalAnalyzer lexical_analyzer)
        {
            LexicalAnalyzer new_lexical_analyzer = new LexicalAnalyzer(lexical_analyzer.t.im);
            new_lexical_analyzer.p = lexical_analyzer.p;
            new_lexical_analyzer.abs_p = lexical_analyzer.abs_p;
            new_lexical_analyzer.le = lexical_analyzer.le;
            Lexeme typ;

            typ = new_lexical_analyzer.Scaner(new_lexical_analyzer.le);
            if (typ == Lexeme.Tiden)
            {
                typ = new_lexical_analyzer.Scaner(lexical_analyzer.le);
                if (typ == Lexeme.TbrackOpen) return true;
            }
            return false;
        }

        public static bool CallFunction(LexicalAnalyzer lexical_analyzer)
        {
            LexicalAnalyzer new_lexical_analyzer = new LexicalAnalyzer(lexical_analyzer.t.im);
            new_lexical_analyzer.p = lexical_analyzer.p;
            new_lexical_analyzer.abs_p = lexical_analyzer.abs_p;
            new_lexical_analyzer.le = lexical_analyzer.le;
            Lexeme typ;

            typ = new_lexical_analyzer.Scaner(lexical_analyzer.le);
            if (typ == Lexeme.TbrackOpen) return true;
            return false;
        }
    }
}

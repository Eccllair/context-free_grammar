using System;
using System.Data;
using System.Reflection;
namespace Lexical_Analyzer
{
    public class LexicalAnalyzer
    {
        public class IM                                        //текст ИМ
        {
            public char[] im;
            public IM(string text = "") { this.im = (text + '\0').ToCharArray(); }
            public IM(char[] text)
            {
                this.im = text;
                Array.Resize(ref this.im, this.im.Length + 1);
                this.im[this.im.Length - 1] = '\0';
            }
        }

        public class LexemeName : Attribute
        {
            public string name { get; set; }
            public LexemeName(string name)
            {
                this.name = name;
            }
        }

        public enum Lexeme                                      //список допустимых лексем
        {
            [LexemeName("while")] Twhile = 1,
            [LexemeName("int")] Tint,
            [LexemeName("bool")] Tbool,
            [LexemeName("true")] Ttrue,
            [LexemeName("false")] Tfalse,
            [LexemeName("return")] Treturn,
            [LexemeName("i")] Tiden = 20,
            [LexemeName("constInt")] TconstInt = 30,
            [LexemeName("constIntHex")] TconstIntHex,
            [LexemeName(",")] Tcom = 40,
            [LexemeName(";")] Tsem,
            [LexemeName("{")] TbraceOpen,
            [LexemeName("}")] TbraceClose,
            [LexemeName("(")] TbrackOpen,
            [LexemeName(")")] TbrackClose,
            [LexemeName("+")] Tplus = 50,
            [LexemeName("-")] Tminus,
            [LexemeName("*")] Tmult,
            [LexemeName("/")] Tdiv,
            [LexemeName("%")] Tmod,
            [LexemeName("=")] Tassign,
            [LexemeName("==")] Teq,
            [LexemeName("<")] Tlt,
            [LexemeName("<=")] Tle,
            [LexemeName("<")] Tgt,
            [LexemeName("<=")] Tge,
            [LexemeName("!=")] Tne = 70,
            [LexemeName("&&")] Tand = 80,
            [LexemeName("||")] Tor,
            [LexemeName("!")] Tnot,
            [LexemeName("end")] Tend = 100,
            [LexemeName("err")] Terr = 200
            //code to get name by Lexeme
            //Lexeme a = Lexeme.Twhile;
            //var name_a = a.GetType().GetCustomAttribute<LexemeName>()?.name;
            //output => "while"
        }

        public Dictionary<string, Lexeme> KeyWords = new Dictionary<string, Lexeme>() {             //список ключевых слов
            { "while", Lexeme.Twhile },
            { "int", Lexeme.Tint },
            { "bool", Lexeme.Tbool},
            { "true", Lexeme.Ttrue },
            { "false", Lexeme.Tfalse },
            { "return", Lexeme.Treturn }
        };

        public class LEX                                        //лексема
        {
            public char[] lex;
            public LEX(string lex = "") { this.lex = lex.ToCharArray(); }
            public LEX(char[] lex) { this.lex = lex; }

            public void Clear() { lex = Array.Empty<char>(); }

            public void Append(char elem)
            {
                Array.Resize(ref lex, lex.Length + 1);
                lex[lex.Length - 1] = elem;
            }
        }


        public IM t;                                           //Исходный модуль
        public int abs_p = 0;                                  //номер символа сканирования
        public (int row, int pos) p = (1, 1);                  //(pointer) Указатель на текущий символ

        public LEX le = new LEX();                      //Изображение лексемы

        /// <summary>
        /// конструкторы класса
        /// </summary>
        public LexicalAnalyzer(string text) { t = new IM(text); }
        public LexicalAnalyzer(char[] text) { t = new IM(text); }
        ///

        void Next() //переход к следующей позиции исходного модуля
        {
            if (t.im[abs_p] == '\n')
            {
                p.row++;
                p.pos = 0;
            }
            else p.pos++;
            abs_p++;
        }
        void Skip() //пропуск незначащих символов
        {
            while (t.im[abs_p] == ' ' ||
                t.im[abs_p] == '\n' ||
                t.im[abs_p] == '\t' ||
                t.im[abs_p] == '\r') Next();
        }

        public Lexeme Scaner(LEX l)
        {
            l.Clear();                                             //очищаем поле лексемы
            Skip();                                                //пропускаем незначащие символы
            if (t.im[abs_p] == '\0') return Lexeme.Tend;   //возврат конца исходного модуля
            if (char.IsLetter(t.im[abs_p]) || t.im[abs_p] == '_')
            {
                while (char.IsLetter(t.im[abs_p]) ||
                    t.im[abs_p] == '_' ||
                    char.IsDigit(t.im[abs_p]))
                {
                    l.Append(t.im[abs_p]);          //изменяем лексему
                    Next();                                 //передвигаем указатель
                }
                return KeyWords.ContainsKey(new string(l.lex)) ? KeyWords[new string(l.lex)] : Lexeme.Tiden;                      //Возвращение типа ключевого слова или идентификатора 
            }
            if (char.IsDigit(t.im[abs_p]))
            {
                l.Append(t.im[abs_p]);
                Next();                                     //переходим ко второму символу для проверки системы счисления
                if (t.im[abs_p] == 'x')
                {
                    l.Append(t.im[abs_p]);       //изменяем лексему
                    Next();
                    while (char.IsDigit(t.im[abs_p]))
                    {
                        l.Append(t.im[abs_p]);
                        Next();
                    }
                    return Lexeme.TconstIntHex;
                }
                else
                {
                    while (char.IsDigit(t.im[abs_p]))
                    {
                        l.Append(t.im[abs_p]);
                        Next();
                    }
                    return Lexeme.TconstInt;
                }

            }
            if (t.im[abs_p] == ',')
            {
                l.Append(t.im[abs_p]);
                Next();
                return Lexeme.Tcom;
            }
            if (t.im[abs_p] == ';')
            {
                l.Append(t.im[abs_p]);
                Next();
                return Lexeme.Tsem;
            }
            if (t.im[abs_p] == '{')
            {
                l.Append(t.im[abs_p]);
                Next();
                return Lexeme.TbraceOpen;
            }
            if (t.im[abs_p] == '}')
            {
                l.Append(t.im[abs_p]);
                Next();
                return Lexeme.TbraceClose;
            }
            if (t.im[abs_p] == '(')
            {
                l.Append(t.im[abs_p]);
                Next();
                return Lexeme.TbrackOpen;
            }
            if (t.im[abs_p] == ')')
            {
                l.Append(t.im[abs_p]);
                Next();
                return Lexeme.TbrackClose;
            }
            if (t.im[abs_p] == '+')
            {
                l.Append(t.im[abs_p]);
                Next();
                return Lexeme.Tplus;
            }
            if (t.im[abs_p] == '-')
            {
                l.Append(t.im[abs_p]);
                Next();
                return Lexeme.Tminus;
            }
            if (t.im[abs_p] == '*')
            {
                l.Append(t.im[abs_p]);
                Next();
                return Lexeme.Tmult;
            }
            if (t.im[abs_p] == '/')
            {
                l.Append(t.im[abs_p]);
                Next();
                return Lexeme.Tdiv;
            }
            if (t.im[abs_p] == '%')
            {
                l.Append(t.im[abs_p]);
                Next();
                return Lexeme.Tmod;
            }
            if (t.im[abs_p] == '=')
            {
                l.Append(t.im[abs_p]);
                Next();
                if (t.im[abs_p] == '=')
                {
                    l.Append(t.im[abs_p]);
                    Next();
                    return Lexeme.Teq;
                }
                else return Lexeme.Tassign;
            }
            if (t.im[abs_p] == '<')
            {
                l.Append(t.im[abs_p]);
                Next();
                if (t.im[abs_p] == '=')
                {
                    l.Append(t.im[abs_p]);
                    Next();
                    return Lexeme.Tle;
                }
                else return Lexeme.Tlt;
            }
            if (t.im[abs_p] == '>')
            {
                l.Append(t.im[abs_p]);
                Next();
                if (t.im[abs_p] == '=')
                {
                    l.Append(t.im[abs_p]);
                    Next();
                    return Lexeme.Tge;
                }
                else return Lexeme.Tgt;
            }
            if (t.im[abs_p] == '!')
            {
                l.Append(t.im[abs_p]);
                Next();
                if (t.im[abs_p] == '=')
                {
                    l.Append(t.im[abs_p]);
                    Next();
                    return Lexeme.Tne;
                }
                else return Lexeme.Tnot;
            }
            if (t.im[abs_p] == '&')
            {
                l.Append(t.im[abs_p]);
                Next();
                if (t.im[abs_p] == '&')
                {
                    l.Append(t.im[abs_p]);
                    Next();
                    return Lexeme.Tand;
                }
                else return Lexeme.Terr;
            }
            if (t.im[abs_p] == '|')
            {
                l.Append(t.im[abs_p]);
                Next();
                if (t.im[abs_p] == '|')
                {
                    l.Append(t.im[abs_p]);
                    Next();
                    return Lexeme.Tor;
                }
                else return Lexeme.Terr;
            }
            return Lexeme.Terr;                                     //возвращает код ошибки, если не найдено соответствия в языке
        }

        public (Lexeme[] types, string[] words) StartScan()
        {
            Lexeme typ = 0;
            Lexeme[] result = Array.Empty<Lexeme>();
            string[] output = Array.Empty<string>();
            while (typ != Lexeme.Tend)
            {
                Array.Resize(ref result, result.Length + 1);
                if (typ == Lexeme.Terr) throw new Exception(
                    $"error at lexem \"{new string(le.lex != Array.Empty<char>() ? le.lex : new char[] { t.im[abs_p] })}\"" +
                    $" on row {p.row}");
                typ = Scaner(le);
                result[result.Length - 1] = typ;

                Array.Resize(ref output, output.Length + 1);
                output[output.Length - 1] = new string(le.lex);
            }
            return (result, output);
        }
    }

    //проверка
    class Test
    {
        //static LexicalAnalyzer lex_analyzer = new LexicalAnalyzer(File.ReadAllText(@"D:\\projects\\context-free_grammar\\Implementing_the_Lexical_Analyzer_Program\\Lexical_Analyzer\\Lexical_Analyzer\\main.cpp"));
        static SyntacticalAnalyzer syn_analyzer = new SyntacticalAnalyzer(File.ReadAllText(@"D:\\projects\\context-free_grammar\\Implementing_the_Lexical_Analyzer_Program\\Lexical_Analyzer\\Lexical_Analyzer\\mainErr.cpp"));
        static void Main(string[] args)
        {
            try
            {
                //(LexicalAnalyzer.Lexeme[] types, string[] words) test = lex_analyzer.StartScan();
                if (syn_analyzer.Analyze()) Console.WriteLine("проблем не обнаружено");
                //Console.WriteLine("Text");
                //foreach (string word in test.words) Console.Write(word + " ");
                //Console.WriteLine("\nTypes");
                //foreach (int type in test.types) Console.Write(type + " ");
            }
            catch (AnalyzeException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

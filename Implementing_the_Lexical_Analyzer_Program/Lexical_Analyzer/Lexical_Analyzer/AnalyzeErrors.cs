using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Lexical_Analyzer.LexicalAnalyzer;

namespace Lexical_Analyzer
{
    class AnalyzeException : Exception
    {
        new public string? Message = null;

        protected AnalyzeException() { }

        public AnalyzeException(char[] detected, string expected, int row, int pos) : base()
        {
            Message = $"обнаружено: {new string(detected)}, ожидалось: {expected}, стрка: {row}, символ: {pos}";
        }
    }

    //так-же DescriptionException, DataException, FunctionException, VariableDeclarationListException
    class TypeException : AnalyzeException
    {
        public TypeException(char[] detected, int row, int pos) : base(detected, "тип", row, pos) { }
    }

    //так-же VariableListException, AssignmentException, CallFunctionException
    class IdentifierException : AnalyzeException
    {
        public IdentifierException(char[] detected, int row, int pos) : base(detected, "идентификатор", row, pos) { }
    }

    //так-же AndException, EqualityException, СomparisonException, AdditionSubtractionException,
    //MultiplicationDivisionException, UnarySignsException
    class ExpressionException : AnalyzeException
    {
        public ExpressionException(char[] detected, int row, int pos) : base(detected, "выражение", row, pos) { }
    }

    class ConstException : AnalyzeException
    {
        public ConstException(char[] detected, int row, int pos) : base(detected, "константа", row, pos) { }
    }

    class CompoundOperatorException : AnalyzeException
    {
        public CompoundOperatorException(char[] detected, int row, int pos) : base(detected, "{", row, pos) { }
    }

    //Так-же OperatorDataException, WhileException
    class OperatorException : AnalyzeException
    {
        public OperatorException(char[] detected, int row, int pos) : base(detected, "оператор", row, pos) { }
    }

    class OperandException : AnalyzeException
    {
        public OperandException(char[] detected, int row, int pos) : base(detected, "операнд", row, pos) { }
    }

    class EndStringException : AnalyzeException
    {
        public EndStringException(char[] detected, int row, int pos) : base(detected, ";", row, pos) { }
    }
}

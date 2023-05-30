using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Lexical_Analyzer.LexicalAnalyzer;

namespace Lexical_Analyzer
{
    internal class SyntacticalAnalyzer
    {
        //Programm
        static LexicalAnalyzer lexical_analyzer;

        public SyntacticalAnalyzer(string text)
        {
            lexical_analyzer = new LexicalAnalyzer(text);
        }

        Lexeme typ;

        public void ExpressionList()
        {
            do
            {
                if (typ == Lexeme.Tcom) typ = lexical_analyzer.Scaner(lexical_analyzer.le);
                if (First.Expression(typ)) Expression();
            } while (typ == Lexeme.Tcom);
        }

        public void CallFunction()
        {

            if (typ == Lexeme.Tiden) typ = lexical_analyzer.Scaner(lexical_analyzer.le);
            else throw new IdentifierException(lexical_analyzer.le.lex, lexical_analyzer.p.row, lexical_analyzer.p.pos);

            if (typ == Lexeme.TbrackOpen) typ = lexical_analyzer.Scaner(lexical_analyzer.le);
            else throw new AnalyzeException(lexical_analyzer.le.lex, "(", lexical_analyzer.p.row, lexical_analyzer.p.pos);

            if (First.ExpressionList(typ)) ExpressionList();

            if (typ == Lexeme.TbrackClose) typ = lexical_analyzer.Scaner(lexical_analyzer.le);
            else throw new AnalyzeException(lexical_analyzer.le.lex, ")", lexical_analyzer.p.row, lexical_analyzer.p.pos);
        }

        public void While()
        {
            if (typ == Lexeme.Twhile) typ = lexical_analyzer.Scaner(lexical_analyzer.le);
            else throw new OperatorException(lexical_analyzer.le.lex, lexical_analyzer.p.row, lexical_analyzer.p.pos);

            if (typ == Lexeme.TbrackOpen) typ = lexical_analyzer.Scaner(lexical_analyzer.le);
            else throw new AnalyzeException(lexical_analyzer.le.lex, "(", lexical_analyzer.p.row, lexical_analyzer.p.pos);

            if (First.Expression(typ)) Expression();
            else throw new ExpressionException(lexical_analyzer.le.lex, lexical_analyzer.p.row, lexical_analyzer.p.pos);

            if (typ == Lexeme.TbrackClose) typ = lexical_analyzer.Scaner(lexical_analyzer.le);
            else throw new AnalyzeException(lexical_analyzer.le.lex, ")", lexical_analyzer.p.row, lexical_analyzer.p.pos);
        }

        public void Assignment()
        {
            if (typ == Lexeme.Tiden) typ = lexical_analyzer.Scaner(lexical_analyzer.le);
            else throw new IdentifierException(lexical_analyzer.le.lex, lexical_analyzer.p.row, lexical_analyzer.p.pos);

            if (typ == Lexeme.Tassign) typ = lexical_analyzer.Scaner(lexical_analyzer.le);
            else throw new AnalyzeException(lexical_analyzer.le.lex, "=", lexical_analyzer.p.row, lexical_analyzer.p.pos);

            if (First.Expression(typ)) Expression();
            else throw new ExpressionException(lexical_analyzer.le.lex, lexical_analyzer.p.row, lexical_analyzer.p.pos);

            if (typ == Lexeme.Tsem) typ = lexical_analyzer.Scaner(lexical_analyzer.le);
            else throw new EndStringException(lexical_analyzer.le.lex, lexical_analyzer.p.row, lexical_analyzer.p.pos);
        }

        public void Operator()
        {
            if (First.Assignment(typ)) Assignment();
            else if (First.While(typ)) While();
            else if (First.CompoundOperator(typ)) CompoundOperator();
            else if (typ == Lexeme.Tsem) typ = lexical_analyzer.Scaner(lexical_analyzer.le);
            else throw new OperandException(lexical_analyzer.le.lex, lexical_analyzer.p.row, lexical_analyzer.p.pos);
        }

        public void OperatorData()
        {
            while (First.OperatorData(typ))
                if (First.Operator(typ)) Operator();
                else if (First.Data(typ)) Data();
                //возможен пустой выход
        }

        public void CompoundOperator()
        {
            if (typ == Lexeme.TbraceOpen) typ = lexical_analyzer.Scaner(lexical_analyzer.le);
            else throw new CompoundOperatorException(lexical_analyzer.le.lex, lexical_analyzer.p.row, lexical_analyzer.p.pos);
            

            if (First.OperatorData(typ)) OperatorData();

            if (typ == Lexeme.TbraceClose) typ = lexical_analyzer.Scaner(lexical_analyzer.le);
            else throw new AnalyzeException(lexical_analyzer.le.lex, "}", lexical_analyzer.p.row, lexical_analyzer.p.pos);
            
        }

        public void VariableDeclarationList()
        {
            do
            {
                if (First.Type(typ))
                {
                    Type();
                    if (First.Variable(typ)) Variable();
                    else throw new IdentifierException(lexical_analyzer.le.lex, lexical_analyzer.p.row, lexical_analyzer.p.pos);
                }
            } while (typ == Lexeme.Tcom);
        }

        public void Function()
        {
            if (First.Type(typ)) Type();
            else throw new TypeException(lexical_analyzer.le.lex, lexical_analyzer.p.row, lexical_analyzer.p.pos);

            if (typ == Lexeme.Tiden) typ = lexical_analyzer.Scaner(lexical_analyzer.le);
            else throw new IdentifierException(lexical_analyzer.le.lex, lexical_analyzer.p.row, lexical_analyzer.p.pos);

            if (typ == Lexeme.TbrackOpen) typ = lexical_analyzer.Scaner(lexical_analyzer.le);
            else throw new AnalyzeException(lexical_analyzer.le.lex, "(", lexical_analyzer.p.row, lexical_analyzer.p.pos);

            //может быть пустым
            if (First.VariableDeclarationList(typ)) VariableDeclarationList();

            if (typ == Lexeme.TbrackClose) typ = lexical_analyzer.Scaner(lexical_analyzer.le);
            else throw new AnalyzeException(lexical_analyzer.le.lex, ")", lexical_analyzer.p.row, lexical_analyzer.p.pos);
            

            if (typ == Lexeme.TbraceOpen) typ = lexical_analyzer.Scaner(lexical_analyzer.le);
            else throw new CompoundOperatorException(lexical_analyzer.le.lex, lexical_analyzer.p.row, lexical_analyzer.p.pos);

            while (typ != Lexeme.Treturn) 
            {
                if (First.OperatorData(typ)) OperatorData();
            }

            if (typ == Lexeme.Treturn) typ = lexical_analyzer.Scaner(lexical_analyzer.le); 
            else throw new AnalyzeException(lexical_analyzer.le.lex, "return", lexical_analyzer.p.row, lexical_analyzer.p.pos);
            

            if (First.Expression(typ)) Expression();
            else throw new ExpressionException(lexical_analyzer.le.lex, lexical_analyzer.p.row, lexical_analyzer.p.pos);

            if (typ == Lexeme.Tsem) typ = lexical_analyzer.Scaner(lexical_analyzer.le);
            else throw new AnalyzeException(lexical_analyzer.le.lex, ";", lexical_analyzer.p.row, lexical_analyzer.p.pos);

            if (typ == Lexeme.TbraceClose) typ = lexical_analyzer.Scaner(lexical_analyzer.le);
            else throw new AnalyzeException(lexical_analyzer.le.lex, "}", lexical_analyzer.p.row, lexical_analyzer.p.pos);
            
        }

        public void Const()
        {
            if (First.Const(typ)) typ = lexical_analyzer.Scaner(lexical_analyzer.le);
            else throw new ConstException(lexical_analyzer.le.lex, lexical_analyzer.p.row, lexical_analyzer.p.pos);
            
        }

        public void Operand()
        {
            if (First.Const(typ)) Const();
            else if (First.CallFunction(typ) && Continue.CallFunction(lexical_analyzer)) CallFunction();
            else if (First.Variable(typ)) Variable();
            else if (typ == Lexeme.TbrackOpen)
            {
                typ = lexical_analyzer.Scaner(lexical_analyzer.le);
                if (First.Expression(typ)) Expression();
                else throw new ExpressionException(lexical_analyzer.le.lex, lexical_analyzer.p.row, lexical_analyzer.p.pos);

                if (typ == Lexeme.TbrackClose) typ = lexical_analyzer.Scaner(lexical_analyzer.le);
                else throw new AnalyzeException(lexical_analyzer.le.lex, ")", lexical_analyzer.p.row, lexical_analyzer.p.pos);
            }
            else throw new ExpressionException(lexical_analyzer.le.lex, lexical_analyzer.p.row, lexical_analyzer.p.pos);
        }

        public void UnarySigns()
        {
            if (typ == Lexeme.Tplus || typ == Lexeme.Tminus ||
            typ == Lexeme.Tnot) typ = lexical_analyzer.Scaner(lexical_analyzer.le);
            if (First.Operand(typ)) Operand();
            else throw new OperandException(lexical_analyzer.le.lex, lexical_analyzer.p.row, lexical_analyzer.p.pos);
        }

        public void MultiplicationDivision()
        {
            do
            {
                if (typ == Lexeme.Tmult || typ == Lexeme.Tdiv ||
                    typ == Lexeme.Tmod) typ = lexical_analyzer.Scaner(lexical_analyzer.le);
                if (First.UnarySigns(typ)) UnarySigns();
            } while (typ == Lexeme.Tmult || typ == Lexeme.Tdiv ||
            typ == Lexeme.Tmod);
        }

        public void AdditionSubtraction()
        {
            do
            {
                if (typ == Lexeme.Tplus || typ == Lexeme.Tminus) typ = lexical_analyzer.Scaner(lexical_analyzer.le);
                if (First.MultiplicationDivision(typ)) MultiplicationDivision();
            } while (typ == Lexeme.Tplus || typ == Lexeme.Tminus);
        }

        public void Сomparison()
        {
            do
            {
                if (typ == Lexeme.Tlt || typ == Lexeme.Tgt ||
                    typ == Lexeme.Tle || typ == Lexeme.Tge) typ = lexical_analyzer.Scaner(lexical_analyzer.le);
                if (First.AdditionSubtraction(typ)) AdditionSubtraction();
            } while (typ == Lexeme.Tlt || typ == Lexeme.Tgt ||
            typ == Lexeme.Tle || typ == Lexeme.Tge);
        }

        public void Equality()
        {
            do
            {
                if (typ == Lexeme.Teq || typ == Lexeme.Tne) typ = lexical_analyzer.Scaner(lexical_analyzer.le);
                if (First.Сomparison(typ)) Сomparison();
            } while (typ == Lexeme.Teq || typ == Lexeme.Tne);
        }

        public void And()
        {
            do
            {
                if (typ == Lexeme.Tand) typ = lexical_analyzer.Scaner(lexical_analyzer.le);
                if (First.Equality(typ)) Equality();
            } while (typ == Lexeme.Tand);
        }

        public void Expression()
        {
            do
            {
                if (typ == Lexeme.Tor) typ = lexical_analyzer.Scaner(lexical_analyzer.le);
                if (First.And(typ)) And();
            } while (typ == Lexeme.Tor);
        }

        public void Variable()
        {
            if (typ == Lexeme.Tiden) typ = lexical_analyzer.Scaner(lexical_analyzer.le);
            else throw new IdentifierException(lexical_analyzer.le.lex, lexical_analyzer.p.row, lexical_analyzer.p.pos);
            if (typ == Lexeme.Tassign)
            {
                typ = lexical_analyzer.Scaner(lexical_analyzer.le);
                if (First.Expression(typ)) Expression();
                else throw new ExpressionException(lexical_analyzer.le.lex, lexical_analyzer.p.row, lexical_analyzer.p.pos);
            }
        }

        public void VariableList()
        {
            do
            {
                if (typ == Lexeme.Tcom) typ = lexical_analyzer.Scaner(lexical_analyzer.le);
                if (First.Variable(typ)) Variable();
            } while (typ == Lexeme.Tcom);
        }

        public void Type()
        {
            //<тип> -> int | bool
            if (First.Type(typ)) typ = lexical_analyzer.Scaner(lexical_analyzer.le);
            else throw new TypeException(lexical_analyzer.le.lex, lexical_analyzer.p.row, lexical_analyzer.p.pos);
        }

        public void Data()
        {
            //<данные> -> <тип><список переменных>;<данные> | <тип><список переменных>;
            if (First.Type(typ)) Type();
            else throw new TypeException(lexical_analyzer.le.lex, lexical_analyzer.p.row, lexical_analyzer.p.pos);

            if (First.VariableList(typ)) VariableList();
            else throw new AnalyzeException(lexical_analyzer.le.lex, "Список переменных", lexical_analyzer.p.row, lexical_analyzer.p.pos);
                
            if (typ == Lexeme.Tsem) typ = lexical_analyzer.Scaner(lexical_analyzer.le);  
            else throw new EndStringException(lexical_analyzer.le.lex, lexical_analyzer.p.row, lexical_analyzer.p.pos);
        }

        public void Description()
        {
            //<описание> -> <данные> | <функция>
            //проверка условия для идентификации присвоения переменной или объявления функции
            if (First.Data(typ) && Continue.Data(lexical_analyzer)) Data();
            else if (First.Function(typ) && Continue.Function(lexical_analyzer)) Function();
            else throw new TypeException(lexical_analyzer.le.lex, lexical_analyzer.p.row, lexical_analyzer.p.pos);
        }


        //Programm
        public bool Analyze()
        {
            //<программа> -> <описание><программа> | ε
            typ = lexical_analyzer.Scaner(lexical_analyzer.le);
            do
            {
                Description();
            } while (typ != Lexeme.Tend && typ != Lexeme.Terr);
            return true;
        }
    }
}

class LexicalAnalyzer
{
    static int MaxText = 1048576;                   //максимальная длинна текста ИМ
    static int MaxLex = 1048576;                    //максимальная длинна лексемы
    class IM                                        //текст ИМ
    {
        public char[] im = new char[MaxText];
        public IM(string text = "") { this.im = text.ToCharArray(); }
        public IM(char[] text) { this.im = text; }
    }

    class LEX                                       //лексема
    {
        public char[] lex = new char[MaxLex];
        public LEX(string lex = "") { this.lex = lex.ToCharArray(); }
        public LEX(char[] lex) { this.lex = lex; }

        public void Clear() { for (int i = 0; i < MaxLex; i++) lex[i] = '\0'; }
    }

    IM t = new IM("");                              //Исходный модуль
    (int row, int pos) p = (0, 0);                  //(pointer) Указатель на текущий символ

    int typ;                                        //Тип лексемы
    static LEX l;                                   //Изображение лексемы

    void Next() //переход к следующей позиции исходного модуля
    {
        if (t.im[p.row + p.pos] == '\n') p.row++;
        else p.pos++;
    }
    void Skip() //пропуск незначащих символов
    {
        while (t.im[p.row + p.pos] == ' ' ||
            t.im[p.row + p.pos] == '\n' ||
            t.im[p.row + p.pos] == '\t') Next();
    }

    int TypeKeyWord(LEX l)
    {
        switch (l.lex.ToString())
        {
            case "while": return 1;
            case "int": return 2;
            case "bool": return 3;
            case "true": return 4;
            case "false": return 5;
        }
        return 20;                                     //если не ключевое слово, то идентификатор
    }

    int Scaner(LEX l)
    {
        int i = 0;                                     //текущая длинна лексемы
        l.Clear();                                     //очищаем поле лексемы
        Skip();                                        //пропускаем незначащие символы
        if (t.im[p.row + p.pos] == '\0') return 100;   //возврат кода конца исходного модуля
        if (char.IsLetter(t.im[p.row + p.pos]) || t.im[p.row + p.pos] == '_')
        {
            while (char.IsLetter(t.im[p.row + p.pos]) ||
                t.im[p.row + p.pos] == '_' ||
                char.IsDigit(t.im[p.row + p.pos]))
            {
                l.lex[i++] = t.im[p.row + p.pos];       //изменяем лексему
                Next();                                 //передвигаем указатель
            }
            return TypeKeyWord(l);                      //Возвращение типа ключевого слова или идентификатора 
        }
        if (char.IsDigit(t.im[p.row + p.pos]))
        {
            l.lex[i++] = t.im[p.row + p.pos];           //изменяем лексему
            Next();                                     //переходим ко второму символу для проверки системы счисления
            if (t.im[p.row + p.pos] == 'x')
            {
                l.lex[i++] = t.im[p.row + p.pos];       //изменяем лексему
                Next();
                while (char.IsDigit(t.im[p.row + p.pos]))
                {
                    l.lex[i++] = t.im[p.row + p.pos];
                    Next();
                }
                return 31;
            }
            else
            {
                while (char.IsDigit(t.im[p.row + p.pos]))
                {
                    l.lex[i++] = t.im[p.row + p.pos];
                    Next();
                }
                return 30;
            }
            
        }
        if (t.im[p.row + p.pos] == ',')
        {
            l.lex[i++] = t.im[p.row + p.pos];
            Next();
            return 40;
        }
        if (t.im[p.row + p.pos] == ';')
        {
            l.lex[i++] = t.im[p.row + p.pos];
            Next();
            return 41;
        }
        if (t.im[p.row + p.pos] == '{')
        {
            l.lex[i++] = t.im[p.row + p.pos];
            Next();
            return 42;
        }
        if (t.im[p.row + p.pos] == '}')
        {
            l.lex[i++] = t.im[p.row + p.pos];
            Next();
            return 43;
        }
        if (t.im[p.row + p.pos] == '(')
        {
            l.lex[i++] = t.im[p.row + p.pos];
            Next();
            return 44;
        }
        if (t.im[p.row + p.pos] == ')')
        {
            l.lex[i++] = t.im[p.row + p.pos];
            Next();
            return 45;
        }
        if (t.im[p.row + p.pos] == '+')
        {
            l.lex[i++] = t.im[p.row + p.pos];
            Next();
            return 50;
        }
        if (t.im[p.row + p.pos] == '-')
        {
            l.lex[i++] = t.im[p.row + p.pos];
            Next();
            return 51;
        }
        if (t.im[p.row + p.pos] == '*')
        {
            l.lex[i++] = t.im[p.row + p.pos];
            Next();
            return 52;
        }
        if (t.im[p.row + p.pos] == '/')
        {
            l.lex[i++] = t.im[p.row + p.pos];
            Next();
            return 53;
        }
        if (t.im[p.row + p.pos] == '%')
        {
            l.lex[i++] = t.im[p.row + p.pos];
            Next();
            return 54;
        }
        if (t.im[p.row + p.pos] == '=')
        {
            l.lex[i++] = t.im[p.row + p.pos];
            Next();
            if (t.im[p.row + p.pos] == '=')
            {
                l.lex[i++] = t.im[p.row + p.pos];
                Next();
                return 56;
            }
            else return 55;
        }
        if (t.im[p.row + p.pos] == '<')
        {
            l.lex[i++] = t.im[p.row + p.pos];
            Next();
            if (t.im[p.row + p.pos] == '=')
            {
                l.lex[i++] = t.im[p.row + p.pos];
                Next();
                return 58;
            }
            else return 57;
        }
        if (t.im[p.row + p.pos] == '>')
        {
            l.lex[i++] = t.im[p.row + p.pos];
            Next();
            if (t.im[p.row + p.pos] == '=')
            {
                l.lex[i++] = t.im[p.row + p.pos];
                Next();
                return 59;
            }
            else return 60;
        }
        if (t.im[p.row + p.pos] == '!')
        {
            l.lex[i++] = t.im[p.row + p.pos];
            Next();
            if (t.im[p.row + p.pos] == '=')
            {
                l.lex[i++] = t.im[p.row + p.pos];
                Next();
                return 70;
            }
            else return 82;
        }
        if (t.im[p.row + p.pos] == '&')
        {
            l.lex[i++] = t.im[p.row + p.pos];
            Next();
            if (t.im[p.row + p.pos] == '&')
            {
                l.lex[i++] = t.im[p.row + p.pos];
                Next();
                return 80;
            }
            else return 200;
        }
        if (t.im[p.row + p.pos] == '|')
        {
            l.lex[i++] = t.im[p.row + p.pos];
            Next();
            if (t.im[p.row + p.pos] == '|')
            {
                l.lex[i++] = t.im[p.row + p.pos];
                Next();
                return 81;
            }
            else return 200;
        }
        return 200;                                     //возвращает код ошибки, если не найдено соответствия в языке
    }
}
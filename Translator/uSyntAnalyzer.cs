using System;
using System.Collections.Generic;
using System.Text;
using Translator;

namespace nsSynt
{
    class uSyntAnalyzer
    {
        private String[] strFSource;
        private String[] strFMessage;
        public String[] strPSource { set { strFSource = value; } get { return strFSource; } }
        public String[] strPMessage { set { strFMessage = value; } get { return strFMessage; } }
        public CLex Lex = new CLex();

        public void S()
        {
            O();  // разбор правила O
            if (Lex.enumPToken == TToken.lxmSpace)
                A();  // если есть пробелы, вызываем A
        }

        public void A()
        {
            if (Lex.enumPToken == TToken.lxmSpace)  // разбор пробела
            {
                Lex.NextToken();
                if (Lex.enumPToken == TToken.lxmSETQ || Lex.enumPToken == TToken.lxmCL)
                    O();  // после пробела ожидается O
                else
                    A();  // или продолжаем разбор пробела
            }
        }

        public void O()
        {
            if (Lex.enumPToken == TToken.lxmOpenBracket)  // проверяем открывающую скобку
            {
                Lex.NextToken();  // переходим к следующему токену после скобки

                if (Lex.enumPToken == TToken.lxmSETQ)  // разбор SETQ
                {
                    Lex.NextToken();  // переходим после SETQ
                    if (Lex.enumPToken == TToken.lxmSpace)  // проверка на пробел
                    {
                        Lex.NextToken();
                        V();  // после пробела ожидается V (переменная или список)
                        if (Lex.enumPToken == TToken.lxmEndBracket)  // проверка на закрывающую скобку
                        {
                            Lex.NextToken();  // завершаем разбор SETQ
                        }
                        else throw new Exception("Ожидалась закрывающая скобка");
                    }
                    else throw new Exception("Ожидался пробел после SETQ");
                }
                else if (Lex.enumPToken == TToken.lxmCL)  // разбор COMMAND "LINE"
                {
                    Lex.NextToken();  // переходим к следующему токену после COMMAND
                    if (Lex.enumPToken == TToken.lxmIdentifier)  // ожидаем первый индефикатор
                    {
                        Lex.NextToken();
                        if (Lex.enumPToken == TToken.lxmIdentifier)  // ожидаем второй индефикатор
                        {
                            Lex.NextToken();
                            if (Lex.enumPToken == TToken.lxmEndBracket)  // проверка на закрывающую скобку
                            {
                                Lex.NextToken();  // завершаем разбор COMMAND "LINE"
                            }
                            else throw new Exception("Ожидалась закрывающая скобка");
                        }
                        else throw new Exception("Ожидался второй индефикатор для LINE");
                    }
                    else throw new Exception("Ожидался первый индефикатор для LINE");
                }
                else throw new Exception("Ожидался SETQ или COMMAND");
            }
            else throw new Exception("Ожидалась открывающая скобка");
        }

        public void V()
        {
            C();  // разбор C как часть V
            if (Lex.enumPToken == TToken.lxmSpace)  // проверяем на пробел для B
                B();  // если есть пробел, вызываем B
        }
        public void B()
        {
            if (Lex.enumPToken == TToken.lxmSpace)  // разбор пробела
            {
                Lex.NextToken();
                C();  // вызываем C после пробела
                if (Lex.enumPToken == TToken.lxmSpace)  // продолжаем разбор пробела
                    B();  // продолжаем разбор B
            }
        }
        public void C()
        {
            if (Lex.enumPToken == TToken.lxmIdentifier)  // ожидаем индефикатор
            {
                Lex.NextToken();
                if (Lex.enumPToken == TToken.lxmSpace)
                {
                    Lex.NextToken();
                    if (Lex.enumPToken == TToken.lxmNumber)  // ожидаем число 
                        Lex.NextToken();
                    else throw new Exception("Ожидался числовое значение");
                }
                else throw new Exception("Ожидался пробел");
            }
            else throw new Exception("Ожидалось индефикатор");
        }
    }
}

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
            O();
            if (Lex.enumPToken == TToken.lxmtz)
                A();
            throw new Exception("Конец слова, текст верный. Для продолжения ожидается ;");
        }
        public void A()
        {
            if (Lex.enumPToken == TToken.lxmtz)
            {
                Lex.NextToken();
                O();
                if (Lex.enumPToken == TToken.lxmtz)
                    A();
            }
        }
        public void O()
        {
                if (Lex.enumPToken == TToken.lxmIdentifier)
                {
                    Lex.NextToken();
                    if (Lex.enumPToken == TToken.lxmdt)
                    {
                        Lex.NextToken();
                        if (Lex.enumPToken == TToken.lxmr)
                        {
                            Lex.NextToken();
                            if (Lex.enumPToken == TToken.lxmls)
                            {
                                Lex.NextToken();
                                B();

                                if (Lex.enumPToken == TToken.lxmrs)
                                {
                                    Lex.NextToken();
                                }
                                else throw new Exception("Ожидалась , или ]");
                            }
                            else throw new Exception("Ожидалась [");
                        }
                        else throw new Exception("Ожидалось =");
                    }
                    else throw new Exception("Ожидалось :");
                }
                else throw new Exception("Ожидался идентификатор");
            }
            public void B()
            {
                if (Lex.enumPToken == TToken.lxmNumber)
                {
                    Lex.NextToken();
                    if (Lex.enumPToken == TToken.lxmComma)
                    {
                        Lex.NextToken();
                        B();
                    }
                }
                else throw new Exception("Ожидался идентификатор");
            }
        }
    }

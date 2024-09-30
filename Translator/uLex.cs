using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translator
{
    // Перечисление состояний анализа
    public enum TState { Start, Continue, Finish }; // Тип состояния
    // Перечисление возможных типов символов
    public enum TCharType { EngLetter, RusLetter, Digit, SETQ, CL, EndRow, EndText, Space, Star, Slash, Exclamation, Equal, Semicolon, AnotherSymbol, OpenBracket, EndBracket, Colon, OpenSquadBracket, EndSquadBracket, Plus, Minus, Comma, Dot, NoInd }; // Тип символа
    // Перечисление типов токенов
    public enum TToken { lxmIdentifier, lxmSETQ, lxmCL, lxmSpace, lxmOpenBracket, lxmEndBracket, lxmNumber, lxmUnknown, lxmEmpty, lxmLeftParenth, lxmRightParenth, lxmIs, lxmDot, lxmComma, lxmText, lxmtz, lxmdt, lxmr, lxmrs, lxmls };

    public class CLex
    {
        // Поля класса
        private String[] strFSource;  // Массив строк, представляющий исходный текст
        private String[] strFMessage;  // Массив строк для сообщений (возможно, для вывода результатов)
        public TCharType enumFSelectionCharType; // Тип текущего символа
        public char chrFSelection; // Текущий символ
        private TState enumFState; // Текущее состояние анализатора
        private int intFSourceRowSelection; // Номер текущей строки
        private int intFSourceColSelection; // Номер текущей колонки в строке
        private String strFLexicalUnit; // Текущая лексическая единица
        private TToken enumFToken; // Текущий токен

        // Свойства для доступа к полям класса
        public String[] strPSource { set { strFSource = value; } get { return strFSource; } }
        public String[] strPMessage { set { strFMessage = value; } get { return strFMessage; } }
        public TState enumPState { set { enumFState = value; } get { return enumFState; } }
        public String strPLexicalUnit { set { strFLexicalUnit = value; } get { return strFLexicalUnit; } }
        public TToken enumPToken { set { enumFToken = value; } get { return enumFToken; } }
        public int intPSourceRowSelection { get { return intFSourceRowSelection; } set { intFSourceRowSelection = value; } }
        public int intPSourceColSelection { get { return intFSourceColSelection; } set { intFSourceColSelection = value; } }

        // Конструктор класса
        public CLex()
        {
        }

        // Метод получения текущего символа из источника
        public void GetSymbol()
        {
            // Проверяем, не вышли ли за пределы строки
            if (intFSourceColSelection > strFSource[intFSourceRowSelection].Length - 1)
            {
                intFSourceRowSelection++; // Переходим на следующую строку
                if (intFSourceRowSelection <= strFSource.Length - 1)
                {
                    // Если еще не конец текста, сбрасываем колонку и задаем символ конца строки
                    intFSourceColSelection = -1;
                    chrFSelection = '\0'; // Устанавливаем текущий символ в '\0'
                    enumFSelectionCharType = TCharType.EndRow; // Указываем, что достигнут конец строки
                    enumFState = TState.Continue; // Состояние анализа продолжается
                }
                else
                {
                    // Если достигли конца всего текста
                    chrFSelection = '\0'; // Устанавливаем текущий символ в '\0'
                    enumFSelectionCharType = TCharType.EndText; // Указываем, что достигнут конец текста
                    enumFState = TState.Finish; // Меняем состояние на "Finish"
                }
            }
            else
            {
                // Получаем текущий символ в строке
                chrFSelection = strFSource[intFSourceRowSelection][intFSourceColSelection];

                // Классифицируем символ
                if (chrFSelection == ' ') enumFSelectionCharType = TCharType.Space;
                else if (chrFSelection >= 'a' && chrFSelection <= 'z') enumFSelectionCharType = TCharType.EngLetter;
                else if (chrFSelection >= 'а' && chrFSelection <= 'я') enumFSelectionCharType = TCharType.RusLetter;
                else if (chrFSelection >= '0' && chrFSelection <= '9') enumFSelectionCharType = TCharType.Digit;
                else if (chrFSelection == 'S' || chrFSelection == 'E' || chrFSelection == 'T' || chrFSelection == 'Q') enumFSelectionCharType = TCharType.SETQ;
                else if (chrFSelection == 'C' || chrFSelection == 'E' || chrFSelection == 'O' || chrFSelection == 'M' || chrFSelection == 'A' || chrFSelection == 'N' || chrFSelection == 'D' || chrFSelection == 'L' || chrFSelection == 'I' || chrFSelection == '"') enumFSelectionCharType = TCharType.CL;
                else if (chrFSelection == '/') enumFSelectionCharType = TCharType.Slash;
                else if (chrFSelection == '*') enumFSelectionCharType = TCharType.Star;
                else if (chrFSelection == '!') enumFSelectionCharType = TCharType.Exclamation;
                else if (chrFSelection == '=') enumFSelectionCharType = TCharType.Equal;
                else if (chrFSelection == ';') enumFSelectionCharType = TCharType.Semicolon;
                else if (chrFSelection == '(') enumFSelectionCharType = TCharType.OpenBracket;
                else if (chrFSelection == ')') enumFSelectionCharType = TCharType.EndBracket;
                else if (chrFSelection == ':') enumFSelectionCharType = TCharType.Colon;
                else if (chrFSelection == '[') enumFSelectionCharType = TCharType.OpenSquadBracket;
                else if (chrFSelection == ']') enumFSelectionCharType = TCharType.EndSquadBracket;
                else if (chrFSelection == '+') enumFSelectionCharType = TCharType.Plus;
                else if (chrFSelection == '-') enumFSelectionCharType = TCharType.Minus;
                else if (chrFSelection == ',') enumFSelectionCharType = TCharType.Comma;
                else if (chrFSelection == '.') enumFSelectionCharType = TCharType.Dot;
                else if (chrFSelection == '^' || chrFSelection == '%' || chrFSelection == '@' || chrFSelection == '<' || chrFSelection == '>' || chrFSelection == '?')
                    enumFSelectionCharType = TCharType.AnotherSymbol; // Считаем эти символы как другие (AnotherSymbol)
                else enumFSelectionCharType = TCharType.NoInd; // Символ не распознан
                enumFState = TState.Continue; // Продолжаем анализ
            }
            intFSourceColSelection++; // Переходим к следующему символу
        }

        // Метод добавления символа к текущей лексической единице
        private void TakeSymbol()
        {
            char[] c = { chrFSelection }; // Создаем массив символов с текущим символом
            String s = new string(c); // Преобразуем массив символов в строку
            strFLexicalUnit += s; // Добавляем символ к текущей лексической единице
            GetSymbol(); // Получаем следующий символ
        }

        // Метод перехода к следующему токену
        public void NextToken()
        {
            strFLexicalUnit = ""; // Сбрасываем текущую лексическую единицу
            char[] allowedChars = { 'a', 'b', 'c', 'd' };
            // Начальная инициализация перед началом анализа
            if (enumFState == TState.Start)
            {
                intFSourceRowSelection = 0; // Устанавливаем начальную строку
                intFSourceColSelection = 0; // Устанавливаем начальный столбец
                GetSymbol(); // Получаем первый символ
            }

            // Пропуск комментариев (если встречается '//')
            if (chrFSelection == '/')
            {
                GetSymbol(); // Получаем следующий символ
                if (chrFSelection == '/')
                {
                    // Игнорируем все символы до конца строки
                    while (enumFSelectionCharType != TCharType.EndRow)
                        GetSymbol();
                    GetSymbol(); // Переходим к следующему символу после конца строки
                }
            }

            // Variant 13
            switch (enumFSelectionCharType)
            {
                case TCharType.EngLetter:
                    {
                    //         a    b    c    d
                    //   A   |BFin|BFin|BFin|BFin|
                    //  BFin |CFin|CFin|cFin|CFin|
                    //  CFin |  D |  D |  D |  D |
                    //   D   |Fin | Fin| Fin| Fin|
                    //  Fin  |    |    |    |    |
                    A:
                        {
                            if (allowedChars.Contains(chrFSelection))
                            {
                                TakeSymbol();
                                goto BFin;
                            }
                            else
                            {
                                enumFToken = TToken.lxmIdentifier;
                                return;
                            }
                        }
                    BFin:
                        {
                            if (allowedChars.Contains(chrFSelection))
                            {
                                TakeSymbol();
                                goto CFin;
                            }
                            else
                            {
                                enumFToken = TToken.lxmIdentifier;
                                return;
                            }
                        }
                    CFin:
                        {
                            if (allowedChars.Contains(chrFSelection))
                            {
                                TakeSymbol();
                                goto D;
                            }
                            else
                            {
                                enumFToken = TToken.lxmIdentifier;
                                return;
                            }
                        }

                    D:
                        {
                            if (allowedChars.Contains(chrFSelection))
                            {
                                TakeSymbol();
                                goto Fin;
                            }
                            else
                            {
                                enumFToken = TToken.lxmIdentifier;
                                return;
                            }
                        }
                    Fin:
                        {
                            enumFToken = TToken.lxmIdentifier;
                            return;
                        }
                    }
                case TCharType.Digit:
                    {
                    //           0     1  
                    //    A   |  B  |  C  |
                    //    B   |  D  |     |
                    //    C   |  E  |     |
                    //    D   |     |  A  |
                    //    E   |     |FFin |
                    //   FFin |     |  G  |
                    //    G   |  H  |     |
                    //    H   | FFin|     |

                        A:
                        if (chrFSelection == '0')
                        {
                            TakeSymbol();
                            goto B;
                        }
                        else if (chrFSelection == '1')
                        {
                            TakeSymbol();
                            goto C;
                        }
                        else throw new Exception("Ожидался 0 или 1");

                        B:
                        if (chrFSelection == '0')
                        {
                            TakeSymbol();
                            goto D;
                        }
                        else throw new Exception("Ожидался 0");

                        C:
                        if (chrFSelection == '0')
                        {
                            TakeSymbol();
                            goto E;
                        }
                        else throw new Exception("Ожидался 0");

                        D:
                        if (chrFSelection == '1')
                        {
                            TakeSymbol();
                            goto A;
                        }
                        else throw new Exception("Ожидалась 1");

                        E:
                        if (chrFSelection == '1')
                        {
                            TakeSymbol();
                            goto FFin;
                        }
                        else throw new Exception("Ожидалась 1");

                        FFin:
                        if (chrFSelection == '1')
                        {
                            TakeSymbol();
                            goto G;
                        }
                        else if (enumFSelectionCharType != TCharType.Digit) { enumFToken = TToken.lxmNumber; return; }
                        else throw new Exception("Ожидалась 1");

                        G:
                        if (chrFSelection == '1')
                        {
                            TakeSymbol();
                            goto H;
                        }
                        else throw new Exception("Ожидался 0");// 


                        H:
                        if (chrFSelection == '0')
                        {
                            TakeSymbol();
                            goto FFin;
                        }
                        else throw new Exception("Ожидался 0");
                    }
                // Создание токена служебного слова SETQ
                case TCharType.SETQ:
                    {
                        A:
                            if (chrFSelection == 'S')
                            {
                                TakeSymbol();
                                goto B;
                            }
                            else throw new Exception("Ожидался S");
                        B:
                            if (chrFSelection == 'E')
                            {
                                TakeSymbol();
                                goto C;
                            }
                            else throw new Exception("Ожидался E");
                        C:
                            if (chrFSelection == 'T')
                            {
                                TakeSymbol();
                                goto D;
                            }
                            else throw new Exception("Ожидался T");

                        D:
                            if (chrFSelection == 'Q')
                            {
                                TakeSymbol();
                                goto Fin;
                            }
                            else throw new Exception("Ожидался Q");

                        Fin:
                            {
                                enumFToken = TToken.lxmSETQ;
                                return;
                            }
                    }
                // Создание токена служебного слова COMMAND "LINE"
                case TCharType.CL:
                    {
                        A:
                            if (chrFSelection == 'C')
                            {
                                TakeSymbol();
                                goto B;
                            }
                            else throw new Exception("Ожидался C");
                        B:
                            if (chrFSelection == 'O')
                            {
                                TakeSymbol();
                                goto C;
                            }
                            else throw new Exception("Ожидался O");
                        C:
                            if (chrFSelection == 'M')
                            {
                                TakeSymbol();
                                goto D;
                            }
                            else throw new Exception("Ожидался M");

                        D:
                            if (chrFSelection == 'M')
                            {
                                TakeSymbol();
                                goto E;
                            }
                            else throw new Exception("Ожидался M");

                        E:
                            if (chrFSelection == 'A')
                            {
                                TakeSymbol();
                                goto F;
                            }
                            else throw new Exception("Ожидался A");

                        F:
                            if (chrFSelection == 'N')
                            {
                                TakeSymbol();
                                goto G;
                            }
                            else throw new Exception("Ожидался N");

                        G:
                            if (chrFSelection == 'D')
                            {
                                TakeSymbol();
                                goto H;
                            }
                            else throw new Exception("Ожидался D");

                        H:
                            if (chrFSelection == '"')
                            {
                                TakeSymbol();
                                goto I;
                            }
                            else throw new Exception("Ожидался \"");

                        I:
                            if (chrFSelection == 'L')
                            {
                                TakeSymbol();
                                goto J;
                            }
                            else throw new Exception("Ожидался L");

                        J:
                            if (chrFSelection == 'I')
                            {
                                TakeSymbol();
                                goto K;
                            }
                            else throw new Exception("Ожидался I");

                        K:
                            if (chrFSelection == 'N')
                            {
                                TakeSymbol();
                                goto L;
                            }
                            else throw new Exception("Ожидался N");

                        L:
                            if (chrFSelection == 'E')
                            {
                                TakeSymbol();
                                goto M;
                            }
                            else throw new Exception("Ожидался E");

                        M:
                            if (chrFSelection == '"')
                            {
                                TakeSymbol();
                                goto Fin;
                            }
                            else throw new Exception("Ожидался \"");

                        Fin:
                            {
                                enumFToken = TToken.lxmCL;
                                return;
                            }
                    }

                case TCharType.AnotherSymbol:
                    {
                        if (chrFSelection == '/')
                        {
                            GetSymbol();
                            if (chrFSelection == '/')
                            {
                                while (enumFSelectionCharType != TCharType.EndRow)
                                    GetSymbol();
                            }
                            GetSymbol();
                        }
                        break;
                    }

                // Добавление токенов
                case TCharType.Space:
                    if (chrFSelection == ' ')
                    {
                        enumFToken = TToken.lxmSpace;
                        GetSymbol();
                        return;
                    }
                    break ;    
                    
                case TCharType.OpenBracket:
                    if (chrFSelection == '(')
                    {
                        enumFToken = TToken.lxmOpenBracket;
                        GetSymbol();
                        return;
                    }
                    break ;

                case TCharType.EndBracket:
                    if (chrFSelection == ')')
                    {
                        enumFToken = TToken.lxmEndBracket;
                        GetSymbol();
                        return;
                    }
                    break ;                

                case TCharType.EndText:
                    {
                        enumFToken = TToken.lxmEmpty;
                        break;
                    }
            }
        }
    }
}

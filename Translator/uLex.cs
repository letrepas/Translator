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
    public enum TCharType { EngLetter, RusLetter, Digit, EndRow, EndText, Space, Star, Slash, Exclamation, Equal, Semicolon, AnotherSymbol, OpenBracket, EndBracket, Colon, OpenSquadBracket, EndSquadBracket, Plus, Minus, Comma, Dot, NoInd }; // Тип символа
    // Перечисление типов токенов
    public enum TToken { lxmIdentifier, lxmNumber, lxmUnknown, lxmEmpty, lxmLeftParenth, lxmRightParenth, lxmIs, lxmDot, lxmComma };

    // Класс лексического анализатора
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
                intFSourceColSelection = -1; // Устанавливаем начальный столбец
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
                    if (chrFSelection == '/')
                    {
                        GetSymbol();
                        if (chrFSelection == '/')
                            while (enumFSelectionCharType != TCharType.EndRow)
                            {
                                GetSymbol();
                            }
                        GetSymbol();
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
                        else throw new Exception("Ожидался 0");


                        H:
                        if (chrFSelection == '0')
                        {
                            TakeSymbol();
                            goto FFin;
                        }
                        else throw new Exception("Ожидался 0");

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
                        if (chrFSelection == '(')
                        {
                            enumFToken = TToken.lxmLeftParenth;
                            GetSymbol();
                            return;
                        }
                        if (chrFSelection == ')')
                        {
                            enumFToken = TToken.lxmRightParenth;
                            GetSymbol();
                            return;
                        }
                        break;
                    }
                case TCharType.EndText:
                    {
                        enumFToken = TToken.lxmEmpty;
                        break;
                    }
            }

        }
    }

}

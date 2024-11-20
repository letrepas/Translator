using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Translator;

namespace nsSynt
{
    class uSyntAnalyzer
    {
        public CLex Lex = new CLex();
        public Dictionary<string, int> initializedVariables = new Dictionary<string, int>();
        string lastVariable;
        int flagCount = 0;
        string lastValue;
        bool isValidSequence = true;
        List<int> points = new List<int>();
        private List<string> intermediateCode = new List<string>();
        int rCount = 0;
        public List<int> Points
        {
            get { return points; }  // Возвращаем текущий список точек
            set { points = value; }  // Устанавливаем новый список точек
        }
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
                        if (Lex.enumPToken == TToken.lxmEndBracket)
                        {
                            // Успешно завершили разбор SETQ
                        }
                        else throw new Exception("Ожидалась закрывающая скобка");
                    }
                    else throw new Exception("Ожидался пробел после SETQ");
                }
                else if (Lex.enumPToken == TToken.lxmCL)  // разбор COMMAND"LINE"
                {
                    Lex.NextToken();  // переходим к следующему токену после COMMAND
                    if (Lex.enumPToken == TToken.lxmSpace)  // проверка на пробел
                    {
                        Lex.NextToken();
                        if (Lex.enumPToken == TToken.lxmIdentifier)  // ожидаем первый индефикатор
                        {
                            string var1 = Lex.CurrentTokenValue();
                            if (!initializedVariables.ContainsKey(var1))
                                throw new Exception($"Переменная {var1} не инициализирована!");
                            int value1 = initializedVariables[var1];
                            Points.Add(value1);
                            Lex.NextToken();
                            if (Lex.enumPToken == TToken.lxmSpace)  // проверка на пробел
                            {
                                Lex.NextToken();
                                if (Lex.enumPToken == TToken.lxmIdentifier)  // ожидаем второй индефикатор
                                {
                                    string var2 = Lex.CurrentTokenValue();
                                    if (var1 == var2)
                                        throw new Exception("Идентификаторы var1 и var2 не должны быть одинаковыми!");
                                    if (!initializedVariables.ContainsKey(var2) && isValidSequence == true)
                                        throw new Exception($"Переменная {var2} не инициализирована!");
                                    if (isValidSequence)
                                    {
                                        int value2 = initializedVariables[var2];
                                        Points.Add(value2);
                                    }
                                    else
                                        throw new Exception($"Ошибка: Идентификатор не соответствует правилу");
                        
                                    intermediateCode.Add($"TEST {isValidSequence}");
                                    Lex.NextToken();
                                    if (Lex.enumPToken == TToken.lxmEndBracket)  // проверка на закрывающую скобку
                                        Lex.NextToken();  // завершаем разбор COMMAND "LINE"
                                    else throw new Exception("Ожидалась закрывающая скобка");
                                }
                                else throw new Exception("Ожидался второй индефикатор для LINE");
                            }
                            else throw new Exception("Ожидался пробел после первого индефикатора");
                        }
                        else throw new Exception("Ожидался первый индефикатор для LINE");
                    }
                    else throw new Exception("Ожидался пробел после COMMAND\"LINE\"");
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
                string identifier = Lex.CurrentTokenValue();
                // Генерация промежуточного кода
                intermediateCode.Add($"MOV {identifier} R{rCount++}");
                // Проверяем, был ли идентификатор уже инициализирован
                if (initializedVariables.ContainsKey(identifier))
                    throw new Exception($"Идентификатор {identifier} уже инициализирован!");
                Lex.NextToken();
                if (Lex.enumPToken == TToken.lxmSpace)
                {
                    Lex.NextToken();
                    if (Lex.enumPToken == TToken.lxmNumber)  // ожидаем число 
                    {
                        string binaryValue = Lex.CurrentTokenValue();

                        // Семантическая проверка для переменных из одной буквы (a, b, c, d)
                        if (identifier.Length == 1)
                        {
                            switch (identifier)
                            {
                                case "a":
                                    if (!binaryValue.StartsWith("00"))
                                        throw new Exception("Значение переменной 'a' должно начинаться с '00'.");
                                    break;
                                case "b":
                                    int onesCount = binaryValue.Count(c => c == '1');
                                    if (onesCount % 2 != 0)
                                        throw new Exception("Значение переменной 'b' должно содержать четное количество единиц.");
                                    break;
                                case "c":
                                    if (binaryValue.Length < 9)
                                        throw new Exception("Значение переменной 'c' должно быть не менее 9 символов.");
                                    break;
                                case "d":
                                    throw new Exception("Переменной 'd' запрещено присваивать значение.");
                                default:
                                    // Если буква неизвестная, считаем значение корректным
                                    break;
                            }
                        }

                        if (flagCount == 1)
                        {
                            if (lastVariable == "a" && lastValue.ToString().StartsWith("001001"))
                            {
                                if (!(new List<string> { "ac", "ad", "acc" }).Contains(identifier))
                                {
                                    isValidSequence = false;
                                    throw new Exception("Ошибка в перввом правиле");
                                }
                            }
                            else if (lastVariable == "a" && lastValue.StartsWith("00"))
                            {
                                if (!(new List<string> { "abc", "ab", "abcd", "aacd" }).Contains(identifier))
                                {
                                    isValidSequence = false;
                                    throw new Exception("Ошибка во втором правиле");
                                }
                            }
                            else if ((new List<string> { "ac", "ad", "acc" }).Contains(lastVariable) && lastValue.StartsWith("001001"))
                            {
                                if (identifier != "a")
                                {
                                    isValidSequence = false;
                                    throw new Exception("Ошибка в третьем правиле");
                                }
                            }
                            else if ((new List<string> { "abc", "ab", "abcd", "aacd" }).Contains(lastVariable) && lastValue.StartsWith("00"))
                            {
                                if (identifier != "a")
                                {
                                    isValidSequence = false;
                                    throw new Exception("Ошибка в четвертом правиле");
                                }
                            }
                        }

                        if (isValidSequence)
                        {
                            intermediateCode.Add($"MOV {binaryValue} R{rCount++}");
                            flagCount++;
                            lastVariable = identifier;
                            lastValue = binaryValue;
                            // Преобразуем значение токена в десятичное число и инициализируем переменную
                            int value = ConvertBinaryToDecimal(binaryValue);
                            initializedVariables[identifier] = value;
                            Lex.NextToken();
                        }
                        else
                        {
                            Lex.NextToken();
                            flagCount = 0;
                        }
                            
                    }
                    else throw new Exception("Ожидался числовое значение");
                }
                else throw new Exception("Ожидался пробел");
            }
            else throw new Exception("Ожидалось индефикатор");
        }
        public int ConvertBinaryToDecimal(string binaryCode)
        {
            return Convert.ToInt32(binaryCode, 2);
        }
        public List<string> GetIntermediateCode()
        {
            return new List<string>(intermediateCode);
        }
    }
}

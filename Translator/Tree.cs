using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Windows.Forms;  // Для работы с TreeView
using Translator;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using TreeView = System.Windows.Forms.TreeView;

namespace Tree
{
    public class TreeV
    {
        public CLex Lex = new CLex();
        public TreeView tree;
        public Dictionary<string, int> initializedVariables = new Dictionary<string, int>();
        public string lastVariable;
        int flagCount = 0;
        public string lastValue;
        public bool isValidSequence = true;
        public string identifier;
        public string firstSymbol;
        public string firstValue;
        public TreeV(TreeView treeView)
        {
            tree = treeView;
        }

        public void S()
        {
            TreeNode sNode = new TreeNode("S");
            tree.Nodes.Add(sNode);
            O(sNode);  // разбор правила O
            if (Lex.enumPToken == TToken.lxmSpace)
                A(sNode);  // если есть пробелы, вызываем A
        }

        public void A(TreeNode parentNode)
        {
            TreeNode aNode = new TreeNode("A");
            parentNode.Nodes.Add(aNode);

            if (Lex.enumPToken == TToken.lxmSpace)  // разбор пробела
            {
                Lex.NextToken();
                if (Lex.enumPToken == TToken.lxmSETQ || Lex.enumPToken == TToken.lxmCL)
                    O(aNode);  // после пробела ожидается O
                else
                    A(aNode);  // или продолжаем разбор пробела
            }
        }

        public void O(TreeNode parentNode)
        {
            TreeNode oNode = new TreeNode("O");
            parentNode.Nodes.Add(oNode);

            if (Lex.enumPToken == TToken.lxmOpenBracket)  // проверяем открывающую скобку
            {
                Lex.NextToken();  // переходим к следующему токену после скобки
                TreeNode openBracketNode = new TreeNode("(");
                oNode.Nodes.Add(openBracketNode);

                if (Lex.enumPToken == TToken.lxmSETQ)  // разбор SETQ
                {
                    TreeNode setqNode = new TreeNode("SETQ");
                    oNode.Nodes.Add(setqNode);
                    Lex.NextToken();
                    if (Lex.enumPToken == TToken.lxmSpace)
                    {
                        Lex.NextToken();
                        V(oNode);  // после пробела ожидается V
                        if (Lex.enumPToken == TToken.lxmEndBracket)
                        {
                            TreeNode closeBracketNode = new TreeNode(")");
                            oNode.Nodes.Add(closeBracketNode);
                        }
                        else throw new Exception("Ожидалась закрывающая скобка");
                    }
                    else throw new Exception("Ожидался пробел после SETQ");
                }
                else if (Lex.enumPToken == TToken.lxmCL)  // разбор COMMAND "LINE"
                {
                    TreeNode commandNode = new TreeNode("COMMAND \"LINE\"");
                    oNode.Nodes.Add(commandNode);
                    Lex.NextToken();
                    if (Lex.enumPToken == TToken.lxmSpace)
                    {
                        Lex.NextToken();
                        if (Lex.enumPToken == TToken.lxmIdentifier)
                        {
                            string var1 = Lex.CurrentTokenValue();
                            if (!initializedVariables.ContainsKey(var1))
                                throw new Exception($"Переменная {var1} не инициализирована!");

                            TreeNode idNode1 = new TreeNode(Lex.strPLexicalUnit);
                            oNode.Nodes.Add(idNode1);
                            Lex.NextToken();
                            if (Lex.enumPToken == TToken.lxmSpace)
                            {
                                Lex.NextToken();
                                if (Lex.enumPToken == TToken.lxmIdentifier)
                                {

                                    string var2 = Lex.CurrentTokenValue();
                                    if (var1 == var2)
                                        throw new Exception("Идентификаторы var1 и var2 не должны быть одинаковыми!");
                                    if (!initializedVariables.ContainsKey(var2) && isValidSequence == true)
                                        throw new Exception($"Переменная {var2} не инициализирована!");
                                    if (isValidSequence)
                                    {
                                        TreeNode idNode2 = new TreeNode(Lex.strPLexicalUnit);
                                        oNode.Nodes.Add(idNode2);
                                    }
                                    else
                                    {
                                        TreeNode errorNode = new TreeNode("Ошибка: Идентификатор не соответствует правилу");
                                        oNode.Nodes.Add(errorNode);
                                    }
                                    Lex.NextToken();
                                    if (Lex.enumPToken == TToken.lxmEndBracket)
                                    {
                                        TreeNode closeBracketNode = new TreeNode(")");
                                        oNode.Nodes.Add(closeBracketNode);
                                    }
                                    else throw new Exception("Ожидалась закрывающая скобка");
                                }
                                else throw new Exception("Ожидался второй индефикатор для LINE");
                            }
                            else throw new Exception("Ожидался пробел после первого индефикатора");
                        }
                        else throw new Exception("Ожидался первый индефикатор для LINE");
                    }
                    else throw new Exception("Ожидался пробел после COMMAND \"LINE\"");
                }
                else throw new Exception("Ожидался SETQ или COMMAND");
            }
            else throw new Exception("Ожидалась открывающая скобка");
        }

        public void V(TreeNode parentNode)
        {
            TreeNode vNode = new TreeNode("V");
            parentNode.Nodes.Add(vNode);
            C(vNode);  // разбор C как часть V
            if (Lex.enumPToken == TToken.lxmSpace)  // проверяем на пробел для B
                B(vNode);  // если есть пробел, вызываем B
        }

        public void B(TreeNode parentNode)
        {
            TreeNode bNode = new TreeNode("B");
            parentNode.Nodes.Add(bNode);
            if (Lex.enumPToken == TToken.lxmSpace)  // разбор пробела
            {
                Lex.NextToken();
                C(bNode);  // вызываем C после пробела
                if (Lex.enumPToken == TToken.lxmSpace)  // продолжаем разбор пробела
                    B(bNode);  // продолжаем разбор B
            }
        }

        public void C(TreeNode parentNode)
        {
            TreeNode cNode = new TreeNode("C");
            parentNode.Nodes.Add(cNode);
            if (Lex.enumPToken == TToken.lxmIdentifier)  // ожидаем индефикатор
            {
                identifier = Lex.CurrentTokenValue();
                if (initializedVariables.ContainsKey(identifier))
                    throw new Exception($"Идентификатор {identifier} уже инициализирован!");
                TreeNode idNode = new TreeNode(Lex.strPLexicalUnit);
                cNode.Nodes.Add(idNode);
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
                                    break;
                            }
                        }

                        if (flagCount == 1)
                        {
                            firstSymbol = lastVariable;
                            firstValue = lastValue;
                            if (lastVariable == "a" && lastValue.ToString().StartsWith("001001"))
                            {
                                if (!(new List<string> { "ac", "ad", "acc" }).Contains(identifier))
                                    isValidSequence = false;
                            }
                            else if (lastVariable == "a" && lastValue.StartsWith("00"))
                            {
                                if (!(new List<string> { "abc", "ab", "abcd", "aacd" }).Contains(identifier))
                                    isValidSequence = false;
                            }
                            else if ((new List<string> { "abc", "ab", "abcd", "aacd" }).Contains(lastVariable) && ToString().StartsWith("00"))
                            {
                                if (identifier != "a")
                                    isValidSequence = false;
                            }
                            else if ((new List<string> { "ac", "ad", "acc" }).Contains(lastVariable) && lastValue.StartsWith("001001"))
                            {
                                if (identifier != "a")
                                    isValidSequence = false;
                            }
                        }
                        if (isValidSequence)
                        {
                            flagCount++;
                            lastVariable = identifier;
                            lastValue = binaryValue;

                            int value = Int32.Parse(Lex.CurrentTokenValue());
                            initializedVariables[identifier] = value;
                            TreeNode numberNode = new TreeNode(Lex.strPLexicalUnit);
                            cNode.Nodes.Add(numberNode);
                            Lex.NextToken();
                        }
                        else
                            Lex.NextToken();
                    }
                    else throw new Exception("Ожидалось числовое значение");
                }
                else throw new Exception("Ожидался пробел");
            }
            else throw new Exception("Ожидался индефикатор");
        }
    }  
}
/*(SETQ a 001101 abc 101)
(COMMAND"LINE" a abc) */
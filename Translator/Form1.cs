using nsHashTables;
using nsSynt;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace Translator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            int n = tbFSource.Lines.Length; // Получение количества строк в tbFSource
        }
        public CHashTableList htl = new CHashTableList(3);
        public void TablesToMemo(object sender, System.EventArgs e)
        {
            List<string> listTable = new List<string>();

            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();

            htl.TableToStringList(0, listTable);
            for (int i = 0; i < listTable.Count; i++)
                listBox1.Items.Add(listTable[i]);
            listTable.Clear();

            htl.TableToStringList(1, listTable);
            for (int i = 0; i < listTable.Count; i++)
                listBox2.Items.Add(listTable[i]);
            listTable.Clear();

            htl.TableToStringList(2, listTable);
            for (int i = 0; i < listTable.Count; i++)
                listBox3.Items.Add(listTable[i]);
            listTable.Clear();
        }

        private void btnFStart_Click(object sender, EventArgs e)
        {
            tbFMessage.Clear();
            uSyntAnalyzer Synt = new uSyntAnalyzer();
            Synt.Lex.strPSource = tbFSource.Lines;
            Synt.Lex.strPMessage = tbFMessage.Lines;
            Synt.Lex.enumPState = TState.Start;
            try
            {
                Synt.Lex.NextToken();
                Synt.S();
                throw new Exception("Текст верный");
            }

            catch (Exception exc) // Обработка исключений
            {
                // Добавление сообщения об ошибке в tbFMessage
                tbFMessage.Text += exc.Message;

                tbFSource.Select(); // Устанавливаем фокус на tbFSource
                tbFSource.SelectionStart = 0; // Устанавливаем начальную позицию выделения
                int n = 0;

                // Подсчет количества символов для выделения текста до текущей позиции
                for (int i = 0; i < Synt.Lex.intPSourceRowSelection; i++)
                    n += tbFSource.Lines[i].Length + 2; // +2 учитывает переход на новую строку

                n += Synt.Lex.intPSourceColSelection; // Добавляем текущую позицию в строке
                tbFSource.SelectionLength = n; // Устанавливаем длину выделения
            }
        }

        private void writeButton_Click(object sender, EventArgs e)
        {
            CLex Lex = new CLex();
            Lex.strPSource = tbFSource.Lines;
            Lex.strPMessage = tbFMessage.Lines;
            Lex.intPSourceColSelection = 0;
            Lex.intPSourceRowSelection = 0;
            int x = tbFSource.TextLength;
            int y = tbFSource.Lines.Length;
            tbFMessage.Text = "";
            try
            {
                while (Lex.enumPState != TState.Finish)
                {
                    Lex.NextToken();
                    string s1 = "", s = "";
                    switch (Lex.enumPToken)
                    {
                        case TToken.lxmIdentifier:
                            {
                                s1 = "id " + Lex.strPLexicalUnit; int b = 0;
                                if (htl.AddLexicalUnit(Lex.strPLexicalUnit, 0, ref b))
                                {
                                    TablesToMemo(this, e);
                                }
                                break;
                            }
                        case TToken.lxmNumber:
                            {
                                s1 = "num " + Lex.strPLexicalUnit; int b = 0;
                                if (htl.AddLexicalUnit(Lex.strPLexicalUnit, 1, ref b))
                                {
                                    TablesToMemo(this, e);
                                }
                                break;
                            }
                        case TToken.lxmEndBracket:
                            {
                                s1 = "spec ) " + Lex.strPLexicalUnit; int b = 0;
                                if (htl.AddLexicalUnit(")", 2, ref b))
                                {
                                    TablesToMemo(this, e);
                                }
                                break;
                            }
                        case TToken.lxmOpenBracket:
                            {
                                s1 = "spec ( " + Lex.strPLexicalUnit; int b = 0;
                                if (htl.AddLexicalUnit("(", 2, ref b))
                                {
                                    TablesToMemo(this, e);
                                }
                                break;
                            }
                        case TToken.lxmCL:
                        case TToken.lxmSETQ:
                            {
                                s1 = "spec " + Lex.strPLexicalUnit; int b = 0;
                                if (htl.AddLexicalUnit(Lex.strPLexicalUnit, 2, ref b))
                                {
                                    TablesToMemo(this, e);
                                }
                                break;
                            }
                    }
                    String m = "(" + s + "" + s1 + ")";
                    tbFMessage.Text += m;
                }
            }
            catch (Exception exc)
            {
                tbFMessage.Text += exc.Message;
                tbFSource.Select();
                tbFSource.SelectionStart = 0;
                int n = 0;
                for (int i = 0; i < Lex.intPSourceRowSelection; i++) n += tbFSource.Lines[i].Length + 2;
                n += Lex.intPSourceColSelection;
                tbFSource.SelectionLength = n;
            }
        }

    }



    //// Обработчик события нажатия на кнопку btnFStart
    //private void btnFStart_Click(object sender, EventArgs e)
    //{
    //    // Создание экземпляра класса CLex для анализа текста
    //    CLex Lex = new CLex();
    //    Lex.strPSource = tbFSource.Lines; // Устанавливаем источник текста из текстового поля tbFSource
    //    Lex.strPMessage = tbFMessage.Lines; // Устанавливаем сообщения из текстового поля tbFMessage

    //    int x = tbFSource.TextLength; // Получаем количество символов в tbFSource
    //    int y = tbFSource.Lines.Length; // Получаем количество строк в tbFSource
    //    tbFMessage.Text = ""; // Очищаем tbFMessage для нового результата

    //    try
    //    {
    //        // Цикл продолжается, пока состояние парсера не станет "Finish"
    //        while (Lex.enumPState != TState.Finish)
    //        {
    //            Lex.GetSymbol(); // Получение текущего символа из исходного текста
    //            Lex.NextToken(); // Получение следующего токена

    //            String s = ""; // Переменная для хранения литеры
    //            String s1 = ""; // Переменная для хранения типа литеры

    //            // Определение типа символа и присваивание значений переменным s и s1
    //            // 1лр
    //            //switch (Lex.enumFSelectionCharType)
    //            //{
    //            //    case TCharType.EngLetter: { s1 = "EngLetter"; break; }
    //            //    case TCharType.RusLetter: { s1 = "RusLetter"; break; }
    //            //    case TCharType.Digit: { s1 = "Digit"; break; }
    //            //    case TCharType.Space: { s1 = "Space"; break; }
    //            //    case TCharType.Star: { s1 = "Star"; break; }
    //            //    case TCharType.Exclamation: { s1 = "Exclamation"; break; }
    //            //    case TCharType.Equal: { s1 = "Equal"; break; }
    //            //    case TCharType.Semicolon: { s1 = "Semicolon"; break; }
    //            //    case TCharType.EndBracket: { s1 = "EndBracket"; break; }
    //            //    case TCharType.OpenBracket: { s1 = "OpenBracket"; break; }
    //            //    case TCharType.OpenSquadBracket: { s1 = "OpenSquadBracket"; break; }
    //            //    case TCharType.EndSquadBracket: { s1 = "EndSquadBracket"; break; }
    //            //    case TCharType.Colon: { s1 = "Colon"; break; }
    //            //    case TCharType.Minus: { s1 = "Mius"; break; }
    //            //    case TCharType.Plus: { s1 = "Plus"; break; }
    //            //    case TCharType.Comma: { s1 = "Comma"; break; }
    //            //    case TCharType.Dot: { s1 = "Dot"; break; }
    //            //    case TCharType.AnotherSymbol: { s1 = "ReservedSymbol"; break; }
    //            //    case TCharType.NoInd: { s1 = "NoInd"; break; }
    //            //    case TCharType.EndRow: { s = "KC"; s1 = "EndRow"; break; }
    //            //    case TCharType.EndText: { s = "KT"; s1 = "EndText"; break; }
    //            //}

    //            // 2лр
    //            //switch (Lex.enumPToken)
    //            //    {
    //            //        case TToken.lxmNumber: { s = "LxmNumber"; s1 = Lex.strPLexicalUnit; break; }
    //            //        case TToken.lxmIdentifier: { s = "lxmId"; s1 = Lex.strPLexicalUnit; break; }
    //            //    }
    //            // Создание строки с литерой и ее типом
    //            String m = "(" + s + "," + s1 + ")";

    //            // Добавление строки в tbFMessage
    //            tbFMessage.Text += m;
    //        }
    //    }
    //    catch (Exception exc) // Обработка исключений
    //    {
    //        // Добавление сообщения об ошибке в tbFMessage
    //        tbFMessage.Text += exc.Message;

    //        tbFSource.Select(); // Устанавливаем фокус на tbFSource
    //        tbFSource.SelectionStart = 0; // Устанавливаем начальную позицию выделения
    //        int n = 0;

    //        // Подсчет количества символов для выделения текста до текущей позиции
    //        for (int i = 0; i < Lex.intPSourceRowSelection; i++)
    //            n += tbFSource.Lines[i].Length + 2; // +2 учитывает переход на новую строку

    //        n += Lex.intPSourceColSelection; // Добавляем текущую позицию в строке
    //        tbFSource.SelectionLength = n; // Устанавливаем длину выделения
    //    }
    //}
}
    


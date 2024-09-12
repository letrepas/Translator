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
            tbFSource.Text = "";
            tbFSource.Text = "";
            int n = tbFSource.Lines.Length;
        }

        private void btnFStart_Click(object sender, EventArgs e)
        {
            CLex Lex = new CLex();
            Lex.strPSource = tbFSource.Lines;
            Lex.strPMessage = tbFMessage.Lines;

            int x = tbFSource.TextLength;
            int y = tbFSource.Lines.Length;
            tbFMessage.Text = "";

            try
            {
                while (Lex.enumPState != TState.Finish)
                {
                    Lex.GetSymbol(); // Выводятся литеры и классификация
                    Lex.NextToken();
                    String s = "";
                    String s1 = "";
                    switch (Lex.enumFSelectionCharType)
                    {
                        case TCharType.EngLetter: { s1 = "EngLetter"; break; }
                        case TCharType.RusLetter: { s1 = "RusLetter"; break; }
                        case TCharType.Digit: { s1 = "Digit"; break; }
                        case TCharType.Space: { s1 = "Space"; break; }
                        case TCharType.Star: { s1 = "Star"; break; }
                        case TCharType.Exclamation: { s1 = "Exclamation"; break; }
                        case TCharType.Equal: { s1 = "Equal"; break; }
                        case TCharType.Semicolon: { s1 = "Semicolon"; break; }
                        case TCharType.EndBracket: { s1 = "EndBracket"; break; }
                        case TCharType.OpenBracket: { s1 = "OpenBracket"; break; }
                        case TCharType.OpenSquadBracket: { s1 = "OpenSquadBracket"; break; }
                        case TCharType.EndSquadBracket: { s1 = "EndSquadBracket"; break; }
                        case TCharType.Colon: { s1 = "Colon"; break; }
                        case TCharType.Minus: { s1 = "Mius"; break; }
                        case TCharType.Plus: { s1 = "Plus"; break; }
                        case TCharType.Comma: { s1 = "Comma"; break; }
                        case TCharType.Dot: { s1 = "Dot"; break; }
                        case TCharType.AnotherSymbol: { s1 = "ReservedSymbol"; break; }
                        case TCharType.NoInd: { s1 = "NoInd"; break; }
                        case TCharType.EndRow: { s = "KC"; s1 = "EndRow"; break; }
                        case TCharType.EndText: { s = "KT"; s1 = "EndText"; break; }
                    }
                    String m = "(" + s + "," + s1 + ")"; //литера и ее тип
                    tbFMessage.Text += m; //добавляется в строку сообщение
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

}

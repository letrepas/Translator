using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp45
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            tbFSource.AppendText("01ab" + "\r\n");
            tbFSource.AppendText("1 a");
            int n = tbFSource.Lines.Length;
        }

        private void btnFStart_Click(object sender, EventArgs e)
        {
            CLex Lex = new CLex();
            Lex.strPSource = tbFSource.Lines;
            Lex.strPMessage = tbFSource.Lines;

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
                        case TCharType.Letter: { s1 = "Letter"; break; }
                        case TCharType.Digit: { s1 = "Digit"; break; }
                        case TCharType.Space: { s1 = "Space"; break; }
                        case TCharType.ReservedSymbol: { s1 = "Reserved"; break; }
                        case TCharType.EndRow: { s = "KC"; s1 = "EndRow"; break; }
                        case TCharType.EndText: { s = "KT"; s1 = "EndText"; break; }
                    }
                    String m = $"({s},{s1})"; //литера и ее тип
                    tbFMessage.Text += m; //добавляется в строку сообщение
                }
            }
            catch (Exception exc)
            {
                tbFMessage.Text += exc.Message;
                tbFMessage.Select();
                tbFMessage.SelectionStart = 0;
                int n = 0;
                for (int i = 0; i < Lex.intPSourceRowSelection; i++) n += tbFMessage.Lines[i].Length + 2;
                n += Lex.intPSourceColSelection;
                tbFMessage.SelectionLength = n;
            }
        }
    }
}


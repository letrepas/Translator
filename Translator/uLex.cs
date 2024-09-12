using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translator
{
    public enum TState { Start, Continue, Finish }; //тип состояния
    public enum TCharType { EngLetter, RusLetter, Digit, EndRow, EndText, Space, Star, Slash, Exclamation, Equal, Semicolon, AnotherSymbol, OpenBracket, EndBracket, Colon, OpenSquadBracket, EndSquadBracket, Plus, Minus, Comma, Dot, NoInd}; // тип символа
    public enum TToken { lxmIdentifier, lxmNumber, lxmUnknown, lxmEmpty, lxmLeftParenth, lxmRightParenth, lxmIs, lxmDot, lxmComma };
    public class CLex  //класс лексический анализатор
    {
        private String[] strFSource;  // указатель на массив строк
        private String[] strFMessage;  // указатель на массив строк
        public TCharType enumFSelectionCharType;
        public char chrFSelection;
        private TState enumFState;
        private int intFSourceRowSelection;
        private int intFSourceColSelection;
        private String strFLexicalUnit;
        private TToken enumFToken;
        public String[] strPSource { set { strFSource = value; } get { return strFSource; } }
        public String[] strPMessage { set { strFMessage = value; } get { return strFMessage; } }
        public TState enumPState { set { enumFState = value; } get { return enumFState; } }
        public String strPLexicalUnit { set { strFLexicalUnit = value; } get { return strFLexicalUnit; } }
        public TToken enumPToken { set { enumFToken = value; } get { return enumFToken; } }
        public int intPSourceRowSelection { get { return intFSourceRowSelection; } set { intFSourceRowSelection = value; } }
        public int intPSourceColSelection { get { return intFSourceColSelection; } set { intFSourceColSelection = value; } }

        public CLex()
        {
        }
        public void GetSymbol() //метод класса лексический анализатор
        {
            if (intFSourceColSelection > strFSource[intFSourceRowSelection].Length - 1)
            {
                intFSourceRowSelection++;
                if (intFSourceRowSelection <= strFSource.Length - 1)
                {
                    intFSourceColSelection = -1;
                    chrFSelection = '\0';
                    enumFSelectionCharType = TCharType.EndRow;
                    enumFState = TState.Continue;
                }
                else
                {
                    chrFSelection = '\0';
                    enumFSelectionCharType = TCharType.EndText;
                    enumFState = TState.Finish;
                }
            }
            else
            {
                chrFSelection = strFSource[intFSourceRowSelection][intFSourceColSelection]; //классификация прочитанной литеры
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
                else if (chrFSelection == ')') enumFSelectionCharType = TCharType.EngLetter;
                else if (chrFSelection == ':') enumFSelectionCharType = TCharType.Colon;
                else if (chrFSelection == '[') enumFSelectionCharType = TCharType.OpenSquadBracket;
                else if (chrFSelection == ']') enumFSelectionCharType = TCharType.EndSquadBracket;
                else if (chrFSelection == '+') enumFSelectionCharType = TCharType.Plus;
                else if (chrFSelection == '-') enumFSelectionCharType = TCharType.Minus;
                else if (chrFSelection == ',') enumFSelectionCharType = TCharType.Comma;
                else if (chrFSelection == '.') enumFSelectionCharType = TCharType.Dot;
                else if (chrFSelection == '^' || chrFSelection == '%' || chrFSelection == '@' || chrFSelection == '<' || chrFSelection == '>' || chrFSelection == '?') enumFSelectionCharType = TCharType.AnotherSymbol;
                else enumFSelectionCharType = TCharType.NoInd;
                enumFState = TState.Continue;
            }
            intFSourceColSelection++;
        }

        private void TakeSymbol()
        {
            char[] c = { chrFSelection };
            String s = new string(c);
            strFLexicalUnit += s;
            GetSymbol();
        }
        public void NextToken()
        {
            strFLexicalUnit = "";
            if (enumFState == TState.Start)
            {
                intFSourceRowSelection = 0;
                intFSourceColSelection = -1;
                GetSymbol();
            }

            if (chrFSelection == '/')
            {
                GetSymbol();
                if (chrFSelection == '/')
                    while (enumFSelectionCharType != TCharType.EndRow)
                        GetSymbol();
                GetSymbol();
            }
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace nsHashTables
{
    public struct THeapItem
    {
        public string strFLexicalUnit;
        public byte byteFHashTable;
        public int intFHashIndex;


        public THeapItem(string strALexicalUnit, byte byteATable, int intAHashIndex)
        {
            strFLexicalUnit = strALexicalUnit;
            byteFHashTable = byteATable;
            intFHashIndex = intAHashIndex;
        }
    }

    public class THeap
    {

        public List<THeapItem> arrFHeapTable = new List<THeapItem>();
        private List<int> arrFDeleted = new List<int>();
        private int intFFreeItem;
        bool boolIsSaved;
        bool boolIsLoaded;
        public bool boolPIsSaved { get { return boolIsSaved; } }
        public bool boolPIsLoaded { get { return boolIsLoaded; } }
        public int intPFreeItem { get { return intFFreeItem; } }
        public THeap()
        {
            Init();
            intFFreeItem = 1;
        }
        protected void Init()
        {
            arrFDeleted.Clear();
            arrFHeapTable.Clear();
            int cnt = 4;
            Resize(arrFHeapTable, cnt);


        }
        static void Resize(List<THeapItem> list, int size)
        {
            if (size > list.Count)
                while (size > list.Count)
                    list.Add(new THeapItem("", 0, 0));
            else if (size < list.Count)
                while (list.Count - size > 0)
                    list.RemoveAt(list.Count - 1);
        }

        static void Resize(List<int> list, int size)
        {
            if (size > list.Count)
                while (size > list.Count)
                    list.Add(new Int32());
            else if (size < list.Count)
                while (list.Count - size > 0)
                    list.RemoveAt(list.Count - 1);
        }

        static void Resize(List<char> list, int size)
        {
            if (size > list.Count)
                while (size > list.Count)
                    list.Add('0');
            else if (size < list.Count)
                while (list.Count - size > 0)
                    list.RemoveAt(list.Count - 1);
        }

        public void Expansion()
        {
            int cardVSize = arrFHeapTable.Count;
            cardVSize = cardVSize + cardVSize % 10 + 1;
            Resize(arrFHeapTable, cardVSize);
            Resize(arrFHeapTable, cardVSize);
        }

        public void AddLexicalUnit(string strALexicalUnit, byte byteAHashTable, int cardAHashIndex, ref int cardALexicalCode)
        {
            int intVIndex;

            if (arrFDeleted.Count == 0)
            {
                intVIndex = intFFreeItem;
                intFFreeItem++;
                if (intFFreeItem >= (Int32)(arrFHeapTable.Count * 0.9))
                    Expansion();
            }
            else
            {
                intVIndex = arrFDeleted[arrFDeleted.Count - 1];
                Resize(arrFDeleted, arrFDeleted.Count - 1);
            }
            THeapItem Item = arrFHeapTable[intVIndex];
            Item.strFLexicalUnit = strALexicalUnit;
            Item.byteFHashTable = byteAHashTable;
            Item.intFHashIndex = cardAHashIndex;
            arrFHeapTable[intVIndex] = Item;
            cardALexicalCode = intVIndex;

        }
        public void DeleteLexicalUnit(int cardALexicalCode)
        {
            int i;
            if (arrFDeleted == null || !arrFDeleted.Any())
                i = 0;
            else i = arrFDeleted.Count();
            Resize(arrFDeleted, i + 1);
            arrFDeleted[i] = cardALexicalCode;
            THeapItem Item = arrFHeapTable[cardALexicalCode];
            Item.strFLexicalUnit = "";
            Item.byteFHashTable = 0;
            Item.intFHashIndex = 0;
        }
        public void Save(ref StreamWriter sw)
        {
            try
            {

                for (int i = 1; i < arrFHeapTable.Count; i++) //type?
                {
                    if (arrFHeapTable[i].strFLexicalUnit == "")
                        break;
                    sw.Write(arrFHeapTable[i].strFLexicalUnit + "\t");
                    sw.Write(arrFHeapTable[i].byteFHashTable.ToString() + "\t");
                    sw.WriteLine(arrFHeapTable[i].intFHashIndex.ToString());
                }
                boolIsSaved = true;
            }
            catch (Exception) { boolIsSaved = false; }

        }
        public void Load(ref StreamReader sr)
        {
            try
            {
                Init();
                int size = arrFHeapTable.Count;
                int readSz = 0;
                while (true)
                {
                    string line = sr.ReadLine();
                    if (line == null)
                        break;
                    if (++readSz >= size)
                    {
                        size *= 2;
                        Resize(arrFHeapTable, size);
                    }
                    char[] delim = { '\t'/*,'\n'*/ };
                    string[] lines = line.Split(delim);
                    THeapItem it = arrFHeapTable[readSz];
                    it.strFLexicalUnit = lines[0];
                    it.byteFHashTable = Convert.ToByte(lines[1]);
                    it.intFHashIndex = Convert.ToInt32(lines[2]);
                    arrFHeapTable[readSz] = it;

                }
                intFFreeItem = readSz + 1;

                boolIsLoaded = true;

            }
            catch (InvalidCastException)
            { boolIsLoaded = false; }
        }
        THeapItem GetItem(int i)
        {
            if (i >= arrFHeapTable.Count)
            {
                MessageBox.Show("GetИндекс кучи вышел за диапазон!");
                THeapItem Item = new THeapItem("", 0, 0);
                return Item;
            }
            else return arrFHeapTable[i];
        }
        void SetItem(int i, THeapItem NewItem)
        {
            if (i >= arrFHeapTable.Count)
                MessageBox.Show("SetИндекс кучи вышел за диапазон!");
            else arrFHeapTable[i] = NewItem;
        }

        public void HeapTableView(List<string> sList)
        {
            for (int i = 0; i < arrFHeapTable.Count; i++)
                sList.Add(arrFHeapTable[i].strFLexicalUnit);

        }

    }
}


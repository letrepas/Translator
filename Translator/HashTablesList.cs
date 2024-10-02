using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Collections;
using System.Threading;

namespace nsHashTables
{
    public class CHashTableList
    {
        private List<THashTable> arrFHashTableList = new List<THashTable>();
        private bool boolFIsSaved;
        public bool boolFIsLoaded;
        private byte byteFTablesSize;

        static THeap objFHeap = new THeap();
        //------------------------------------------------------------------------------
        public CHashTableList(byte byteATableCount)
        {
            this.byteFTablesSize = byteATableCount;
            objFHeap = new THeap();
            Resize(arrFHashTableList, byteATableCount);


        }
        //------------------------------------------------------------------------------
        public CHashTableList(string strAFileName)
        {
            try
            {
                boolFIsLoaded = Load(strAFileName);
            }
            catch (InvalidCastException)
            {
                MessageBox.Show("Ошибка при восстановлении хеш-таблиц из файла !");
                boolFIsLoaded = false;
            }
        }
        //------------------------------------------------------------------------------
        public byte GetTableNumber(int intALexicalCode) { return objFHeap.arrFHeapTable[intALexicalCode].byteFHashTable; }
        //------------------------------------------------------------------------------
        public int GetTablesCount()
        {
            return arrFHashTableList.Count();
        }
        //------------------------------------------------------------------------------         
        static void Resize(List<THashTable> list, int size)
        {
            if (size > list.Count)
                while (size > list.Count)
                {
                    list.Add(new THashTable(ref objFHeap));
                }
            else if (size < list.Count)
                while (list.Count - size > 0)
                    list.RemoveAt(list.Count - 1);

        }
        //------------------------------------------------------------------------------
        static void Resize(List<object> list, int intANewSize)
        {
            if (intANewSize > list.Count)
                while (intANewSize > list.Count)
                    list.Add(new object());
            else if (intANewSize < list.Count)
                while (list.Count - intANewSize > 0)
                    list.RemoveAt(list.Count - 1);
        }
        //------------------------------------------------------------------------------
        static void Resize(List<int> list, int intANewSize)
        {
            if (intANewSize > list.Count)
                while (intANewSize > list.Count)
                    list.Add(new Int32());
            else if (intANewSize < list.Count)
                while (list.Count - intANewSize > 0)
                    list.RemoveAt(list.Count - 1);
        }
        //------------------------------------------------------------------------------      
        public object GetUserData(int intALexicalCode)
        {
            if ((0 < intALexicalCode) && (intALexicalCode < objFHeap.intPFreeItem))
                return arrFHashTableList[GetTableNumber(intALexicalCode)].arrFUserTable[intALexicalCode];
            else
            {
                MessageBox.Show("Неверно задан лексический код при чтении пользовательских данных");
                return null;
            }
        }
        //------------------------------------------------------------------------------
        public void SetUserData(int intALexicalCode, object objAUserData)
        {
            if ((0 < intALexicalCode) && (intALexicalCode < objFHeap.intPFreeItem))
            {
                if (arrFHashTableList[GetTableNumber(intALexicalCode)].arrFUserTable.Count > 0)
                    arrFHashTableList[GetTableNumber(intALexicalCode)].arrFUserTable[intALexicalCode] = objAUserData;
                else
                    MessageBox.Show("Попытка записи адреса в несозданный массив пользовательских данных!");
            }
            else MessageBox.Show("Неверно задан лексический код при записи пользовательских данных!");
        }
        //------------------------------------------------------------------------------
        public string GetLexicalUnit(int intALexicalCode)
        {
            if ((0 < intALexicalCode) && (intALexicalCode < objFHeap.intPFreeItem)) return objFHeap.arrFHeapTable[intALexicalCode].strFLexicalUnit;
            else
            {
                MessageBox.Show("Неверно задан лексический код при чтении пользовательских данных!");
                return "";
            }
        }
        //------------------------------------------------------------------------------
        public bool SearchLexicalUnit(string strALexicalUnit, byte byteATable, ref int intALexicalCode)
        {
            return arrFHashTableList[byteATable].SearchLexicalUnit(strALexicalUnit, ref intALexicalCode);
        }
        //------------------------------------------------------------------------------
        public bool AddLexicalUnit(string strALexicalUnit, byte byteATable, ref int intALexicalCode)
        {
            if (byteATable >= arrFHashTableList.Count)
            {
                if (MessageBox.Show("Увеличить количество таблиц?", "Запрашиваемый индекс таблицы не существует.", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    Resize(arrFHashTableList, byteATable + 1);
                else
                    return false;
            }
            return arrFHashTableList[byteATable].AddLexicalUnit(strALexicalUnit, byteATable, ref intALexicalCode);
        }
        //------------------------------------------------------------------------------
        public void DeleteLexicalUnit(string strALexicalUnit, byte byteATable)
        {
            arrFHashTableList[byteATable].DeleteLexicalUnit(strALexicalUnit);
        }
        //------------------------------------------------------------------------------
        public void DeleteLexicalCode(int intALexicalCode)
        {
            short T = objFHeap.arrFHeapTable[intALexicalCode].byteFHashTable;
            arrFHashTableList[T].DeleteLexicalCode(intALexicalCode);
        }
        //------------------------------------------------------------------------------
        public void SetUserTable(byte byteATable)
        {
            arrFHashTableList[byteATable].SetUserTable();
        }
        //------------------------------------------------------------------------------   
        public void Expantion()
        {
            Resize(arrFHashTableList, ++byteFTablesSize);
        }
        //------------------------------------------------------------------------------
        public void Save(string strAFileName)
        {

            try
            {
                StreamWriter fl = File.CreateText(strAFileName);
                fl.WriteLine(byteFTablesSize.ToString());
                for (int i = 0; i < byteFTablesSize; i++)
                    fl.Write(arrFHashTableList[i].arrFHashTable.Count.ToString() + "\t");
                fl.WriteLine("");
                objFHeap.Save(ref fl);
                boolFIsSaved = true;
                fl.Close();
            }
            catch (InvalidDataException)
            { boolFIsSaved = false; }

        }
        //------------------------------------------------------------------------------
        public bool Load(string strAFileName)
        {

            boolFIsLoaded = false;
            try
            {
                StreamReader sr = new StreamReader(strAFileName);
                byteFTablesSize = Convert.ToByte(sr.ReadLine());
                if (byteFTablesSize < 1 || byteFTablesSize > 16)
                {
                    MessageBox.Show("Unbelivable count of tables: " + byteFTablesSize.ToString());
                    return boolFIsLoaded;
                }
                arrFHashTableList.Clear();
                Resize(arrFHashTableList, byteFTablesSize/*+1*/);
                string line = sr.ReadLine();
                char[] delim = { '\t'/*,'\n'*/ };
                string[] counts = line.Split(delim);
                for (int i = 0; i < byteFTablesSize; ++i)
                {
                    arrFHashTableList[i].Init(Convert.ToInt32(counts[i]));
                }

                objFHeap.Load(ref sr);
                sr.Close();
                int n = objFHeap.arrFHeapTable.Count;

                for (int i = 1; i < n; ++i)
                {
                    THeapItem Item = objFHeap.arrFHeapTable[i];
                    if (Item.strFLexicalUnit.Length == 0)
                        break;
                    arrFHashTableList[Item.byteFHashTable].arrFHashTable[Item.intFHashIndex] = i;
                }

                boolFIsLoaded = true;
            }
            catch (InvalidDataException)
            {
                MessageBox.Show("Ошибка при восстановлении из файла хеш-таблиц!"); boolFIsLoaded = false;
            }

            return boolFIsLoaded;
        }

        //------------------------------------------------------------------------------
        // отладка
        public void HeapTableView(List<string> sList)
        {
            objFHeap.HeapTableView(sList);
        }
        //------------------------------------------------------------------------------
        public void TableToStringList(byte byteATable, List<string> sList)
        {
            arrFHashTableList[byteATable].GetLexicalUnitList(ref sList);

        }
        //------------------------------------------------------------------------------
        public int GetHashIndex(byte Table)
        {
            return arrFHashTableList[Table].intFHashIndex;

        }
        //------------------------------------------------------------------------------
    }
}

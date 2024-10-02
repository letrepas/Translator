using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using System.Threading;
using System.Security.Cryptography;
using System.Text;

namespace nsHashTables
{
    public class THashTable
    {
        public List<int> arrFHashTable = new List<int>();
        private int intFCurrentPrimeNumber;
        private int intFItemReserve;
        private bool boolIsSaved;
        public int intFHashIndex;
        public int cardPTableSize { get { return arrFHashTable.Count; } }
        public List<object> arrFUserTable = null;
        static THeap objFHeap;

        public THashTable(ref THeap objAHeap)
        {
            objFHeap = objAHeap;
            Init(7);
            intFItemReserve = 0;
        }
        public void Init(int count)
        {
            arrFHashTable.Clear();
            Resize(arrFHashTable, count);
            intFCurrentPrimeNumber = count;
        }

        static void Resize(List<object> list, int size)
        {
            if (size > list.Count)
                while (size > list.Count)
                    list.Add(new object());
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

        int NextPrimeNumber(int cardAOldPrimeNumber)
        {
            int intVLowerBound, intVUpperBound, intVNextPrimeNumber;
            bool boolVIsDivisor;
            intVNextPrimeNumber = cardAOldPrimeNumber + cardAOldPrimeNumber / 10 + 1;  // увеличиваем на 10 процентов
            if ((intVNextPrimeNumber % 2) == 0) intVNextPrimeNumber++;
            do
            {
                boolVIsDivisor = true; intVNextPrimeNumber = intVNextPrimeNumber + 2;
                intVLowerBound = 3; intVUpperBound = intVNextPrimeNumber / 3 + 1; // диапазон делителей
                while (boolVIsDivisor && (intVLowerBound < intVUpperBound))
                {
                    if ((intVNextPrimeNumber % intVLowerBound) == 0) boolVIsDivisor = false;
                    else intVLowerBound = intVLowerBound + 2;
                }
            } while (!boolVIsDivisor);
            return intVNextPrimeNumber;
        }

        UInt32 HashFunction_Wainberger(string strALexicalUnit)
        {
            // Используем SHA-256 для вычисления хеша   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(strALexicalUnit));

                UInt32 h = BitConverter.ToUInt32(bytes, 0);

                return h;
            }
        }


       int ReHashFunction_Line(int h, string strALexicalUnit)
{
    if (h == 0) h = arrFHashTable.Count / 3;
    else if (h == 1) h = arrFHashTable.Count * 3 / 4;
    
    int i = 1, hi = h;
    bool boolVFinish = false;
    
    do
    {
        if (hi >= arrFHashTable.Count || hi < 0)
        {
            throw new IndexOutOfRangeException($"Индекс hi ({hi}) выходит за пределы массива.");
        }

        if (arrFHashTable[hi] == 0)
        {
            boolVFinish = true;
        }
        else if (objFHeap.arrFHeapTable[arrFHashTable[hi]].strFLexicalUnit == strALexicalUnit)
        {
            boolVFinish = true;
        }
        else
        {
            i++;
            hi = (h * (i + 1)) % arrFHashTable.Count;
            
            // Исправляем отрицательный индекс, если результат mod дал отрицательное значение
            if (hi < 0)
            {
                hi += arrFHashTable.Count;
            }
        }
    } while (!boolVFinish);
    
    return hi;
}



        public void HashIndex(string strALexicalUnit)
        {
            int h;
            h = (Int32)HashFunction_Wainberger(strALexicalUnit);

            if (h < 0)
                h = -h;

            h = h % (Int32)(arrFHashTable.Count);
            if (h < 0)
                h += arrFHashTable.Count;

            intFHashIndex = ReHashFunction_Line(h, strALexicalUnit);
        }

        void TableReHashing()
        {
            int i, j;
            List<int> cardarrVHashTableImage = new List<int>();
            List<object> arrVUserTableImage = new List<object>();
            Resize(cardarrVHashTableImage, arrFHashTable.Count);
            if (arrFUserTable != null)
                Resize(arrVUserTableImage, arrFHashTable.Count);

            for (i = 0; i < arrFHashTable.Count; i++)
            {
                cardarrVHashTableImage[i] = arrFHashTable[i];
                if (arrFUserTable != null) arrVUserTableImage[i] = arrFUserTable[i];
            }
            arrFHashTable.Clear();
            if (arrFUserTable != null) arrFUserTable.Clear();
            Resize(arrFHashTable, intFCurrentPrimeNumber);
            if (arrFUserTable != null) Resize(arrFUserTable, intFCurrentPrimeNumber);
            for (i = 0; i < cardarrVHashTableImage.Count; i++)
            {
                if (cardarrVHashTableImage[i] != 0)
                {
                    j = cardarrVHashTableImage[i];
                    HashIndex(objFHeap.arrFHeapTable[j].strFLexicalUnit);

                    arrFHashTable[intFHashIndex] = j;
                    if (arrFUserTable != null) arrFUserTable[intFHashIndex] = arrVUserTableImage[i];
                    THeapItem Th2 = objFHeap.arrFHeapTable[j];
                    Th2.intFHashIndex = intFHashIndex;
                    objFHeap.arrFHeapTable[j] = Th2;
                }
            }
            cardarrVHashTableImage.Clear();
            if (arrFUserTable != null) arrVUserTableImage.Clear();
        }

        void Expansion()
        {
            intFCurrentPrimeNumber = NextPrimeNumber(intFCurrentPrimeNumber);
            TableReHashing();
        }
        object GetUserPointer(int cardILexicalCode)
        {
            THeapItem Item = objFHeap.arrFHeapTable[cardILexicalCode];
            if (Item.intFHashIndex >= cardPTableSize)
            {
                MessageBox.Show("Индекс пользовательского массива вышел за диапазон!");
                return null;
            }
            else
            {
                return arrFUserTable[objFHeap.arrFHeapTable[cardILexicalCode].intFHashIndex];
            }
        }
        void SetUserPointer(int cardILexicalCode, object ptrANewPoint)
        {
            if (objFHeap.arrFHeapTable[cardILexicalCode].intFHashIndex >= cardPTableSize)
                MessageBox.Show("Индекс пользовательского массива вышел за диапазон!");
            else
                arrFUserTable[objFHeap.arrFHeapTable[cardILexicalCode].intFHashIndex] = ptrANewPoint;
        }
        public void SetUserTable()
        {
            arrFUserTable = new List<object>();
            Resize(arrFUserTable, arrFHashTable.Count);
        }

        public bool SearchLexicalUnit(string strAlexicalUnit, ref int intALexicalCode)
        {
            HashIndex(strAlexicalUnit);
            if (arrFHashTable[intFHashIndex] == 0) return false;
            else
            {
                intALexicalCode = arrFHashTable[intFHashIndex];
                return true;
            }
        }
        public bool AddLexicalUnit(string strALexicalUnit, byte byteAHashTable, ref int intALexicalCode)
        {
            HashIndex(strALexicalUnit);
            if (arrFHashTable[intFHashIndex] != 0)
            {
                intALexicalCode = arrFHashTable[intFHashIndex];
                return true;
            }
            else
            {
                if ((intFItemReserve + 2) > (cardPTableSize * 0.9))
                {
                    Expansion();
                    HashIndex(strALexicalUnit);
                }
                objFHeap.AddLexicalUnit(strALexicalUnit, byteAHashTable, intFHashIndex, ref intALexicalCode);
                arrFHashTable[intFHashIndex] = intALexicalCode;
                intFItemReserve++;
                return false;

            }
        }
        public void DeleteLexicalUnit(string strAlexicalUnit)
        {
            HashIndex(strAlexicalUnit);
            if (arrFHashTable[intFHashIndex] != 0)
            {
                if (arrFUserTable != null)
                {
                    if (arrFUserTable[intFHashIndex] != null)
                        MessageBox.Show("Удаление из таблицы связанного данного");
                    else
                    {
                        objFHeap.DeleteLexicalUnit(arrFHashTable[intFHashIndex]);
                        arrFHashTable[intFHashIndex] = 0;
                        intFItemReserve--;
                        TableReHashing();
                    }
                }
                else
                {
                    objFHeap.DeleteLexicalUnit(arrFHashTable[intFHashIndex]);
                    arrFHashTable[intFHashIndex] = 0;
                    intFItemReserve--;
                    TableReHashing();
                }

            }
        }
        public void DeleteLexicalCode(int cardALexicalCode)
        {
            int VHashIndex;

            VHashIndex = objFHeap.arrFHeapTable[cardALexicalCode].intFHashIndex;
            if (arrFHashTable[VHashIndex] != 0)
                if (arrFUserTable.Count != 0)
                    if (arrFUserTable[VHashIndex] != null)
                        MessageBox.Show("Удаление из таблицы связанного данного");
                    else
                    {
                        objFHeap.DeleteLexicalUnit(cardALexicalCode);
                        arrFHashTable[VHashIndex] = 0;
                        intFItemReserve--;
                        TableReHashing();
                    }
                else
                {
                    objFHeap.DeleteLexicalUnit(cardALexicalCode);
                    arrFHashTable[VHashIndex] = 0;
                    intFItemReserve--;
                    TableReHashing();
                }
        }
        public void Save(ref StreamWriter fl)
        {
            try
            {

                fl.WriteLine(cardPTableSize.ToString());
                fl.WriteLine(intFItemReserve.ToString());
                for (int i = 1; i < cardPTableSize; i++)
                    fl.Write("\t" + arrFHashTable[i].ToString());
                fl.Write("\n");
                boolIsSaved = true;
            }
            catch (InvalidCastException)
            { boolIsSaved = false; }

        }

        public void GetLexicalUnitList(ref List<string> sList)
        {
            for (int i = 0; i < arrFHashTable.Count; i++) if (arrFHashTable[i] != 0) sList.Add(objFHeap.arrFHeapTable[arrFHashTable[i]].strFLexicalUnit);

        }
    }
}

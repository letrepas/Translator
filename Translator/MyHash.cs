using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace laba4
{
    public class MyHash
    {
        // Метод для вычисления хэша SHA256
        public string HashFunction(string word)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Преобразуем строку в байты и вычисляем хэш
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(word));

                // Преобразуем байты в строку в шестнадцатеричном формате
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                    builder.Append(b.ToString("x2"));
                return builder.ToString();
            }
        }

        // Метод для добавления слова в хэш-таблицу
        public void AddWord(Dictionary<string, List<string>> hashTable, string word)
        {
            string hashValue = HashFunction(word); // вычисляется хэш-код для слова

            // Проверяем, содержится ли уже такой хэш-код в хэш-таблице
            if (!hashTable.ContainsKey(hashValue))
                hashTable[hashValue] = new List<string>(); // в таблице создается новая пустая коллекция
            else
            {
                if (hashTable[hashValue].Contains(word)) // проверяем, содержится ли уже переданное слово
                {
                    Console.WriteLine("Слово уже существует: " + word); // сообщение о существующем слове
                    return;
                }
            }
            hashTable[hashValue].Add(word); // добавляем слово в коллекцию
        }

        // Метод для поиска слова в хэш-таблице
        public bool SearchWord(Dictionary<string, List<string>> hashTable, string word)
        {
            string hashValue = HashFunction(word);
            if (hashTable.ContainsKey(hashValue)) // проверяем, содержится ли такой хэш-код
                return hashTable[hashValue].Contains(word); // проверяем, содержится ли слово в коллекции
            return false; // хэш-код не найден
        }

        // Метод для удаления слова из хэш-таблицы
        public bool RemoveWord(Dictionary<string, List<string>> hashTable, string word)
        {
            string hashValue = HashFunction(word);
            if (hashTable.ContainsKey(hashValue)) // проверяем наличие в хэш-таблице
            {
                List<string> words = hashTable[hashValue];
                if (words.Contains(word))
                {
                    words.Remove(word); // если слово найдено, оно удаляется
                    if (words.Count == 0) // если коллекция становится пустой
                        hashTable.Remove(hashValue); // удаляем ее из хэш-таблицы
                    return true;
                }
            }
            return false; // слово не найдено
        }
    }
}


using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

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

            string uniqueKey = hashValue + "_" + word;
            // Проверяем, содержится ли уже такой хэш-код в хэш-таблице
            if (!hashTable.ContainsKey(uniqueKey))
                hashTable[uniqueKey] = new List<string>(); // в таблице создается новая пустая коллекция
            else
            {
                if (hashTable[uniqueKey].Contains(word)) // проверяем, содержится ли уже переданное слово
                    return;
            }
            hashTable[uniqueKey].Add(word); // добавляем слово в коллекцию
        }

        // Метод для поиска слова в хэш-таблице
        public bool FindhWord(Dictionary<string, List<string>> hashTable, string word)
        {
            string hashValue = HashFunction(word);
            string uniqueKey = hashValue + "_" + word;
            if (hashTable.ContainsKey(uniqueKey)) // проверяем, содержится ли такой хэш-код
                return hashTable[uniqueKey].Contains(word); // проверяем, содержится ли слово в коллекции
            return false; // хэш-код не найден
        }

        // Метод для удаления слова из хэш-таблицы
        public bool RemoveWord(Dictionary<string, List<string>> hashTable, string word)
        {
            string hashValue = HashFunction(word);
            string uniqueKey = hashValue + "_" + word;
            if (hashTable.ContainsKey(uniqueKey)) // проверяем наличие в хэш-таблице
            {
                List<string> words = hashTable[uniqueKey];
                if (words.Contains(word))
                {
                    words.Remove(word); // если слово найдено, оно удаляется
                    if (words.Count == 0) // если коллекция становится пустой
                        hashTable.Remove(uniqueKey); // удаляем ее из хэш-таблицы
                    return true;
                }
            }
            return false; // слово не найдено
        }
    }
}


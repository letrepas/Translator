using nsSynt;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using laba4;
using Tree;
using System.Reflection;
namespace Translator
{
    public partial class Form1 : Form
    {
        Dictionary<string, List<string>> hashTableIdentifier = new Dictionary<string, List<string>>();
        Dictionary<string, List<string>> hashTableDigital = new Dictionary<string, List<string>>();
        Dictionary<string, List<string>> hashTableSpecial = new Dictionary<string, List<string>>();
        public MyHash hashFunction = new MyHash();
        bool isFirstButtonValid = false;
        private Panel drawingPanel;
        uSyntAnalyzer Synt = new uSyntAnalyzer();
        private DrawTreeNodeEventHandler customDrawNodeHandler;
        public TreeV parserTree;

        public Form1()
        {
            InitializeComponent();
            InitializeDrawingPanel();
            int n = tbFSource.Lines.Length; // Получение количества строк в tbFSource
            // Перерисовка формы с полученными значениями X
        }

        public void TablesToMemo(object sender, System.EventArgs e)
        {
            List<string> listTable = new List<string>();

            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();

            foreach (var entry in hashTableIdentifier)
                listBox1.Items.Add(string.Join(", ", entry.Value));
            listTable.Clear();

            foreach (var entry in hashTableDigital)
                listBox2.Items.Add(string.Join(", ", entry.Value));
            listTable.Clear();
            foreach (var entry in hashTableSpecial)
                listBox3.Items.Add(string.Join(", ", entry.Value));
            listTable.Clear();

        }

        private void btnFStart_Click(object sender, EventArgs e)
        {
            tbFMessage.Clear();
            Synt.Lex.strPSource = tbFSource.Lines;
            Synt.Lex.strPMessage = tbFMessage.Lines;
            Synt.Lex.enumPState = TState.Start;
            Synt.initializedVariables.Clear();

            Synt.Lex.intPSourceRowSelection = 0;
            Synt.Lex.intPSourceColSelection = 0;
            isFirstButtonValid = false;
            try
            {
                for (int i = 0; i < Synt.Lex.strPSource.Length; i++)  // Обрабатываем каждую строку
                {

                    string line = Synt.Lex.strPSource[i];  // Получаем текущую строку
                    Synt.Lex.NextToken();  // Получаем следующий токен
                    Synt.S();  // Проверяем синтаксис текущей строки

                    // Переход на следующую строку после завершения анализа одной строки
                    if (Synt.Lex.enumPState == TState.Continue && i < Synt.Lex.strPSource.Length - 1)
                        Synt.Lex.enumPState = TState.Start;  // Возвращаем состояние в начало
                }
                throw new Exception("Текст верный"); // Успешное завершение

            }

            catch (Exception exc)
            {
                tbFMessage.Text += exc.Message;
                tbFSource.Select();
                tbFSource.SelectionStart = 0;
                int n = 0;
                for (int i = 0; i < Synt.Lex.intPSourceRowSelection; i++) n += tbFSource.Lines[i].Length + 2;
                n += Synt.Lex.intPSourceColSelection;
                tbFSource.SelectionLength = n;
                // Проверяем, является ли сообщение "Текст верный"
                if (exc.Message == "Текст верный")
                {
                    isFirstButtonValid = true;  // В случае успеха устанавливаем true
                    writeButton.Enabled = true;  // Разблокируем кнопку
                    drawingPanel.Invalidate();
                }
                else
                {
                    isFirstButtonValid = false;  // В случае ошибки флаг остается false
                    writeButton.Enabled = false;  // Отключаем кнопку writeButton
                    drawingPanel.Invalidate();
                }

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
                    string s1 = "";
                    switch (Lex.enumPToken)
                    {
                        case TToken.lxmIdentifier:
                            {
                                hashFunction.AddWord(hashTableIdentifier, Lex.strPLexicalUnit);
                                s1 = "id " + Lex.strPLexicalUnit;
                                TablesToMemo(this, e);
                                break;
                            }
                        case TToken.lxmNumber:
                            {
                                hashFunction.AddWord(hashTableDigital, Lex.strPLexicalUnit);
                                s1 = "num " + Lex.strPLexicalUnit;
                                TablesToMemo(this, e);
                                break;
                            }
                        case TToken.lxmEndBracket:
                            {
                                hashFunction.AddWord(hashTableSpecial, ")");
                                s1 = "spec ) " + Lex.strPLexicalUnit;
                                TablesToMemo(this, e);
                                break;
                            }
                        case TToken.lxmOpenBracket:
                            {
                                hashFunction.AddWord(hashTableSpecial, "(");
                                s1 = "spec ( " + Lex.strPLexicalUnit;
                                TablesToMemo(this, e);
                                break;
                            }
                        case TToken.lxmCL:
                            {
                                hashFunction.AddWord(hashTableSpecial, "COMMAND\"LINE\"");
                                s1 = "spec " + Lex.strPLexicalUnit;
                                TablesToMemo(this, e);
                                break;
                            }
                        case TToken.lxmSETQ:
                            {
                                hashFunction.AddWord(hashTableSpecial, "SETQ");
                                s1 = "spec " + Lex.strPLexicalUnit;
                                TablesToMemo(this, e);
                                break;
                            }
                    }
                    String m = "(" + s1 + ")";
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

        private void deleteButton_Click(object sender, EventArgs e)
        {
            ListBox selectedListBox = GetSelectedListBox(); // Определяем выбранный ListBox
            Dictionary<string, List<string>> selectedHashTable = GetSelectedHashTable(selectedListBox); // Получаем нужную хэш-таблицу

            if (selectedListBox != null && selectedHashTable != null && selectedListBox.SelectedItem != null)
            {
                string selectedItem = selectedListBox.SelectedItem.ToString();

                // Удаляем выбранное слово из соответствующей хэш-таблицы
                if (hashFunction.RemoveWord(selectedHashTable, selectedItem))
                    deleteButton.BackColor = Color.Green;
                else
                    deleteButton.BackColor = Color.Red;
            }
            else
                deleteButton.BackColor = Color.Red;
        }

        private void reloadButton_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            foreach (var entry in hashTableIdentifier)
                listBox1.Items.Add(string.Join(", ", entry.Value));
            foreach (var entry in hashTableDigital)
                listBox2.Items.Add(string.Join(", ", entry.Value));
            foreach (var entry in hashTableSpecial)
                listBox3.Items.Add(string.Join(", ", entry.Value));
        }

        private void findButton_Click(object sender, EventArgs e)
        {
            string searchTerm = textBox1.Text;

            if (string.IsNullOrEmpty(searchTerm))
            {
                MessageBox.Show("Введите поисковое значение.");
                return;
            }

            listBox1.ClearSelected();
            listBox2.ClearSelected();
            listBox3.ClearSelected();

            if (FindAndSelectInListBox(listBox1, searchTerm))
            {
                findButton.BackColor = Color.Green;
                return;
            }

            if (FindAndSelectInListBox(listBox2, searchTerm))
            {
                findButton.BackColor = Color.Green;
                return;
            }

            if (FindAndSelectInListBox(listBox3, searchTerm))
            {
                findButton.BackColor = Color.Green;
                return;
            }
            findButton.BackColor = Color.Red;
        }

        // Метод для поиска и выделения элемента в ListBox
        private bool FindAndSelectInListBox(ListBox listBox, string searchTerm)
        {
            for (int i = 0; i < listBox.Items.Count; i++)
            {
                if (listBox.Items[i].ToString().Equals(searchTerm, StringComparison.OrdinalIgnoreCase))
                {
                    listBox.SelectedIndex = i;
                    return true;
                }
            }
            return false;
        }

        private void changeButton_Click(object sender, EventArgs e)
        {
            ListBox selectedListBox = GetSelectedListBox(); // Метод для определения выбранного ListBox
            Dictionary<string, List<string>> selectedHashTable = GetSelectedHashTable(selectedListBox); // Метод для получения нужной хэш-таблицы

            if (selectedListBox != null && selectedHashTable != null && selectedListBox.SelectedItem != null)
            {
                string selectedItem = selectedListBox.SelectedItem.ToString();

                // Удаляем выбранное слово
                if (hashFunction.RemoveWord(selectedHashTable, selectedItem))
                {
                    // Добавляем новое слово из textBox1
                    hashFunction.AddWord(selectedHashTable, textBox1.Text.ToString());
                    changeButton.BackColor = Color.Green;
                }
                else
                    changeButton.BackColor = Color.Red;
            }
            else
                changeButton.BackColor = Color.Red;
        }

        // Метод для определения, какой ListBox был выбран
        private ListBox GetSelectedListBox()
        {
            if (listBox1.SelectedItem != null)
                return listBox1;
            if (listBox2.SelectedItem != null)
                return listBox2;
            if (listBox3.SelectedItem != null)
                return listBox3;

            return null; // Если ни один элемент не выбран
        }
        // Метод для выбора соответствующей хэш-таблицы
        private Dictionary<string, List<string>> GetSelectedHashTable(ListBox listBox)
        {
            if (listBox == listBox1)
                return hashTableIdentifier;
            if (listBox == listBox2)
                return hashTableDigital;
            if (listBox == listBox3)
                return hashTableSpecial;

            return null; // Если ListBox не совпадает ни с одним из ожидаемых
        }

        private void button2_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            parserTree = new TreeV(treeView1);
            parserTree.Lex.strPSource = tbFSource.Lines;
            parserTree.Lex.strPMessage = tbFMessage.Lines;
            parserTree.Lex.enumPState = TState.Start;

            parserTree.Lex.intPSourceRowSelection = 0;
            parserTree.Lex.intPSourceColSelection = 0;

            try
            {
                for (int i = 0; i < parserTree.Lex.strPSource.Length; i++)  // Обрабатываем каждую строку
                {

                    string line = parserTree.Lex.strPSource[i];  // Получаем текущую строку
                    parserTree.Lex.NextToken();  // Получаем следующий токен
                    parserTree.S();  // Проверяем синтаксис текущей строки

                    // Переход на следующую строку после завершения анализа одной строки
                    if (parserTree.Lex.enumPState == TState.Continue && i < parserTree.Lex.strPSource.Length - 1)
                        parserTree.Lex.enumPState = TState.Start;  // Возвращаем состояние в начало
                }

                treeView1.ExpandAll();
                CreateSampleTree(treeView2, parserTree);
            }
            catch (Exception ex)
            {
                // Выводим сообщение об ошибке
                MessageBox.Show("Ошибка разбора: " + ex.Message);
            }
        }

        private void InitializeDrawingPanel()
        {
            drawingPanel = new Panel
            {
                Location = new Point(36, 300),
                Size = new Size(202, 130),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };
            Controls.Add(drawingPanel);
            drawingPanel.Paint += new PaintEventHandler(DrawLine);
        }

        private void DrawLinesFromPoints(Graphics g)
        {
            List<int> xValues = Synt.Points;
            int fixedY = 75;
            if (xValues.Count > 1)
            {
                for (int i = 0; i < xValues.Count - 1; i++)
                    g.DrawLine(Pens.Black, xValues[i], fixedY, xValues[i + 1], fixedY);
            }
        }

        private void DrawLine(object sender, PaintEventArgs e)
        {
            if (isFirstButtonValid)
            {
                Graphics g = e.Graphics;
                DrawLinesFromPoints(g);
            }
            else e.Graphics.Clear(drawingPanel.BackColor); 
        }

        public void CreateSampleTree(TreeView treeView2, TreeV parserTree)
        {
            treeView2.Nodes.Clear();
            // Первый вариант: a -> 001001... -> ac | ad | acc
            TreeNode root1 = new TreeNode("a");
            TreeNode node1 = new TreeNode("001001...");
            TreeNode acNode = new TreeNode("ac");
            TreeNode adNode = new TreeNode("ad");
            TreeNode accNode = new TreeNode("acc");

            node1.Nodes.Add(acNode);
            node1.Nodes.Add(adNode);
            node1.Nodes.Add(accNode);
            SetSpecificNodeColor(treeView2, acNode, Brushes.Green, Brushes.White, parserTree); // Закрашиваем "ac"
            root1.Nodes.Add(node1);
            treeView2.Nodes.Add(root1);


            // Второй вариант: a -> 00... -> abc | ab | abcd | aacd
            TreeNode node2 = new TreeNode("00...");
            TreeNode abNode = new TreeNode("ab");
            TreeNode abсNode = new TreeNode("abc");
            TreeNode abcdNode = new TreeNode("abcd");
            TreeNode aacdNode = new TreeNode("aacd");

            node2.Nodes.Add(abNode);
            node2.Nodes.Add(abсNode);
            node2.Nodes.Add(abcdNode);
            node2.Nodes.Add(aacdNode);
            SetSpecificNodeColor(treeView2, abсNode, Brushes.Green, Brushes.White, parserTree);

            TreeNode root2 = new TreeNode("a");
            root2.Nodes.Add(node2);
            treeView2.Nodes.Add(root2);

            // Третий вариант: abc -> 00... -> a
            TreeNode root3 = new TreeNode("abc");
            TreeNode node3 = new TreeNode("00...");
            node3.Nodes.Add(new TreeNode("a"));
            root3.Nodes.Add(node3);
            treeView2.Nodes.Add(root3);

            // Четвёртый вариант: ab -> 00... -> a
            TreeNode root4 = new TreeNode("ab");
            TreeNode node4 = new TreeNode("00...");
            node4.Nodes.Add(new TreeNode("a"));
            root4.Nodes.Add(node4);
            treeView2.Nodes.Add(root4);

            // Пятый вариант: abcd -> 00... -> a
            TreeNode root5 = new TreeNode("abcd");
            TreeNode node5 = new TreeNode("00...");
            node5.Nodes.Add(new TreeNode("a"));
            root5.Nodes.Add(node5);
            treeView2.Nodes.Add(root5);

            // Шестой вариант: aacd -> 00... -> a
            TreeNode root6 = new TreeNode("aacd");
            TreeNode node6 = new TreeNode("00...");
            node6.Nodes.Add(new TreeNode("a"));
            root6.Nodes.Add(node6);
            treeView2.Nodes.Add(root6);

            // Седьмой вариант: ac -> 001001... -> a
            TreeNode root7 = new TreeNode("ac");
            TreeNode node7 = new TreeNode("001001...");
            node7.Nodes.Add(new TreeNode("a"));
            root7.Nodes.Add(node7);
            treeView2.Nodes.Add(root7);

            // Восьмой вариант: ad -> 001001... -> a
            TreeNode root8 = new TreeNode("ad");
            TreeNode node8 = new TreeNode("001001...");
            node8.Nodes.Add(new TreeNode("a"));
            root8.Nodes.Add(node8);
            treeView2.Nodes.Add(root8);

            // Девятый вариант: acc -> 001001... -> a
            TreeNode root9 = new TreeNode("acc");
            TreeNode node9 = new TreeNode("001001...");
            node9.Nodes.Add(new TreeNode("a"));
            root9.Nodes.Add(node9);
            treeView2.Nodes.Add(root9);

            treeView2.ExpandAll(); // Раскрыть все узлы
        }
        // Функция для закрашивания определенного узла
        public void SetSpecificNodeColor(TreeView treeView, TreeNode targetNode, Brush backgroundColor, Brush textColor, TreeV parserTree)
        {
            // Устанавливаем режим пользовательской отрисовки
            treeView.DrawMode = TreeViewDrawMode.OwnerDrawText;

            // Убираем старый обработчик, если он был установлен
            if (customDrawNodeHandler != null)
            {
                treeView.DrawNode -= customDrawNodeHandler;
            }

            // Определяем новый обработчик
            customDrawNodeHandler = (sender, e) =>
            {
                if (e.Node == targetNode && e.Node.Text == parserTree.identifier) // Проверяем узел
                {
                    // Закрашиваем фон и текст
                    e.Graphics.FillRectangle(backgroundColor, e.Bounds);
                    e.Graphics.DrawString(e.Node.Text, treeView.Font, textColor, e.Bounds);
                }
                else
                {
                    e.DrawDefault = true; // Используем стандартную отрисовку
                }
            };

            // Привязываем новый обработчик
            treeView.DrawNode += customDrawNodeHandler;

            // Перерисовываем дерево
            treeView.Invalidate();
        }
    }
}



    //// Обработчик события нажатия на кнопку btnFStart
    //private void btnFStart_Click(object sender, EventArgs e)
    //{
    //    // Создание экземпляра класса CLex для анализа текста
    //    CLex Lex = new CLex();
    //    Lex.strPSource = tbFSource.Lines; // Устанавливаем источник текста из текстового поля tbFSourceи
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
//}
    


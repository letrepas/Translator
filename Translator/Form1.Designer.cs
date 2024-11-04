namespace Translator
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.tbFSource = new System.Windows.Forms.TextBox();
            this.tbFMessage = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.writeButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.findButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.changeButton = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.listBox3 = new System.Windows.Forms.ListBox();
            this.reloadButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(362, 85);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(102, 34);
            this.button1.TabIndex = 0;
            this.button1.Text = "sumbit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnFStart_Click);
            // 
            // tbFSource
            // 
            this.tbFSource.Location = new System.Drawing.Point(54, 26);
            this.tbFSource.Multiline = true;
            this.tbFSource.Name = "tbFSource";
            this.tbFSource.Size = new System.Drawing.Size(295, 92);
            this.tbFSource.TabIndex = 1;
            // 
            // tbFMessage
            // 
            this.tbFMessage.Location = new System.Drawing.Point(54, 162);
            this.tbFMessage.Multiline = true;
            this.tbFMessage.Name = "tbFMessage";
            this.tbFMessage.Size = new System.Drawing.Size(608, 141);
            this.tbFMessage.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(66, 138);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Результат";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(66, 311);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Первое слово";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(369, 311);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Второе слово";
            // 
            // writeButton
            // 
            this.writeButton.Enabled = false;
            this.writeButton.Location = new System.Drawing.Point(477, 85);
            this.writeButton.Name = "writeButton";
            this.writeButton.Size = new System.Drawing.Size(102, 34);
            this.writeButton.TabIndex = 8;
            this.writeButton.Text = "write";
            this.writeButton.UseVisualStyleBackColor = true;
            this.writeButton.Click += new System.EventHandler(this.writeButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(668, 311);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(142, 20);
            this.label4.TabIndex = 10;
            this.label4.Text = "Служебное слово";
            // 
            // findButton
            // 
            this.findButton.Location = new System.Drawing.Point(592, 85);
            this.findButton.Name = "findButton";
            this.findButton.Size = new System.Drawing.Size(102, 34);
            this.findButton.TabIndex = 11;
            this.findButton.Text = "find";
            this.findButton.UseVisualStyleBackColor = true;
            this.findButton.Click += new System.EventHandler(this.findButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(708, 85);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(102, 34);
            this.deleteButton.TabIndex = 12;
            this.deleteButton.Text = "delete";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // changeButton
            // 
            this.changeButton.Location = new System.Drawing.Point(825, 85);
            this.changeButton.Name = "changeButton";
            this.changeButton.Size = new System.Drawing.Size(102, 34);
            this.changeButton.TabIndex = 13;
            this.changeButton.Text = "change";
            this.changeButton.UseVisualStyleBackColor = true;
            this.changeButton.Click += new System.EventHandler(this.changeButton_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 20;
            this.listBox1.Location = new System.Drawing.Point(54, 343);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(301, 104);
            this.listBox1.TabIndex = 14;
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 20;
            this.listBox2.Location = new System.Drawing.Point(363, 343);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(301, 104);
            this.listBox2.TabIndex = 15;
            // 
            // listBox3
            // 
            this.listBox3.FormattingEnabled = true;
            this.listBox3.ItemHeight = 20;
            this.listBox3.Location = new System.Drawing.Point(672, 343);
            this.listBox3.Name = "listBox3";
            this.listBox3.Size = new System.Drawing.Size(301, 104);
            this.listBox3.TabIndex = 16;
            // 
            // reloadButton
            // 
            this.reloadButton.Location = new System.Drawing.Point(873, 271);
            this.reloadButton.Name = "reloadButton";
            this.reloadButton.Size = new System.Drawing.Size(102, 34);
            this.reloadButton.TabIndex = 17;
            this.reloadButton.Text = "reload";
            this.reloadButton.UseVisualStyleBackColor = true;
            this.reloadButton.Click += new System.EventHandler(this.reloadButton_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(672, 162);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(298, 101);
            this.textBox1.TabIndex = 18;
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(994, 26);
            this.treeView1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(288, 421);
            this.treeView1.TabIndex = 19;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(825, 23);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(102, 34);
            this.button2.TabIndex = 20;
            this.button2.Text = "build tree";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1323, 680);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.reloadButton);
            this.Controls.Add(this.listBox3);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.changeButton);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.findButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.writeButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbFMessage);
            this.Controls.Add(this.tbFSource);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Транслитератор";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tbFSource;
        private System.Windows.Forms.TextBox tbFMessage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button writeButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button findButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button changeButton;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.ListBox listBox3;
        private System.Windows.Forms.Button reloadButton;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Button button2;
    }
}


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
            this.treeView2 = new System.Windows.Forms.TreeView();
            this.button3 = new System.Windows.Forms.Button();
            this.listBox4 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(358, 17);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(68, 22);
            this.button1.TabIndex = 0;
            this.button1.Text = "sumbit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnFStart_Click);
            // 
            // tbFSource
            // 
            this.tbFSource.Location = new System.Drawing.Point(36, 19);
            this.tbFSource.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbFSource.Multiline = true;
            this.tbFSource.Name = "tbFSource";
            this.tbFSource.Size = new System.Drawing.Size(193, 58);
            this.tbFSource.TabIndex = 1;
            // 
            // tbFMessage
            // 
            this.tbFMessage.Location = new System.Drawing.Point(36, 92);
            this.tbFMessage.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbFMessage.Multiline = true;
            this.tbFMessage.Name = "tbFMessage";
            this.tbFMessage.Size = new System.Drawing.Size(600, 110);
            this.tbFMessage.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 77);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Результат";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 203);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Первое слово";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(240, 203);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Второе слово";
            // 
            // writeButton
            // 
            this.writeButton.Enabled = false;
            this.writeButton.Location = new System.Drawing.Point(358, 43);
            this.writeButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.writeButton.Name = "writeButton";
            this.writeButton.Size = new System.Drawing.Size(68, 22);
            this.writeButton.TabIndex = 8;
            this.writeButton.Text = "write";
            this.writeButton.UseVisualStyleBackColor = true;
            this.writeButton.Click += new System.EventHandler(this.writeButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(441, 203);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Служебное слово";
            // 
            // findButton
            // 
            this.findButton.Location = new System.Drawing.Point(429, 17);
            this.findButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.findButton.Name = "findButton";
            this.findButton.Size = new System.Drawing.Size(68, 22);
            this.findButton.TabIndex = 11;
            this.findButton.Text = "find";
            this.findButton.UseVisualStyleBackColor = true;
            this.findButton.Click += new System.EventHandler(this.findButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(501, 17);
            this.deleteButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(68, 22);
            this.deleteButton.TabIndex = 12;
            this.deleteButton.Text = "delete";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // changeButton
            // 
            this.changeButton.Location = new System.Drawing.Point(430, 43);
            this.changeButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.changeButton.Name = "changeButton";
            this.changeButton.Size = new System.Drawing.Size(68, 22);
            this.changeButton.TabIndex = 13;
            this.changeButton.Text = "change";
            this.changeButton.UseVisualStyleBackColor = true;
            this.changeButton.Click += new System.EventHandler(this.changeButton_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(36, 217);
            this.listBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(197, 69);
            this.listBox1.TabIndex = 14;
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(237, 218);
            this.listBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(197, 69);
            this.listBox2.TabIndex = 15;
            // 
            // listBox3
            // 
            this.listBox3.FormattingEnabled = true;
            this.listBox3.Location = new System.Drawing.Point(438, 218);
            this.listBox3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.listBox3.Name = "listBox3";
            this.listBox3.Size = new System.Drawing.Size(198, 69);
            this.listBox3.TabIndex = 16;
            // 
            // reloadButton
            // 
            this.reloadButton.Location = new System.Drawing.Point(574, 43);
            this.reloadButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.reloadButton.Name = "reloadButton";
            this.reloadButton.Size = new System.Drawing.Size(68, 22);
            this.reloadButton.TabIndex = 17;
            this.reloadButton.Text = "reload";
            this.reloadButton.UseVisualStyleBackColor = true;
            this.reloadButton.Click += new System.EventHandler(this.reloadButton_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(241, 16);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(113, 59);
            this.textBox1.TabIndex = 18;
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(843, 17);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(193, 438);
            this.treeView1.TabIndex = 19;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(573, 17);
            this.button2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(68, 22);
            this.button2.TabIndex = 20;
            this.button2.Text = "build tree";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // treeView2
            // 
            this.treeView2.Location = new System.Drawing.Point(644, 218);
            this.treeView2.Name = "treeView2";
            this.treeView2.Size = new System.Drawing.Size(193, 237);
            this.treeView2.TabIndex = 21;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(502, 43);
            this.button3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(68, 22);
            this.button3.TabIndex = 22;
            this.button3.Text = "generate";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // listBox4
            // 
            this.listBox4.FormattingEnabled = true;
            this.listBox4.Location = new System.Drawing.Point(645, 16);
            this.listBox4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.listBox4.Name = "listBox4";
            this.listBox4.Size = new System.Drawing.Size(193, 186);
            this.listBox4.TabIndex = 23;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1087, 479);
            this.Controls.Add(this.listBox4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.treeView2);
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
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
        private System.Windows.Forms.TreeView treeView2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ListBox listBox4;
    }
}


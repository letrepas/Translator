namespace WindowsFormsApp45
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnF = new System.Windows.Forms.Button();
            this.tbFMessage = new System.Windows.Forms.TextBox();
            this.tbFSource = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // btnF
            // 
            this.btnF.Location = new System.Drawing.Point(371, 12);
            this.btnF.Name = "btnF";
            this.btnF.Size = new System.Drawing.Size(75, 23);
            this.btnF.TabIndex = 0;
            this.btnF.Text = "button1";
            this.btnF.UseVisualStyleBackColor = true;
            this.btnF.Click += new System.EventHandler(this.btnFStart_Click);
            // 
            // tbFMessage
            // 
            this.tbFMessage.Location = new System.Drawing.Point(12, 135);
            this.tbFMessage.Name = "tbFMessage";
            this.tbFMessage.Size = new System.Drawing.Size(353, 20);
            this.tbFMessage.TabIndex = 1;
            // 
            // tbFSource
            // 
            this.tbFSource.Location = new System.Drawing.Point(12, 14);
            this.tbFSource.Name = "tbFSource";
            this.tbFSource.Size = new System.Drawing.Size(353, 96);
            this.tbFSource.TabIndex = 2;
            this.tbFSource.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 167);
            this.Controls.Add(this.tbFSource);
            this.Controls.Add(this.tbFMessage);
            this.Controls.Add(this.btnF);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnF;
        private System.Windows.Forms.TextBox tbFMessage;
        private System.Windows.Forms.RichTextBox tbFSource;
    }
}


namespace DrabadanCoreLib.UOClientInteractions.GetSomethingFromTarget.GUI
{
    partial class GetSomethingFromTarget_UI
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

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Type_label = new System.Windows.Forms.Label();
            this.ID_label = new System.Windows.Forms.Label();
            this.GetId_button = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.Type_label);
            this.panel1.Controls.Add(this.ID_label);
            this.panel1.Controls.Add(this.GetId_button);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(127, 98);
            this.panel1.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(0, 57);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(124, 41);
            this.textBox1.TabIndex = 3;
            // 
            // Type_label
            // 
            this.Type_label.AutoSize = true;
            this.Type_label.Location = new System.Drawing.Point(0, 41);
            this.Type_label.Name = "Type_label";
            this.Type_label.Size = new System.Drawing.Size(37, 13);
            this.Type_label.TabIndex = 2;
            this.Type_label.Text = "Type: ";
            // 
            // ID_label
            // 
            this.ID_label.AutoSize = true;
            this.ID_label.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ID_label.Location = new System.Drawing.Point(0, 26);
            this.ID_label.Name = "ID_label";
            this.ID_label.Size = new System.Drawing.Size(24, 13);
            this.ID_label.TabIndex = 1;
            this.ID_label.Text = "ID: ";
            // 
            // GetId_button
            // 
            this.GetId_button.Dock = System.Windows.Forms.DockStyle.Top;
            this.GetId_button.Location = new System.Drawing.Point(0, 0);
            this.GetId_button.Name = "GetId_button";
            this.GetId_button.Size = new System.Drawing.Size(127, 26);
            this.GetId_button.TabIndex = 0;
            this.GetId_button.Text = "Get Info";
            this.GetId_button.UseVisualStyleBackColor = true;
            this.GetId_button.Click += new System.EventHandler(this.GetId_button_Click);
            // 
            // GetSomethingFromTarget_UI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "GetSomethingFromTarget_UI";
            this.Size = new System.Drawing.Size(127, 98);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label ID_label;
        private System.Windows.Forms.Button GetId_button;
        private System.Windows.Forms.Label Type_label;
        private System.Windows.Forms.TextBox textBox1;
    }
}

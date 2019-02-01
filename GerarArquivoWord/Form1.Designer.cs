namespace GerarArquivoWord
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
            this.btnGerar = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnGerarDocx2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnGerar
            // 
            this.btnGerar.Location = new System.Drawing.Point(32, 389);
            this.btnGerar.Name = "btnGerar";
            this.btnGerar.Size = new System.Drawing.Size(98, 23);
            this.btnGerar.TabIndex = 0;
            this.btnGerar.Text = "Gerar Petição";
            this.btnGerar.UseVisualStyleBackColor = true;
            this.btnGerar.Click += new System.EventHandler(this.btnGerar_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(170, 389);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Gerar Docx";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnGerarDocx2
            // 
            this.btnGerarDocx2.Location = new System.Drawing.Point(285, 389);
            this.btnGerarDocx2.Name = "btnGerarDocx2";
            this.btnGerarDocx2.Size = new System.Drawing.Size(93, 23);
            this.btnGerarDocx2.TabIndex = 2;
            this.btnGerarDocx2.Text = "Gerar DocX1.2";
            this.btnGerarDocx2.UseVisualStyleBackColor = true;
            this.btnGerarDocx2.Click += new System.EventHandler(this.btnGerarDocx2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnGerarDocx2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnGerar);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGerar;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnGerarDocx2;
    }
}


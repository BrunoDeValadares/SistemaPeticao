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
            this.components = new System.ComponentModel.Container();
            this.btnGerar = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnGerarDocx2 = new System.Windows.Forms.Button();
            this.btnGerarDocX3 = new System.Windows.Forms.Button();
            this.btnGerarDocX4 = new System.Windows.Forms.Button();
            this.btnGerarDocX5 = new System.Windows.Forms.Button();
            this.btnGerarDocx6 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.advocaciaDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.advocaciaDataSet = new GerarArquivoWord.advocaciaDataSet();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.advocaciaDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.advocaciaDataSet)).BeginInit();
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
            this.button1.Location = new System.Drawing.Point(136, 389);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Gerar Docx";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnGerarDocx2
            // 
            this.btnGerarDocx2.Location = new System.Drawing.Point(227, 389);
            this.btnGerarDocx2.Name = "btnGerarDocx2";
            this.btnGerarDocx2.Size = new System.Drawing.Size(93, 23);
            this.btnGerarDocx2.TabIndex = 2;
            this.btnGerarDocx2.Text = "Gerar DocX1.2";
            this.btnGerarDocx2.UseVisualStyleBackColor = true;
            this.btnGerarDocx2.Click += new System.EventHandler(this.btnGerarDocx2_Click);
            // 
            // btnGerarDocX3
            // 
            this.btnGerarDocX3.Location = new System.Drawing.Point(326, 389);
            this.btnGerarDocX3.Name = "btnGerarDocX3";
            this.btnGerarDocX3.Size = new System.Drawing.Size(107, 23);
            this.btnGerarDocX3.TabIndex = 3;
            this.btnGerarDocX3.Text = "Gerar Docx 1.3";
            this.btnGerarDocX3.UseVisualStyleBackColor = true;
            this.btnGerarDocX3.Click += new System.EventHandler(this.btnGerarDocX3_Click);
            // 
            // btnGerarDocX4
            // 
            this.btnGerarDocX4.Location = new System.Drawing.Point(439, 389);
            this.btnGerarDocX4.Name = "btnGerarDocX4";
            this.btnGerarDocX4.Size = new System.Drawing.Size(121, 23);
            this.btnGerarDocX4.TabIndex = 4;
            this.btnGerarDocX4.Text = "Gerar DocX 1.4";
            this.btnGerarDocX4.UseVisualStyleBackColor = true;
            this.btnGerarDocX4.Click += new System.EventHandler(this.btnGerarDocX4_Click);
            // 
            // btnGerarDocX5
            // 
            this.btnGerarDocX5.Location = new System.Drawing.Point(566, 389);
            this.btnGerarDocX5.Name = "btnGerarDocX5";
            this.btnGerarDocX5.Size = new System.Drawing.Size(115, 23);
            this.btnGerarDocX5.TabIndex = 5;
            this.btnGerarDocX5.Text = "btnGerarDocX5";
            this.btnGerarDocX5.UseVisualStyleBackColor = true;
            this.btnGerarDocX5.Click += new System.EventHandler(this.btnGerarDocX5_Click);
            // 
            // btnGerarDocx6
            // 
            this.btnGerarDocx6.Location = new System.Drawing.Point(696, 389);
            this.btnGerarDocx6.Name = "btnGerarDocx6";
            this.btnGerarDocx6.Size = new System.Drawing.Size(110, 23);
            this.btnGerarDocx6.TabIndex = 6;
            this.btnGerarDocx6.Text = "GerarDocsx6";
            this.btnGerarDocx6.UseVisualStyleBackColor = true;
            this.btnGerarDocx6.Click += new System.EventHandler(this.btnGerarDocx6_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.DataSource = this.advocaciaDataSetBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(80, 81);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(685, 150);
            this.dataGridView1.TabIndex = 7;
            // 
            // advocaciaDataSetBindingSource
            // 
            this.advocaciaDataSetBindingSource.DataSource = this.advocaciaDataSet;
            this.advocaciaDataSetBindingSource.Position = 0;
            this.advocaciaDataSetBindingSource.CurrentChanged += new System.EventHandler(this.advocaciaDataSetBindingSource_CurrentChanged);
            // 
            // advocaciaDataSet
            // 
            this.advocaciaDataSet.DataSetName = "advocaciaDataSet";
            this.advocaciaDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(821, 389);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1093, 453);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnGerarDocx6);
            this.Controls.Add(this.btnGerarDocX5);
            this.Controls.Add(this.btnGerarDocX4);
            this.Controls.Add(this.btnGerarDocX3);
            this.Controls.Add(this.btnGerarDocx2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnGerar);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.advocaciaDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.advocaciaDataSet)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button btnGerar;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnGerarDocx2;
        private System.Windows.Forms.Button btnGerarDocX3;
        private System.Windows.Forms.Button btnGerarDocX4;
        private System.Windows.Forms.Button btnGerarDocX5;
        private System.Windows.Forms.Button btnGerarDocx6;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.BindingSource advocaciaDataSetBindingSource;
        private advocaciaDataSet advocaciaDataSet;
    }
}


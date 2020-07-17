namespace ReminderApplicationWinForms
{
    partial class PatientDisplayer
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.patientGrid = new System.Windows.Forms.DataGridView();
            this.patientBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.checkBoxOverdue = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.patientGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.patientBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // patientGrid
            // 
            this.patientGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.patientGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.patientGrid.Location = new System.Drawing.Point(15, 16);
            this.patientGrid.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.patientGrid.Name = "patientGrid";
            this.patientGrid.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.patientGrid.Size = new System.Drawing.Size(548, 313);
            this.patientGrid.TabIndex = 1;
            this.patientGrid.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.patientGrid_RowValidating);
            // 
            // checkBoxOverdue
            // 
            this.checkBoxOverdue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxOverdue.AutoSize = true;
            this.checkBoxOverdue.Location = new System.Drawing.Point(15, 337);
            this.checkBoxOverdue.Name = "checkBoxOverdue";
            this.checkBoxOverdue.Size = new System.Drawing.Size(325, 17);
            this.checkBoxOverdue.TabIndex = 3;
            this.checkBoxOverdue.Text = "Показать только пациентов, которым нужно напоминание";
            this.checkBoxOverdue.UseVisualStyleBackColor = true;
            this.checkBoxOverdue.CheckedChanged += new System.EventHandler(this.CheckBoxOverdueCheckedChanged);
            // 
            // PatientDisplayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkBoxOverdue);
            this.Controls.Add(this.patientGrid);
            this.Name = "PatientDisplayer";
            this.Size = new System.Drawing.Size(579, 354);
            this.Load += new System.EventHandler(this.PatientDisplayerLoad);
            ((System.ComponentModel.ISupportInitialize)(this.patientGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.patientBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView patientGrid;
        private System.Windows.Forms.BindingSource patientBindingSource;
        private System.Windows.Forms.CheckBox checkBoxOverdue;
    }
}

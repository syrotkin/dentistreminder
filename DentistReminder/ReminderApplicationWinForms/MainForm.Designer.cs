namespace ReminderApplicationWinForms
{
    partial class MainForm
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
            this.patientGrid = new System.Windows.Forms.DataGridView();
            this.columnId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnLastName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnFirstName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnPatronymic = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnPhoneNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnLastVisit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.patientGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // patientGrid
            // 
            this.patientGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.patientGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnId,
            this.columnLastName,
            this.columnFirstName,
            this.columnPatronymic,
            this.columnPhoneNumber,
            this.columnLastVisit});
            this.patientGrid.Location = new System.Drawing.Point(12, 31);
            this.patientGrid.Name = "patientGrid";
            this.patientGrid.Size = new System.Drawing.Size(753, 277);
            this.patientGrid.TabIndex = 0;
            // 
            // columnId
            // 
            this.columnId.HeaderText = "ID";
            this.columnId.Name = "columnId";
            this.columnId.ReadOnly = true;
            // 
            // columnLastName
            // 
            this.columnLastName.HeaderText = "Last Name";
            this.columnLastName.Name = "columnLastName";
            // 
            // columnFirstName
            // 
            this.columnFirstName.HeaderText = "First Name";
            this.columnFirstName.Name = "columnFirstName";
            // 
            // columnPatronymic
            // 
            this.columnPatronymic.HeaderText = "Patronymic";
            this.columnPatronymic.Name = "columnPatronymic";
            // 
            // columnPhoneNumber
            // 
            this.columnPhoneNumber.HeaderText = "Phone Number";
            this.columnPhoneNumber.Name = "columnPhoneNumber";
            // 
            // columnLastVisit
            // 
            this.columnLastVisit.HeaderText = "Last Visit";
            this.columnLastVisit.Name = "columnLastVisit";
            this.columnLastVisit.Width = 150;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(793, 347);
            this.Controls.Add(this.patientGrid);
            this.Name = "MainForm";
            this.Text = "Dentist\'s Reminder";
            this.Load += new System.EventHandler(this.MainFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.patientGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView patientGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnId;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnLastName;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnFirstName;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnPatronymic;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnPhoneNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnLastVisit;
    }
}


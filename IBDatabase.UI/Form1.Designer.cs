namespace IBDatabase.UI
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
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.databaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contractsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quotesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sendBarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.futureContractsCsvToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quotesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.redisToCsvToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quotesToCsvToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.receiveQuotesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialogFeed = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(107, 102);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 23);
            this.buttonStart.TabIndex = 0;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.ButtonStart_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(213, 102);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(75, 23);
            this.buttonStop.TabIndex = 1;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.ButtonStop_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(107, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Press Start";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.databaseToolStripMenuItem,
            this.quotesToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(462, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // databaseToolStripMenuItem
            // 
            this.databaseToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contractsToolStripMenuItem,
            this.quotesToolStripMenuItem,
            this.sendBarToolStripMenuItem,
            this.futureContractsCsvToolStripMenuItem});
            this.databaseToolStripMenuItem.Name = "databaseToolStripMenuItem";
            this.databaseToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.databaseToolStripMenuItem.Text = "Database";
            // 
            // contractsToolStripMenuItem
            // 
            this.contractsToolStripMenuItem.Name = "contractsToolStripMenuItem";
            this.contractsToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.contractsToolStripMenuItem.Text = "Contracts Sync";
            this.contractsToolStripMenuItem.Click += new System.EventHandler(this.ContractsToolStripMenuItem_Click);
            // 
            // quotesToolStripMenuItem
            // 
            this.quotesToolStripMenuItem.Name = "quotesToolStripMenuItem";
            this.quotesToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.quotesToolStripMenuItem.Text = "Quotes";
            this.quotesToolStripMenuItem.Click += new System.EventHandler(this.QuotesToolStripMenuItem_Click);
            // 
            // sendBarToolStripMenuItem
            // 
            this.sendBarToolStripMenuItem.Name = "sendBarToolStripMenuItem";
            this.sendBarToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.sendBarToolStripMenuItem.Text = "Send Bar";
            this.sendBarToolStripMenuItem.Click += new System.EventHandler(this.SendBarToolStripMenuItem_Click);
            // 
            // futureContractsCsvToolStripMenuItem
            // 
            this.futureContractsCsvToolStripMenuItem.Name = "futureContractsCsvToolStripMenuItem";
            this.futureContractsCsvToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.futureContractsCsvToolStripMenuItem.Text = "Future Contracts csv";
            this.futureContractsCsvToolStripMenuItem.Click += new System.EventHandler(this.FutureContractsCsvToolStripMenuItem_Click);
            // 
            // quotesToolStripMenuItem1
            // 
            this.quotesToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.redisToCsvToolStripMenuItem,
            this.quotesToCsvToolStripMenuItem,
            this.receiveQuotesToolStripMenuItem});
            this.quotesToolStripMenuItem1.Name = "quotesToolStripMenuItem1";
            this.quotesToolStripMenuItem1.Size = new System.Drawing.Size(57, 20);
            this.quotesToolStripMenuItem1.Text = "Quotes";
            // 
            // redisToCsvToolStripMenuItem
            // 
            this.redisToCsvToolStripMenuItem.Name = "redisToCsvToolStripMenuItem";
            this.redisToCsvToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.redisToCsvToolStripMenuItem.Text = "RedisToCsv";
            this.redisToCsvToolStripMenuItem.Click += new System.EventHandler(this.RedisToCsvToolStripMenuItem_Click);
            // 
            // quotesToCsvToolStripMenuItem
            // 
            this.quotesToCsvToolStripMenuItem.Name = "quotesToCsvToolStripMenuItem";
            this.quotesToCsvToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.quotesToCsvToolStripMenuItem.Text = "QuotesToCsv";
            this.quotesToCsvToolStripMenuItem.Click += new System.EventHandler(this.QuotesToCsvToolStripMenuItem_Click);
            // 
            // receiveQuotesToolStripMenuItem
            // 
            this.receiveQuotesToolStripMenuItem.Name = "receiveQuotesToolStripMenuItem";
            this.receiveQuotesToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.receiveQuotesToolStripMenuItem.Text = "Receive Quotes";
            this.receiveQuotesToolStripMenuItem.Click += new System.EventHandler(this.ReceiveQuotesToolStripMenuItem_Click);
            // 
            // openFileDialogFeed
            // 
            this.openFileDialogFeed.InitialDirectory = "H:\\IBData";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 261);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "IB Database";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem databaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem contractsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quotesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sendBarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quotesToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem redisToCsvToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quotesToCsvToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialogFeed;
        private System.Windows.Forms.ToolStripMenuItem futureContractsCsvToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem receiveQuotesToolStripMenuItem;
    }
}


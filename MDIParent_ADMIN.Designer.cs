namespace NL_BANK
{
    partial class MDIParent_ADMIN
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MDIParent_ADMIN));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.customerAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Customer = new System.Windows.Forms.ToolStripMenuItem();
            this.bANKSERVICESToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cUSTOMERACCOUNTSERVICEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.systemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label_RealCurrentUserName = new System.Windows.Forms.Label();
            this.label_CurrentUser = new System.Windows.Forms.Label();
            this.statusStrip.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 604);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1442, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "StatusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(39, 17);
            this.toolStripStatusLabel.Text = "Status";
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.customerAccountToolStripMenuItem,
            this.bANKSERVICESToolStripMenuItem,
            this.systemToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1442, 31);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "MenuStrip";
            // 
            // customerAccountToolStripMenuItem
            // 
            this.customerAccountToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_Customer});
            this.customerAccountToolStripMenuItem.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customerAccountToolStripMenuItem.Name = "customerAccountToolStripMenuItem";
            this.customerAccountToolStripMenuItem.Size = new System.Drawing.Size(207, 27);
            this.customerAccountToolStripMenuItem.Text = "MASTER ACCOUNT";
            this.customerAccountToolStripMenuItem.Click += new System.EventHandler(this.customerAccountToolStripMenuItem_Click);
            // 
            // toolStripMenuItem_Customer
            // 
            this.toolStripMenuItem_Customer.Name = "toolStripMenuItem_Customer";
            this.toolStripMenuItem_Customer.Size = new System.Drawing.Size(284, 28);
            this.toolStripMenuItem_Customer.Text = "Customer Bank Account";
            this.toolStripMenuItem_Customer.Click += new System.EventHandler(this.toolStripMenuItem_Customer_Click);
            // 
            // bANKSERVICESToolStripMenuItem
            // 
            this.bANKSERVICESToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cUSTOMERACCOUNTSERVICEToolStripMenuItem});
            this.bANKSERVICESToolStripMenuItem.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bANKSERVICESToolStripMenuItem.Name = "bANKSERVICESToolStripMenuItem";
            this.bANKSERVICESToolStripMenuItem.Size = new System.Drawing.Size(180, 27);
            this.bANKSERVICESToolStripMenuItem.Text = "BANK SERVICES";
            // 
            // cUSTOMERACCOUNTSERVICEToolStripMenuItem
            // 
            this.cUSTOMERACCOUNTSERVICEToolStripMenuItem.Name = "cUSTOMERACCOUNTSERVICEToolStripMenuItem";
            this.cUSTOMERACCOUNTSERVICEToolStripMenuItem.Size = new System.Drawing.Size(384, 28);
            this.cUSTOMERACCOUNTSERVICEToolStripMenuItem.Text = "CUSTOMER ACCOUNT SERVICE";
            this.cUSTOMERACCOUNTSERVICEToolStripMenuItem.Click += new System.EventHandler(this.cUSTOMERACCOUNTSERVICEToolStripMenuItem_Click);
            // 
            // systemToolStripMenuItem
            // 
            this.systemToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.systemToolStripMenuItem.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.systemToolStripMenuItem.Name = "systemToolStripMenuItem";
            this.systemToolStripMenuItem.Size = new System.Drawing.Size(102, 27);
            this.systemToolStripMenuItem.Text = "SYSTEM";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(180, 28);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // label_RealCurrentUserName
            // 
            this.label_RealCurrentUserName.AutoSize = true;
            this.label_RealCurrentUserName.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_RealCurrentUserName.Location = new System.Drawing.Point(1034, 0);
            this.label_RealCurrentUserName.Name = "label_RealCurrentUserName";
            this.label_RealCurrentUserName.Size = new System.Drawing.Size(98, 23);
            this.label_RealCurrentUserName.TabIndex = 4;
            this.label_RealCurrentUserName.Text = "UserName";
            // 
            // label_CurrentUser
            // 
            this.label_CurrentUser.AutoSize = true;
            this.label_CurrentUser.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_CurrentUser.Location = new System.Drawing.Point(964, 0);
            this.label_CurrentUser.Name = "label_CurrentUser";
            this.label_CurrentUser.Size = new System.Drawing.Size(60, 23);
            this.label_CurrentUser.TabIndex = 6;
            this.label_CurrentUser.Text = "User :";
            // 
            // MDIParent_ADMIN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(1442, 626);
            this.Controls.Add(this.label_CurrentUser);
            this.Controls.Add(this.label_RealCurrentUserName);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.DoubleBuffered = true;
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MDIParent_ADMIN";
            this.Text = "NL BANK SOFWARE";
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem customerAccountToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Customer;
        private System.Windows.Forms.Label label_RealCurrentUserName;
        private System.Windows.Forms.Label label_CurrentUser;
        private System.Windows.Forms.ToolStripMenuItem systemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bANKSERVICESToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cUSTOMERACCOUNTSERVICEToolStripMenuItem;
    }
}




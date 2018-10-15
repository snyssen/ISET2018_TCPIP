namespace ISET2018_TCPIP
{
	partial class EcranPrincipal
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
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.mCommunication = new System.Windows.Forms.ToolStripMenuItem();
			this.mUtilitaire = new System.Windows.Forms.ToolStripMenuItem();
			this.muVerifier = new System.Windows.Forms.ToolStripMenuItem();
			this.mQuitter = new System.Windows.Forms.ToolStripMenuItem();
			this.lblServeur = new System.Windows.Forms.Label();
			this.tbServeur = new System.Windows.Forms.TextBox();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mCommunication,
            this.mUtilitaire,
            this.mQuitter});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(364, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// mCommunication
			// 
			this.mCommunication.Name = "mCommunication";
			this.mCommunication.Size = new System.Drawing.Size(106, 20);
			this.mCommunication.Text = "Communication";
			// 
			// mUtilitaire
			// 
			this.mUtilitaire.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.muVerifier});
			this.mUtilitaire.Name = "mUtilitaire";
			this.mUtilitaire.Size = new System.Drawing.Size(68, 20);
			this.mUtilitaire.Text = "Utilitaires";
			// 
			// muVerifier
			// 
			this.muVerifier.Name = "muVerifier";
			this.muVerifier.Size = new System.Drawing.Size(180, 22);
			this.muVerifier.Text = "Vérifier";
			this.muVerifier.Click += new System.EventHandler(this.muVerifier_Click);
			// 
			// mQuitter
			// 
			this.mQuitter.Name = "mQuitter";
			this.mQuitter.Size = new System.Drawing.Size(56, 20);
			this.mQuitter.Text = "Quitter";
			this.mQuitter.Click += new System.EventHandler(this.mQuitter_Click);
			// 
			// lblServeur
			// 
			this.lblServeur.AutoSize = true;
			this.lblServeur.Location = new System.Drawing.Point(13, 28);
			this.lblServeur.Name = "lblServeur";
			this.lblServeur.Size = new System.Drawing.Size(44, 13);
			this.lblServeur.TabIndex = 1;
			this.lblServeur.Text = "Serveur";
			// 
			// tbServeur
			// 
			this.tbServeur.Location = new System.Drawing.Point(13, 45);
			this.tbServeur.Name = "tbServeur";
			this.tbServeur.Size = new System.Drawing.Size(339, 20);
			this.tbServeur.TabIndex = 2;
			this.tbServeur.Text = "DESKTOP-BFM28KU";
			// 
			// EcranPrincipal
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(364, 341);
			this.ControlBox = false;
			this.Controls.Add(this.tbServeur);
			this.Controls.Add(this.lblServeur);
			this.Controls.Add(this.menuStrip1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.MainMenuStrip = this.menuStrip1;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "EcranPrincipal";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Communication TCP/IP";
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem mCommunication;
		private System.Windows.Forms.ToolStripMenuItem mUtilitaire;
		private System.Windows.Forms.ToolStripMenuItem mQuitter;
		private System.Windows.Forms.Label lblServeur;
		private System.Windows.Forms.TextBox tbServeur;
		private System.Windows.Forms.ToolStripMenuItem muVerifier;
	}
}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISET2018_TCPIP
{
	public partial class EcranPrincipal : Form
	{
		public EcranPrincipal()
		{
			InitializeComponent();
		}

		private void mQuitter_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void muVerifier_Click(object sender, EventArgs e)
		{
			//try
			//{
				MessageBox.Show("Ping : " + Utilitaires.Verifier(tbServeur.Text));
			//}
			//catch { MessageBox.Show("Problème de 'ping'"); }
		}
	}
}

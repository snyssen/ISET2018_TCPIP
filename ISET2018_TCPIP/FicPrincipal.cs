using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;

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
			try
			{
				IPAddress ipaTmp = Utilitaires.Verifier(tbServeur.Text);
				if (ipaTmp == null)
				{
					MessageBox.Show("Problème d'adresse");
				}
				else
					MessageBox.Show("Adresse correcte : " + ipaTmp.ToString());
			}
			catch (Exception ex)
			{ MessageBox.Show("Problème de 'ping', raison : " + ex.ToString()); }
		}

		private void mcLCEcouter_Click(object sender, EventArgs e)
		{
			mcLCConnecter.Enabled = mcLCEcouter.Enabled = false;
			IPAddress IPLocal = Utilitaires.Verifier(tbServeur.Text);
			TcpListener MonServeur = new TcpListener(IPLocal, 8000);
			MonServeur.Start();
			TcpClient MonClient = MonServeur.AcceptTcpClient();
			NetworkStream flux = MonClient.GetStream();
			BinaryWriter bw = new BinaryWriter(flux);
			bw.Write("Connection initialisée");
			BinaryReader br = new BinaryReader(flux);
			lbReponses.Items.Add(br.ReadString());
			bw.Write("Ordre de déconnexion");
			lbReponses.Items.Add(br.ReadString());
			MonClient.Close();
			MonServeur.Stop();
			mcLCConnecter.Enabled = mcLCEcouter.Enabled = true;
		}

		private void mcLCConnecter_Click(object sender, EventArgs e)
		{
			mcLCConnecter.Enabled = mcLCEcouter.Enabled = false;
			IPAddress IPServeur = Utilitaires.Verifier(tbServeur.Text);
			TcpClient MonClient = new TcpClient();
			MonClient.Connect(IPServeur, 8000);
			NetworkStream flux = MonClient.GetStream();
			BinaryReader br = new BinaryReader(flux);
			lbReponses.Items.Add(br.ReadString());
			BinaryWriter bw = new BinaryWriter(flux);
			IPServeur = Utilitaires.Verifier(Dns.GetHostName());
			bw.Write("Machine " + IPServeur.ToString() + " Connectée");
			lbReponses.Items.Add(br.ReadString());
			bw.Write("Déconnexion effectuée");
			MonClient.Close();
			mcLCConnecter.Enabled = mcLCEcouter.Enabled = true;
		}
	}
}

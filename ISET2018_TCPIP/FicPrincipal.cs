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

		private void mcULEcouter_Click(object sender, EventArgs e)
		{
			mcULConnecter.Enabled = mcULEcouter.Enabled = false;
			string Donnees;
			byte[] tabOctets;
			IPAddress IPLocal = Utilitaires.Verifier(tbServeur.Text);
			IPEndPoint IPEP = new IPEndPoint(IPLocal, 8000);
			UdpClient MonServeur = new UdpClient(8000);
			lbReponses.Items.Add("UDP prêt à recevoir des données de " + IPEP.ToString());
			tabOctets = MonServeur.Receive(ref IPEP); // Fonction bloquante
			Donnees = Encoding.ASCII.GetString(tabOctets, 0, tabOctets.Length);
			lbReponses.Items.Add("Données reçues : ");
			lbReponses.Items.Add(Donnees);
			lbReponses.Items.Add("Fermeture serveur");
			MonServeur.Close();
			mcULConnecter.Enabled = mcULEcouter.Enabled = true;
		}

		private void mcULConnecter_Click(object sender, EventArgs e)
		{
			mcULConnecter.Enabled = mcULEcouter.Enabled = false;
			IPAddress IPServeur = Utilitaires.Verifier(tbServeur.Text);
			UdpClient MonClient = new UdpClient();
			MonClient.Connect(IPServeur, 8000);
			lbReponses.Items.Add("Client connecté à " + IPServeur.ToString() + ":8000");
			byte[] tabOctets = Encoding.ASCII.GetBytes(tbQuestion.Text);
			MonClient.Send(tabOctets, tabOctets.Length);
			lbReponses.Items.Add("Message envoyé");
			lbReponses.Items.Add(tbQuestion.Text);
			lbReponses.Items.Add("fermeture de la connexion");
			MonClient.Close();
			mcULConnecter.Enabled = mcULEcouter.Enabled = true;
		}
	}
}

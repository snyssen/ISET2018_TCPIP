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
using System.Threading;

namespace ISET2018_TCPIP
{
	public partial class EcranPrincipal : Form
	{
		private Socket socServeur, socClient;
		private byte[] socBuffer = new byte[256];
		private int socFlag = 0; // 0 => rien | 1 => serveur | 2 => client
		public EcranPrincipal()
		{
			InitializeComponent();
		}

		#region Récupération du mode Debug
		delegate void RenvoiVersInserer(string text);
		private void InsererItemThread(string texte)
		{
			Thread ThreadInsererItem
			 = new Thread(new ParameterizedThreadStart(InsererItem));
			ThreadInsererItem.Start(texte);
		}
		private void InsererItem(object texte)
		{
			if (lbReponses.InvokeRequired)
			{
				RenvoiVersInserer d = new RenvoiVersInserer(InsererItem);
				Invoke(d, new object[] { texte });
			}
			else lbReponses.Items.Insert(0, (string)texte);
		}
		#endregion

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

		private void mcsEcouter_Click(object sender, EventArgs e)
		{
			mcsEcouter.Enabled = mcsConnecter.Enabled = false;
			mcsDeconnecter.Enabled = true;
			socFlag = 1;
			socClient = null;
			IPAddress IPServeur = Utilitaires.Verifier(Dns.GetHostName());
			socServeur = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			socServeur.Bind(new IPEndPoint(IPServeur, 8000));
			socServeur.Listen(1);
			lbReponses.Items.Add("Serveur à l'écoute des connexions distantes");
			socServeur.BeginAccept(new AsyncCallback(SurDemandeConnexion), socServeur); // AsyncCallback exécute la méthode indiquée (par un pointeur) en lui envoyant l'argument IAsyncResult
			
		}

		private void SurDemandeConnexion(IAsyncResult iAR)
		{
			if (socFlag == 1)
			{
				Socket sTmp = (Socket)iAR.AsyncState;
				socClient = sTmp.EndAccept(iAR);
				socClient.Send(Encoding.Unicode.GetBytes("Connexion acceptée"));
				//lbReponses.Items.Add("Connexion effectuée par " + ((IPEndPoint)socClient.RemoteEndPoint).Address);
				InsererItemThread("Connexion effectuée par " + ((IPEndPoint)socClient.RemoteEndPoint).Address);
				InitialiserReception(socClient);
			}
		}

		private void InitialiserReception(Socket sArg)
		{
			try
			{
				socBuffer = new byte[256];
				sArg.BeginReceive(socBuffer, 0, socBuffer.Length, SocketFlags.None, new AsyncCallback(Reception), sArg);
			}
			catch
			{
				MessageBox.Show("Réception impossible !");
			}
		}

		private void Reception(IAsyncResult iAR)
		{
			if (socFlag>0)
			{
				Socket sTmp = (Socket)iAR.AsyncState;
				try
				{
					int nRec = sTmp.EndReceive(iAR);
					if (nRec>0)
					{
						//lbReponses.Items.Add(Encoding.Unicode.GetString(socBuffer));
						InsererItemThread(Encoding.Unicode.GetString(socBuffer));
						InitialiserReception(sTmp);
					}
					else
					{
						sTmp.Disconnect(true);
						sTmp.Close();
						socServeur.BeginAccept(new AsyncCallback(SurDemandeConnexion), socServeur);
						socClient = null;
					}
				}
				catch
				{
					MessageBox.Show("Erreur pendant la réception");
				}
			}
		}

		private void mcsConnecter_Click(object sender, EventArgs e)
		{
			if (tbServeur.Text.Trim().Length > 0)
			{
				mcsConnecter.Enabled = mcsEcouter.Enabled = false;
				mcsDeconnecter.Enabled = true;
				socFlag = 2;
				try
				{
					socClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
					socClient.Blocking = false;
					IPAddress IPDistant = Utilitaires.Verifier(tbServeur.Text);
					socClient.BeginConnect(new IPEndPoint(IPDistant, 8000), new AsyncCallback(SurConnexion), socClient);
				}
				catch
				{
					MessageBox.Show("Connexion au serveur impossible");
				}
			}
			else
				MessageBox.Show("Renseignez le serveur SVP");
		}

		private void SurConnexion(IAsyncResult iAR)
		{
			Socket sTmp = (Socket)iAR.AsyncState;
			if (sTmp.Connected)
				InitialiserReception(sTmp);
			else
				MessageBox.Show("Serveur inaccessible");
		}

		private void mcsDeconnecter_Click(object sender, EventArgs e)
		{
			if (socFlag == 1)
			{
				if (socClient == null)
				{
					socFlag = 0;
					mcsConnecter.Enabled = mcsEcouter.Enabled = true;
					mcsDeconnecter.Enabled = false;
					socServeur.Close();
					lbReponses.Items.Add("Serveur éteint");
				}
				else
					MessageBox.Show("Un client est connecté !");
			}
			else if (socFlag == 2)
			{
				socClient.Send(Encoding.Unicode.GetBytes("Déconnexion de " + ((IPEndPoint)socClient.LocalEndPoint).Address));
				socClient.Shutdown(SocketShutdown.Both);
				socFlag = 0;
				socClient.BeginDisconnect(false, new AsyncCallback(DemandeDeconnexion), socClient);
				lbReponses.Items.Add("Client déconnecté");
				mcsEcouter.Enabled = mcsConnecter.Enabled = true;
				mcsDeconnecter.Enabled = false;
			}
		}

		private void DemandeDeconnexion(IAsyncResult iAR)
		{
			Socket sTmp = (Socket)iAR.AsyncState;
			sTmp.EndDisconnect(iAR);
		} 

		private void btnValider_Click(object sender, EventArgs e)
		{
			if ((socFlag == 1 && socClient != null) || socFlag == 2) // 1) je suis serveur ET un client est connecté || 2) je suis client
			{
				socClient.Send(Encoding.Unicode.GetBytes(tbQuestion.Text));
			}
		}
	}
}

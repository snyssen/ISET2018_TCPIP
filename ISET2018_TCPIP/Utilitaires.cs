using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.NetworkInformation;

namespace ISET2018_TCPIP
{
	public class Utilitaires
	{
		public static IPAddress Verifier(string sAdresse)
		{
			IPAddress rep = null;
			if (sAdresse.Trim().Length > 0)
			{
				//IPAddress ipVerif = Dns.GetHostEntry(sAdresse).AddressList[0];
				IPAddress[] ipVerifs = Dns.GetHostEntry(sAdresse).AddressList;
				for (int i = 0; i < ipVerifs.Length; i++)
				{
					if (ipVerifs[i].AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
						if (ipVerifs[i] != new IPAddress(0x0100007f))
						{
							Ping pVerif = new Ping();
							PingReply pRepon = pVerif.Send(ipVerifs[i]);
							if (pRepon.Status == IPStatus.Success)
								rep = "Ping réussi";
							else
								rep = "Ping échoué, raison : " + pRepon.Status.ToString();
						}
				}
			}
			else
				rep = "Veuillez renseigner une adresse SVP";
			return rep;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwatInc.Lis.Lis01A2.Interfaces
{
	public interface ILis01A2Connection
	{
		void ClearBuffers();
		void Connect();
		void DisConnect();
		void WriteData(string value);
		event EventHandler<LISConnectionReceivedDataEventArgs> OnReceiveString;
	}
}

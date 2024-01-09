using slf4net;
using SwatInc.Lis.Lis01A2;
using SwatInc.Lis.Lis01A2.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;


namespace SwatInc.Lis.Lis02A2
{
	public class LISParser
	{
		private ILogger fLog;

		private ILisConnection fConnection;

		private ReceiveRecordEventHandler fOnReceivedRecord;

		private ThreadExceptionEventHandler fOnExceptionHappened;

		[CompilerGenerated]
		private Guid @_ThreadGuid;

		private EventHandler<SendProgressEventArgs> @_OnSendProgress;

		public ILisConnection Connection
		{
			get
			{
				return fConnection;
			}
			set
			{
				if (fConnection != null && fConnection != value)
				{
					fConnection.OnReceiveString -= ReceivedData;
					fConnection.OnReceiveTimeOut -= Connection_OnReceiveTimeOut;
				}
				fConnection = value;
				fConnection.OnReceiveString += ReceivedData;
				fConnection.OnReceiveTimeOut += Connection_OnReceiveTimeOut;
			}
		}

		public Guid ThreadGuid
		{
			get
			{
				return @_ThreadGuid;
			}
			set
			{
				@_ThreadGuid = value;
			}
		}

		public event ReceiveRecordEventHandler OnReceivedRecord
		{
			add
			{
				fOnReceivedRecord = Delegate.Combine(fOnReceivedRecord, value) as ReceiveRecordEventHandler;
			}
			remove
			{
				fOnReceivedRecord = Delegate.Remove(fOnReceivedRecord, value) as ReceiveRecordEventHandler;
			}
		}

		public event ThreadExceptionEventHandler OnExceptionHappened
		{
			add
			{
				fOnExceptionHappened = Delegate.Combine(fOnExceptionHappened, value) as ThreadExceptionEventHandler;
			}
			remove
			{
				fOnExceptionHappened = Delegate.Remove(fOnExceptionHappened, value) as ThreadExceptionEventHandler;
			}
		}

		public event EventHandler<SendProgressEventArgs> OnSendProgress
		{
			add
			{
				@_OnSendProgress = Delegate.Combine(@_OnSendProgress, value) as EventHandler<SendProgressEventArgs>;
			}
			remove
			{
				@_OnSendProgress = Delegate.Remove(@_OnSendProgress, value) as EventHandler<SendProgressEventArgs>;
			}
		}

		private void Connection_OnReceiveTimeOut(object sender, EventArgs e)
		{
			if (fOnExceptionHappened != null)
			{
				fLog.Error("No incoming data within timeout");
				ThreadExceptionEventArgs ea = new ThreadExceptionEventArgs(new LISParserReceiveTimeOutException("No incoming data within timeout"));
				fOnExceptionHappened(this, ea);
			}
		}

		public virtual AbstractLisRecord CreateHeaderRecord(string aLisString)
		{
			return new HeaderRecord(aLisString);
		}

        public virtual AbstractLisRecord CreatePatientRecord(string aLisString)
        {
            return new PatientRecord(aLisString);
        }

        public virtual AbstractLisRecord CreateOrderRecord(string aLisString)
        {
            return new OrderRecord(aLisString);
        }

        public virtual AbstractLisRecord CreateResultRecord(string aLisString)
        {
            return new ResultRecord(aLisString);
        }

		public virtual AbstractLisRecord CreateQueryRecord(string aLisString)
		{
            return new QueryRecord(aLisString);
        }

        private void ParseReceivedRecord(string aReceivedRecordString)
		{
			fLog.Trace(aReceivedRecordString);
			ReceiveRecordEventArgs tempArgs = new ReceiveRecordEventArgs();
			char RecordTypeChar = aReceivedRecordString[0];
			switch (RecordTypeChar)
			{
			default:
				if (RecordTypeChar != 'H')
				{
					if (RecordTypeChar == 'P')
					{
						goto case 'P';
					}
					if (RecordTypeChar == 'O')
					{
						goto case 'O';
					}
					if (RecordTypeChar != 'Q')
					{
						switch (RecordTypeChar)
						{
						default:
							return;
						case 'R':
							break;
						case 'L':
							tempArgs.ReceivedRecord = new TerminatorRecord(aReceivedRecordString);
							tempArgs.RecordType = LisRecordType.Terminator;
							fOnReceivedRecord(this, tempArgs);
							return;
						}
						break;
					}
					goto case 'Q';
				}
				tempArgs.ReceivedRecord = CreateHeaderRecord(aReceivedRecordString);
				tempArgs.RecordType = LisRecordType.Header;
				fOnReceivedRecord(this, tempArgs);
				return;
			case 'P':
				tempArgs.ReceivedRecord = CreatePatientRecord(aReceivedRecordString);
				tempArgs.RecordType = LisRecordType.Patient;
				fOnReceivedRecord(this, tempArgs);
				return;
			case 'O':
				tempArgs.ReceivedRecord = CreateOrderRecord(aReceivedRecordString);
				tempArgs.RecordType = LisRecordType.Order;
				fOnReceivedRecord(this, tempArgs);
				return;
			case 'Q':
					tempArgs.ReceivedRecord = CreateQueryRecord(aReceivedRecordString);
				tempArgs.RecordType = LisRecordType.Query;
				fOnReceivedRecord(this, tempArgs);
				return;
			case 'R':
				break;
			}
			tempArgs.ReceivedRecord = CreateResultRecord(aReceivedRecordString);
			tempArgs.RecordType = LisRecordType.Result;
			fOnReceivedRecord(this, tempArgs);
		}

		private void ReceivedData(object Sender, LISConnectionReceivedDataEventArgs e)
		{
			if (fOnReceivedRecord == null)
			{
				return;
			}
			try
			{
				string[] records = e.ReceivedData.Split(new char[1] { '\r' });
				int i = 0;
				string[] array = records;
				if (array == null)
				{
					return;
				}
				for (; i < (int)array.LongLength; i++)
				{
					string r = array[i];
					if (!string.IsNullOrEmpty(r))
					{
						ParseReceivedRecord(r);
					}
				}
			}
			catch (Exception ex)
			{
				if (fOnExceptionHappened != null)
				{
					fLog.Error(ex.Message);
					ThreadExceptionEventArgs ea = new ThreadExceptionEventArgs(ex);
					fOnExceptionHappened(this, ea);
				}
			}
		}

		public LISParser(ILisConnection aConnection)
		{
			Connection = aConnection;
			//slf4net.LoggerFactory.GetLogger(typeof(MyClass));

			fLog =  LoggerFactory.GetLogger(typeof(LISParser));
		}

		public void SendRecords(IEnumerable<AbstractLisRecord> records)
		{
			try
			{
				if (records == null)
				{
					return;
				}
				if (!Connection.EstablishSendMode() && !Connection.EstablishSendMode())
				{
					fLog.Error("The LIS did not acknowledge the ENQ request.");
					throw new LISParserEstablishmentFailedException("The LIS did not acknowledge the ENQ request.");
				}
				fLog.Info("Send Mode Established");
				double recCount = 0.0;
				if (records is IList)
				{
					recCount = (records as IList).Count;
				}
				if (records != null)
				{
					int sendCounter = 0;
					IEnumerator<AbstractLisRecord> enumerator = records.GetEnumerator();
					if (enumerator != null)
					{
						try
						{
							while (enumerator.MoveNext())
							{
								AbstractLisRecord rec = enumerator.Current;
								Connection.SendMessage(rec.ToLISString());
								if (@_OnSendProgress != null && recCount > 0.0)
								{
									@_OnSendProgress(this, new SendProgressEventArgs((double)sendCounter / recCount, @_ThreadGuid));
								}
								sendCounter++;
							}
						}
						finally
						{
							enumerator.Dispose();
						}
					}
				}
				Connection.StopSendMode();
			}
			catch (Exception ex)
			{
				fLog.Error(ex, "Exception in SendRecords");
				if (fOnExceptionHappened != null)
				{
					ThreadExceptionEventArgs ea = new ThreadExceptionEventArgs(ex);
					fOnExceptionHappened(this, ea);
				}
			}
		}
	}
}

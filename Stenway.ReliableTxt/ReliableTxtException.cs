using System;

namespace Stenway.ReliableTxt
{
	public class ReliableTxtException : Exception
	{
		public ReliableTxtException(String message) : base(message)
		{
			
		}
		
		public ReliableTxtException(String message, Exception innerException) : base(message, innerException)
		{
			
		}
	}
}
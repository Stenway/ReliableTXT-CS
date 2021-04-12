using System;
using System.Text;

namespace Stenway.ReliableTxt
{
	public static class ReliableTxtDecoder
	{
		public static ReliableTxtEncoding GetEncoding(byte[] bytes)
		{
			if (bytes == null) throw new ArgumentNullException();
			
			if (bytes.Length >= 3
					&& bytes[0] == (byte)0xEF 
					&& bytes[1] == (byte)0xBB
					&& bytes[2] == (byte)0xBF)
			{
				return ReliableTxtEncoding.Utf8;
			}
			else if (bytes.Length >= 2
					&& bytes[0] == (byte)0xFE 
					&& bytes[1] == (byte)0xFF) 
			{
				return ReliableTxtEncoding.Utf16;
			}
			else if (bytes.Length >= 2
					&& bytes[0] == (byte)0xFF 
					&& bytes[1] == (byte)0xFE)
			{
				return ReliableTxtEncoding.Utf16Reverse;
			}
			else if (bytes.Length >= 4
					&& bytes[0] == 0 
					&& bytes[1] == 0
					&& bytes[2] == (byte)0xFE 
					&& bytes[3] == (byte)0xFF)
			{
				return ReliableTxtEncoding.Utf32;
			}
			else
			{
				throw new ReliableTxtException("Document does not have a ReliableTXT preamble");
			}
		}
		
		public static Tuple<ReliableTxtEncoding,string> Decode(byte[] bytes)
		{
			ReliableTxtEncoding detectedEncoding = ReliableTxtDecoder.GetEncoding(bytes);
			Encoding mappedEncoding = detectedEncoding.GetTextEncoding();
			
			byte[] preamble = mappedEncoding.GetPreamble();
			if (preamble == null) throw new InvalidOperationException();
			int preambleLength = preamble.Length;
			
			int dataLength = bytes.Length-preambleLength;
			if (dataLength < 0) throw new InvalidOperationException();
			
			string text = null;
			try {
				text = mappedEncoding.GetString(bytes,preambleLength,dataLength);
			} catch (Exception e) {
				throw new ReliableTxtException("The "+detectedEncoding+" encoded text contains invalid data.", e);
			}
			return new Tuple<ReliableTxtEncoding, string>(detectedEncoding,text);
		}
	}
}

using System;
using System.Linq;
using System.Text;

namespace Stenway.ReliableTxt
{
	public static class ReliableTxtEncoder
	{
		public static byte[] Encode(string text, ReliableTxtEncoding encoding)
		{
			Encoding mappedEncoding = encoding.GetTextEncoding();
			byte[] preamble = mappedEncoding.GetPreamble();
			byte[] bytes = mappedEncoding.GetBytes(text);
			return preamble.Concat(bytes).ToArray();
		}
	}
}
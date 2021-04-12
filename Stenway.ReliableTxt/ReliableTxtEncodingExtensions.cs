using System;
using System.Text;

namespace Stenway.ReliableTxt
{
	public static class ReliableTxtEncodingExtensions
	{
		private static readonly Encoding[] _encodings = 
		{
			new UTF8Encoding(true,true),
			new UnicodeEncoding(true,true,true),
			new UnicodeEncoding(false,true,true),
			new UTF32Encoding(true,true,true)
		};
		
		public static Encoding GetTextEncoding(this ReliableTxtEncoding encoding)
		{
			int encodingIndex = (int)encoding;
			if (encodingIndex < 0 || encodingIndex > 3) throw new ArgumentOutOfRangeException();
			return _encodings[encodingIndex];
		}
	}
}
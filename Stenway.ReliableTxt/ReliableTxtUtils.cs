using System;
using System.Linq;
using System.Text;

namespace Stenway.ReliableTxt
{
	public static class ReliableTxtUtils
	{
		public static int[] GetCodePoints(string str)
		{
			bool platformIsBigEndian = !BitConverter.IsLittleEndian;
        	UTF32Encoding utf32Encoding = new UTF32Encoding(platformIsBigEndian, false, true);
        	byte[] bytes = utf32Encoding.GetBytes(str);
        	return Enumerable.Range(0, bytes.Length / 4).Select(i => BitConverter.ToInt32(bytes, i * 4)).ToArray();
		}
		
		public static string GetString(int[] codepoints)
		{
			StringBuilder sb = new StringBuilder();
			foreach (int codepoint in codepoints)
			{
				sb.Append(Char.ConvertFromUtf32(codepoint));
			}
			return sb.ToString();
		}
		
		public static string GetString(int[] codepoints, int start, int length)
		{
			StringBuilder sb = new StringBuilder();
			for (int i=start; i<start+length; i++)
			{
				int codepoint = codepoints[i];
				sb.Append(Char.ConvertFromUtf32(codepoint));
			}
			return sb.ToString();
		}
	}
}
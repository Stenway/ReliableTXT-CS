using System;
using System.Text;

namespace Stenway.ReliableTxt
{
	public class ReliableTxtCharIterator
	{
		protected readonly StringBuilder sb = new StringBuilder();
		protected readonly int[] chars;
		protected int index;
		
		public ReliableTxtCharIterator(string text)
		{
			if (text == null)
			{
				throw new ArgumentNullException();
			}
			chars = ReliableTxtUtils.GetCodePoints(text);
		}
		
		public String GetText()
		{
			return ReliableTxtUtils.GetString(chars);
		}
	
		public int[] GetLineInfo()
		{
			int lineIndex = 0;
			int linePosition = 0;
			for (int i=0; i<index; i++)
			{
				if (chars[i] == '\n')
				{
					lineIndex++;
					linePosition = 0;
				}
				else
				{
					linePosition++;
				}
			}
			return new int[] {lineIndex, linePosition};
		}
		
		public bool IsEndOfText
		{
			get
			{
				return index >= chars.Length;
			}
		}
		
	
		public bool IsChar(int c)
		{
			if (IsEndOfText) return false;
			return chars[index] == c;
		}
		
		public bool TryReadChar(int c)
		{
			if (!IsChar(c)) return false;
			index++;
			return true;
		}
	}
}

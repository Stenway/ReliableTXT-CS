using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Stenway.ReliableTxt
{	
	public class ReliableTxtDocument
	{
		private string _text = "";
		private ReliableTxtEncoding _encoding;
		
		public ReliableTxtEncoding Encoding 
		{
			get { return _encoding; } 
			set
			{
				int encodingIndex = (int)value;
				if (encodingIndex < 0 || encodingIndex > 3) throw new ArgumentOutOfRangeException();
				_encoding = value;
			}
		}
		
		public string Text
		{
			get { return _text; } 
			set { SetText(value); }
		}
		
		public ReliableTxtDocument()
		{
			
		}
		
		public ReliableTxtDocument(string text)
		{
			SetText(text);
		}
		
		public ReliableTxtDocument(string text, ReliableTxtEncoding encoding)
		{
			SetText(text);
			Encoding = encoding;
		}
		
		public ReliableTxtDocument(params string[] lines)
		{
			SetLines(lines);
		}
		
		public ReliableTxtDocument(IEnumerable<string> lines)
		{
			SetLines(lines);
		}
		
		public ReliableTxtDocument(byte[] bytes)
		{
			if (bytes == null) return;
			
			Tuple<ReliableTxtEncoding,string> decoderResult = ReliableTxtDecoder.Decode(bytes);
			SetText(decoderResult.Item2);
			_encoding = decoderResult.Item1;
		}
		
		private void SetText(string text)
		{
			if (text == null) text = "";
			_text = text;
		}
		
		public string[] GetLines()
		{
			return ReliableTxtLines.Split(_text);
		}
		
		public void SetLines(IEnumerable<string> lines)
		{
			if (lines == null)
			{
				SetText("");
				return;
			}
			string text = ReliableTxtLines.Join(lines);
			SetText(text);
		}
		
		public void Save(string filePath)
		{
			if (filePath == null) throw new ArgumentNullException();
			
			Encoding mappedEncoding = _encoding.GetTextEncoding();
			File.WriteAllText(filePath, Text, mappedEncoding);
		}
		
		public override string ToString()
		{
			return _text;
		}
		
		public byte[] GetBytes()
		{
			return ReliableTxtEncoder.Encode(_text,_encoding);
		}

		public static ReliableTxtDocument Load(string filePath)
		{
			if (filePath == null) throw new ArgumentNullException();
			byte[] bytes = File.ReadAllBytes(filePath);
			return new ReliableTxtDocument(bytes);
		}
	}
}
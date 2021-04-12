using System;
using System.Collections.Generic;

namespace Stenway.ReliableTxt
{
	public static class ReliableTxtLines
	{
		public static string Join(params string[] lines) {
			return String.Join("\n", lines);
		}
		
		public static string Join(IEnumerable<string> lines) {
			return String.Join("\n", lines);
		}
		
		public static string[] Split(string text) {
			return text.Split('\n');
		}
	}
}
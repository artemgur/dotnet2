using System;
using BenchmarkDotNet.Running;

namespace MethodsBenchmark
{
	public static class Program
	{
		static void Main(string[] args)
		{
			BenchmarkRunner.Run<BenchmarkLauncher>();
		}
	}
}
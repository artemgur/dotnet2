using System.Reflection;
using BenchmarkDotNet.Attributes;

namespace MethodsBenchmark
{
	[MemoryDiagnoser]
	public class BenchmarkLauncher
	{
		private static BenchmarkedMethods instance = new BenchmarkedMethods();
		private static dynamic dynamic = (dynamic) instance;
		private static MethodInfo methodInfo = typeof(BenchmarkedMethods).GetMethod("Run");
		private static object[] emptyArray = new object[] { };
		
		[Benchmark]
		public string Static() => BenchmarkedMethods.Static();
		
		[Benchmark]
		public string Instance() => instance.Instance();
		
		[Benchmark]
		public string Virtual() => instance.Virtual();
		
		[Benchmark]
		public string Generic() => instance.Generic<string>();

		[Benchmark]
		public string Reflection()
		{
			var type = typeof(BenchmarkedMethods);
			var method = type.GetMethod("Run");
			return (string)method?.Invoke(null, emptyArray);
		}

		[Benchmark]
		public string PreparedReflection() => (string)methodInfo.Invoke(null, emptyArray);

		[Benchmark]
		public string Dynamic() => dynamic.Instance();
	}
}
namespace MethodsBenchmark
{
	public class BenchmarkedMethods
	{
		public static string Run()
		{
			var a = ")";
			for (var i = 0; i < 3; i++)
				a += ")";
			return a;
		}

		public static string Static() => Run();

		public string Instance() => Run();

		public virtual string Virtual() => Run();

		public string Generic<T>() => Run();
	}
}
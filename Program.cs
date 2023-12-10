using BenchmarkDotNet.Running;


BenchmarkSwitcher.FromAssembly(typeof(Hash).Assembly).Run(args);
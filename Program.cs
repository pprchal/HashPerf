using BenchmarkDotNet.Running;

// var inst = new Hash().HashLookup("10*a,1*b,1*c,1000*b,200*a");

BenchmarkSwitcher.FromAssembly(typeof(Hash).Assembly).Run(args);
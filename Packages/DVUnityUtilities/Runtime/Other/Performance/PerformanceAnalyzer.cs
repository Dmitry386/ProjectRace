using System;
using System.Diagnostics;

public class PerformanceAnalyzer : IDisposable
{
    public string OutputName;
    private Stopwatch _stopwatch;
    private bool _print_on_dispose;

    public PerformanceAnalyzer(string name = null, bool print_on_dispose = true)
    {
        _print_on_dispose = print_on_dispose;
        if (name == null) OutputName = new StackTrace().GetFrame(1).GetMethod().Name;
        else OutputName = name;

        _stopwatch = new Stopwatch();
        _stopwatch.Start();
    }

    public void Print()
    {
        UnityEngine.Debug.Log($"[{DateTime.Now}] {OutputName} elapsed time: {_stopwatch.Elapsed} ms");
    }

    public void Dispose()
    {
        _stopwatch.Stop();
        if (_print_on_dispose) Print();

        OutputName = null;
        _stopwatch = null;
    }
}
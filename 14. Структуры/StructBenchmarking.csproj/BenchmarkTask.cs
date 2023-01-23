using System;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;
using System.Text;

namespace StructBenchmarking
{
    public class Benchmark : IBenchmark
    {
        public double MeasureDurationInMs(ITask task, int repetitionCount)
        {
            GC.Collect();                   // Эти две строчки нужны, чтобы уменьшить вероятность того,
            GC.WaitForPendingFinalizers();  // что Garbadge Collector вызовется в середине измерений
            task.Run();                     // и как-то повлияет на них.
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < repetitionCount; i++)
            {
                task.Run();
            }
            sw.Stop();

            return (double)sw.ElapsedMilliseconds / repetitionCount;
        }
    }

    public class SpecialStringTest : ITask
    {
        public void Run()
        {
            new string('a', 10000);
        }
    }

    public class StringBuilderTest : ITask
    {
        public void Run()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 10000; i++)
            {
                sb.Append('a');
            }
            sb.ToString();
        }
    }


    [TestFixture]
    public class RealBenchmarkUsageSample
    {
        [Test]
        public void StringConstructorFasterThanStringBuilder()
        {
            Benchmark benchmark = new Benchmark();
            int repetitionCount = 50000;
            SpecialStringTest specStr = new SpecialStringTest();
            StringBuilderTest sb = new StringBuilderTest();

            double specStrDuration = benchmark.MeasureDurationInMs(specStr, repetitionCount);
            double sbDuration = benchmark.MeasureDurationInMs(sb, repetitionCount);
            Assert.Less(specStrDuration, sbDuration);
        }
    }
}

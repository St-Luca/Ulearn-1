using System.Collections.Generic;

namespace StructBenchmarking
{//Тесты на время выполнения на юлерне дикие какие-то. Пришлось несколько раз все капитально переделывать
    public interface IExperiments
    {
        ITask Task(int i, string task);
    }

    public class CreateTheTask : IExperiments
    {
        public ITask Task(int i, string task)
        {
            if (task == "Structure")
            {
                return new StructArrayCreationTask(i);
            }
            return new ClassArrayCreationTask(i);
        }
    }

    public class CallTheTask : IExperiments
    {
        public ITask Task(int i, string task)
        {
            if (task == "Structure")
            {
                return new MethodCallWithStructArgumentTask(i);
            }
            return new MethodCallWithClassArgumentTask(i);
        }
    }

    public class Experiments : DemoExperiments
    {
        public static ChartData BuildChartDataForArrayCreation(IBenchmark benchmark, int repetitionsCount)
        {
            return DemoExperiments.FillLists("Create array", benchmark, repetitionsCount, new CreateTheTask());
        }

        public static ChartData BuildChartDataForMethodCall(IBenchmark benchmark, int repetitionsCount)
        {
            return DemoExperiments.FillLists("Call method with argument", benchmark, repetitionsCount, new CallTheTask());
        }
    }

    public class DemoExperiments
    {
        public static ChartData FillLists(string title, IBenchmark bm, int repetitionsCount, IExperiments experiment)
        {
            var classesTimes = new List<ExperimentResult>();
            var structuresTimes = new List<ExperimentResult>();

            foreach (int i in Constants.FieldCounts)
            {
                classesTimes.Add(new ExperimentResult(i,
                bm.MeasureDurationInMs(experiment.Task(i, "Class"), repetitionsCount)));

                structuresTimes.Add(new ExperimentResult(i,
                bm.MeasureDurationInMs(experiment.Task(i, "Structure"), repetitionsCount)));
            }
            return new ChartData
            {
                Title = title,
                ClassPoints = classesTimes,
                StructPoints = structuresTimes,
            };
        }
    }
}
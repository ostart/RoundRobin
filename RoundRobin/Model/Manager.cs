using System;
using System.Collections.ObjectModel;

namespace RoundRobin.Model
{
    public class Manager
    {
        public int TickCounter { get; set; }

        public ObservableCollection<Executor> ExecutorsList { get; set; } 

        public ObservableCollection<TaskUnit> TaskUnitsList { get; set; }

        public Manager(Settings appSettings, Random random)
        {
            TickCounter = 0;
            ExecutorsList = new ObservableCollection<Executor>();
            var executorsNumber = random.Next(appSettings.MinNumberOfExecutors, appSettings.MaxNumberOfExecutors + 1);
            for (var i = 0; i < executorsNumber; i++)
            {
                var performance = random.Next(appSettings.MinValueOfExecutorPerformance, appSettings.MaxValueOfExecutorPerformance + 1);
                ExecutorsList.Add(new Executor(performance, $"Executor-{i}"));
            }
            TaskUnitsList = new ObservableCollection<TaskUnit>();
            var tasksNumber = random.Next(appSettings.MinNumberOfTasks, appSettings.MaxNumberOfTasks + 1);
            for (var i = 0; i < tasksNumber; i++)
            {
                var complexity = random.Next(appSettings.MinValueOfTaskComplexity, appSettings.MaxValueOfTaskComplexity + 1);
                TaskUnitsList.Add(new TaskUnit(complexity, $"Task-{i}"));
            }
        }

        public void MethodA() //метод A распределяет список существующих задач между исполнителями по принципу round - robin
        {
            for (var i = 0; i < TaskUnitsList.Count; i++)
            {
                var executorIndex = i % ExecutorsList.Count;
                ExecutorsList[executorIndex].Enqueue(TaskUnitsList[i]);
            }
        }

        public void MethodB() //метод B перераспределяет задачи: все первые задачи каждого исполнителя переходят к следующему исполнителю(помещаются в начало списка задач).Задача последнего исполнителя переходит к первому исполнителю. Остальные задачи не трогаются.
        {
            var lastExecutorIndex = ExecutorsList.Count - 1;
            var taskFromLastExecutor = ExecutorsList[lastExecutorIndex].Dequeue();
            for (var i = lastExecutorIndex; i > 0 ; i--)
            {
                var firstTask = ExecutorsList[i - 1].Dequeue();
                if (firstTask == null) continue;
                ExecutorsList[i].AddFirst(firstTask);
            }
            if (taskFromLastExecutor != null)
                ExecutorsList[0].AddFirst(taskFromLastExecutor);
        }

        public void DoWork()
        {
            foreach (var executor in ExecutorsList)
            {
                executor.DoWork(TickCounter, TaskFinished);
            }
            TickCounter += 1;
        }

        public event EventHandler<TracingEventArgs> TaskFinished;
    }
}

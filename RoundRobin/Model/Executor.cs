using System;
using System.Collections.ObjectModel;

namespace RoundRobin.Model
{
    public class Executor
    {
        public int Performance { get; set; }

        public string Name { get; set; }

        public ObservableCollection<TaskUnit> TaskQueue { get; set; }

        public Executor(int performance, string name, ObservableCollection<TaskUnit> tasksUnits = null)
        {
            Performance = performance;
            Name = name;
            TaskQueue = tasksUnits ?? new ObservableCollection<TaskUnit>();
        }

        public void Enqueue(TaskUnit taskUnit)
        {
            TaskQueue.Add(taskUnit);
        }

        public TaskUnit Dequeue()
        {
            if (TaskQueue.Count == 0) return null;

            var first = TaskQueue[0];
            TaskQueue.RemoveAt(0);
            return first;
        }

        public void AddFirst(TaskUnit taskUnit)
        {
            TaskQueue.Insert(0, taskUnit);
        }

        public void DoWork(int tickCounter, EventHandler<TracingEventArgs> taskFinished)
        {
            if (TaskQueue.Count == 0) return;

            if (TaskQueue[0].IsTaskUnitCompleted())
            {
                taskFinished?.Invoke(this, new TracingEventArgs {Message = $"{Name} finished {TaskQueue[0].Name} in model cycle No.{tickCounter}"});
                Dequeue();
            }
            if (TaskQueue.Count == 0) return;
            TaskQueue[0].DoWork(Performance);
        }
    }
}

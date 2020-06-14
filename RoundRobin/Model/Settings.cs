namespace RoundRobin.Model
{
    public class Settings
    {
        public int TimerTicksInSeconds { get; set; } = 1;
        public int MinNumberOfExecutors { get; set; } = 2;
        public int MaxNumberOfExecutors { get; set; } = 10;
        public int MinValueOfExecutorPerformance { get; set; } = 1;
        public int MaxValueOfExecutorPerformance { get; set; } = 10;
        public int MinNumberOfTasks { get; set; } = 20;
        public int MaxNumberOfTasks { get; set; } = 100;
        public int MinValueOfTaskComplexity { get; set; } = 1;
        public int MaxValueOfTaskComplexity { get; set; } = 100;
    }
}

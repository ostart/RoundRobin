namespace RoundRobin.BL
{
    public class TaskUnit
    {
        public int Complexity { get; set; }

        public int Progress { get; set; }

        public string Name { get; set; }

        public TaskUnit(int complexity, string name)
        {
            Name = name;
            Complexity = complexity;
            Progress = 0;
        }

        public void DoWork(int amountOfWork)
        {
            Progress += amountOfWork;
        }

        public bool IsTaskUnitCompleted()
        {
            return Complexity <= Progress;
        }
    }
}

namespace RoundRobin.Model
{
    public class TaskUnit
    {
        public int Complexity { get; set; }

        public string Name { get; set; }

        public TaskUnit(int complexity, string name)
        {
            Name = name;
            Complexity = complexity;
        }

        public void DoWork(int amountOfWork)
        {
            Complexity -= amountOfWork;
        }

        public bool IsTaskUnitCompleted()
        {
            return Complexity <= 0;
        }
    }
}

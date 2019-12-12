namespace HumansLancher.Models.Executable
{
    public abstract class AbstractExecutable
    {
        public AbstractExecutable(string exePath)
        {
            ExePath = exePath;
        }

        public string ExePath { get; private set; }

        public abstract void Execute();
    }
}

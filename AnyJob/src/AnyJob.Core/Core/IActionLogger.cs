namespace AnyJob
{
    public interface IActionLogger
    {
        void WriteLine(string fmt, params object[] args);
    }
}

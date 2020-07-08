namespace AnyJob.Runner.Process
{
    public class TypedProcessResult
    {
        public object Result { get; set; }
        public TypedError Error { get; set; }

    }
    public class TypedError
    {
        public string Type { get; set; }
        public string Message { get; set; }
        public string Stack { get; set; }
        public string Code { get; set; }
        public TypedError Inner { get; set; }
    }
}

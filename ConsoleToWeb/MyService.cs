namespace ConsoleToWeb
{
    public class MyService : IMyService
    {
        private Guid _guid;
        public MyService()
        {
            _guid = Guid.NewGuid();
            Console.WriteLine($"New instance created with ID: {_guid}");
        }

        public Guid GetOperationID()
        {
            return _guid;
        }
    }
}

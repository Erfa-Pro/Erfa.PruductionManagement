namespace Erfa.ProductionManagement.Application.Exceptions
{
    public class PersistenceFailedException : Exception
    {
        public PersistenceFailedException(string name, object key)
            : base($"{name} ({key}) is not saved")
        {
        }
    }
}

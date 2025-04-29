namespace PositionManagement.Shared.Common
{
    /// <summary>
    /// Represents an API key with its associated header and key value.
    /// This class is used to store and manage API key information for authentication purposes.
    /// </summary>
    public class ApiKey
    {
        public string Header { get; set; }
        public string Key { get; set; }
    }
}

namespace KeyVaultStore
{
    public interface IKeyValueStore
    {
        Task<string> Get(string key, CancellationToken ct);
        Task<bool> Store(string key, string value, CancellationToken ct);
        Task<bool> Remove(string key, CancellationToken ct);
    }
}

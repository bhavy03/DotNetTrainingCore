namespace AssetAPI.Repositories
{
    public interface IAssetRepository<T>
    {
        IEnumerable<T> GetAll();
        T? Search(int id);
        T Add(T asset);
        T? Update(int id, T asset);
        bool Delete(int id);
    }
}

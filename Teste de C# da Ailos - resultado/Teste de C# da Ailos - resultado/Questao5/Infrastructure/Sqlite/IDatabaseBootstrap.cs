namespace Questao5.Infrastructure.Sqlite
{
    public interface IDatabaseBootstrap
    {
        void Setup();
        Task<T> QueryFirstOrDefaultAsync<T>(string command);
        Task ExecuteAsync(string command, object entity);
        Task<List<T>> QueryAsync<T>(string command);
    }
}
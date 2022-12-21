namespace Jorda.Infrastructure.Persistance.Initializer;
public interface IDbInitialiser
{
    public Task InitialiseAsync();
    public Task SeedAsync();
}

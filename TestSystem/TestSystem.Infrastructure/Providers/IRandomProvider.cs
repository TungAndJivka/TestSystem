namespace TestSystem.Infrastructure.Providers
{
    public interface IRandomProvider
    {
        int Next(int min, int max);
    }
}

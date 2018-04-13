namespace TestSystem.Data.Data.Saver
{
    public interface ISaver
    {
        void SaveChanges();

        void SaveChangesAsync();
    }
}

using Microsoft.EntityFrameworkCore;

namespace TestSystem.Data.Data.Saver
{
    public class Saver : ISaver
    {
        private readonly DbContext context;

        public Saver(DbContext context)
        {
            this.context = context;
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }

        public async void SaveChangesAsync()
        {
            await this.context.SaveChangesAsync();
        }
    }
}

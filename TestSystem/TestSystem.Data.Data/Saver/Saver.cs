using Microsoft.EntityFrameworkCore;
using TestSystem.Web.Data;

namespace TestSystem.Data.Data.Saver
{
    public class Saver : ISaver
    {
        private readonly ApplicationDbContext context;

        public Saver(ApplicationDbContext context)
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

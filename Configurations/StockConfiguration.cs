using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication10.Models.StockModels;

namespace WebApplication10.Configurations
{
    public class StockConfiguration : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(s => s.PortfoliosStock).WithOne(ps => ps.Stock).HasForeignKey(ps => ps.StockId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}

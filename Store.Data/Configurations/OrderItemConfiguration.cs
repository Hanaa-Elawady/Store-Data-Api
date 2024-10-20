using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Data.Entities.Order;

namespace Store.Data.Configurations
{
	public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItems>
	{
		public void Configure(EntityTypeBuilder<OrderItems> builder)
		{
			builder.OwnsOne(orderItem => orderItem.ProductItem, x =>
			{
				x.WithOwner();
			});
		}
	}
}

namespace SprintPlanner.Infrastructure.Data.Configurations;

public class DeveloperConfiguration : IEntityTypeConfiguration<Developer>
{
    public void Configure(EntityTypeBuilder<Developer> builder)
    {
        builder
            .HasMany(x => x.Tasks)
            .WithOne(x => x.Developer)
            .HasForeignKey(x => x.DeveloperId)
            .IsRequired(false);
    }
}
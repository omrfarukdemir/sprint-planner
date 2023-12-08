namespace SprintPlanner.Infrastructure.Data.Configurations;

public class SprintTaskConfiguration : IEntityTypeConfiguration<SprintTask>
{
    public void Configure(EntityTypeBuilder<SprintTask> builder)
    {
        builder
            .Property(x => x.ExpectedDuration)
            .HasConversion<double>()
            .HasPrecision(4, 2);
    }
}
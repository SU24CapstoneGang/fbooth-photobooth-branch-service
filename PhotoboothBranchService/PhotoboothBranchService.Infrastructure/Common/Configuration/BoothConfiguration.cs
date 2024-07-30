using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Infrastructure.Common.Configuration
{
    public class BoothConfiguration : IEntityTypeConfiguration<Booth>
    {
        public void Configure(EntityTypeBuilder<Booth> builder)
        {
            builder.ToTable("Booths");
            // Primary key
            builder.HasKey(u => u.BoothID);
            builder.Property(u => u.BoothID).HasColumnName("BoothID").ValueGeneratedOnAdd();
            builder.Property(b => b.BoothName).IsRequired().HasMaxLength(50);
            builder.Property(b => b.BackgroundColor).IsRequired();
            builder.Property(b => b.Concept).IsRequired();
            builder.Property(b => b.PeopleInBooth).IsRequired();

            // ManufactureStatus enum mapping
            builder.Property(pb => pb.Status)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => (BoothStatus)Enum.Parse(typeof(BoothStatus), v));
            builder.Property(s => s.CreateDate)
             .ValueGeneratedOnAdd()
             .HasDefaultValueSql("GETDATE()")
             .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            builder.HasMany(s => s.SessionOrders)
                .WithOne(a => a.Booth)
                .HasForeignKey(c => c.BoothID)
                .IsRequired();
            builder.HasMany(d => d.Devices)
                .WithOne(i => i.Booth)
                .HasForeignKey(v => v.BoothID)
                .IsRequired();

            builder.HasData(new Booth
            {
                BoothID = new Guid("1671ccd8-d367-47c7-9c48-335da54ec34d"),
                BoothName = "Booth 01",
                BranchID = new Guid("b7fb8774-e3ac-4316-862c-23b81869c381"),
                Status = BoothStatus.Active,
                Concept = "Hallucination",
                PeopleInBooth = 5,
                BackgroundColor = "yellow"
            },
            new Booth
            {
                BoothID = new Guid("b8b615f3-a04f-4bbf-8ab2-f42dd69a65fd"),
                BoothName = "Booth 02",
                BranchID = new Guid("b7fb8774-e3ac-4316-862c-23b81869c381"),
                Status = BoothStatus.Active,
                Concept = "Nightmare",
                PeopleInBooth = 6,
                BackgroundColor = "yellow"
            },
            new Booth
            {
                BoothID = new Guid("bc8c737b-9a92-49b2-b9b1-bd8321c7e594"),
                BoothName = "Booth 03",
                BranchID = new Guid("0a1f2e05-f744-4d9b-937c-bfe7bad52a90"),
                Status = BoothStatus.Active,
                Concept = "Nightmare",
                PeopleInBooth = 4,
                BackgroundColor = "yellow"
            },
            new Booth
            {
                BoothID = new Guid("28110b4a-bf04-4c04-a19b-1b91d976ee7c"),
                BoothName = "Booth 04",
                BranchID = new Guid("0a1f2e05-f744-4d9b-937c-bfe7bad52a90"),
                Status = BoothStatus.Active,
                Concept = "Hallucination",
                PeopleInBooth = 3,
                BackgroundColor = "yellow"
            });
        }
    }
}

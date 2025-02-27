﻿using backendfepon.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backendfepon.ModelConfigurations
{
    public class CAccountingAccountConfiguration : IEntityTypeConfiguration<CAccountingAccount>
    {
        public void Configure(EntityTypeBuilder<CAccountingAccount> modelBuilder)
        {
            modelBuilder.ToTable("ACCOUNTING_ACCOUNT");
            modelBuilder.HasKey(p => p.Account_Id);

            // Relationships
            modelBuilder.HasOne(e => e.AccountType)
                .WithMany(c => c.CAccountingAccounts)
                .HasForeignKey(e => e.Account_Type_Id);

            modelBuilder.HasMany(a => a.OriginTransactions)
                .WithOne(t => t.OriginAccount)
                .HasForeignKey(t => t.Origin_Account);


            modelBuilder.HasMany(a => a.DestinationTransactions)
                .WithOne(t => t.DestinationAccount)
                .HasForeignKey(t => t.Destination_Account);


        }
    }
}

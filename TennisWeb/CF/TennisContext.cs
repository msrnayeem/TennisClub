using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TennisWeb.CF
{
    public class TennisContext : DbContext
    {
        public TennisContext() : base("name=TennisContext")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<CoachInfo> CoachInfoes { get; set; }
        public DbSet<PlayerInfo> PlayerInfoes { get; set; }
        public DbSet<Slot> Slots { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<MatchPlayer> MatchPlayers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Define composite primary key for MatchPlayer entity
            modelBuilder.Entity<MatchPlayer>()
                .HasKey(mp => new { mp.MatchId, mp.PlayerId });

            // Configure one-to-many relationship between User and CoachInfo
            modelBuilder.Entity<User>()
                .HasMany(u => u.CoachInfoes)
                .WithRequired(c => c.User)
                .HasForeignKey(c => c.UserId)
                .WillCascadeOnDelete(false); // ON DELETE NO ACTION

            // Configure one-to-many relationship between User and PlayerInfo
            modelBuilder.Entity<User>()
                .HasMany(u => u.PlayerInfoes)
                .WithRequired(p => p.User)
                .HasForeignKey(p => p.UserId)
                .WillCascadeOnDelete(false); // ON DELETE NO ACTION

            // Configure one-to-many relationship between CoachInfo and Match
            modelBuilder.Entity<CoachInfo>()
                .HasMany(c => c.Matches)
                .WithRequired(m => m.CoachInfo)
                .HasForeignKey(m => m.CoachId)
                .WillCascadeOnDelete(false); // ON DELETE NO ACTION

            // Configure one-to-many relationship between Match and MatchPlayer
            modelBuilder.Entity<Match>()
                .HasMany(m => m.MatchPlayers)
                .WithRequired(mp => mp.Match)
                .HasForeignKey(mp => mp.MatchId);

            // Configure one-to-many relationship between Match and Slot
            modelBuilder.Entity<Match>()
                .HasRequired(m => m.Slot)
                .WithMany(s => s.Matches)
                .HasForeignKey(m => m.SlotId);

            // Additional configuration for MatchPlayer entity
            modelBuilder.Entity<MatchPlayer>()
                .HasRequired(mp => mp.Match)
                .WithMany(m => m.MatchPlayers)
                .HasForeignKey(mp => mp.MatchId);

            modelBuilder.Entity<MatchPlayer>()
                .HasRequired(mp => mp.PlayerInfo)
                .WithMany() // Assuming there's no navigation property from PlayerInfo to MatchPlayer
                .HasForeignKey(mp => mp.PlayerId);

            base.OnModelCreating(modelBuilder);
        }


    }
}
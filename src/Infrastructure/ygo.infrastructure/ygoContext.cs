using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ygo.domain.Models;
using Attribute = ygo.domain.Models.Attribute;
using Type = ygo.domain.Models.Type;

namespace ygo.infrastructure
{
    public partial class ygoContext : DbContext
    {
        public virtual DbSet<Archetype> Archetype { get; set; }
        public virtual DbSet<ArchetypeCard> ArchetypeCard { get; set; }
        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<Attribute> Attribute { get; set; }
        public virtual DbSet<Banlist> Banlist { get; set; }
        public virtual DbSet<BanlistCard> BanlistCard { get; set; }
        public virtual DbSet<Card> Card { get; set; }
        public virtual DbSet<CardAttribute> CardAttribute { get; set; }
        public virtual DbSet<CardRuling> CardRuling { get; set; }
        public virtual DbSet<CardSubCategory> CardSubCategory { get; set; }
        public virtual DbSet<CardTip> CardTip { get; set; }
        public virtual DbSet<CardTrivia> CardTrivia { get; set; }
        public virtual DbSet<CardType> CardType { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Format> Format { get; set; }
        public virtual DbSet<Limit> Limit { get; set; }
        public virtual DbSet<SubCategory> SubCategory { get; set; }
        public virtual DbSet<Type> Type { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=ygo;Data Source=msi");
            }
        }

    }
}

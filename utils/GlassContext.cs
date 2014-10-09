using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace Utils
{
    public class GlassContext : DbContext
    {
        public DbSet<Credential> Credentials { get; set; }

        #region "Accesor único"
        private static GlassContext instancia = null;
        public static GlassContext Instancia
        {
            get
            {
                if (instancia == null)
                    instancia = new GlassContext();

                return instancia;
            }
        }
        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Credential>().ToTable("Credentials");
        }

        public void AddCredential(Credential pCredential)
        {
            Credentials.Add(pCredential);
            SaveChanges();
        }

        public Credential GetCredential(string pEmail)
        {
            return (from u in Credentials
                    where u.UserEmail.Equals(pEmail)
                    select u).FirstOrDefault<Credential>();
        }
    }

}

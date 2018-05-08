using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using Documaster.Model.Entities;

namespace Documaster.Data.DataAccess
{
    public class DocumasterDbContext : DbContext, IDbContext
    {
        // See connectionStrings section of UI project's web.config
        public DocumasterDbContext() : base("name = Documaster.ConnectionString")
        { }

        IQueryable<TEntity> IDbContext.Get<TEntity>() => Set<TEntity>();

        TEntity IDbContext.Create<TEntity>(TEntity entity) => Set<TEntity>().Add(entity);

        void IDbContext.Update<TEntity>(TEntity originalEntity, TEntity updatedEntity, IEnumerable<string> propertiesToUpdate)
        {
            foreach (var propertyToUpdate in propertiesToUpdate)
            {
                var dbProperty = Entry(originalEntity).Property(propertyToUpdate);
                var updatedPropertyInfo = updatedEntity.GetType().GetProperty(propertyToUpdate);
                if (dbProperty == null || updatedPropertyInfo == null)
                {
                    continue;
                }

                var newValue = updatedPropertyInfo.GetValue(updatedEntity, null);
                if (newValue?.ToString() != dbProperty.OriginalValue?.ToString())
                {
                    dbProperty.IsModified = true;
                    dbProperty.CurrentValue = newValue;
                }
            }
        }

        TEntity IDbContext.Delete<TEntity>(TEntity entity) => Set<TEntity>().Remove(entity);

        int IDbContext.SaveChanges() => base.SaveChanges();

        /// <inheritdoc />
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public IDbSet<Customer> Customers { get; set; }
        public IDbSet<OutputDocument> OutputDocuments { get; set; }
        public IDbSet<Project> Projects { get; set; }
        public IDbSet<ProjectRequirement> ProjectRequirements { get; set; }
        public IDbSet<Requirement> Requirements { get; set; }
        public IDbSet<Template> Templates { get; set; }
        public IDbSet<Category> Categories { get; set; }
    }
}
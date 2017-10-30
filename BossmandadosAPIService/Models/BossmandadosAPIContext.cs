using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Tables;
using BossmandadosAPIService.DataObjects;

namespace BossmandadosAPIService.Models
{
    public class BossmandadosAPIContext : DbContext
    {

        private const string connectionStringName = "Name=MS_TableConnectionString";

        public BossmandadosAPIContext() : base(connectionStringName)
        {
        }
        public DbSet<Manboss_usuario> Manboss_usuarios { get; set; }
        public DbSet<Manboss_repartidor> Manboss_repartidores { get; set; }
        public DbSet<Manboss_repartidores_calificacion> Manboss_repartidores_calificaciones { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<BossmandadosAPIContext>(null);
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Add(
                new AttributeToColumnAnnotationConvention<TableColumnAttribute, string>(
                    "ServiceTableColumn", (property, attributes) => attributes.Single().ColumnType.ToString()));
        }
    }

}

using Microsoft.EntityFrameworkCore;
using RPGProjConsole.models;

namespace RPGProjConsole.context
{
    class RPGContext : DbContext
    {
        //public RPGContext(DbContextOptions<RPGContext> options):base(options)
        //{
        //}
        string connectionLocal = "Data Source=f326\\sqlexpress;Initial Catalog=RPGDB;Integrated Security=True";

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(connectionLocal);
            //using(var results = new DbManagerContext())
            //builder.UseMySQL(connection);
            //builder.UseMySql(connectionAWS
            //, new MySqlServerVersion(new Version(8, 0, 31)));DESKTOP-HD4SRFB\SQLEXPRESS
        }
        public DbSet<Jogador> Jogadores { get; set; }
        public DbSet<Npcs> Npcs { get; set; }
        public DbSet<Inventario> Inventario { get; set; }
        public DbSet<ItemGeneral> Items { get; set; }
        public DbSet<Arma> Armas { get; set; }
        public DbSet<Armadura> Armaduras { get; set; }
        public DbSet<InventarioArma> InventarioArmas { get; set; }
        public DbSet<InventarioArmadura> InventarioArmaduras { get; set; }
        public DbSet<InventarioItem> InventarioItems { get; set; }
        public DbSet<Pericia> Pericias { get; set; }
        public DbSet<JogadorPericia> JogadorPericia { get; set; }
        public DbSet<ArvoreDeHabilidades> ArvoreHabilidades { get; set; }
        public DbSet<JogadorArvore> JogadorArvores { get; set; }

    }
}


namespace RPGProjConsole.models
{
    class Npcs : Personagem
    {
        public Npcs()
        {

        }
        public Npcs(Npcs npc)
        {
            Nome = npc.Nome;
            Id = npc.Id;
            Nivel = npc.Nivel;
            VidaMaxima = npc.VidaMaxima;
            Vitalidade = npc.Vitalidade;
            Medo = npc.Medo;
            AtaqueBase = npc.AtaqueBase;
            CA = npc.CA;
            Exp = npc.Exp;
            Forca = npc.Forca;
            Destreza = npc.Destreza;
            Constituicao = npc.Constituicao;
            Inteligencia = npc.Inteligencia;
            Sabedoria = npc.Sabedoria;
            Carisma = npc.Carisma;
            Inventario = npc.Inventario;
            MunicaoArmaEquipada = npc.MunicaoArmaEquipada;
            ArmaEquipada = npc.ArmaEquipada;
            ArmaduraEquipada = npc.ArmaduraEquipada;
            IsNpc = true;
            IsMob = true;
        }
        public bool IsMob { get; set; }
    }
}

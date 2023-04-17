namespace RPGProjConsole.models
{
    internal class JogadorPericia
    {
        public int Id { get; set; }
        public Jogador Jogador { get; set; }
        public Pericia Pericia { get; set; }
        public int Quantidade { get; set; }

    }
}

using RPGProjConsole.service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGProjConsole.models
{
    class Personagem
    {
        public int Id { get; set; }
        public int Nivel { get; set; }
        public string Nome { get; set; }
        public int VidaMaxima { get; set; }
        public int Vitalidade { get; set; }
        public int Medo { get; set; }
        public int AtaqueBase { get; set; }
        public int CA { get; set; }
        public int Exp { get; set; }
        public int Forca { get; set; }
        public int Destreza { get; set; }
        public int Constituicao { get; set; }
        public int Inteligencia { get; set; }
        public int Sabedoria { get; set; }
        public int Carisma { get; set; }
        public int MunicaoArmaEquipada { get; set; }
        public Arma ArmaEquipada { get; set; }
        public Armadura ArmaduraEquipada { get; set; }
        public Inventario Inventario { get; set; }
        [NotMapped]
        public InventarioConfig InventarioObj { get; set; }
        [NotMapped]
        public JogadorPericia Pericia { get; set; }
        [NotMapped]
        public int Iniciativa { get; set; }
        [NotMapped]
        public bool IsNpc { get; set; }
        [NotMapped]
        public bool EstaMirando{ get; set; }
        //[NotMapped]
        //public bool IsMobIdentifier { get; set; }

        public void Acertar(Personagem alvo)
        {
            Dado dado = new Dado();
            Console.WriteLine("Rode 1d20");
            var DadoResult = dado.RodarDado(Nome, 1, 20);
            var result = DadoResult;//result é a soma do acerto
            result += AtaqueBase;
            if (ArmaEquipada != null)
            {
                if (ArmaEquipada.IsCorpoACorpo)
                {
                    result += Modifier(1);
                }
                else
                {
                    if(EstaMirando == true)
                    {
                        result += 10;
                    }
                    result += Modifier(2);
                }
            }
            else
            {
                result += Modifier(1);
            }
            var alvoArmadura = alvo.CA + alvo.Modifier(2);//agora soma a defesa do inimigo
            if(alvo.ArmaduraEquipada != null)
            {
                alvoArmadura += alvo.ArmaduraEquipada.CA;
                //alvoArmadura 
            }
            Console.WriteLine("soma ataq "+result+ " some def "+ alvoArmadura);
            if (result >= alvoArmadura || DadoResult == 20)//testa para ve se acerta
            {
                Console.WriteLine("Rode um para causar dano");
                var dano = CausarDano(dado,DadoResult);
                if (EstaMirando)
                {
                    EstaMirando = false;
                }
                
                Console.WriteLine("Você causou {0} de dano", dano);
                alvo.Vitalidade -= dano;
            }
            else//errou
            {
                Console.WriteLine("ih acertou nnn");
            }
            Console.ReadLine();
            
        }
        public void AcertarComOportunidade(Personagem alvo)
        {
            Dado dado = new Dado();
            Console.WriteLine("Rode 1d20");
            var DadoResult = dado.RodarDado(Nome, 1, 20);
            var result = DadoResult;//result é a soma do acerto
            result += AtaqueBase;
            if (ArmaEquipada != null)
            {
                if (ArmaEquipada.IsCorpoACorpo)
                {
                    result += Modifier(1);
                }
                else
                {
                    if (EstaMirando == true)
                    {
                        result += 10;
                    }
                    result += Modifier(2);
                }
            }
            else
            {
                result += Modifier(1);
            }
            var alvoArmadura = alvo.CA;//agora soma a defesa do inimigo
            if (alvo.ArmaduraEquipada != null)
            {
                alvoArmadura += alvo.ArmaduraEquipada.CA;
            }
            Console.WriteLine("soma ataq " + result + " some def " + alvoArmadura);
            if (result >= alvoArmadura || DadoResult == 20)//testa para ve se acerta
            {
                Console.WriteLine("Rode um para causar dano");
                var dano = CausarDano(dado, DadoResult);
                if (EstaMirando)
                {
                    EstaMirando = false;
                }

                Console.WriteLine("Você causou {0} de dano", dano);
                alvo.Vitalidade -= dano;
            }
            else//errou
            {
                Console.WriteLine("ih acertou nnn");
            }
            Console.ReadLine();

        }
        public int CausarDano(Dado dado, int dadoResult)
        {
            var dano = 0;
            if (ArmaEquipada != null)
            {
                var danoAdd = 0;
                if (ArmaEquipada.IsCorpoACorpo)
                {
                    dano += dado.RodarDado(Nome, ArmaEquipada.Dados, ArmaEquipada.ValorDado);
                    danoAdd = Modifier(1);
                    ArmaEquipada.Durabilidade -= 1;
                }
                else
                {
                    dano = dado.RodarDado(Nome, ArmaEquipada.Dados, ArmaEquipada.ValorDado);
                    danoAdd = Modifier(2);
                    if (EstaMirando)
                    {
                        dano += dado.RodarDado(Nome, 1, 3);
                    }
                    MunicaoArmaEquipada -= 1;
                    InventarioObj.Atirar(ArmaEquipada);
                }
                if (dadoResult == 20)
                {
                    dano = dano * ArmaEquipada.IncrementoDecisivo;
                }
                dano += danoAdd;
            }
            else
            {
                dano = dado.RodarDado(Nome, 1, 3);
                dano += Modifier(1);
            }           
            return dano;
        }
        public int Modifier(int index)
        {
            decimal valor = 0;
            if(index == 1)
            {
                valor = Forca;
            }
            else if(index == 2)
            {
                valor = Destreza;
            }
            else if(index == 3)
            {
                valor = Constituicao;
            }
            else if(index == 4)
            {
                valor = Inteligencia;
            }
            else if(index == 5)
            {
                valor = Sabedoria;
            }
            else if(index == 6)
            {
                valor = Carisma;
            }

            var value = Math.Floor((valor - 10) / 2);
            return Convert.ToInt32(value);
            //if (valor <= 1)
            //{
            //    return -5;
            //}
            //else if (valor <= 3)
            //{
            //    return -4;
            //}
            //else if (valor <= 5)
            //{
            //    return -3;
            //}
            //else if
        }
    }
}

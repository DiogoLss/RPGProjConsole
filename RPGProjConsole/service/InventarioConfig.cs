using Microsoft.EntityFrameworkCore;
using RPGProjConsole.context;
using RPGProjConsole.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGProjConsole.service
{
    class InventarioConfig
    {
        public Personagem Personagem { get; set; }
        //public Inventario Inventario { get; set; } = new Inventario();
        //public ItemConfig Item { get; set; }
        public bool IsOpen { get; set; } = true;

        public RPGContext _context;
        public InventarioConfig(Personagem personagem, RPGContext context)
        {
            _context = context;
            if (!personagem.IsNpc)
            {
                Personagem = _context.Jogadores
                    .Include(x => x.Inventario)
                    .Include(x=>x.ArmaduraEquipada)
                    .Include(x => x.ArmaEquipada)
                    .FirstOrDefault(x => x.Id == personagem.Id);
            }
            else
            {
                Personagem = _context.Npcs
                    .Include(x => x.Inventario)
                    .Include(x => x.ArmaduraEquipada)
                    .Include(x => x.ArmaEquipada)
                    .FirstOrDefault(x => x.Id == personagem.Id);
            }
            Personagem.Inventario.InventarioArmas =
                _context.InventarioArmas.Include(x => x.Arma)
                .Where(x => x.Inventario.Id == personagem.Inventario.Id).ToList();
            Personagem.Inventario.InventarioArmaduras =
                _context.InventarioArmaduras.Include(x => x.Armadura)
                .Where(x => x.Inventario.Id == personagem.Inventario.Id).ToList();
            Personagem.Inventario.InventarioItems =
                _context.InventarioItems.Include(x => x.Item)
                .Where(x => x.Inventario.Id == personagem.Inventario.Id).ToList();

            //Item = new ItemConfig(_context);
        }
        public int Digitar()
        {
            int value;
            bool ok = int.TryParse(Console.ReadLine(), out value);
            if (ok)
            {
                return value;
            }
            else
            {
                return 0;
            }
        }
        public void ItensNaLoja()
        {
            Console.Clear();
            Console.WriteLine("1 - É um drop");
            Console.WriteLine("2 - É uma compra");
            var drop = Digitar();
            if(drop != 1)
            {
                drop = 0;
            }            
            Console.WriteLine("Deseja o que?");
            Console.WriteLine("1 - Arma");
            Console.WriteLine("2 - Armadura");
            Console.WriteLine("3 - Item");
            Console.WriteLine("4 - Munição");
            var item = Digitar();
            if(item == 1)
            {
                var estaEscolhendo = true;
                while (estaEscolhendo)
                {
                    Console.Clear();
                    var armas = _context.Armas.ToArray();
                    Console.WriteLine("Armas disponíveis:");
                    for (int i = 0; i < armas.Length; i++)
                    {
                        Console.WriteLine("{0} - {1} com dano {2} que pesa {3} e custa {4}.",
                            i + 1, armas[i].Nome, armas[i].Dano, armas[i].Peso, armas[i].Preco);
                    }
                    Console.WriteLine("Digite o index da arma escolhida, 0 para voltar");
                    var armaEscolhidaIndex = Digitar();
                    if (armaEscolhidaIndex == 0 || armaEscolhidaIndex > armas.Length)
                    {
                        estaEscolhendo = false;
                    }
                    else
                    {
                        var armaEscolhida = armas[armaEscolhidaIndex - 1];
                        ComprarArma(armaEscolhida,drop);
                    }
                }               
            }
            else if(item == 2)
            {
                var estaEscolhendo = true;
                while (estaEscolhendo)
                {
                    Console.Clear();
                    var armaduras = _context.Armaduras.ToArray();
                    Console.WriteLine("Armaduras disponíveis:");
                    for (int i = 0; i < armaduras.Length; i++)
                    {
                        Console.WriteLine("{0} - {1} com defesa {2} que pesa {3}, custa {4} e tem penalidade {5}.",
                            i + 1, armaduras[i].Nome, armaduras[i].CA, armaduras[i].Peso, armaduras[i].Preco,
                            armaduras[i].PenalidadeDeDestreza);
                    }
                    Console.WriteLine("Digite o index da arma escolhida, 0 para voltar");
                    var armaEscolhidaIndex = Digitar();
                    if (armaEscolhidaIndex == 0 || armaEscolhidaIndex > armaduras.Length)
                    {
                        estaEscolhendo = false;
                    }
                    else
                    {
                        var armaEscolhida = armaduras[armaEscolhidaIndex - 1];
                        ComprarArmadura(armaEscolhida,drop);
                    }
                }
            }
            else if (item == 3)
            {
                var estaEscolhendo = true;
                while (estaEscolhendo)
                {
                    Console.Clear();
                    var items = _context.Items.ToArray();
                    Console.WriteLine("Items disponíveis:");
                    for (int i = 0; i < items.Length; i++)
                    {
                        Console.WriteLine("{0} - {1} que custa {2} e pesa {3}.",
                            i + 1, items[i].Nome, items[i].Preco, items[i].Peso);
                    }
                    Console.WriteLine("Digite o index do item escolhido, 0 para voltar");
                    var itemEscolhidoIndex = Digitar();
                    if (itemEscolhidoIndex == 0 || itemEscolhidoIndex > items.Length)
                    {
                        estaEscolhendo = false;
                    }
                    else
                    {
                        var itemEscolhido = items[itemEscolhidoIndex - 1];
                        ComprarItem(itemEscolhido,drop);
                    }
                }
            }
            else if (item == 4)
            {
                var inventario = Personagem.Inventario.InventarioArmas;
                Console.Clear();                
                for (int i = 0; i < inventario.Count; i++)
                {
                    Console.WriteLine("{0} - {1} com dano {2}", i + 1,
                        inventario[i].Arma.Nome,
                        inventario[i].Arma.Dano);
                }
                Console.WriteLine();
                Console.WriteLine("De que arma?");
                var escolha = Digitar();
                Console.WriteLine("Quantas munições?");
                var quantidade = Digitar();
                var armaSelecionada = inventario[escolha - 1];
                armaSelecionada.MunicaoReserva = +quantidade;               
                _context.Entry(armaSelecionada).State = EntityState.Modified;
                _context.Update(armaSelecionada);
                _context.SaveChanges();
                Console.WriteLine("Você agora tem {0} munições reservas e {1} carregadas",
                    armaSelecionada.MunicaoReserva, armaSelecionada.MunicaoCarregada);
                Console.ReadLine();
            }
        }
        public int ComprarArma(Arma arma, int edrop)
        {
            if (arma == null)
            {
                Console.WriteLine("Nenhum item escolhido");
            }
            else
            {
                double valor = arma.Preco;
                var sn = 0;
                if(edrop == 1)
                {
                    Console.WriteLine("Digite 1 para colocar no inventario esse item");
                    sn = Digitar();
                }
                else
                {
                    Console.WriteLine("Digite 1 para comprar essa arma por " + valor);
                    sn = Digitar();
                }               
                if(sn == 0 || sn > 1)
                {
                    Console.WriteLine("Erro ao comprar");
                }
                else if (valor <= Personagem.Inventario.Dinheiro || edrop == 1)
                {

                        var inventarioArmas = new InventarioArma()
                        {
                            Arma = arma,
                            Inventario = Personagem.Inventario,
                        };                        
                        _context.InventarioArmas.Add(inventarioArmas);
                        if(edrop == 0)
                        {
                            Personagem.Inventario.Dinheiro -= valor;
                            _context.Entry(Personagem.Inventario).State = EntityState.Modified;
                            _context.Update(Personagem.Inventario);
                        }                        
                        return _context.SaveChanges();
                        
                }
                else
                {
                    Console.WriteLine("Dinheiro insuficiente");
                }
            }
            
            return 0;
        }
        public int ComprarArmadura(Armadura armadura, int edrop)
        {
            Console.WriteLine("Quantas armaduras dessa deseja?");
            var qtd = Digitar();
            if (qtd == 0)
            {
                Console.WriteLine("Escolha pelo menos uma");
            }
            else if (armadura == null)
            {
                Console.WriteLine("Nenhum item escolhido");
            }
            else
            {
                double valor = armadura.Preco * qtd;
                var sn = 0;
                if (edrop == 1)
                {
                    Console.WriteLine("Digite 1 para colocar no inventario esse item");
                    sn = Digitar();
                }
                else
                {
                    Console.WriteLine("Digite 1 para comprar essa(s) armadura(s) por " + valor);
                    sn = Digitar();
                }
                if (sn == 0 || sn > 1)
                {
                    Console.WriteLine("Erro ao comprar");
                }
                else if (valor <= Personagem.Inventario.Dinheiro || edrop == 1)
                {
                        var inventarioArmaduras = new InventarioArmadura()
                        {
                            Armadura = armadura,
                            Inventario = Personagem.Inventario,
                        };                        
                        _context.InventarioArmaduras.Add(inventarioArmaduras);
                        if(edrop == 0)
                        {
                            Personagem.Inventario.Dinheiro -= valor;
                            _context.Entry(Personagem.Inventario).State = EntityState.Modified;
                            _context.Update(Personagem.Inventario);
                        }                       
                        return _context.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Dinheiro insuficiente");
                }

            }
            return 0;
        }
        public int ComprarItem(ItemGeneral item, int edrop)
        {
            Console.WriteLine("Quantos itens dessa deseja?");
            var qtd = Digitar();
            if (qtd == 0)
            {
                Console.WriteLine("Escolha pelo menos um");
            }
            else if (item == null)
            {
                Console.WriteLine("Nenhum item escolhido");
            }
            else
            {
                double valor = item.Preco * qtd;
                var sn = 0;
                if (edrop == 1)
                {
                    Console.WriteLine("Digite 1 para colocar no inventario esse item");
                    sn = Digitar();
                }
                else
                {
                    Console.WriteLine("Digite 1 para comprar essa(s) item(ns) por " + valor);
                    sn = Digitar();
                }
                if (sn == 0 || sn > 1)
                {
                    Console.WriteLine("Erro ao comprar");
                }
                else if (valor <= Personagem.Inventario.Dinheiro || edrop == 1)
                {
                    var id = 0;
                    foreach (var itemI in Personagem.Inventario.InventarioItems)
                    {
                        if (item.Id == itemI.Item.Id)
                        {
                            id = item.Id;
                        }
                    }

                    if (id == 0)
                    {
                        var inventarioItems = new InventarioItem()
                        {
                            Item = item,
                            Inventario = Personagem.Inventario,
                            Qtd = qtd
                        };                        
                        _context.InventarioItems.Add(inventarioItems);
                        if(edrop == 0)
                        {
                            Personagem.Inventario.Dinheiro -= valor;
                            _context.Entry(Personagem.Inventario).State = EntityState.Modified;
                            _context.Update(Personagem.Inventario);
                        }
                        
                        return _context.SaveChanges();

                    }
                    else
                    {
                        var arm = Personagem.Inventario.InventarioItems.FirstOrDefault(x => x.Id == id);
                        arm.Qtd += qtd;
                        _context.Entry(arm).State = EntityState.Modified;
                        _context.Update(arm);
                        if(edrop == 0)
                        {
                            Personagem.Inventario.Dinheiro -= valor;
                            _context.Entry(Personagem.Inventario).State = EntityState.Modified;
                            _context.Update(Personagem.Inventario);
                        }
                        
                        return _context.SaveChanges();
                    }
                }
                else
                {
                    Console.WriteLine("Dinheiro insuficiente");
                }

            }
            return 0;
        }
        public void CalcularInventario()
        {
            double sum = 0;
            foreach (var inventario in Personagem.Inventario.InventarioArmas)
            {
                var valor = inventario.Arma.Peso;
                sum += valor;
            }
            foreach (var inventario in Personagem.Inventario.InventarioArmaduras)
            {
                var valor = inventario.Armadura.Peso;
                sum += valor;
            }
            foreach (var inventario in Personagem.Inventario.InventarioItems)
            {
                var valor = inventario.Item.Peso * inventario.Qtd;
                sum += valor;
            }
            Personagem.Inventario.PesoTotal = sum;
            _context.Entry(Personagem.Inventario).State = EntityState.Modified;
            _context.Update(Personagem.Inventario);
        }
        public void MostrarTodosOsItens()
        {
            Console.Clear();
            Console.WriteLine("Itens de: " + Personagem.Nome);
            
            if(Personagem.Inventario.InventarioArmas != null)
            {
                Console.WriteLine("Armas:");
                foreach (var arma in Personagem.Inventario.InventarioArmas)
                {
                    Console.WriteLine("arma {0} com dano {1} que pesa {2} e vale {3}.",
                        arma.Arma.Nome, arma.Arma.Dano, arma.Arma.Peso, arma.Arma.Preco);
                }
            }
            Console.WriteLine();
            if (Personagem.Inventario.InventarioArmaduras != null)
            {
                
                Console.WriteLine("Armaduras:");
                foreach (var armadura in Personagem.Inventario.InventarioArmaduras)
                {
                    Console.WriteLine("armadura {0} com defesa {1} que pesa {2}, tem {3} de penalidade e vale {4}.",
                        armadura.Armadura.Nome, armadura.Armadura.CA,
                        armadura.Armadura.Peso, armadura.Armadura.PenalidadeDeDestreza, armadura.Armadura.Preco);
                }
            }
            Console.WriteLine();
            if(Personagem.Inventario.InventarioItems != null)
            {
                Console.WriteLine("Itens:");
                foreach (var item in Personagem.Inventario.InventarioItems)
                {
                    Console.WriteLine("{0} item(ns) {1} que pesa {2} e vale {3}.",
                        item.Qtd, item.Item.Nome, item.Item.Peso, item.Item.Preco);
                }
            }
            Console.WriteLine("Enter para continuar");
            Console.ReadLine();
            Console.Clear();
        }
        public void AbrirOpcoesInventario()
        {
            if(Personagem.Inventario is null)
            {
                Console.WriteLine("Não tem inventário");
                return;
            }
            while (IsOpen)
            {
                Console.Clear();
                Console.WriteLine("Veja as opções de inventário");
                Console.WriteLine("1 - Todos seus itens / Loja / Loot externo");
                Console.WriteLine("2 - Trocar de arma/armadura");
                Console.WriteLine("3 - Lootear esse inventario");

                Console.WriteLine("469 - Sair do inventário");
                var escolha = Digitar();
                Console.WriteLine();
                if (escolha == 0)
                {
                    Console.WriteLine("Escolha novamente, não entendi");
                }
                else if (escolha == 1)//ITENS / LOJA / LOOT
                {
                    Console.WriteLine("1 - Visualizar todos seus itens");
                    Console.WriteLine("2 - Novos itens");
                    var x = Digitar();
                    if (x == 1)
                    {
                        MostrarTodosOsItens();
                    }
                    else if (x == 2)
                    {
                        ItensNaLoja();
                        CalcularInventario();
                    }                    
                }
                else if (escolha == 2) // TROCAR DE ARMA ARMADURA
                {
                    Console.WriteLine("O que deseja fazer?");
                    Console.WriteLine("1 - Trocar de arma");
                    Console.WriteLine("2 - Trocar de armadura");
                    Console.WriteLine("0 - Voltar");
                    var x = Digitar();
                    if (x == 0)
                    {

                    }
                    else if(x == 1)
                    {
                        TrocarPegarArma();

                    }

                }
                else if (escolha == 3)
                {
                    RecarregarArma();
                }
                else if (escolha == 469)
                {
                    IsOpen = false;
                }
            }
        }

        //COMBATE OPTS
        public void TrocarPegarArma()
        {
            Console.Clear();
            var armaEquipada = Personagem.ArmaEquipada;
            var inventarioArma = Personagem.Inventario.InventarioArmas;
            if (armaEquipada != null)
            {
                Console.WriteLine("Sua arma atual é: {0} de {1} de dano",
                    armaEquipada.Nome, armaEquipada.Dano);
            }
            else
            {
                Console.WriteLine("Você está desarmado!");
            }
            
            for (int i = 0; i < inventarioArma.Count; i++)
            {
                Console.Write("{0} - arma {1} com {2} de dano tem {3} munições carregadas e {4} na mochila",
                    i + 1, inventarioArma[i].Arma.Nome, inventarioArma[i].Arma.Dano,
                    inventarioArma[i].MunicaoCarregada, inventarioArma[i].MunicaoReserva);
                if(armaEquipada != null)
                {
                    if (inventarioArma[i].Arma.Id == armaEquipada.Id)
                    {
                        Console.Write(" Equipada");
                    }
                }
                
                
                Console.WriteLine();
            }
            Console.WriteLine("Escolha sua arma, 0 para desarmar, 469 para sair");
            Console.WriteLine();
            var escolha = Digitar();
            if (escolha == 0)
            {
                Personagem.ArmaEquipada = null;
            }
            else if (escolha == 469)
            {
                return;
            }
            else if(escolha > 0 && escolha <= inventarioArma.Count)
            {
                Personagem.ArmaEquipada = inventarioArma[escolha - 1].Arma;
            }
            _context.Entry(armaEquipada).State = EntityState.Modified;
            _context.Update(armaEquipada);
            _context.SaveChanges();
        }
        public void TrocarPegarArmadura()
        {
            Console.Clear();
            var armaduraEquipada = Personagem.ArmaduraEquipada;
            var inventarioArmaduras = Personagem.Inventario.InventarioArmaduras;
            if (armaduraEquipada != null)
            {
                Console.WriteLine("Sua arma atual é: {0} de {1} de CA",
                    armaduraEquipada.Nome, armaduraEquipada.CA);
            }
            else
            {
                Console.WriteLine("Você está sem armadura!");
            }

            for (int i = 0; i < inventarioArmaduras.Count; i++)
            {
                Console.Write("{0} - armadura {1} com {2} de CA com {0} de penalidade",
                    i + 1, inventarioArmaduras[i].Armadura.Nome, inventarioArmaduras[i].Armadura.CA,
                    inventarioArmaduras[i].Armadura.PenalidadeDeDestreza);
                if (armaduraEquipada != null)
                {
                    if (inventarioArmaduras[i].Armadura.Id == armaduraEquipada.Id)
                    {
                        Console.Write("Equipada");
                    }
                }


                Console.WriteLine();
            }
            Console.WriteLine("Escolha sua armadura, 0 para desequipar, 469 para sair");
            Console.WriteLine();
            var escolha = Digitar();
            if (escolha == 0)
            {
                Personagem.ArmaduraEquipada = null;
            }
            else if (escolha == 469)
            {
                return;
            }
            else if (escolha > 0 && escolha <= inventarioArmaduras.Count)
            {
                Personagem.ArmaduraEquipada = inventarioArmaduras[escolha - 1].Armadura;
            }
            _context.Entry(armaduraEquipada).State = EntityState.Modified;
            _context.Update(armaduraEquipada);
            _context.SaveChanges();
        }
        public void RecarregarArma()
        {
            var id = 0;
            foreach(var item in Personagem.Inventario.InventarioArmas)
            {
                if (item.Arma.Id == Personagem.ArmaEquipada.Id)
                {
                    id = item.Arma.Id;
                }
            }
            var slotArma = Personagem.Inventario.InventarioArmas.FirstOrDefault(x => x.Arma.Id == id);
            if (slotArma.MunicaoCarregada == Personagem.ArmaEquipada.MunicaoMaxima)
            {
                Console.WriteLine("Arma já está carregada");
            }
            
            var dif = Personagem.ArmaEquipada.MunicaoMaxima - slotArma.MunicaoCarregada;
            Console.WriteLine("Sua arma equipada tem {0} munições carregadas", slotArma.MunicaoCarregada);
            Console.WriteLine("Sua mochila tem {0} munições desta arma", slotArma.MunicaoReserva);
            Console.WriteLine();
            var municaoInventario = slotArma.MunicaoReserva;
            if(municaoInventario <= dif)
            {
                slotArma.MunicaoCarregada += municaoInventario;
                municaoInventario = 0;
            }
            else
            {
                var carregado = slotArma.MunicaoCarregada - slotArma.Arma.MunicaoMaxima;
                slotArma.MunicaoCarregada += carregado;
                municaoInventario -= carregado;
            }
            slotArma.MunicaoReserva = municaoInventario;
            _context.Entry(slotArma).State = EntityState.Modified;
            _context.Update(slotArma);
            //_context.Entry(Personagem.Inventario.InventarioArmas).State = EntityState.Modified;
            //_context.Update(Personagem.Inventario.InventarioArmas);
            Console.WriteLine("Agora sua arma equipada tem {0} munições carregadas", slotArma.MunicaoCarregada);
            Console.WriteLine("Agora sua mochila tem {0} munições desta arma", slotArma.MunicaoReserva);
            Console.WriteLine();
            _context.SaveChanges();
            Console.ReadLine();
        }
        public void Saquear()
        {            
            var isSaqueando = true;

            //while (isSaqueando)
            //{
            //    Console.Clear();
            //    Console.WriteLine("Você está saqueando o(a) {0}", Personagem.Nome);


            //}

        }
        public void Atirar(Arma arma)
        {
            var armaInv = Personagem.Inventario.InventarioArmas.FirstOrDefault(x => x.Arma.Id == arma.Id);
            if(armaInv != null)
            {
                armaInv.MunicaoCarregada -= 1;
                _context.Entry(armaInv).State = EntityState.Modified;
                _context.Update(armaInv);
            }            
            _context.SaveChanges();
        }
    }
}

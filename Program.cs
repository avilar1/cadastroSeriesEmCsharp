
using Dio.EntregaFinal;
using Dio.EntregaFinal.Classes;
using Dio.EntregaFinal.Interfaces;
using System;
using System.Text.RegularExpressions;


namespace Start
{
    class Program
    {
        static SerieIRepositorio repositorio = new SerieIRepositorio();
        static void Main(string[] args)
        {
            string opcaoUsuario = ObterOpcaoUsuario();

            while (opcaoUsuario.ToUpper() != "X")
            {
                switch (opcaoUsuario)
                {
                    case "1":{
                        ListaSeries();
                        break;
                        }
                    case "2":{
                        InserirSerie();
                        break;
                        }
                    case "3":{
                        AtualizarSerie();
                        break;
                        }
                    case "4":{
                        ExcluirSerie();
                        break;
                        }
                    case "5": {
                        VisualizarSerie();
                        break;
                        }
                    case "C": {
                        Console.Clear();
                        break;
                        }

                    default:
                        throw new ArgumentOutOfRangeException();

                }

                opcaoUsuario = ObterOpcaoUsuario();
                
            }
        }

        private static void VisualizarSerie()
        {
            //Console.Write("Digite Id da serie: ");
             int indiceSerie = pegarId();
            
            var serie = repositorio.RetornaPorId(indiceSerie);

            System.Console.WriteLine(serie);
            
        }

        private static void ExcluirSerie()
        {
            Console.Write("Digite Id da serie: ");
            
            int indiceSerie = pegarId();

            System.Console.WriteLine("confirma? 1 - Sim | 2 - Não (ou qualquer outra tecla)");
            string confirma = Console.ReadLine();
            if( confirma == "1"){
                repositorio.Exclui(indiceSerie);
            }else{
                 Console.Write("exclusão abortada");
            }

        }

        private static void AtualizarSerie()
        {
            //Console.Write("Digite a Id da serie: ");
            int indiceSerie = pegarId();
        
            int entradaGenero = cadastroEnum();

            Console.Write("Digite  oTitulo da Serie: ");
            string entradaTitulo = Console.ReadLine();

            Console.Write("DigiteaAno de Inicio da Serie: ");
            int entradaAno = int.Parse(Console.ReadLine());

            Console.Write("Digite a Descricao da Serie: ");
            string entradaDescricao = Console.ReadLine();
            Serie atualizaSerie = new Serie(id: indiceSerie,
                                        genero: (Genero)entradaGenero,
                                        titulo: entradaTitulo,
                                        ano: entradaAno,
                                        descricao: entradaDescricao);
                                        
            repositorio.Atualiza(indiceSerie, atualizaSerie);

        }

        private static void InserirSerie()
        {
            Console.WriteLine("Inserir nova série");

            int entradaGenero = cadastroEnum();
            
            Console.Write("Digite  oTitulo da Serie: ");
            string entradaTitulo = Console.ReadLine();
            

            Console.Write("DigiteaAno de Inicio da Serie: ");
            int entradaAno = int.Parse(Console.ReadLine());

            Console.Write("Digite a Descricao da Serie: ");
            string entradaDescricao = Console.ReadLine();
            Serie novaSerie = new Serie(id: repositorio.ProximoId(),
                            genero: (Genero)entradaGenero,
                                        titulo: entradaTitulo,
                                        ano: entradaAno,
                                        descricao: entradaDescricao);
                                        repositorio.Insere(novaSerie);
        }

        private static void ListaSeries()
        {
            System.Console.WriteLine("Lista de serie:");
            var lista = repositorio.Lista();

            if (lista.Count == 0)
            {
                System.Console.WriteLine("Nenhuma serie");
                return;
            }

            foreach (var serie in lista)
            {
                var excluido = serie.retornaExcluido();
                
                System.Console.WriteLine("#ID {0}: - {1} - {2}", serie.retornaId(), serie.retornaTitulo(), (excluido ? "*Excluido*" : ""));  //? se sim, : se não
                
            }
        }

        private static int pegarId() {
            int indiceSerie;
            do {
                Console.Write("Digite a Id da serie: ");
                string preIndiceSerie = Console.ReadLine();
                if(Regex.IsMatch(preIndiceSerie, @"^[0-9]+$")){
                    indiceSerie = int.Parse(preIndiceSerie);
                }else{
                    Console.Write("Digite pelo menos um número dessa vez!" + Environment.NewLine);
                    indiceSerie = -2;
                }
            }while(indiceSerie > repositorio.Lista().Count-1 || indiceSerie<0);
            return indiceSerie;

        }

        public static int cadastroEnum() {
            
            int j=0;
            foreach(int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
                j++;
            }
            int entradaGenero;
            do{Console.Write("Digite o gênero entre as apções acima:");
            string preIndice = Console.ReadLine();
            if(Regex.IsMatch(preIndice, @"^[0-9]+$")){
                    entradaGenero = int.Parse(preIndice);
                }else{
                    Console.Write("Digite pelo menos um número dessa vez!" + Environment.NewLine);
                    entradaGenero = -2;
                }
            }while(entradaGenero > j || entradaGenero < 0);
            
            return entradaGenero;

            
        }
        public static string ObterOpcaoUsuario()
        {

            Console.WriteLine();
            Console.WriteLine("Inicio do aplicativo");
            Console.WriteLine("Informe a opção que deseja:");

            Console.WriteLine("1 - Listar series");
            Console.WriteLine("2 - Inserir series");
            Console.WriteLine("3 - Atualizar serie");
            Console.WriteLine("4 - Excluir serie");
            Console.WriteLine("5 - Visualizar uma serie especifica");
            Console.WriteLine("C - Limpar Tela");
            Console.WriteLine("X - Sair");
            Console.WriteLine();

            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcaoUsuario;
        }
    }
}

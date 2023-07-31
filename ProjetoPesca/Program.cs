using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPesca
{
    internal class Program
    {
        //Lago e suas dimensões 
        const int linhaLago = 5, colunaLago = 10;
        static string[,] lago = new string[linhaLago, colunaLago];

        //var peixes
        static int peixes;
        static void Main(string[] args)
        {
            //Uma looping até o usuário interrompe-lo.
            bool fechar = false;
            do
            {
                //variáveis
                int numJogadores = 0, iscas = 0;
                String[] nomeJogador;


                //Introdução do jogo (Comentário inical + criação do lago)
                Console.Write("Bem vindo ao Jogo da Pesca.\nJogue contra seus amigos, quem pescar mais peixes ganha, boa sorte!\n");
                Console.WriteLine("\n-------------- LAGO COM OS PEIXES --------------");
                criarLago();
                Console.WriteLine("\n\nLEGENDA \n\n[ ] - Lugar livre para jogada \t [X]- Lugar com peixe \t [A]- Lugar sem peixe \n\n --------------------------------------------------------------------------------\n");


                //Informações a serem coletadas
                //Quantos Jogadores? (Apenas números)
                bool lol = false;
                do
                {
                    //Validação para o caso do usuário inserir números
                    try
                    {
                        Console.WriteLine("\nQual a quantidade de jogadores");
                        numJogadores = int.Parse(Console.ReadLine());
                        lol = true;
                    }
                    catch (System.FormatException)
                    {
                        Console.WriteLine("\nDigite apenas números!");
                        Console.ReadKey();
                        lol = false;
                    }
                } while (lol == false);


                //Nome dos jogadores
                nomeJogador = new String[numJogadores];
                for (int i = 0; i < numJogadores; i++)
                {
                    Console.WriteLine("\nEntre com o nome do jogador " + (i + 1));
                    nomeJogador[i] = Console.ReadLine();
                }

                //Quantas iscas? (Apenas números)
                bool lool = true;
                do
                {
                    try
                    {
                        Console.WriteLine("\nQuantas iscas cada jogador terá? ");
                        iscas = int.Parse(Console.ReadLine());
                        //Verificar o número de iscas possíveis de se jogar
                        if (iscas <= colunaLago * linhaLago / (double)numJogadores)
                        {
                            lool = false;
                        }
                        else
                        {
                            Console.WriteLine("\n------------\nInsira uma quantidade menor do que " + (colunaLago * linhaLago / numJogadores) + " iscas por jogadores \n------------\n");
                            lool = true;
                        }
                    }
                    catch (System.FormatException)
                    {
                        Console.WriteLine("\nDigite apenas números!");
                        Console.ReadKey();
                        lol = true;
                    }

                } while (lool == true);


                //Colocando os peixinhos no lago
                inserirPeixes();
                mostrarLago();


                //Rodadas de cada jogador
                int[] pontuacaoJogadores = new int[numJogadores]; // Matriz para guardar as pontuações de cada jogador
                pescarPeixes(numJogadores, nomeJogador, iscas, pontuacaoJogadores);


                //Resultado (FIM DE JOGO!)
                campeao(pontuacaoJogadores, nomeJogador);

                //DESEJA JOGAR NOVAMENTE?
                Console.WriteLine("Deseja jogar novamente? \n1 para sim \t 2 para não");
                int resposta = int.Parse(Console.ReadLine());
                if (resposta == 1)
                {
                    fechar = false;
                }
                else
                {
                    if (resposta == 2)
                    {
                        fechar = true;
                    }
                }
                Console.Clear();
            } while (fechar == false);

            //Finalização do jogo
            Console.WriteLine("\n\n\nObrigador por jogar, até a proxima! \n--------------------- \nPressione qualuqer tecla para encerrar!");
            Console.ReadKey();
        }


        //MÉTODOS
        //Método para criar um lago 5X10 Vazio
        private static void criarLago()
        {
            //gerando o lago
            for (int i = 0; i < linhaLago; i++) //linhas
            {
                Console.WriteLine();
                for (int j = 0; j < colunaLago; j++) //colunas
                {
                    lago[i, j] = " [ ] ";
                    Console.Write(lago[i, j]);
                }
            }
        }


        //Método para mostrar o lago
        private static void mostrarLago()
        {
            for (int i = 0; i < linhaLago; i++) //linhas do lago
            {
                Console.WriteLine();
                for (int j = 0; j < colunaLago; j++) //colunas do lago
                {
                    //Ocultando os peixes ao olho do jogador
                    if (lago[i, j] == " [P] ")
                    {
                        Console.Write(" [ ] ");
                    }
                    else
                    {
                        Console.Write(lago[i, j]);
                    }

                }
            }
            Console.WriteLine("\n\nLEGENDA \n\n[ ] - Lugar livre para jogada \t [X]- Lugar com peixe \t [A]- Lugar sem peixe \n\n -----------------------------------------------------------------\n");
        }




        //Método para colocar os peixes no lago
        private static void inserirPeixes()
        {
            bool limite = false;

            //Não deixar o programa fechar caso ocorrá
            do
            {
                try
                {
                    //entradas
                    Console.WriteLine("\nInsira com quantos peixes você deseja jogar ");
                    peixes = int.Parse(Console.ReadLine());

                    //Validação caso a pessoa exceda o máximo de posições do lago
                    if (peixes < linhaLago * colunaLago)
                    {
                        //classe randomica para gerar números aleatórios
                        Random numRand = new Random();

                        //colocando os peixes no lago
                        for (int i = 0; i < peixes; i++)
                        {
                            int linha = numRand.Next(linhaLago);
                            int coluna = numRand.Next(colunaLago);

                            if (lago[linha, coluna] == " [ ] ")
                            {
                                lago[linha, coluna] = " [P] ";
                            }
                            else
                            {
                                i--; //caso haja caído um peixe em casa repetida, decrementa um do indice para repetir o processo
                            }
                        }

                        limite = true;
                    }
                    else
                    {
                        Console.WriteLine("\n-------------------------\nNão é possível colocar mais de " + (linhaLago * colunaLago) + " Peixes no lago, tente novamente\n-------------------------");
                        limite = false;
                    }
                }
                catch (System.FormatException)
                {

                    Console.WriteLine("Escreva apenas números!");
                }

            } while (limite == false);
        }


        public static void pescarPeixes(int numJogadores, string[] nomejogador, int iscas, int[] pontuacaoJogadores)
        {
            //Rodadas
            //int contarPeixes = 0;
            //bool acabouPeixes = false;

            for (int i = 0; i < iscas; i++)
            {
                for (int j = 0; j < numJogadores; j++)
                {

                    //Cabeçalho
                    Console.Clear();
                    Console.Write("\nRodada " + (i + 1));
                    Console.Write(" " + nomejogador[j]);
                    Console.WriteLine("\n-------------------------");
                    mostrarLago();

                    //Perguntando a cordenada
                    bool testCol, testLin;
                    int colunaPesca = 0, linhaPesca = 0;
                    //testando a coluna
                    do
                    {
                        try
                        {
                            Console.WriteLine("\nInsira a coluna do lago que deseja jogar");
                            colunaPesca = int.Parse(Console.ReadLine());
                            testCol = true;
                        }
                        catch (System.FormatException)
                        {
                            Console.WriteLine("\n---------\nEscreva apenas números\n---------\n");
                            testCol = false;
                        }

                    } while (testCol == false);
                    //testando a linha
                    do
                    {
                        try
                        {
                            Console.WriteLine("\nInsira a linha do lago que deseja jogar");
                            linhaPesca = int.Parse(Console.ReadLine());
                            testLin = true;
                        }
                        catch (System.FormatException)
                        {
                            Console.WriteLine("\n---------\nEscreva apenas números\n---------\n");
                            testLin = false;
                        }

                    } while (testLin == false);




                    //Verificar se as cordenadas estão dentro das dimensões do lago
                    try
                    {
                        //Verificando se outro jogador ja jogou naquela posição
                        if (lago[(linhaPesca - 1), (colunaPesca - 1)] != " [A] " && lago[(linhaPesca - 1), (colunaPesca - 1)] != " [X]")
                        {
                            //Verificando se tem ou não peixes naquela posição
                            if (lago[(linhaPesca - 1), (colunaPesca - 1)] == " [P] ")
                            {
                                Console.WriteLine("Você pescou um peixe!");
                                lago[linhaPesca - 1, colunaPesca - 1] = " [X] ";
                                Console.ReadKey();
                                pontuacaoJogadores[j]++;
                                //contarPeixes++;
                            }
                            else
                            {
                                Console.WriteLine("Não há nenhum peixe nessa posição");
                                lago[linhaPesca - 1, colunaPesca - 1] = " [A] ";
                                Console.ReadKey();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Outro Jogador já jogou nessa posição, tente novamente");
                            j--;
                            Console.ReadKey();
                        }
                    }
                    catch (System.IndexOutOfRangeException)
                    {

                        Console.WriteLine("Você jogou a isca fora do lago!");
                        Console.ReadKey();
                    }
                    mostrarLago();
                    //TENTEI MAS NÃO CONSEGUI: Caso acabasse os peixes antes das rodadas acabassem ele interrompesse o programa e cortaca pro resultado
                    //if(contarPeixes < peixes)
                    //{
                    //    acabouPeixe = false;
                    //}
                    //else
                    //{
                    //    Console.WriteLine("FIM DE JOGO \nACABARAM-SE OS PEIXES DO LAGO");
                    //    acabouPeixe = true;
                    //}
                }
            }
        }



        //método para definir e mostrar o campeão
        public static void campeao(int[] pontuacaoJogadores, string[] nomeJogador)
        {
            //variáveis
            Console.Clear();
            string vencedor = "", aux = "";
            int maior = 0;
            bool empate = false;

            //Definir um vencedor
            for (int i = 0; i < pontuacaoJogadores.Length; i++)
            {
                //colocando na variável a maior pontuação e seu respectivo vencedor na variável vencedor e auxiliar
                if (maior < pontuacaoJogadores[i])
                {
                    maior = pontuacaoJogadores[i];
                    vencedor = nomeJogador[i];
                    aux = vencedor;
                }
            }
            //Verificar um possível empate, usando o a variável aux de referência
            for (int i = 0; i < pontuacaoJogadores.Length; i++)
            {
                //
                if (maior == pontuacaoJogadores[i] && nomeJogador[i] != aux)
                {
                    vencedor = vencedor + " | " + nomeJogador[i];
                    empate = true;
                }
            }
            //Mostrar o vencedor ou o empate
            if (empate)
            {
                Console.WriteLine("\nOcorreu um empate entre os jogadores " + vencedor);
            }
            else
            {
                Console.WriteLine("\nVENCEDOR:  " + vencedor);

            }
            Console.WriteLine("\nPONTUAÇÕES GERAIS ");
            //For para mostrar a pontuação dos jogadores
            for (int i = 0; i < pontuacaoJogadores.Length; i++)
            {
                Console.WriteLine("--------------------------\n" + nomeJogador[i] + "\t" + pontuacaoJogadores[i] + " Peixes \n");
            }
        }
        
    }
}

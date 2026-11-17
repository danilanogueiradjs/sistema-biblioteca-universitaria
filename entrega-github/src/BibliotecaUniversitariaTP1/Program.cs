using System;

// TP1 - Esboço Inicial (Paradigma Procedural - Sem Orientação a Objetos)
// Simulação das regras centrais: Validação de Empréstimo, Cálculo de Multa e Elegibilidade de Reserva.

int opcaoMenu = 0;

// Variáveis primitivas simulando o estado atual do sistema
int emprestimosAtivosMembro = 2;
const int LIMITE_EMPRESTIMOS = 3;
const int PRAZO_PADRAO_DIAS = 14;
const double VALOR_MULTA_DIA = 2.50;

do
{
    Console.WriteLine("\n==================================================");
    Console.WriteLine("   SISTEMA DE BIBLIOTECA UNIVERSITÁRIA (TP1)      ");
    Console.WriteLine("==================================================");
    Console.WriteLine("1. Simular Novo Empréstimo");
    Console.WriteLine("2. Simular Devolução e Cálculo de Multa");
    Console.WriteLine("3. Verificar Elegibilidade de Reserva");
    Console.WriteLine("4. Sair");
    Console.Write("Escolha uma opção: ");

    if (!int.TryParse(Console.ReadLine(), out opcaoMenu))
    {
        Console.WriteLine("Entrada inválida! Digite um número de 1 a 4.");
        continue;
    }

    switch (opcaoMenu)
    {
        case 1:
            Console.WriteLine("\n--- SIMULAÇÃO DE EMPRÉSTIMO ---");
            Console.WriteLine($"Empréstimos ativos atuais do membro: {emprestimosAtivosMembro}/{LIMITE_EMPRESTIMOS}");
            Console.Write("O exemplar físico está disponível na estante? (S/N): ");
            string respDisponivel = Console.ReadLine()?.Trim().ToUpper() ?? "N";
            bool exemplarDisponivel = (respDisponivel == "S");

            // Regra de negócio: máximo 3 empréstimos ativos e exemplar disponível
            if (emprestimosAtivosMembro < LIMITE_EMPRESTIMOS && exemplarDisponivel)
            {
                emprestimosAtivosMembro++;
                Console.WriteLine("Sucesso: Empréstimo registrado!");
                Console.WriteLine($"Prazo de devolução: {PRAZO_PADRAO_DIAS} dias corridos.");
                Console.WriteLine($"Novo total de empréstimos ativos: {emprestimosAtivosMembro}/{LIMITE_EMPRESTIMOS}");
            }
            else if (emprestimosAtivosMembro >= LIMITE_EMPRESTIMOS)
            {
                Console.WriteLine("Bloqueado: O membro já atingiu o limite máximo de 3 empréstimos simultâneos.");
            }
            else
            {
                Console.WriteLine("Bloqueado: O exemplar selecionado não está disponível no momento.");
            }
            break;

        case 2:
            Console.WriteLine("\n--- SIMULAÇÃO DE DEVOLUÇÃO E MULTA ---");
            Console.Write("Informe quantos dias corridos o livro ficou com o membro: ");
            
            if (int.TryParse(Console.ReadLine(), out int diasComLivro) && diasComLivro >= 0)
            {
                if (diasComLivro > PRAZO_PADRAO_DIAS)
                {
                    int diasAtraso = diasComLivro - PRAZO_PADRAO_DIAS;
                    double valorMulta = diasAtraso * VALOR_MULTA_DIA;
                    Console.WriteLine($"Status: Devolução com ATRASO de {diasAtraso} dia(s).");
                    Console.WriteLine($"Multa aplicada: R$ {valorMulta:F2} (Tarifa de R$ {VALOR_MULTA_DIA:F2}/dia).");
                }
                else
                {
                    Console.WriteLine("Status: Devolução dentro do prazo. Nenhuma multa aplicada.");
                }

                if (emprestimosAtivosMembro > 0)
                {
                    emprestimosAtivosMembro--;
                }
            }
            else
            {
                Console.WriteLine("Erro: Quantidade de dias inválida.");
            }
            break;

        case 3:
            Console.WriteLine("\n--- VERIFICAÇÃO DE RESERVA ---");
            Console.Write("Informe a quantidade de exemplares DISPONÍVEIS deste título na biblioteca: ");

            if (int.TryParse(Console.ReadLine(), out int exemplaresDisponiveis) && exemplaresDisponiveis >= 0)
            {
                // Regra de negócio: só pode reservar se todos os exemplares estiverem emprestados (0 disponíveis)
                if (exemplaresDisponiveis == 0)
                {
                    Console.WriteLine("Reserva PERMITIDA: Todos os exemplares estão emprestados no momento.");
                }
                else
                {
                    Console.WriteLine($"Reserva NEGADA: Existem {exemplaresDisponiveis} exemplar(es) disponível(is) para retirada imediata.");
                }
            }
            else
            {
                Console.WriteLine("Erro: Número de exemplares inválido.");
            }
            break;

        case 4:
            Console.WriteLine("Encerrando o protótipo do sistema...");
            break;

        default:
            Console.WriteLine("Opção inexistente. Escolha entre 1 e 4.");
            break;
    }

} while (opcaoMenu != 4);

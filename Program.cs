using System.Text;
using DesafioProjetoHospedagem.Models;

Console.OutputEncoding = Encoding.UTF8;

List<Suite> suites = new List<Suite>();

Console.WriteLine("Seja bem vindo ao Sistema de Reservas!");

bool exibirMenu = true;

// Realiza o loop do menu
while (exibirMenu)
{
    Console.Clear();
    Console.WriteLine("Digite a sua opção:");
    Console.WriteLine("1 - Listar suítes disponíveis");
    Console.WriteLine("2 - Cadastrar Nova Suíte");
    Console.WriteLine("3 - Cadastrar Nova Reserva");
    Console.WriteLine("4 - Encerrar");

    switch (Console.ReadLine())
    {
        case "1":
            ListarSuites();
            break;

        case "2":
            CadastrarNovaSuite();
            break;

        case "3":
             CadastrarReserva();
            break;

        case "4":
            exibirMenu = false;
            break;

        default:
            Console.WriteLine("Opção inválida");
            break;
    }

    Console.WriteLine("Pressione uma tecla para continuar");
    Console.ReadLine();
}


void ListarSuites()
{
    if(suites.Count > 0)
    {
        foreach(var item in suites)
        {
            Console.WriteLine($" Código: {item.Codigo} - Suite: {item.TipoSuite} - Capacidade: {item.Capacidade} - Valor: {item.ValorDiaria}");    
        }
    }
    else
    {
        Console.WriteLine("Não há suites cadastradas.");
    }
}

void CadastrarNovaSuite()
{   
    string tipoSuite = string.Empty;
    int capacidade = 0;
    decimal valorDiaria = 0M;

    Console.WriteLine("Informe no nome da Suite:");
    tipoSuite = Console.ReadLine();
    Console.WriteLine("Qual é a capacidade máxima:");
    bool capacidadeValida = int.TryParse(Console.ReadLine(), out capacidade);
    if(!capacidadeValida) 
    {
        Console.WriteLine("Capacidade informada é inválida... Comece novamente.");
        return;
    }
    Console.WriteLine("Qual é o valor da diária:");
    bool valorDiariaValida = decimal.TryParse(Console.ReadLine(), out valorDiaria);
    if(!valorDiariaValida) 
    {
        Console.WriteLine("Valor da diária informado é inválido... Comece novamente.");
        return;
    }
    int codigo = suites.Count + 1;
    Suite novaSuite = new Suite(codigo, tipoSuite, capacidade, valorDiaria);
    suites.Add(novaSuite);
    Console.WriteLine("Suite cadastrada com sucesso.");
}

void CadastrarReserva()
{
    int quantidadeHospedes = 0; 
    Console.WriteLine("Informe a quantidade de hóspedes para essa reserva:");
    bool valorQuantidadeValida = int.TryParse(Console.ReadLine(), out quantidadeHospedes);
    if(!valorQuantidadeValida) 
    {
        Console.WriteLine("Quantidade informada é inválida... Comece novamente.");
        return;
    }
    // Cria os modelos de hóspedes e cadastra na lista de hóspedes
    List<Pessoa> hospedes = new List<Pessoa>();
    for(int i = 0; i < quantidadeHospedes; i++)
    {
        Console.WriteLine($"Nome do Hospede {i+1}:");
        hospedes.Add(new Pessoa(Console.ReadLine()));
    }
    // Escolha da Suite
    Console.WriteLine($"Lista das suítes cadastradas: ");
    ListarSuites();
    Console.WriteLine($"Informe o código da suíte escolhida: ");
    int codigo = 0; 
    bool codigoSuiteValida = int.TryParse(Console.ReadLine(), out codigo);
    if(!codigoSuiteValida) 
    {
        Console.WriteLine("Código de suíte informado é inválido... Comece novamente.");
        return;
    }
    // Selecionar a Suite escolhida
    Suite suiteSelecionada = suites.Find(x=> x.Codigo.Equals(codigo));    
    if(suiteSelecionada == null)
    {
        Console.WriteLine("Código de suíte informado não encontrado... Comece novamente.");
        return;
    }

    //Quantidade de dias da Reserva
    Console.WriteLine("Quantos dias deseja reservar:");
    int diasReservados = 0; 
    bool diasReservadosValida = int.TryParse(Console.ReadLine(), out diasReservados);
    if(!codigoSuiteValida) 
    {
        Console.WriteLine("Quantidade de dias informado é inválido... Comece novamente.");
        return;
    }

    // Cria uma nova reserva, passando a suíte e os hóspedes
    Reserva reserva = new Reserva(diasReservados: diasReservados);
    reserva.CadastrarSuite(suiteSelecionada);
    reserva.CadastrarHospedes(hospedes);

    // Exibe a quantidade de hóspedes e o valor da diária
    Console.WriteLine($"Hóspedes: {reserva.ObterQuantidadeHospedes()}");
    Console.WriteLine($"Valor diária: {reserva.CalcularValorDiaria().ToString("C")}");
}
/*


Pessoa p1 = new Pessoa(nome: "Hóspede 1");
Pessoa p2 = new Pessoa(nome: "Hóspede 2");

hospedes.Add(p1);
hospedes.Add(p2);

// Cria a suíte
Suite suite = new Suite(tipoSuite: "Premium", capacidade: 2, valorDiaria: 30);

// Cria uma nova reserva, passando a suíte e os hóspedes
Reserva reserva = new Reserva(diasReservados: 5);
reserva.CadastrarSuite(suite);
reserva.CadastrarHospedes(hospedes);

// Exibe a quantidade de hóspedes e o valor da diária
Console.WriteLine($"Hóspedes: {reserva.ObterQuantidadeHospedes()}");
Console.WriteLine($"Valor diária: {reserva.CalcularValorDiaria()}");
*/
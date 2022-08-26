//Utilização dps Namespaces
using VoeAirlinesSenai.Contexts;
using VoeAirlinesSenai.Entities;
using VoeAirlinesSenai.ViewModels;
//Definição do namespace
namespace VoeAirlinesSenai.Services;
//classe de serviço - Trabalhar com funcionalidade do sistema ! ( CRUD DE INSERT DA AERONAVE )
public class AeronaveService
{    //RF - REQYUSUTI FUNCIONAL
    //RF - NÃO FUNCIONAIS
    //NESSE MOMENTO VOCÊ VAI TRABALAHAR COM REQUISITO FUNCIONAIS.
    //==================================================================
    //EX : 
    //O sistema deve cadastrar a Aeronave ! 
    //nesse ponto do código veremos na pratica injeção de Dependência 
    private readonly VoeAirlinesContext _context;
    //Gerar construtor abaixo !  CTL+.
    //ter o mesmo nome da classe
    //Ele não tem retorno
    //Atribuir no momento da Instânciação
    //Ex: Animal a = new Animal ("Cobra,"som.mp3");
    // Ex : Aeronave a1 = new Aeronave ("A5","Boing");
    //Tem o comando Public -> Acesso Global 
    //AeronaveService -> Nome do Costrutor ( mesmo da classe )
    //(VoeAirlineContext contex)- Parâmetro
    //_Context = context " Usamos _ para Diferenciar o Parâmetro
    // O Sinal de "=" é Atribuição 
    //=======================================================================
    //Na Linha de baixo ao ser injetado será chamado o construtor e receberá a atribuição do contexto    
    public AeronaveService(VoeAirlinesContext context)
    {
        _context = context;
    }
    //Criamos na ViewModels várias classes    
    // A ESTRATÉGIA E :
    // ao Adicionar a Aeronave no Banco será gerando o Id
    // E Assim retornaremos todos os dados da Aeronave Incluindo seu Id
    public DetalhesAeronaveViewModel AdicionarAeronave(AdicionarAeronaveViewModel dados)
    { //vamos criar o Objeto Aeronave
        var aeronave = new Aeronave(dados.Fabricante, dados.Modelo, dados.Codigo);
        //EntityFramework Core - ORM
        _context.Add(aeronave);
        _context.SaveChanges(); // -> SALVA O OBJETO  NO BANCO "O CONTEXTO" !

        return new DetalhesAeronaveViewModel
        (
            aeronave.Id,
            aeronave.Fabricante,
            aeronave.Modelo,
            aeronave.Codigo
        );

    }
    //LISTAR AERONAVES
    public IEnumerable<ListarAeronvaveViewModel> ListarAeronaves()
    {
        return _context.Aeronaves.Select(a => new ListarAeronvaveViewModel(a.Id, a.Modelo, a.Codigo));
    }
    // Buscar pelo ID AERONAVES
    public DetalhesAeronaveViewModel? ListarAeronavePeloId(int id)
    {
        var aeronave = _context.Aeronaves.Find(id);
        if (aeronave != null)
        {
            return new DetalhesAeronaveViewModel(
                aeronave.Id,
                aeronave.Fabricante,
                aeronave.Modelo,
                aeronave.Codigo
            );

        }
        return null;
    }
    // Atualizar Aeronave
    public DetalhesAeronaveViewModel? AtualizarAeronave(AtualizarAeronaveViewModel dados)
    {
        var aeronave = _context.Aeronaves.Find(dados.Id);
        if(aeronave!= null){
            aeronave.Fabricante = dados.Fabricante;
            aeronave.Modelo = dados.Modelo;
            aeronave.Codigo = dados.Codigo;
            _context.Update(aeronave);
            _context.SaveChanges();
            return new DetalhesAeronaveViewModel(aeronave.Id,aeronave.Fabricante,aeronave.Modelo,aeronave.Codigo);
        }
        return null;
    }
    //Deletar AERONAVE !
    public void ExcluirAeronave(int id)
    {
        var aeronave = _context.Aeronaves.Find(id);
        if (aeronave != null)
        {
            _context.Remove(aeronave);
            _context.SaveChanges();
        }
    }

}
using GeradorDeTestes.Dominio.Compartilhado;

namespace GeradorDeTestes.Dominio.ModuloQuestao;

public class Alternativa : EntidadeBase<Alternativa>
{
    public char Letra { get; set; }
    public string Resposta { get; set; }
    public bool Correta { get; set; }
    public Questao Questao { get; set; }

    public Alternativa() { }

    public Alternativa(char letra, string resposta, bool correta, Questao questao) : this()
    {
        Letra = letra;
        Resposta = resposta;
        Questao = questao;
        Correta = correta;
    }


    public override void AtualizarRegistro(Alternativa registroEditado)
    {
        throw new NotImplementedException();
    }
}

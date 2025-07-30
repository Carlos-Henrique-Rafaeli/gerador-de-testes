using GeradorDeTestes.Dominio.ModuloTeste;
using QuestPDF.Fluent;

namespace GeradorDeTestes.Infraestrutura.Pdf;

public class GeradorTesteEmPdf : IGeradorTeste
{
    public byte[] GerarNovoTeste(Teste teste, bool gabarito)
    {
        var documento = new TesteDocument(teste, gabarito);

        var pdfBytes = documento.GeneratePdf();

        return pdfBytes;
    }
}

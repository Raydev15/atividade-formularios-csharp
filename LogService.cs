using System.Text;

namespace AtividadeFormularios;

public static class LogService
{
    public static string CaminhoLog => Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
        "AtividadeFormularios",
        "LOG.txt");

    public static void Salvar(DadosCadastro dados)
    {
        string? pasta = Path.GetDirectoryName(CaminhoLog);
        if (!string.IsNullOrWhiteSpace(pasta)) Directory.CreateDirectory(pasta);

        using var writer = new StreamWriter(CaminhoLog, append: true, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));
        writer.WriteLine("============================================================");
        writer.WriteLine($"Data/hora: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
        writer.WriteLine($"Nome: {Limpar(dados.Nome)}");
        writer.WriteLine($"RA: {Limpar(dados.Ra)}");
        writer.WriteLine($"E-mail: {Limpar(dados.Email)}");
        writer.WriteLine($"Endereço: {Limpar(dados.Endereco)}");
        writer.WriteLine($"Telefone: {Limpar(dados.Telefone)}");
    }

    private static string Limpar(string valor) => valor.Replace("\r", " ").Replace("\n", " ").Trim();
}

namespace AtividadeFormularios;

public sealed class FormularioDadosPessoais : Form
{
    private readonly DadosCadastro _dados;
    private readonly TextBox _txtEmail = new();
    private readonly TextBox _txtEndereco = new();
    private readonly TextBox _txtTelefone = new();

    public FormularioDadosPessoais(DadosCadastro dados)
    {
        _dados = dados;
        Text = "Formulário 2 — Dados Pessoais";
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        ClientSize = new Size(560, 470);
        Font = new Font("Segoe UI", 10F);
        BackColor = Color.WhiteSmoke;

        var painel = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(28),
            ColumnCount = 1,
            RowCount = 8,
            BackColor = Color.WhiteSmoke
        };
        painel.RowStyles.Add(new RowStyle(SizeType.Absolute, 46));
        for (int i = 0; i < 6; i++) painel.RowStyles.Add(new RowStyle(SizeType.Absolute, i % 2 == 0 ? 30 : 38));
        painel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        Controls.Add(painel);

        painel.Controls.Add(new Label { Text = "Formulário 2 — Dados Pessoais", Font = new Font("Segoe UI", 16F, FontStyle.Bold), Dock = DockStyle.Fill, ForeColor = Color.FromArgb(28, 74, 130) }, 0, 0);
        AdicionarCampo(painel, "E-mail:", _txtEmail, 1, _dados.Email);
        AdicionarCampo(painel, "Endereço:", _txtEndereco, 3, _dados.Endereco);
        AdicionarCampo(painel, "Telefone:", _txtTelefone, 5, _dados.Telefone);

        var botoes = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft, Padding = new Padding(0, 16, 0, 0) };
        var btnSalvar = new Button { Text = "Salvar", Width = 120, Height = 40, BackColor = Color.FromArgb(25, 104, 210), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
        btnSalvar.FlatAppearance.BorderSize = 0;
        btnSalvar.Click += (_, _) => Salvar();
        var btnVoltar = new Button { Text = "Voltar", Width = 120, Height = 40, FlatStyle = FlatStyle.Flat };
        btnVoltar.Click += (_, _) =>
        {
            CapturarDados();
            DialogResult = DialogResult.Cancel;
            Close();
        };
        botoes.Controls.Add(btnSalvar);
        botoes.Controls.Add(btnVoltar);
        painel.Controls.Add(botoes, 0, 7);
        AcceptButton = btnSalvar;
    }

    private static void AdicionarCampo(TableLayoutPanel painel, string rotulo, TextBox campo, int linha, string valor)
    {
        painel.Controls.Add(new Label { Text = rotulo, Dock = DockStyle.Fill, TextAlign = ContentAlignment.BottomLeft }, 0, linha);
        campo.Text = valor;
        campo.Dock = DockStyle.Fill;
        campo.BorderStyle = BorderStyle.FixedSingle;
        campo.Font = new Font("Segoe UI", 11F);
        campo.Margin = new Padding(0, 3, 0, 4);
        painel.Controls.Add(campo, 0, linha + 1);
    }

    private void CapturarDados()
    {
        _dados.Email = _txtEmail.Text.Trim();
        _dados.Endereco = _txtEndereco.Text.Trim();
        _dados.Telefone = _txtTelefone.Text.Trim();
    }

    private void Salvar()
    {
        CapturarDados();
        if (_dados.Email.Length == 0 || _dados.Endereco.Length == 0 || _dados.Telefone.Length == 0)
        {
            MessageBox.Show("Preencha E-mail, Endereço e Telefone para salvar.", "Campos obrigatórios", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            LogService.Salvar(_dados);
            MessageBox.Show($"Dados salvos com sucesso!\n\nArquivo LOG:\n{LogService.CaminhoLog}", "Salvo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Não foi possível gravar o arquivo LOG.\n{ex.Message}", "Erro ao salvar", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}

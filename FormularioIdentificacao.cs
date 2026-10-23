namespace AtividadeFormularios;

public sealed class FormularioIdentificacao : Form
{
    private readonly DadosCadastro _dados = new();
    private readonly TextBox _txtNome = new();
    private readonly TextBox _txtRa = new();

    public FormularioIdentificacao()
    {
        Text = "Formulário 1 — Identificação";
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = true;
        ClientSize = new Size(520, 350);
        Font = new Font("Segoe UI", 10F);
        BackColor = Color.WhiteSmoke;

        var painel = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(28),
            ColumnCount = 1,
            RowCount = 6,
            BackColor = Color.WhiteSmoke
        };
        painel.RowStyles.Add(new RowStyle(SizeType.Absolute, 46));
        painel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
        painel.RowStyles.Add(new RowStyle(SizeType.Absolute, 38));
        painel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
        painel.RowStyles.Add(new RowStyle(SizeType.Absolute, 38));
        painel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        Controls.Add(painel);

        painel.Controls.Add(new Label { Text = "Formulário 1 — Identificação", Font = new Font("Segoe UI", 16F, FontStyle.Bold), Dock = DockStyle.Fill, ForeColor = Color.FromArgb(28, 74, 130) }, 0, 0);
        painel.Controls.Add(new Label { Text = "Nome:", Dock = DockStyle.Fill, TextAlign = ContentAlignment.BottomLeft }, 0, 1);
        PrepararCampo(_txtNome);
        painel.Controls.Add(_txtNome, 0, 2);
        painel.Controls.Add(new Label { Text = "RA:", Dock = DockStyle.Fill, TextAlign = ContentAlignment.BottomLeft }, 0, 3);
        PrepararCampo(_txtRa);
        painel.Controls.Add(_txtRa, 0, 4);

        var botoes = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft, Padding = new Padding(0, 16, 0, 0) };
        var btnProximo = new Button { Text = "Prossiga", Width = 130, Height = 40, BackColor = Color.FromArgb(25, 104, 210), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
        btnProximo.FlatAppearance.BorderSize = 0;
        btnProximo.Click += (_, _) => AbrirProximo();
        botoes.Controls.Add(btnProximo);
        painel.Controls.Add(botoes, 0, 5);
        AcceptButton = btnProximo;
    }

    private static void PrepararCampo(TextBox campo)
    {
        campo.Dock = DockStyle.Fill;
        campo.BorderStyle = BorderStyle.FixedSingle;
        campo.Font = new Font("Segoe UI", 11F);
        campo.Margin = new Padding(0, 3, 0, 4);
    }

    private void AbrirProximo()
    {
        _dados.Nome = _txtNome.Text.Trim();
        _dados.Ra = _txtRa.Text.Trim();
        if (_dados.Nome.Length == 0 || _dados.Ra.Length == 0)
        {
            MessageBox.Show("Preencha Nome e RA para continuar.", "Campos obrigatórios", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        using var proximo = new FormularioDadosPessoais(_dados);
        Hide();
        proximo.ShowDialog(this);
        _txtNome.Text = _dados.Nome;
        _txtRa.Text = _dados.Ra;
        Show();
        Activate();
    }
}

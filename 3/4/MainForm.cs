using System.ComponentModel;
using System.Text.Json;

namespace FinanceTrackerApp;

public class MainForm : Form
{
    private readonly string[] incomeCategories = { "Зарплата", "Стипендія", "Подарунок", "Інше" };
    private readonly string[] expenseCategories = { "Їжа", "Транспорт", "Розваги", "Комунальні", "Навчання", "Інше" };

    private readonly BindingList<Transaction> transactions = new();
    private BindingSource bindingSource = new();

    private DateTimePicker dtpDate = new();
    private ComboBox cmbType = new();
    private ComboBox cmbCategory = new();
    private TextBox txtAmount = new();
    private TextBox txtNote = new();
    private Button btnAdd = new();
    private Button btnRemove = new();

    private ComboBox cmbFilterCategory = new();
    private Label lblBalance = new();

    private Button btnSave = new();
    private Button btnLoad = new();

    private DataGridView gridTransactions = new();

    public MainForm()
    {
        Text = "Облік доходів і витрат";
        Width = 800;
        Height = 620;
        StartPosition = FormStartPosition.CenterScreen;

        var lblDate = new Label { Text = "Дата:", Location = new Point(20, 20), AutoSize = true };
        dtpDate.Location = new Point(120, 17);
        dtpDate.Width = 150;
        dtpDate.Format = DateTimePickerFormat.Short;

        var lblType = new Label { Text = "Тип:", Location = new Point(300, 20), AutoSize = true };
        cmbType.Location = new Point(400, 17);
        cmbType.Width = 150;
        cmbType.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbType.Items.AddRange(new object[] { "Дохід", "Витрата" });
        cmbType.SelectedIndex = 0;
        cmbType.SelectedIndexChanged += CmbType_SelectedIndexChanged;

        var lblCategory = new Label { Text = "Категорія:", Location = new Point(20, 55), AutoSize = true };
        cmbCategory.Location = new Point(120, 52);
        cmbCategory.Width = 200;
        cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;

        var lblAmount = new Label { Text = "Сума (грн):", Location = new Point(340, 55), AutoSize = true };
        txtAmount.Location = new Point(440, 52);
        txtAmount.Width = 110;

        var lblNote = new Label { Text = "Примітка:", Location = new Point(20, 90), AutoSize = true };
        txtNote.Location = new Point(120, 87);
        txtNote.Width = 430;

        btnAdd.Text = "Додати";
        btnAdd.Location = new Point(570, 52);
        btnAdd.Width = 100;
        btnAdd.Height = 28;
        btnAdd.Click += BtnAdd_Click;

        btnRemove.Text = "Видалити вибране";
        btnRemove.Location = new Point(570, 87);
        btnRemove.Width = 150;
        btnRemove.Height = 28;
        btnRemove.Click += BtnRemove_Click;

        var lblFilter = new Label { Text = "Фільтр за категорією:", Location = new Point(20, 130), AutoSize = true };
        cmbFilterCategory.Location = new Point(180, 127);
        cmbFilterCategory.Width = 200;
        cmbFilterCategory.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbFilterCategory.SelectedIndexChanged += CmbFilterCategory_SelectedIndexChanged;

        btnSave.Text = "Зберегти у файл";
        btnSave.Location = new Point(500, 125);
        btnSave.Width = 130;
        btnSave.Height = 28;
        btnSave.Click += BtnSave_Click;

        btnLoad.Text = "Завантажити з файлу";
        btnLoad.Location = new Point(640, 125);
        btnLoad.Width = 140;
        btnLoad.Height = 28;
        btnLoad.Click += BtnLoad_Click;

        lblBalance.Location = new Point(20, 165);
        lblBalance.AutoSize = true;
        lblBalance.Font = new Font(Font.FontFamily, 12, FontStyle.Bold);

        gridTransactions.Location = new Point(20, 200);
        gridTransactions.Width = 745;
        gridTransactions.Height = 370;
        gridTransactions.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        gridTransactions.ReadOnly = true;
        gridTransactions.AllowUserToAddRows = false;
        gridTransactions.AllowUserToDeleteRows = false;
        gridTransactions.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        gridTransactions.MultiSelect = false;
        gridTransactions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        gridTransactions.AutoGenerateColumns = false;
        gridTransactions.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Date", HeaderText = "Дата", DefaultCellStyle = new DataGridViewCellStyle { Format = "dd.MM.yyyy" } });
        gridTransactions.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Type", HeaderText = "Тип" });
        gridTransactions.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Category", HeaderText = "Категорія" });
        gridTransactions.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Amount", HeaderText = "Сума", DefaultCellStyle = new DataGridViewCellStyle { Format = "N2" } });
        gridTransactions.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Note", HeaderText = "Примітка" });

        bindingSource.DataSource = transactions;
        gridTransactions.DataSource = bindingSource;

        Controls.AddRange(new Control[]
        {
            lblDate, dtpDate,
            lblType, cmbType,
            lblCategory, cmbCategory,
            lblAmount, txtAmount,
            lblNote, txtNote,
            btnAdd, btnRemove,
            lblFilter, cmbFilterCategory,
            btnSave, btnLoad,
            lblBalance,
            gridTransactions
        });

        UpdateCategoryList();
        UpdateFilterList();
        UpdateBalance();
    }

    private void CmbType_SelectedIndexChanged(object? sender, EventArgs e)
    {
        UpdateCategoryList();
    }

    private void UpdateCategoryList()
    {
        cmbCategory.Items.Clear();
        var source = cmbType.SelectedItem?.ToString() == "Дохід" ? incomeCategories : expenseCategories;
        cmbCategory.Items.AddRange(source);
        if (cmbCategory.Items.Count > 0)
            cmbCategory.SelectedIndex = 0;
    }

    private void UpdateFilterList()
    {
        string? previous = cmbFilterCategory.SelectedItem?.ToString();
        cmbFilterCategory.Items.Clear();
        cmbFilterCategory.Items.Add("Усі категорії");
        foreach (var category in incomeCategories.Concat(expenseCategories).Distinct())
            cmbFilterCategory.Items.Add(category);

        if (previous != null && cmbFilterCategory.Items.Contains(previous))
            cmbFilterCategory.SelectedItem = previous;
        else
            cmbFilterCategory.SelectedIndex = 0;
    }

    private void CmbFilterCategory_SelectedIndexChanged(object? sender, EventArgs e)
    {
        ApplyFilter();
    }

    private void ApplyFilter()
    {
        string? selected = cmbFilterCategory.SelectedItem?.ToString();

        if (string.IsNullOrEmpty(selected) || selected == "Усі категорії")
        {
            bindingSource.DataSource = transactions;
        }
        else
        {
            var filtered = new BindingList<Transaction>(
                transactions.Where(t => t.Category == selected).ToList());
            bindingSource.DataSource = filtered;
        }

        gridTransactions.DataSource = bindingSource;
    }

    private void BtnAdd_Click(object? sender, EventArgs e)
    {
        if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
        {
            MessageBox.Show("Введіть коректну суму", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (cmbCategory.SelectedItem is null)
        {
            MessageBox.Show("Виберіть категорію", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        transactions.Add(new Transaction
        {
            Date = dtpDate.Value,
            Type = cmbType.SelectedItem!.ToString()!,
            Category = cmbCategory.SelectedItem!.ToString()!,
            Amount = amount,
            Note = txtNote.Text
        });

        txtAmount.Clear();
        txtNote.Clear();

        UpdateFilterList();
        ApplyFilter();
        UpdateBalance();
    }

    private void BtnRemove_Click(object? sender, EventArgs e)
    {
        if (gridTransactions.CurrentRow?.DataBoundItem is Transaction selected)
        {
            transactions.Remove(selected);
            ApplyFilter();
            UpdateBalance();
        }
    }

    private void UpdateBalance()
    {
        decimal income = transactions.Where(t => t.Type == "Дохід").Sum(t => t.Amount);
        decimal expense = transactions.Where(t => t.Type == "Витрата").Sum(t => t.Amount);
        decimal balance = income - expense;

        lblBalance.Text = $"Баланс: {balance:N2} грн   (Доходи: {income:N2} грн, Витрати: {expense:N2} грн)";
        lblBalance.ForeColor = balance >= 0 ? Color.DarkGreen : Color.DarkRed;
    }

    private void BtnSave_Click(object? sender, EventArgs e)
    {
        using var dialog = new SaveFileDialog { Filter = "JSON файл (*.json)|*.json", FileName = "transactions.json" };
        if (dialog.ShowDialog() != DialogResult.OK)
            return;

        var json = JsonSerializer.Serialize(transactions, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(dialog.FileName, json);
        MessageBox.Show("Дані збережено", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void BtnLoad_Click(object? sender, EventArgs e)
    {
        using var dialog = new OpenFileDialog { Filter = "JSON файл (*.json)|*.json" };
        if (dialog.ShowDialog() != DialogResult.OK)
            return;

        var json = File.ReadAllText(dialog.FileName);
        var loaded = JsonSerializer.Deserialize<List<Transaction>>(json);

        if (loaded is null)
            return;

        transactions.Clear();
        foreach (var t in loaded)
            transactions.Add(t);

        UpdateFilterList();
        ApplyFilter();
        UpdateBalance();
    }
}

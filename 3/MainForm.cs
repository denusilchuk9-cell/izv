namespace DailyExpenseApp;

public class MainForm : Form
{
    private readonly (string Name, decimal Price)[] referenceItems =
    {
        ("Чашка кави в кав'ярні", 70m),
        ("Пачка сигарет", 90m),
        ("Поїздка на таксі", 120m),
        ("Смартфон середнього класу", 15000m),
        ("Флагманський смартфон", 45000m),
        ("Ноутбук", 25000m),
        ("Відпустка на морі (тиждень)", 30000m),
        ("Велосипед", 12000m),
        ("Ігрова консоль", 18000m)
    };

    private ComboBox cmbCategory = new();
    private TextBox txtDailyAmount = new();
    private Button btnCalculate = new();
    private Label lblMonth = new();
    private Label lblYear = new();
    private Label lblFiveYears = new();
    private DataGridView gridComparison = new();

    public MainForm()
    {
        Text = "Калькулятор щоденних витрат";
        Width = 650;
        Height = 550;
        StartPosition = FormStartPosition.CenterScreen;

        var lblCategory = new Label { Text = "Категорія витрат:", Location = new Point(20, 20), AutoSize = true };
        cmbCategory.Location = new Point(180, 17);
        cmbCategory.Width = 200;
        cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbCategory.Items.AddRange(new object[] { "Кава", "Сигарети", "Таксі", "Інше" });
        cmbCategory.SelectedIndex = 0;

        var lblDailyAmount = new Label { Text = "Сума за день (грн):", Location = new Point(20, 55), AutoSize = true };
        txtDailyAmount.Location = new Point(180, 52);
        txtDailyAmount.Width = 200;

        btnCalculate.Text = "Розрахувати";
        btnCalculate.Location = new Point(180, 90);
        btnCalculate.Width = 200;
        btnCalculate.Height = 32;
        btnCalculate.Click += BtnCalculate_Click;

        lblMonth.Location = new Point(20, 140);
        lblMonth.AutoSize = true;
        lblMonth.Font = new Font(Font, FontStyle.Bold);

        lblYear.Location = new Point(20, 165);
        lblYear.AutoSize = true;
        lblYear.Font = new Font(Font, FontStyle.Bold);

        lblFiveYears.Location = new Point(20, 190);
        lblFiveYears.AutoSize = true;
        lblFiveYears.Font = new Font(Font, FontStyle.Bold);

        var lblComparisonTitle = new Label
        {
            Text = "За ці гроші за 5 років можна купити:",
            Location = new Point(20, 225),
            AutoSize = true
        };

        gridComparison.Location = new Point(20, 250);
        gridComparison.Width = 590;
        gridComparison.Height = 260;
        gridComparison.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        gridComparison.ReadOnly = true;
        gridComparison.AllowUserToAddRows = false;
        gridComparison.AllowUserToDeleteRows = false;
        gridComparison.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        gridComparison.Columns.Add("Item", "Річ");
        gridComparison.Columns.Add("Price", "Ціна (грн)");
        gridComparison.Columns.Add("Count", "Скільки штук можна купити");

        Controls.AddRange(new Control[]
        {
            lblCategory, cmbCategory,
            lblDailyAmount, txtDailyAmount,
            btnCalculate,
            lblMonth, lblYear, lblFiveYears,
            lblComparisonTitle, gridComparison
        });
    }

    private void BtnCalculate_Click(object? sender, EventArgs e)
    {
        if (!decimal.TryParse(txtDailyAmount.Text, out decimal dailyAmount) || dailyAmount <= 0)
        {
            MessageBox.Show("Введіть коректну суму витрат за день", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        decimal monthTotal = dailyAmount * 30;
        decimal yearTotal = dailyAmount * 365;
        decimal fiveYearsTotal = yearTotal * 5;

        string category = cmbCategory.SelectedItem?.ToString() ?? "Витрата";

        lblMonth.Text = $"{category} за місяць: {monthTotal:N2} грн";
        lblYear.Text = $"{category} за рік: {yearTotal:N2} грн";
        lblFiveYears.Text = $"{category} за 5 років: {fiveYearsTotal:N2} грн";

        gridComparison.Rows.Clear();
        foreach (var item in referenceItems)
        {
            decimal count = fiveYearsTotal / item.Price;
            gridComparison.Rows.Add(item.Name, item.Price.ToString("N0"), count.ToString("N1"));
        }
    }
}

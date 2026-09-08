namespace LoanCalculatorApp;

public class MainForm : Form
{
    private TextBox txtAmount = new();
    private TextBox txtRate = new();
    private NumericUpDown numTermMonths = new();
    private Button btnCalculate = new();
    private Label lblMonthlyPayment = new();
    private Label lblOverpayment = new();
    private DataGridView gridSchedule = new();

    public MainForm()
    {
        Text = "Кредитний калькулятор";
        Width = 700;
        Height = 600;
        StartPosition = FormStartPosition.CenterScreen;

        var lblAmount = new Label { Text = "Сума кредиту (грн):", Location = new Point(20, 20), AutoSize = true };
        txtAmount.Location = new Point(180, 17);
        txtAmount.Width = 150;

        var lblRate = new Label { Text = "Відсоткова ставка (% річних):", Location = new Point(20, 55), AutoSize = true };
        txtRate.Location = new Point(180, 52);
        txtRate.Width = 150;

        var lblTerm = new Label { Text = "Термін (місяців):", Location = new Point(20, 90), AutoSize = true };
        numTermMonths.Location = new Point(180, 87);
        numTermMonths.Width = 150;
        numTermMonths.Minimum = 1;
        numTermMonths.Maximum = 600;
        numTermMonths.Value = 12;

        btnCalculate.Text = "Розрахувати";
        btnCalculate.Location = new Point(180, 125);
        btnCalculate.Width = 150;
        btnCalculate.Height = 32;
        btnCalculate.Click += BtnCalculate_Click;

        lblMonthlyPayment.Location = new Point(360, 20);
        lblMonthlyPayment.AutoSize = true;
        lblMonthlyPayment.Font = new Font(Font, FontStyle.Bold);

        lblOverpayment.Location = new Point(360, 45);
        lblOverpayment.AutoSize = true;

        gridSchedule.Location = new Point(20, 175);
        gridSchedule.Width = 640;
        gridSchedule.Height = 380;
        gridSchedule.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        gridSchedule.ReadOnly = true;
        gridSchedule.AllowUserToAddRows = false;
        gridSchedule.AllowUserToDeleteRows = false;
        gridSchedule.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        gridSchedule.Columns.Add("Month", "Місяць");
        gridSchedule.Columns.Add("Payment", "Платіж");
        gridSchedule.Columns.Add("Interest", "Відсотки");
        gridSchedule.Columns.Add("Principal", "Тіло кредиту");
        gridSchedule.Columns.Add("Balance", "Залишок боргу");

        Controls.AddRange(new Control[]
        {
            lblAmount, txtAmount,
            lblRate, txtRate,
            lblTerm, numTermMonths,
            btnCalculate,
            lblMonthlyPayment, lblOverpayment,
            gridSchedule
        });
    }

    private void BtnCalculate_Click(object? sender, EventArgs e)
    {
        if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
        {
            MessageBox.Show("Введіть коректну суму кредиту", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (!decimal.TryParse(txtRate.Text, out decimal annualRate) || annualRate <= 0)
        {
            MessageBox.Show("Введіть коректну відсоткову ставку", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        int termMonths = (int)numTermMonths.Value;
        decimal monthlyRate = annualRate / 100m / 12m;

        double r = (double)monthlyRate;
        double p = (double)amount;
        double factor = Math.Pow(1 + r, termMonths);
        decimal monthlyPayment = (decimal)(p * r * factor / (factor - 1));

        gridSchedule.Rows.Clear();
        decimal balance = amount;
        decimal totalPaid = 0;

        for (int month = 1; month <= termMonths; month++)
        {
            decimal interestPart = balance * monthlyRate;
            decimal principalPart = monthlyPayment - interestPart;
            decimal currentPayment = monthlyPayment;

            if (month == termMonths || principalPart > balance)
            {
                principalPart = balance;
                currentPayment = principalPart + interestPart;
            }

            balance -= principalPart;
            if (balance < 0) balance = 0;
            totalPaid += currentPayment;

            gridSchedule.Rows.Add(
                month,
                currentPayment.ToString("N2"),
                interestPart.ToString("N2"),
                principalPart.ToString("N2"),
                balance.ToString("N2"));
        }

        decimal overpayment = totalPaid - amount;

        lblMonthlyPayment.Text = $"Щомісячний платіж: {monthlyPayment:N2} грн";
        lblOverpayment.Text = $"Переплата за весь термін: {overpayment:N2} грн";
    }
}

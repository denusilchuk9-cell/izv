namespace TariffAdvisor
{
    // ===== "Зашиті" в код тарифи =====
    internal class Tariff
    {
        public string Name = "";
        public decimal BasePrice;      // абонплата, грн
        public decimal IncludedGb;     // включено гігабайт
        public decimal IncludedMin;    // включено хвилин
        public decimal IncludedSms;    // включено SMS
        public decimal OverGbPrice;    // ціна за 1 ГБ понад ліміт
        public decimal OverMinPrice;   // ціна за 1 хв понад ліміт
        public decimal OverSmsPrice;   // ціна за 1 SMS понад ліміт

        public decimal CalculateCost(decimal gb, decimal minutes, decimal sms)
        {
            decimal cost = BasePrice;

            if (gb > IncludedGb)
                cost += (gb - IncludedGb) * OverGbPrice;

            if (minutes > IncludedMin)
                cost += (minutes - IncludedMin) * OverMinPrice;

            if (sms > IncludedSms)
                cost += (sms - IncludedSms) * OverSmsPrice;

            return cost;
        }
    }

    public class MainForm : Form
    {
        private NumericUpDown numGb = null!;
        private NumericUpDown numMinutes = null!;
        private NumericUpDown numSms = null!;
        private Button btnCheck = null!;
        private DataGridView dgvTariffs = null!;
        private Label lblRecommendation = null!;

        private readonly List<Tariff> _tariffs = new()
        {
            new Tariff
            {
                Name = "Економ",
                BasePrice = 99,
                IncludedGb = 5,
                IncludedMin = 100,
                IncludedSms = 50,
                OverGbPrice = 10,
                OverMinPrice = 1,
                OverSmsPrice = 0.5m
            },
            new Tariff
            {
                Name = "Стандарт",
                BasePrice = 199,
                IncludedGb = 15,
                IncludedMin = 300,
                IncludedSms = 150,
                OverGbPrice = 7,
                OverMinPrice = 0.7m,
                OverSmsPrice = 0.3m
            },
            new Tariff
            {
                Name = "Преміум",
                BasePrice = 349,
                IncludedGb = 50,
                IncludedMin = 1000,
                IncludedSms = 500,
                OverGbPrice = 5,
                OverMinPrice = 0.5m,
                OverSmsPrice = 0.2m
            },
            new Tariff
            {
                Name = "Безліміт",
                BasePrice = 499,
                IncludedGb = 100000,
                IncludedMin = 100000,
                IncludedSms = 100000,
                OverGbPrice = 0,
                OverMinPrice = 0,
                OverSmsPrice = 0
            }
        };

        public MainForm()
        {
            BuildUi();
        }

        private void BuildUi()
        {
            Text = "Підбір найвигіднішого тарифу";
            ClientSize = new Size(560, 400);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;

            var lblGb = new Label { Text = "Інтернет, ГБ:", Location = new Point(12, 20), Size = new Size(100, 20) };
            numGb = new NumericUpDown
            {
                Location = new Point(120, 18),
                Size = new Size(100, 23),
                Minimum = 0,
                Maximum = 1000,
                DecimalPlaces = 1
            };

            var lblMinutes = new Label { Text = "Хвилини дзвінків:", Location = new Point(12, 55), Size = new Size(100, 20) };
            numMinutes = new NumericUpDown
            {
                Location = new Point(120, 53),
                Size = new Size(100, 23),
                Minimum = 0,
                Maximum = 10000
            };

            var lblSms = new Label { Text = "SMS:", Location = new Point(12, 90), Size = new Size(100, 20) };
            numSms = new NumericUpDown
            {
                Location = new Point(120, 88),
                Size = new Size(100, 23),
                Minimum = 0,
                Maximum = 10000
            };

            btnCheck = new Button
            {
                Text = "Підібрати тариф",
                Location = new Point(12, 125),
                Size = new Size(180, 35)
            };
            btnCheck.Click += BtnCheck_Click;

            dgvTariffs = new DataGridView
            {
                Location = new Point(12, 175),
                Size = new Size(530, 150),
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            dgvTariffs.Columns.Add("Name", "Тариф");
            dgvTariffs.Columns.Add("Base", "Абонплата");
            dgvTariffs.Columns.Add("Cost", "Вартість з урахуванням витрат");

            lblRecommendation = new Label
            {
                Text = "Рекомендація: —",
                Location = new Point(12, 335),
                Size = new Size(530, 40),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold)
            };

            Controls.Add(lblGb);
            Controls.Add(numGb);
            Controls.Add(lblMinutes);
            Controls.Add(numMinutes);
            Controls.Add(lblSms);
            Controls.Add(numSms);
            Controls.Add(btnCheck);
            Controls.Add(dgvTariffs);
            Controls.Add(lblRecommendation);
        }

        private void BtnCheck_Click(object? sender, EventArgs e)
        {
            decimal gb = numGb.Value;
            decimal minutes = numMinutes.Value;
            decimal sms = numSms.Value;

            dgvTariffs.Rows.Clear();

            Tariff? best = null;
            decimal bestCost = decimal.MaxValue;

            foreach (var tariff in _tariffs)
            {
                decimal cost = tariff.CalculateCost(gb, minutes, sms);

                dgvTariffs.Rows.Add(tariff.Name, tariff.BasePrice.ToString("F2"), cost.ToString("F2"));

                // Проста логіка вибору найвигіднішого варіанту через if/else
                if (best == null)
                {
                    best = tariff;
                    bestCost = cost;
                }
                else if (cost < bestCost)
                {
                    best = tariff;
                    bestCost = cost;
                }
            }

            if (best != null)
            {
                lblRecommendation.Text = $"Рекомендація: «{best.Name}» — {bestCost:F2} грн/міс.";
            }
        }
    }

    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}

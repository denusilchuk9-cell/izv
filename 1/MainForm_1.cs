namespace CO2Calculator
{
   
    internal static class Coefficients
    {
        public const decimal CO2PerCarTrip = 2.3m;
        public const decimal CO2PerApplianceHour = 0.5m;
    }

    public class MainForm : Form
    {
        private NumericUpDown numTrips = null!;
        private NumericUpDown numHours = null!;
        private Button btnCalculate = null!;
        private Label lblResult = null!;

        public MainForm()
        {
            BuildUi();
        }

        private void BuildUi()
        {
            Text = "Калькулятор викидів CO₂";
            ClientSize = new Size(420, 260);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;

            var lblTrips = new Label
            {
                Text = "Кількість поїздок на авто (за період):",
                Location = new Point(12, 20),
                Size = new Size(300, 20)
            };

            numTrips = new NumericUpDown
            {
                Location = new Point(12, 45),
                Size = new Size(120, 23),
                Minimum = 0,
                Maximum = 10000,
                Value = 0
            };

            var lblHours = new Label
            {
                Text = "Годин використання електроприладів:",
                Location = new Point(12, 85),
                Size = new Size(320, 20)
            };

            numHours = new NumericUpDown
            {
                Location = new Point(12, 110),
                Size = new Size(120, 23),
                Minimum = 0,
                Maximum = 10000,
                DecimalPlaces = 1,
                Increment = 0.5m,
                Value = 0
            };

            btnCalculate = new Button
            {
                Text = "Розрахувати викиди",
                Location = new Point(12, 150),
                Size = new Size(180, 35)
            };
            btnCalculate.Click += BtnCalculate_Click;

            lblResult = new Label
            {
                Text = "Загальні викиди CO₂: 0.00 кг",
                Location = new Point(12, 200),
                Size = new Size(390, 50),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold)
            };

            Controls.Add(lblTrips);
            Controls.Add(numTrips);
            Controls.Add(lblHours);
            Controls.Add(numHours);
            Controls.Add(btnCalculate);
            Controls.Add(lblResult);
        }

        private void BtnCalculate_Click(object? sender, EventArgs e)
        {
            decimal trips = numTrips.Value;
            decimal hours = numHours.Value;

            decimal co2FromTrips = trips * Coefficients.CO2PerCarTrip;
            decimal co2FromAppliances = hours * Coefficients.CO2PerApplianceHour;
            decimal total = co2FromTrips + co2FromAppliances;

            lblResult.Text =
                $"Авто: {co2FromTrips:F2} кг{Environment.NewLine}" +
                $"Прилади: {co2FromAppliances:F2} кг{Environment.NewLine}" +
                $"Загальні викиди CO₂: {total:F2} кг";
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

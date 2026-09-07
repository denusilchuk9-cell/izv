namespace UtilityCalculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object? sender, EventArgs e)
        {
            dgvResults.Rows.Clear();
            decimal total = 0m;

            if (!TryCalculateService(
                    "Вода",
                    textBoxWaterPrev, textBoxWaterCurr, textBoxWaterTariff,
                    out decimal waterAmount))
            {
                return;
            }
            total += waterAmount;

            if (!TryCalculateService(
                    "Електроенергія",
                    textBoxElecPrev, textBoxElecCurr, textBoxElecTariff,
                    out decimal elecAmount))
            {
                return;
            }
            total += elecAmount;

            if (!TryCalculateService(
                    "Газ",
                    textBoxGasPrev, textBoxGasCurr, textBoxGasTariff,
                    out decimal gasAmount))
            {
                return;
            }
            total += gasAmount;

            lblTotal.Text = $"Загальна сума до сплати: {total:F2} грн";
        }

        /// <summary>
        /// Зчитує, перевіряє та рахує суму для однієї послуги.
        /// У разі помилки показує повідомлення, підсвічує поле та повертає false.
        /// Якщо все успішно — додає рядок у DataGridView та повертає суму через out.
        /// </summary>
        private bool TryCalculateService(
            string serviceName,
            TextBox prevBox, TextBox currBox, TextBox tariffBox,
            out decimal amount)
        {
            amount = 0m;

            ResetError(prevBox);
            ResetError(currBox);
            ResetError(tariffBox);

            if (!decimal.TryParse(prevBox.Text.Trim(), out decimal prev) || prev < 0)
            {
                ShowError(prevBox, $"«{serviceName}»: введіть коректне попереднє показання (число ≥ 0).");
                return false;
            }

            if (!decimal.TryParse(currBox.Text.Trim(), out decimal curr) || curr < 0)
            {
                ShowError(currBox, $"«{serviceName}»: введіть коректне поточне показання (число ≥ 0).");
                return false;
            }

            if (curr < prev)
            {
                ShowError(currBox, $"«{serviceName}»: поточне показання не може бути меншим за попереднє.");
                return false;
            }

            if (!decimal.TryParse(tariffBox.Text.Trim(), out decimal tariff) || tariff < 0)
            {
                ShowError(tariffBox, $"«{serviceName}»: введіть коректний тариф (число ≥ 0).");
                return false;
            }

            decimal consumption = curr - prev;
            amount = consumption * tariff;

            dgvResults.Rows.Add(
                serviceName,
                consumption.ToString("F2"),
                tariff.ToString("F2"),
                amount.ToString("F2"));

            return true;
        }

        private void ShowError(TextBox box, string message)
        {
            box.BackColor = Color.MistyRose;
            box.Focus();
            MessageBox.Show(message, "Помилка вводу", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void ResetError(TextBox box)
        {
            box.BackColor = SystemColors.Window;
        }

        private void btnClear_Click(object? sender, EventArgs e)
        {
            foreach (Control groupBox in new Control[] { groupBoxWater, groupBoxElectricity, groupBoxGas })
            {
                foreach (Control control in groupBox.Controls)
                {
                    if (control is TextBox textBox)
                    {
                        textBox.Clear();
                        textBox.BackColor = SystemColors.Window;
                    }
                }
            }

            dgvResults.Rows.Clear();
            lblTotal.Text = "Загальна сума до сплати: 0.00 грн";
        }
    }
}

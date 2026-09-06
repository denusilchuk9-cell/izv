using System;
using System.Drawing;
using System.Windows.Forms;

namespace NumberCalculator
{
    public partial class Form1 : Form
    {
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private Button calculateButton;
        private Label resultSum;
        private Label resultDiff;
        private Label resultProduct;

        public Form1()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            // Налаштування форми
            this.Text = "Калькулятор чисел";
            this.Size = new Size(400, 320);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.LightGray;

            // Мітки
            Label label1 = new Label() { Text = "Число 1:", Location = new Point(30, 30), Size = new Size(60, 20) };
            Label label2 = new Label() { Text = "Число 2:", Location = new Point(30, 60), Size = new Size(60, 20) };
            Label label3 = new Label() { Text = "Число 3:", Location = new Point(30, 90), Size = new Size(60, 20) };

            // Поля вводу
            textBox1 = new TextBox() { Location = new Point(100, 27), Size = new Size(60, 20), TextAlign = HorizontalAlignment.Center };
            textBox2 = new TextBox() { Location = new Point(100, 57), Size = new Size(60, 20), TextAlign = HorizontalAlignment.Center };
            textBox3 = new TextBox() { Location = new Point(100, 87), Size = new Size(60, 20), TextAlign = HorizontalAlignment.Center };

            // Кнопка
            calculateButton = new Button()
            {
                Text = "Обчислити",
                Location = new Point(100, 130),
                Size = new Size(100, 30),
                BackColor = Color.LightBlue,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            calculateButton.Click += CalculateButton_Click;

            // Мітки результатів
            Label resultLabel1 = new Label() { Text = "Сума:", Location = new Point(30, 180), Size = new Size(60, 20), Font = new Font("Arial", 10, FontStyle.Bold) };
            Label resultLabel2 = new Label() { Text = "Різниця:", Location = new Point(30, 210), Size = new Size(60, 20), Font = new Font("Arial", 10, FontStyle.Bold) };
            Label resultLabel3 = new Label() { Text = "Добуток:", Location = new Point(30, 240), Size = new Size(60, 20), Font = new Font("Arial", 10, FontStyle.Bold) };

            // Виведення результатів
            resultSum = new Label() { Location = new Point(100, 180), Size = new Size(100, 20), Font = new Font("Arial", 10, FontStyle.Bold), ForeColor = Color.Blue };
            resultDiff = new Label() { Location = new Point(100, 210), Size = new Size(100, 20), Font = new Font("Arial", 10, FontStyle.Bold), ForeColor = Color.Red };
            resultProduct = new Label() { Location = new Point(100, 240), Size = new Size(100, 20), Font = new Font("Arial", 10, FontStyle.Bold), ForeColor = Color.Green };

            // Додавання на форму
            this.Controls.Add(label1);
            this.Controls.Add(label2);
            this.Controls.Add(label3);
            this.Controls.Add(textBox1);
            this.Controls.Add(textBox2);
            this.Controls.Add(textBox3);
            this.Controls.Add(calculateButton);
            this.Controls.Add(resultLabel1);
            this.Controls.Add(resultLabel2);
            this.Controls.Add(resultLabel3);
            this.Controls.Add(resultSum);
            this.Controls.Add(resultDiff);
            this.Controls.Add(resultProduct);
        }

        private void CalculateButton_Click(object sender, EventArgs e)
        {
            try
            {
                double num1 = Convert.ToDouble(textBox1.Text);
                double num2 = Convert.ToDouble(textBox2.Text);
                double num3 = Convert.ToDouble(textBox3.Text);

                resultSum.Text = (num1 + num2 + num3).ToString("0.##");
                resultDiff.Text = (num1 - num2 - num3).ToString("0.##");
                resultProduct.Text = (num1 * num2 * num3).ToString("0.##");
            }
            catch (FormatException)
            {
                MessageBox.Show("Будь ласка, введіть коректні числа!", "Помилка вводу", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
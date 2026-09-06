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
            this.Size = new Size(400, 300);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.LightGray;

            // Створення міток для комірок
            Label label1 = new Label();
            label1.Text = "Число 1:";
            label1.Location = new Point(30, 30);
            label1.Size = new Size(60, 20);

            Label label2 = new Label();
            label2.Text = "Число 2:";
            label2.Location = new Point(30, 60);
            label2.Size = new Size(60, 20);

            Label label3 = new Label();
            label3.Text = "Число 3:";
            label3.Location = new Point(30, 90);
            label3.Size = new Size(60, 20);

            // Створення текстових полів для вводу
            textBox1 = new TextBox();
            textBox1.Location = new Point(100, 27);
            textBox1.Size = new Size(60, 20);
            textBox1.TextAlign = HorizontalAlignment.Center;

            textBox2 = new TextBox();
            textBox2.Location = new Point(100, 57);
            textBox2.Size = new Size(60, 20);
            textBox2.TextAlign = HorizontalAlignment.Center;

            textBox3 = new TextBox();
            textBox3.Location = new Point(100, 87);
            textBox3.Size = new Size(60, 20);
            textBox3.TextAlign = HorizontalAlignment.Center;

            // Створення кнопки
            calculateButton = new Button();
            calculateButton.Text = "Обчислити";
            calculateButton.Location = new Point(100, 130);
            calculateButton.Size = new Size(100, 30);
            calculateButton.BackColor = Color.LightBlue;
            calculateButton.Font = new Font("Arial", 10, FontStyle.Bold);
            calculateButton.Click += CalculateButton_Click;

            // Створення міток для результатів
            Label resultLabel1 = new Label();
            resultLabel1.Text = "Сума:";
            resultLabel1.Location = new Point(30, 180);
            resultLabel1.Size = new Size(60, 20);
            resultLabel1.Font = new Font("Arial", 10, FontStyle.Bold);

            Label resultLabel2 = new Label();
            resultLabel2.Text = "Різниця:";
            resultLabel2.Location = new Point(30, 210);
            resultLabel2.Size = new Size(60, 20);
            resultLabel2.Font = new Font("Arial", 10, FontStyle.Bold);

            Label resultLabel3 = new Label();
            resultLabel3.Text = "Добуток:";
            resultLabel3.Location = new Point(30, 240);
            resultLabel3.Size = new Size(60, 20);
            resultLabel3.Font = new Font("Arial", 10, FontStyle.Bold);

            // Створення міток для виведення результатів
            resultSum = new Label();
            resultSum.Location = new Point(100, 180);
            resultSum.Size = new Size(100, 20);
            resultSum.Font = new Font("Arial", 10, FontStyle.Bold);
            resultSum.ForeColor = Color.Blue;

            resultDiff = new Label();
            resultDiff.Location = new Point(100, 210);
            resultDiff.Size = new Size(100, 20);
            resultDiff.Font = new Font("Arial", 10, FontStyle.Bold);
            resultDiff.ForeColor = Color.Red;

            resultProduct = new Label();
            resultProduct.Location = new Point(100, 240);
            resultProduct.Size = new Size(100, 20);
            resultProduct.Font = new Font("Arial", 10, FontStyle.Bold);
            resultProduct.ForeColor = Color.Green;

            // Додавання всіх елементів на форму
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
                // Отримання чисел з текстових полів
                double num1 = Convert.ToDouble(textBox1.Text);
                double num2 = Convert.ToDouble(textBox2.Text);
                double num3 = Convert.ToDouble(textBox3.Text);

                // Обчислення
                double sum = num1 + num2 + num3;
                double diff = num1 - num2 - num3;
                double product = num1 * num2 * num3;

                // Виведення результатів
                resultSum.Text = sum.ToString("0.##");
                resultDiff.Text = diff.ToString("0.##");
                resultProduct.Text = product.ToString("0.##");
            }
            catch (FormatException)
            {
                MessageBox.Show("Будь ласка, введіть коректні числа!", "Помилка вводу", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Сталася помилка: {ex.Message}", "Помилка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
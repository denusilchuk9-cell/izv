namespace SeatMap
{
    public class MainForm : Form
    {
        private const int Rows = 5;
        private const int Cols = 8;
        private const int ButtonSize = 45;
        private const int Gap = 6;

        private static decimal PriceForRow(int row)
        {
            if (row < 2) return 250m;   // VIP-ряди
            if (row < 4) return 150m;   // Стандарт
            return 100m;                // Економ
        }

        private readonly Button[,] _seats = new Button[Rows, Cols];
        private readonly bool[,] _booked = new bool[Rows, Cols];

        private Label lblTotal = null!;
        private Label lblLegend = null!;

        public MainForm()
        {
            BuildUi();
        }

        private void BuildUi()
        {
            Text = "Схема залу — вибір місць";
            ClientSize = new Size(Cols * (ButtonSize + Gap) + 40, Rows * (ButtonSize + Gap) + 160);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;

            var lblScreen = new Label
            {
                Text = "Е К Р А Н",
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(20, 15),
                Size = new Size(Cols * (ButtonSize + Gap) - Gap, 25),
                BackColor = Color.Gainsboro,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };
            Controls.Add(lblScreen);

            int startY = 55;

            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Cols; col++)
                {
                    var seatButton = new Button
                    {
                        Size = new Size(ButtonSize, ButtonSize),
                        Location = new Point(20 + col * (ButtonSize + Gap), startY + row * (ButtonSize + Gap)),
                        Text = $"{row + 1}-{col + 1}",
                        BackColor = Color.LightGreen, // вільно
                        Tag = (row, col),
                        FlatStyle = FlatStyle.Flat
                    };
                    seatButton.Click += SeatButton_Click;

                    _seats[row, col] = seatButton;
                    Controls.Add(seatButton);
                }
            }

            int bottomY = startY + Rows * (ButtonSize + Gap) + 10;

            lblLegend = new Label
            {
                Text = "Зелений — вільно, Червоний — заброньовано. VIP (1-2 ряд): 250 грн, Стандарт (3-4 ряд): 150 грн, Економ (5 ряд): 100 грн.",
                Location = new Point(20, bottomY),
                Size = new Size(Cols * (ButtonSize + Gap) - Gap, 40)
            };
            Controls.Add(lblLegend);

            lblTotal = new Label
            {
                Text = "Обрано місць: 0. Сума до сплати: 0.00 грн",
                Location = new Point(20, bottomY + 45),
                Size = new Size(Cols * (ButtonSize + Gap) - Gap, 30),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold)
            };
            Controls.Add(lblTotal);
        }

        private void SeatButton_Click(object? sender, EventArgs e)
        {
            if (sender is not Button clicked || clicked.Tag is not ValueTuple<int, int> pos)
                return;

            var (row, col) = pos;

            _booked[row, col] = !_booked[row, col];
            clicked.BackColor = _booked[row, col] ? Color.IndianRed : Color.LightGreen;

            RecalculateTotal();
        }

        private void RecalculateTotal()
        {
            int count = 0;
            decimal total = 0m;

            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Cols; col++)
                {
                    if (_booked[row, col])
                    {
                        count++;
                        total += PriceForRow(row);
                    }
                }
            }

            lblTotal.Text = $"Обрано місць: {count}. Сума до сплати: {total:F2} грн";
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

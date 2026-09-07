namespace UtilityCalculator
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.groupBoxWater = new GroupBox();
            this.labelWaterPrev = new Label();
            this.textBoxWaterPrev = new TextBox();
            this.labelWaterCurr = new Label();
            this.textBoxWaterCurr = new TextBox();
            this.labelWaterTariff = new Label();
            this.textBoxWaterTariff = new TextBox();

            this.groupBoxElectricity = new GroupBox();
            this.labelElecPrev = new Label();
            this.textBoxElecPrev = new TextBox();
            this.labelElecCurr = new Label();
            this.textBoxElecCurr = new TextBox();
            this.labelElecTariff = new Label();
            this.textBoxElecTariff = new TextBox();

            this.groupBoxGas = new GroupBox();
            this.labelGasPrev = new Label();
            this.textBoxGasPrev = new TextBox();
            this.labelGasCurr = new Label();
            this.textBoxGasCurr = new TextBox();
            this.labelGasTariff = new Label();
            this.textBoxGasTariff = new TextBox();

            this.btnCalculate = new Button();
            this.btnClear = new Button();
            this.dgvResults = new DataGridView();
            this.lblTotal = new Label();

            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            this.SuspendLayout();

            // ===== groupBoxWater =====
            this.groupBoxWater.Text = "Вода";
            this.groupBoxWater.Location = new Point(12, 12);
            this.groupBoxWater.Size = new Size(600, 110);

            this.labelWaterPrev.Text = "Попередні показники, м³:";
            this.labelWaterPrev.Location = new Point(10, 28);
            this.labelWaterPrev.Size = new Size(180, 20);

            this.textBoxWaterPrev.Location = new Point(200, 25);
            this.textBoxWaterPrev.Size = new Size(100, 23);

            this.labelWaterCurr.Text = "Поточні показники, м³:";
            this.labelWaterCurr.Location = new Point(320, 28);
            this.labelWaterCurr.Size = new Size(160, 20);

            this.textBoxWaterCurr.Location = new Point(490, 25);
            this.textBoxWaterCurr.Size = new Size(100, 23);

            this.labelWaterTariff.Text = "Тариф, грн/м³:";
            this.labelWaterTariff.Location = new Point(10, 65);
            this.labelWaterTariff.Size = new Size(180, 20);

            this.textBoxWaterTariff.Location = new Point(200, 62);
            this.textBoxWaterTariff.Size = new Size(100, 23);

            this.groupBoxWater.Controls.Add(this.labelWaterPrev);
            this.groupBoxWater.Controls.Add(this.textBoxWaterPrev);
            this.groupBoxWater.Controls.Add(this.labelWaterCurr);
            this.groupBoxWater.Controls.Add(this.textBoxWaterCurr);
            this.groupBoxWater.Controls.Add(this.labelWaterTariff);
            this.groupBoxWater.Controls.Add(this.textBoxWaterTariff);

            // ===== groupBoxElectricity =====
            this.groupBoxElectricity.Text = "Електроенергія";
            this.groupBoxElectricity.Location = new Point(12, 130);
            this.groupBoxElectricity.Size = new Size(600, 110);

            this.labelElecPrev.Text = "Попередні показники, кВт·год:";
            this.labelElecPrev.Location = new Point(10, 28);
            this.labelElecPrev.Size = new Size(180, 20);

            this.textBoxElecPrev.Location = new Point(200, 25);
            this.textBoxElecPrev.Size = new Size(100, 23);

            this.labelElecCurr.Text = "Поточні показники, кВт·год:";
            this.labelElecCurr.Location = new Point(320, 28);
            this.labelElecCurr.Size = new Size(160, 20);

            this.textBoxElecCurr.Location = new Point(490, 25);
            this.textBoxElecCurr.Size = new Size(100, 23);

            this.labelElecTariff.Text = "Тариф, грн/кВт·год:";
            this.labelElecTariff.Location = new Point(10, 65);
            this.labelElecTariff.Size = new Size(180, 20);

            this.textBoxElecTariff.Location = new Point(200, 62);
            this.textBoxElecTariff.Size = new Size(100, 23);

            this.groupBoxElectricity.Controls.Add(this.labelElecPrev);
            this.groupBoxElectricity.Controls.Add(this.textBoxElecPrev);
            this.groupBoxElectricity.Controls.Add(this.labelElecCurr);
            this.groupBoxElectricity.Controls.Add(this.textBoxElecCurr);
            this.groupBoxElectricity.Controls.Add(this.labelElecTariff);
            this.groupBoxElectricity.Controls.Add(this.textBoxElecTariff);

            // ===== groupBoxGas =====
            this.groupBoxGas.Text = "Газ";
            this.groupBoxGas.Location = new Point(12, 248);
            this.groupBoxGas.Size = new Size(600, 110);

            this.labelGasPrev.Text = "Попередні показники, м³:";
            this.labelGasPrev.Location = new Point(10, 28);
            this.labelGasPrev.Size = new Size(180, 20);

            this.textBoxGasPrev.Location = new Point(200, 25);
            this.textBoxGasPrev.Size = new Size(100, 23);

            this.labelGasCurr.Text = "Поточні показники, м³:";
            this.labelGasCurr.Location = new Point(320, 28);
            this.labelGasCurr.Size = new Size(160, 20);

            this.textBoxGasCurr.Location = new Point(490, 25);
            this.textBoxGasCurr.Size = new Size(100, 23);

            this.labelGasTariff.Text = "Тариф, грн/м³:";
            this.labelGasTariff.Location = new Point(10, 65);
            this.labelGasTariff.Size = new Size(180, 20);

            this.textBoxGasTariff.Location = new Point(200, 62);
            this.textBoxGasTariff.Size = new Size(100, 23);

            this.groupBoxGas.Controls.Add(this.labelGasPrev);
            this.groupBoxGas.Controls.Add(this.textBoxGasPrev);
            this.groupBoxGas.Controls.Add(this.labelGasCurr);
            this.groupBoxGas.Controls.Add(this.textBoxGasCurr);
            this.groupBoxGas.Controls.Add(this.labelGasTariff);
            this.groupBoxGas.Controls.Add(this.textBoxGasTariff);

            // ===== buttons =====
            this.btnCalculate.Text = "Розрахувати";
            this.btnCalculate.Location = new Point(12, 372);
            this.btnCalculate.Size = new Size(140, 35);
            this.btnCalculate.Click += new EventHandler(this.btnCalculate_Click);

            this.btnClear.Text = "Очистити";
            this.btnClear.Location = new Point(162, 372);
            this.btnClear.Size = new Size(140, 35);
            this.btnClear.Click += new EventHandler(this.btnClear_Click);

            // ===== dgvResults =====
            this.dgvResults.Location = new Point(12, 420);
            this.dgvResults.Size = new Size(600, 180);
            this.dgvResults.ReadOnly = true;
            this.dgvResults.AllowUserToAddRows = false;
            this.dgvResults.AllowUserToDeleteRows = false;
            this.dgvResults.AllowUserToResizeRows = false;
            this.dgvResults.RowHeadersVisible = false;
            this.dgvResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvResults.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvResults.Columns.Add("Service", "Послуга");
            this.dgvResults.Columns.Add("Consumption", "Витрачено");
            this.dgvResults.Columns.Add("Tariff", "Тариф, грн/од.");
            this.dgvResults.Columns.Add("Amount", "Сума, грн");

            // ===== lblTotal =====
            this.lblTotal.Text = "Загальна сума до сплати: 0.00 грн";
            this.lblTotal.Location = new Point(12, 612);
            this.lblTotal.Size = new Size(600, 30);
            this.lblTotal.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblTotal.TextAlign = ContentAlignment.MiddleLeft;

            // ===== Form1 =====
            this.ClientSize = new Size(630, 660);
            this.Text = "Калькулятор комунальних платежів";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            this.Controls.Add(this.groupBoxWater);
            this.Controls.Add(this.groupBoxElectricity);
            this.Controls.Add(this.groupBoxGas);
            this.Controls.Add(this.btnCalculate);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.dgvResults);
            this.Controls.Add(this.lblTotal);

            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            this.ResumeLayout(false);
        }

        private GroupBox groupBoxWater;
        private Label labelWaterPrev;
        private TextBox textBoxWaterPrev;
        private Label labelWaterCurr;
        private TextBox textBoxWaterCurr;
        private Label labelWaterTariff;
        private TextBox textBoxWaterTariff;

        private GroupBox groupBoxElectricity;
        private Label labelElecPrev;
        private TextBox textBoxElecPrev;
        private Label labelElecCurr;
        private TextBox textBoxElecCurr;
        private Label labelElecTariff;
        private TextBox textBoxElecTariff;

        private GroupBox groupBoxGas;
        private Label labelGasPrev;
        private TextBox textBoxGasPrev;
        private Label labelGasCurr;
        private TextBox textBoxGasCurr;
        private Label labelGasTariff;
        private TextBox textBoxGasTariff;

        private Button btnCalculate;
        private Button btnClear;
        private DataGridView dgvResults;
        private Label lblTotal;
    }
}

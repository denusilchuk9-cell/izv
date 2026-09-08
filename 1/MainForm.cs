namespace RegistrationApp;

public class MainForm : Form
{
    private TextBox txtFullName = new();
    private DateTimePicker dtpBirthDate = new();
    private ComboBox cmbGroup = new();
    private RadioButton rbMale = new();
    private RadioButton rbFemale = new();
    private CheckBox chkConsent = new();
    private Button btnRegister = new();

    public MainForm()
    {
        Text = "Реєстрація студента";
        Width = 420;
        Height = 400;
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;

        var lblFullName = new Label { Text = "ПІБ:", Location = new Point(20, 20), AutoSize = true };
        txtFullName.Location = new Point(150, 17);
        txtFullName.Width = 230;

        var lblBirthDate = new Label { Text = "Дата народження:", Location = new Point(20, 60), AutoSize = true };
        dtpBirthDate.Location = new Point(150, 57);
        dtpBirthDate.Width = 230;
        dtpBirthDate.Format = DateTimePickerFormat.Short;
        dtpBirthDate.MaxDate = DateTime.Today;

        var lblGroup = new Label { Text = "Група/спеціальність:", Location = new Point(20, 100), AutoSize = true };
        cmbGroup.Location = new Point(150, 97);
        cmbGroup.Width = 230;
        cmbGroup.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbGroup.Items.AddRange(new object[]
        {
            "ІПЗ-4/1",
            "ІПЗ-3/2",
            "КН-2/1",
            "КН-2/2",
            "ІПЗ 3/1",
        });

        var lblGender = new Label { Text = "Стать:", Location = new Point(20, 140), AutoSize = true };
        rbMale.Text = "Чоловіча";
        rbMale.Location = new Point(150, 138);
        rbMale.AutoSize = true;
        rbFemale.Text = "Жіноча";
        rbFemale.Location = new Point(260, 138);
        rbFemale.AutoSize = true;

        chkConsent.Text = "Згоден(на) на обробку персональних даних";
        chkConsent.Location = new Point(20, 180);
        chkConsent.Width = 360;
        chkConsent.AutoSize = true;

        btnRegister.Text = "Зареєструвати";
        btnRegister.Location = new Point(150, 230);
        btnRegister.Width = 150;
        btnRegister.Height = 35;
        btnRegister.Click += BtnRegister_Click;

        Controls.AddRange(new Control[]
        {
            lblFullName, txtFullName,
            lblBirthDate, dtpBirthDate,
            lblGroup, cmbGroup,
            lblGender, rbMale, rbFemale,
            chkConsent,
            btnRegister
        });
    }

    private void BtnRegister_Click(object? sender, EventArgs e)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(txtFullName.Text))
            errors.Add("Не заповнено ПІБ");

        if (cmbGroup.SelectedItem is null)
            errors.Add("Не вибрано групу/спеціальність");

        if (!rbMale.Checked && !rbFemale.Checked)
            errors.Add("Не вибрано стать");

        if (!chkConsent.Checked)
            errors.Add("Не надано згоду на обробку даних");

        if (errors.Count > 0)
        {
            MessageBox.Show(
                string.Join(Environment.NewLine, errors),
                "Помилка валідації",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        string gender = rbMale.Checked ? "Чоловіча" : "Жіноча";

        string summary =
            $"ПІБ: {txtFullName.Text}\n" +
            $"Дата народження: {dtpBirthDate.Value:dd.MM.yyyy}\n" +
            $"Група: {cmbGroup.SelectedItem}\n" +
            $"Стать: {gender}\n" +
            $"Згода на обробку даних: Так";

        MessageBox.Show(summary, "Реєстрацію завершено успішно", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}

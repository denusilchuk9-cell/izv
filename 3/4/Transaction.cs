namespace FinanceTrackerApp;

public class Transaction
{
    public DateTime Date { get; set; }
    public string Type { get; set; } = "";
    public string Category { get; set; } = "";
    public decimal Amount { get; set; }
    public string Note { get; set; } = "";
}

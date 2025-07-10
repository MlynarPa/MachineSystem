namespace PlcDataCollector.Models;

public class Machine
{
    public string Abbreviation { get; set; } = string.Empty;
    public bool IsRunning { get; set; }
    public int PowerConsumption { get; set; }
    public bool DI1 { get; set; }           // nový vstup
    public bool DI2 { get; set; }           // nový vstup
    public DateTime Timestamp { get; set; }
}

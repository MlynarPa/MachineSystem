using PlcDataCollector.Services;

// IP PLC, rack, slot
PlcReader plcReader;
try
{
    plcReader = new PlcReader("192.168.0.10", 0, 1);
    Console.WriteLine("✅ PLC připojení navázáno");
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Chyba připojení k PLC: {ex.Message}");
    return;
}

// ⚠️ Komunikace přes localhost, protože běží na stejném zařízení
var apiSender = new ApiSender("http://localhost:5500/api/machines");

while (true)
{
    var machines = plcReader.ReadMachines();

    foreach (var m in machines)
    {
        Console.WriteLine($"{m.Abbreviation} | Run: {m.IsRunning,-5} | Power: {m.PowerConsumption,3} W | DI1: {m.DI1,-5} | DI2: {m.DI2,-5}");
    }

    await apiSender.SendAsync(machines);

    await Task.Delay(1000); // každou sekundu
}

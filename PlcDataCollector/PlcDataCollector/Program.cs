using PlcDataCollector.Services;

// IP PLC, rack, slot
var plcReader = new PlcReader("192.168.0.10", 0, 1);

// ⚠️ Tady změna: Tailscale IP místo localhost
var apiSender = new ApiSender("http://100.115.13.31:5500/api/machines");

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

using PlcDataCollector.Models;
using S7.Net;

namespace PlcDataCollector.Services;

public class PlcReader
{
    private readonly Plc _plc;

    // nový tuple: (Abbr, runByte, runBit, powerOffset, di1Byte, di1Bit, di2Byte, di2Bit)
    private readonly (string Abbr, int RunByte, int RunBit, int PowerOffset, int DI1_Byte, int DI1_Bit, int DI2_Byte, int DI2_Bit)[] _machineMap =
    {
    ("DRS_0001", 0, 0, 2, 16, 0, 16, 1),
    ("DRS_0002", 4, 0, 6, 16, 2, 16, 3),
    ("DRS_0003", 8, 0, 10, 16, 4, 16, 5),
    ("DRS_0004", 12, 0, 14, 16, 6, 16, 7),
};


    public PlcReader(string ip, short rack, short slot)
    {
        _plc = new Plc(CpuType.S71500, ip, rack, slot);
        _plc.Open();

        if (!_plc.IsConnected)
            throw new Exception("Nepodařilo se připojit k PLC.");
    }

    public List<Machine> ReadMachines()
    {
        var machines = new List<Machine>();

        foreach (var (abbr, runByte, runBit, powerOffset, di1Byte, di1Bit, di2Byte, di2Bit) in _machineMap)
        {
            bool isRunning = ReadBool(runByte, runBit);
            short power = ReadInt(powerOffset);
            bool di1 = ReadBool(di1Byte, di1Bit);
            bool di2 = ReadBool(di2Byte, di2Bit);

            machines.Add(new Machine
            {
                Abbreviation = abbr,
                IsRunning = isRunning,
                PowerConsumption = power,
                DI1 = di1,
                DI2 = di2,
                Timestamp = DateTime.UtcNow
            });
        }

        return machines;
    }


    private bool ReadBool(int byteOffset, int bitOffset)
    {
        return (bool)_plc.Read(S7.Net.DataType.DataBlock, 1, byteOffset, VarType.Bit, 1, (byte)bitOffset);
    }

    private short ReadInt(int offset)
    {
        byte[] raw = (byte[])_plc.ReadBytes(S7.Net.DataType.DataBlock, 1, offset, 2);
        return BitConverter.ToInt16(raw.Reverse().ToArray(), 0);
    }

    public void Close() => _plc.Close();
}

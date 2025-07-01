using System;
using System.Diagnostics;

namespace Task2
{
    internal class Program
    {
        static double CalculateLossPercent(double trafficRate, double channelRate)
        {
            if (trafficRate <= channelRate)
                return 0.0;
            else
                return (trafficRate - channelRate) / trafficRate * 100.0;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("== UDP Traffic Emulator with Loss ==");
            string srcMac = "AA:BB:CC:DD:EE:01"; //MAC отправителя
            string dstMac = "FF:EE:DD:CC:BB:01"; //MAC получателя
            string srcIp = "127.0.0.1"; //IP отправителя
            string dstIp = "127.0.0.1"; //IP получателя
            Random rnd = new Random();
            int channelMbps = rnd.Next(10, 1001); //пропускная способность канала
            Console.WriteLine($"Пропускная способность канала:{channelMbps} Мбит/с");
            int packetSize =  1400; //Размер пакета(байт)
            int durationSec = 10; //Длительность теста
            long sentPackets = 0;
            long receivedPackets = 0;
            long lostPackets = 0;
            long totalBytes = 0;
            Stopwatch stopwatch = Stopwatch.StartNew();
            Console.WriteLine("Эмуляция UDP-трафика");
            int secondsElapsed = 0;
            while (secondsElapsed < durationSec)
            {
                int speedMbps = rnd.Next(1, 1001);
                double lossPercent = CalculateLossPercent(speedMbps, channelMbps);
                long packetsPerSec = (speedMbps * 1_000_000) / (packetSize * 8);
                long packetsThisSec = (long)(packetsPerSec-(packetsPerSec * lossPercent/100));
                sentPackets+=packetsPerSec;
                receivedPackets+= packetsThisSec;
                secondsElapsed++;
            }
            stopwatch.Stop();
            totalBytes = sentPackets * packetSize;
            lostPackets = sentPackets - receivedPackets;
            double mbSent = totalBytes / (1024.0 * 1024.0);
            Console.WriteLine("\n====== Отчёт эмуляции ======");
            Console.WriteLine($"Пакетов отправлено: {sentPackets}");
            Console.WriteLine($"Пакетов принято:    {receivedPackets}");
            Console.WriteLine($"Пакетов потеряно:   {lostPackets}");
            Console.WriteLine($"Суммарно байт:      {totalBytes}");
            Console.WriteLine($"Суммарно МБ:        {mbSent:F2}");
            Console.WriteLine("============================");
        }
    }
}
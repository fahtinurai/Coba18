using System;

class Program
{
    static void Main(string[] args)
    {
        // Load konfigurasi
        var config = CinemaBookingConfig.LoadConfig();

        // Bahasa: English atau Indonesia
        string promptJumlah = config.lang == "en" ?
            "Please insert the number of tickets to book:" :
            "Masukkan jumlah tiket yang ingin dipesan:";

        Console.WriteLine(promptJumlah);

        int jumlahTiket;
        while (!int.TryParse(Console.ReadLine(), out jumlahTiket) || jumlahTiket <= 0)
        {
            Console.WriteLine("Input tidak valid. Masukkan angka positif.");
        }

        // Hitung biaya
        int biayaPerTiket = jumlahTiket <= config.threshold ? config.low_fee : config.high_fee;
        int totalBiaya = biayaPerTiket * jumlahTiket;

        if (config.lang == "en")
        {
            Console.WriteLine($"Booking fee per ticket = {biayaPerTiket}");
            Console.WriteLine($"Total amount = {totalBiaya}");
        }
        else
        {
            Console.WriteLine($"Biaya booking per tiket = {biayaPerTiket}");
            Console.WriteLine($"Total biaya = {totalBiaya}");
        }

        // Tampilkan pilihan jenis kursi
        Console.WriteLine(config.lang == "en" ? "Select seat type:" : "Pilih jenis kursi:");
        for (int i = 0; i < config.seating.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {config.seating[i]}");
        }

        int pilihanKursi;
        while (!int.TryParse(Console.ReadLine(), out pilihanKursi) || pilihanKursi < 1 || pilihanKursi > config.seating.Count)
        {
            Console.WriteLine("Pilihan tidak valid. Silakan pilih nomor kursi yang tersedia.");
        }

        // Konfirmasi pemesanan
        string konfirmasiPrompt = config.lang == "en"
            ? $"Please type \"{config.confirm_en}\" to confirm the booking:"
            : $"Ketik \"{config.confirm_id}\" untuk mengkonfirmasi pemesanan:";

        Console.WriteLine(konfirmasiPrompt);
        string userInput = Console.ReadLine();

        bool isConfirmed = (config.lang == "en" && userInput == config.confirm_en)
                         || (config.lang == "id" && userInput == config.confirm_id);

        if (isConfirmed)
        {
            Console.WriteLine(config.lang == "en" ? "Booking successful!" : "Pemesanan berhasil!");
        }
        else
        {
            Console.WriteLine(config.lang == "en" ? "Booking cancelled." : "Pemesanan dibatalkan.");
        }
    }
}

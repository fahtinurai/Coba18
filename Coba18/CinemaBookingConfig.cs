using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

public class CinemaBookingConfig
{
    public string lang { get; set; }
    public int threshold { get; set; }
    public int low_fee { get; set; }
    public int high_fee { get; set; }
    public List<string> seating { get; set; }
    public string confirm_en { get; set; }
    public string confirm_id { get; set; }

    private const string ConfigFileName = "cinema_booking_config.json";

    public static CinemaBookingConfig LoadConfig()
    {
        if (!File.Exists(ConfigFileName))
        {
            var defaultConfig = new CinemaBookingConfig
            {
                lang = "en",
                threshold = 3,
                low_fee = 10000,
                high_fee = 20000,
                seating = new List<string> { "Regular", "VIP", "Couple", "Deluxe" },
                confirm_en = "confirm",
                confirm_id = "konfirmasi"
            };

            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(defaultConfig, options);
            File.WriteAllText(ConfigFileName, json);
        }

        string configText = File.ReadAllText(ConfigFileName);
        return JsonSerializer.Deserialize<CinemaBookingConfig>(configText);
    }
}

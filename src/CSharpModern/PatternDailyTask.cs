
using System;

public class PatternDailyTask
{
  // 1. Record Shipment
  private record Shipment(
      string Id,
      decimal WeightKg,
      string Status,
      string? Courier
  );

  // 2. Main Method
  public static void Run()
  {
    Console.WriteLine("\n=== PATTERN MATCHING PRACTICE ===");

    // 3. Data Shipment
    var shipments = new[]
    {
            new Shipment("SHP-001", 1500000m, "Delivered", "Mulyono"),
            new Shipment("SHP-002", 500000m, "Unknown", "Gibran"),
            new Shipment("SHP-003", -3000m, "", null),
            new Shipment("SHP-004", 0, "Delivered", "Arman"),
            new Shipment("SHP-005", 500000m, "Unknown", null),
            new Shipment("SHP-006", 0, "", null),
            new Shipment("SHP-007", 100000m, "Delivered", "Bambang"),
            new Shipment("SHP-008", 3000m, "Shipped", "Anggi"),
            new Shipment("SHP-009", 2000m, "Shipped", "Prab"),
            new Shipment("SHP-010", 1000m, "Shipped", "Udin"),
            new Shipment("SHP-011", 4000m, "Shipped", null),
        };

    // 4. Testing Shipment
    foreach (var shp in shipments)
    {
      Console.WriteLine(
          $"{shp.Id}: {ValidateShipment(shp)} -> " +
          $"{shp.WeightKg} ({ClassifyWeight(shp.WeightKg)})"
      );
    }

    // 5. Testing Type Pattern
    object?[] items =
    {
            new Shipment("SHP-012", 10m, "Shipped", "Courier A"),
            12,
            "Hello",
            null
        };

    foreach (var item in items)
    {
      Console.WriteLine(Describe(item));
    }
  }

  // 6. Property Pattern
  private static string ValidateShipment(Shipment s) => s switch
  {
    { WeightKg: <= 0 } =>
        "Invalid: Weight must be greater than 0",

    { Status: "Shipped", Courier: null } =>
        "Invalid: Shipped without Courier",

    { Status: "Shipped", Courier: not null } =>
        "Shipped",

    { Status: "Delivered" } =>
        "Delivered",

    _ => "Unknown status"
  };

  // 7. Relational + Logical Pattern
  private static string ClassifyWeight(decimal kg) => kg switch
  {
    > 5m => "Berat",
    > 1m and <= 5m => "Sedang",
    > 0m and <= 1m => "Ringan",
    _ => "Tidak Valid"
  };

  // 8. Type Pattern
  private static string Describe(object? value) => value switch
  {
    null => "null value",
    Shipment s => $"Shipment: {s.Id}",
    int n => $"int: {n}",
    string s => $"string: \"{s}\" (length {s.Length})",
    _ => $"other type: {value.GetType().Name}"
  };

  // 9. Eksperimen
  // Pattern "_" selalu cocok dengan seluruh input sehingga harus berada di bawah.
  // Jika "_" diletakkan di atas pattern spesifik, compiler menghasilkan error CS8510
  // (unreachable pattern), sehingga program tidak dapat dikompilasi.
}

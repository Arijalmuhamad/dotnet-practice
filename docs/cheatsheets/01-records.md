# C# Cheat Sheet – Records & Init-only Properties

## 1. Record

Record adalah tipe data yang cocok untuk merepresentasikan data dan memiliki value-based equality secara default.

```csharp
public record Product(string Name, decimal Price);
```

### Value Equality

```csharp
var p1 = new Product("Monitor", 2500000);
var p2 = new Product("Monitor", 2500000);

Console.WriteLine(p1 == p2); // True
```

Record membandingkan nilai property, bukan identitas object.

## 2. with Expression

Digunakan untuk membuat salinan record dengan perubahan tertentu.

```csharp
var p2 = p1 with { Price = 2300000 };
```

- `p1` tidak berubah.
- `p2` adalah salinan dengan nilai berbeda.
- Tidak melakukan mutasi pada object original.

## 3. ToString()

Record menyediakan implementasi `ToString()` otomatis.

```csharp
Console.WriteLine(p1);
// Product { Name = Monitor, Price = 2500000 }
```

## 4. init-only Property

Property hanya dapat diisi ketika object diinisialisasi.

```csharp
public class Customer
{
    public required string Name { get; init; }
}
```

```csharp
var customer = new Customer { Name = "Arijal" };
```

Assignment setelah object dibuat tidak diperbolehkan:

```csharp
customer.Name = "Budi"; // Compile error
```

## 5. required vs init

| Keyword    | Fungsi                                       |
| ---------- | -------------------------------------------- |
| `required` | Property wajib diinisialisasi                |
| `init`     | Property hanya dapat diisi saat inisialisasi |

## 6. Record vs Class

| Record                           | Class                                        |
| -------------------------------- | -------------------------------------------- |
| Value equality default           | Reference equality default                   |
| Cocok untuk data                 | Cocok untuk object dengan identitas/behavior |
| Mendukung `with`                 | Tidak otomatis mendukung `with`              |
| `ToString()` otomatis informatif | `ToString()` default nama tipe               |

## Key Takeaways

- Gunakan `record` untuk data yang equality-nya berdasarkan nilai.
- Gunakan `class` untuk entity dengan identitas dan behavior.
- `init` membantu membatasi perubahan property.
- `required` memastikan property wajib diisi.
- `with` membuat salinan record dengan perubahan tertentu.

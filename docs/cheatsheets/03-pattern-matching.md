# Pattern Matching Cheat Sheet

## 1. Overview

Pattern matching adalah cara memeriksa **nilai, tipe, atau isi property** sebuah data lalu langsung bertindak berdasarkan hasilnya dalam satu ekspresi. Digunakan dengan operator `is` dan `switch` (statement maupun expression), dan menggantikan rantai `if-else` yang panjang.

## 2. Key Concepts

- **Pattern** = bentuk atau kondisi yang dicocokkan terhadap sebuah nilai.
- **Switch expression** menghasilkan nilai, bukan sekadar menjalankan blok kode.
- **First match wins**: kondisi dievaluasi dari atas ke bawah, kondisi pertama yang cocok dipakai dan sisanya diabaikan.
- **Discard `_`** cocok dengan nilai apa pun (setara `default`).
- Pattern bisa **digabung** (`and`, `or`, `not`) dan **di-nest** (property pattern berisi relational pattern).
- Setelah pengecekan `null` atau type pattern, compiler tahu status null/tipe variabel di cabang berikutnya (flow analysis dari Nullable Reference Types).

## 3. Syntax & Examples

| Pattern       | Contoh                               | Makna                             |
| ------------- | ------------------------------------ | --------------------------------- |
| Constant      | `status is "Paid"`                   | Sama dengan nilai tertentu        |
| Relational    | `age is >= 18`                       | Perbandingan `<`, `>`, `<=`, `>=` |
| Logical `and` | `age is >= 18 and <= 60`             | Kedua kondisi terpenuhi           |
| Logical `or`  | `status is "Shipped" or "Delivered"` | Salah satu terpenuhi              |
| Logical `not` | `name is not null`                   | Kebalikan kondisi                 |
| Type          | `value is string text`               | Cek tipe + deklarasi variabel     |
| Property      | `trx is { Status: "Paid" }`          | Cek isi property                  |
| Discard       | `_ => ...`                           | Semua kondisi lain                |

```csharp
// Relational + switch expression
string grade = score switch
{
    >= 90 => "A",
    >= 80 => "B",
    >= 70 => "C",
    _ => "D"
};

// Logical pattern
bool isWorkingAge = age is >= 18 and <= 60;

// Null pattern
if (nickname is not null) { Console.WriteLine(nickname.Length); }

// Type pattern
static string Describe(object? value) => value switch
{
    null => "null value",
    int n => $"int: {n}",
    string s => $"string: {s} (length {s.Length})",
    _ => $"other: {value.GetType().Name}"
};

// Property pattern (cocok dengan record)
static string Validate(Order o) => o switch
{
    { Total: <= 0 } => "Invalid",
    { Status: "Paid", Email: null } => "Paid (no email)",
    { Status: "Paid" } => "Paid",
    { Status: "Shipped" or "Delivered" } => "On the way / Done",
    _ => "Unknown status"
};
```

## 4. Important Differences

| Konsep                                       | Perbedaan                                                                                                      |
| -------------------------------------------- | -------------------------------------------------------------------------------------------------------------- |
| `x != null` vs `x is not null`               | Hasil sama pada umumnya; `is not null` lebih mudah dibaca dan tidak terpengaruh operator `!=` yang di-overload |
| `if-else` vs `switch expression`             | `switch expression` menghasilkan nilai dan lebih ringkas untuk percabangan berdasarkan data                    |
| `switch` statement vs `switch` expression    | Statement menjalankan blok kode; expression mengembalikan nilai dan wajib menangani semua kemungkinan          |
| `Email: null` vs `string.IsNullOrWhiteSpace` | `null` pattern hanya cocok dengan `null`, bukan `""` atau spasi                                                |
| `a is 1 or 2` vs `(a is 1) or 2`             | Yang pertama benar: `or` bekerja di dalam pattern                                                              |

## 5. Common Mistakes

- **Urutan kondisi salah**: kondisi umum ditaruh di atas kondisi spesifik, sehingga yang spesifik tidak pernah tercapai. Jika benar-benar tertutup, compiler memberi error (mis. CS8510: pattern sudah ditangani oleh pattern sebelumnya).
- **Lupa `_ =>`** pada switch expression: compiler memberi warning, dan nilai tak terduga bisa melempar `SwitchExpressionException` saat runtime.
- **Memeriksa `null` dengan `Email: null` saja** padahal data bisa berupa string kosong.
- **Menaruh guard invalid terlalu bawah**: data invalid (mis. `Total <= 0`) bisa lolos sebagai status valid.
- **ID/data test duplikat atau kurang** sehingga ada cabang yang tidak pernah teruji.

## 6. Best Practices

- Urutkan: **guard invalid → kondisi spesifik → kondisi umum → `_`**.
- Selalu sediakan `_ =>` sebagai fallback yang bermakna (`"Unknown status"`), jangan dibiarkan melempar exception.
- Gunakan `is null` / `is not null` untuk pengecekan null.
- Pisahkan logika validasi ke method sendiri (`ValidateOrder`) agar mudah diuji.
- Uji setiap cabang, termasuk batas nilai (0, tepat 1.000.000, null, string kosong).
- Jangan memaksakan pattern matching untuk logika yang sangat panjang; pecah menjadi method kecil.

## 7. Real-World Application

- Validasi status transaksi atau order (Paid, Pending, Cancelled).
- Klasifikasi nilai ke kategori/grade berdasarkan rentang angka.
- Menangani data bertipe campuran dari JSON/API (`object`).
- Aturan bisnis berdasarkan kombinasi property DTO atau record.
- Menggantikan `if-else` bertingkat di service layer ASP.NET Core.

## 8. Key Takeaways

1. Pattern matching = cek bentuk data + aksi, dalam satu ekspresi.
2. **Switch expression: yang pertama cocok dipakai**, urutan sangat penting.
3. Urutan umum yang aman: invalid → spesifik → umum → `_`.
4. Property pattern + record = validasi yang ringkas dan mudah dibaca.
5. `is not null` dan type pattern memanfaatkan flow analysis compiler dari Nullable Reference Types.
6. `null` pattern tidak menangkap string kosong; tangani secara eksplisit bila perlu.

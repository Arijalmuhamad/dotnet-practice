# LINQ Dasar Cheat Sheet

## 1. Overview

LINQ (Language Integrated Query) adalah cara melakukan query (filter, urut, proyeksi, pengelompokan, agregasi) pada koleksi data langsung dari C#. Operator dirangkai (_method chaining_) sehingga menggantikan `foreach` + `if` + list sementara. Dipakai pada koleksi di memori (`IEnumerable<T>`) maupun sumber data eksternal seperti database lewat EF Core (`IQueryable<T>`).

Butuh `using System.Linq;` (biasanya sudah otomatis lewat implicit usings).

## 2. Key Concepts

- **Method chaining**: `Where(...).OrderBy(...).Select(...)`; urutan operator memengaruhi hasil dan efisiensi.
- **Proyeksi**: `Select` mengubah bentuk tiap item (satu field, string, atau object anonim).
- **Grouping**: `GroupBy` menghasilkan grup; `g.Key` adalah **nilai pengelompok**, bukan item pertama. `g` berisi seluruh item dalam grup.
- **Deferred execution**: query tidak dijalankan saat ditulis, tetapi saat hasilnya dipakai (di-loop, `ToList()`, `Count()`, `string.Join`, dst).
- **Immediate execution**: `ToList()`, `ToArray()`, `Count()`, `Sum()`, `First()`, `Any()` langsung menjalankan query.
- **Expression tree**: pada `IQueryable`, lambda ditangkap sebagai `Expression<Func<T, bool>>` lalu diterjemahkan (mis. menjadi SQL), bukan dieksekusi sebagai delegate.

## 3. Syntax & Examples

| Operator                            | Fungsi                   | Contoh                                          |
| ----------------------------------- | ------------------------ | ----------------------------------------------- |
| `Where`                             | Filter                   | `sales.Where(s => s.Status == "Paid")`          |
| `Select`                            | Proyeksi                 | `sales.Select(s => new { s.Id, s.Amount })`     |
| `OrderBy` / `OrderByDescending`     | Urut naik / turun        | `.OrderByDescending(s => s.Amount)`             |
| `ThenBy`                            | Urutan kedua             | `.OrderBy(p => p.Category).ThenBy(p => p.Name)` |
| `Take`                              | Ambil N pertama          | `.Take(3)`                                      |
| `Distinct`                          | Hapus duplikat           | `.Select(s => s.Customer).Distinct()`           |
| `Count` / `Sum` / `Average` / `Max` | Agregasi                 | `sales.Sum(s => s.Amount)`                      |
| `Any`                               | Ada yang cocok?          | `sales.Any(s => s.Status == "Pending")`         |
| `FirstOrDefault`                    | Item pertama atau `null` | `sales.FirstOrDefault(s => s.Id == "S001")`     |
| `GroupBy`                           | Kelompokkan              | `sales.GroupBy(s => s.Customer)`                |

```csharp
// Filter -> urut -> ambil 3 -> proyeksi
var top3Paid = sales
    .Where(s => s.Status == "Paid")
    .OrderByDescending(s => s.Amount)
    .Take(3)
    .Select(s => new { s.Id, s.Amount });

// GroupBy + agregasi (g.Key = nama customer)
var totals = sales
    .Where(s => s.Status == "Paid")
    .GroupBy(s => s.Customer)
    .Select(g => new { Customer = g.Key, Total = g.Sum(s => s.Amount) })
    .OrderByDescending(x => x.Total);

// Pattern matching di dalam Where
var issues = sales.Where(s => s.Status is "Pending" or "Cancelled");

// FirstOrDefault + null handling
Sale? found = sales.FirstOrDefault(s => s.Id == "S999");
string name = found?.Customer ?? "Tidak ditemukan";

// Deferred vs snapshot
var list = new List<int> { 1, 2, 3 };
var deferred = list.Where(x => x > 1);            // belum dijalankan
var snapshot = list.Where(x => x > 1).ToList();   // dijalankan sekarang
list.Add(4);
// deferred -> 2,3,4   snapshot -> 2,3
```

## 4. Important Differences

| Konsep                              | Perbedaan                                                                                                                                                                                                          |
| ----------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| `IEnumerable<T>` vs `IQueryable<T>` | `IEnumerable`: filter dijalankan di memori aplikasi (delegate `Func<T,bool>`). `IQueryable`: lambda jadi expression tree (`Expression<Func<T,bool>>`) dan diterjemahkan menjadi SQL, filter dijalankan di database |
| `Where` vs `Select`                 | `Where` memilih baris (jumlah item berkurang); `Select` mengubah bentuk tiap item (jumlah tetap)                                                                                                                   |
| `g.Key` vs `g.First()`              | `Key` = nilai pengelompok; `First()` = item pertama dalam grup                                                                                                                                                     |
| Deferred vs `ToList()`              | Tanpa `ToList()`, query dijalankan ulang tiap di-enumerate dan mengikuti perubahan sumber data; `ToList()` membekukan hasil                                                                                        |
| `First()` vs `FirstOrDefault()`     | `First()` melempar exception jika kosong; `FirstOrDefault()` mengembalikan `null` (tangani dengan `is null` / `??`)                                                                                                |
| `Count() > 0` vs `Any()`            | Hasil sama; `Any()` lebih jelas dan berhenti saat menemukan item pertama                                                                                                                                           |

## 5. Common Mistakes

- **Mengira `g.Key` adalah item pertama grup.** `g.Key` adalah nilai yang dipakai untuk mengelompokkan.
- **Mengira query sudah dihitung saat `Where` ditulis.** Query baru jalan saat di-enumerate (deferred execution), jadi data yang ditambahkan setelahnya ikut terbaca.
- **`ToList()` sebelum `Where` pada query EF Core**: seluruh tabel masuk ke memori, baru difilter di aplikasi.
- **Enumerasi berulang atas query yang sama**: pada `IQueryable` setiap enumerasi bisa berarti satu query ke database. Simpan hasil dengan `ToList()` bila dipakai berkali-kali.
- **`First()` pada hasil yang bisa kosong** sehingga melempar `InvalidOperationException`.
- **Lupa menampilkan hasil**: query yang tidak pernah di-enumerate tidak pernah dijalankan.
- **Tidak menanyakan filter pada agregasi** (mis. rata-rata ikut menghitung transaksi Pending/Cancelled padahal laporan hanya butuh `Paid`).
- **Magic string** (`"Paid"`, `"Pending"`) berulang; pada proyek nyata gunakan `enum`.

## 6. Best Practices

- Urutan umum: **`Where` dulu → `OrderBy` → `Take` → `Select`**, supaya data yang diproses sudah sedikit.
- Pada EF Core, lakukan **filter, urut, agregasi, dan pagination sebelum `ToList()`**.
- Pakai `FirstOrDefault()` dan tangani `null` secara eksplisit.
- Gunakan `Any()` untuk cek keberadaan, bukan `Count() > 0`.
- Hindari menjalankan query yang sama dua kali; manfaatkan hasil yang sudah ada.
- Tambahkan `ToList()` hanya ketika memang butuh snapshot atau akan dienumerasi berulang.
- Beri nama hasil proyeksi yang jelas (`CustomerName`, `TotalAmount`) dan format output (`:N0`).
- Gunakan `enum` untuk status pada kode produksi.

## 7. Real-World Application

- Laporan ringkasan: total penjualan per customer atau per kategori (`GroupBy` + `Sum`/`Average`).
- Endpoint API dengan filter, sorting, dan pagination (`Where`, `OrderBy`, `Skip`/`Take`) di EF Core.
- Dashboard: Top N, jumlah per status, pengecekan data tertunda.
- Validasi dan pencarian: `FirstOrDefault` + pengecekan `null`.
- Menggabungkan LINQ dengan pattern matching untuk aturan bisnis (`Status is "Pending" or "Cancelled"`).

## 8. Key Takeaways

1. LINQ menggantikan `foreach` + `if` dengan rantai operator yang ringkas dan mudah dibaca.
2. **`g.Key` adalah nilai pengelompok**, `g` adalah isi grupnya.
3. **Query LINQ ditunda sampai di-enumerate**; `ToList()` membekukan hasil.
4. `IEnumerable` = filter di memori aplikasi; `IQueryable` = lambda jadi expression tree dan dijalankan di database.
5. Pada EF Core, `ToList()` terlalu awal menarik seluruh tabel ke memori: risiko out-of-memory dan performa lambat.
6. Susun query: `Where` → `OrderBy` → `Take` → `Select`.
7. Selalu tangani hasil `null` dari `FirstOrDefault()`.

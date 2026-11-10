# Learning Log

**Tanggal:** 2026-11-04
**Topik:** LINQ Dasar (Materi 4, C# Modern)
**Kategori:** LINQ
**Total Jam Belajar:** 1 jam

## **Yang Dipelajari:**

- Operator dasar LINQ: `Where`, `Select`, `OrderBy`/`OrderByDescending`/`ThenBy`, `Take`, `Distinct`, `FirstOrDefault`.
- Agregasi: `Count`, `Sum`, `Average`, `Max`, `Any`.
- `GroupBy` + agregasi (total per customer, jumlah dan rata-rata per kategori, jumlah per status, top spender).
- Deferred execution: perbedaan hasil query tanpa `ToList()` (`deferred`) dan dengan `ToList()` (`snapshot`).
- `IEnumerable` vs `IQueryable`: filter di memori aplikasi vs diterjemahkan menjadi SQL di database; dampak `ToList()` sebelum `Where` pada EF Core.
- Menggabungkan pattern matching (`is "Pending" or "Cancelled"`) dan null handling (`FirstOrDefault`, `is null`, `?.`, `??`) ke dalam LINQ.
- 10 latihan Sales Analysis diselesaikan dan 3 kalimat penjelasan IEnumerable vs IQueryable ditulis.

## **Kendala:**

- Kuis evaluasi 50% (2 dari 4). Dua konsep keliru: `g.Key` (dikira item pertama grup, padahal nilai pengelompok) dan deferred execution (dikira query sudah dihitung saat `Where` ditulis).
- Konsep diperbaiki setelah review; tebak output deferred vs snapshot dan penjelasan dampak `ToList()` pada 1 juta baris sudah benar.
- Catatan kode: komentar sisa proses, nama record `Sales` sebaiknya `Sale`, query yang sama (latihan 6 dan 9) dijalankan dua kali.

## **Tindakan Berikutnya:**

- Kerjakan Tugas Harian LINQ.
- Lanjut materi async/await (Task, Task.WhenAll, CancellationToken) sesuai rencana Kamis.
- Tandai tugas 11, 12, dan 13 di W1_Checklist setelah commit.

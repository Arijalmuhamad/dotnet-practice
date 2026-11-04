# Learning Log

**Tanggal:** 2026-11-04
**Topik:** Pattern Matching (Materi 3, C# Modern)
**Kategori:** C# Modern
**Total Jam Belajar:** [ISI dengan durasi aktual, mis. 0,5 jam]

## **Yang Dipelajari:**

- Konsep pattern matching dan perbandingannya dengan `if-else` bertingkat.
- Relational pattern (`>=`, `<=`), logical pattern (`and`, `or`, `not`), dan null pattern (`is null`, `is not null`).
- Type pattern pada `object?` dengan `switch expression`.
- Property pattern pada `record` (`{ Status: "Paid", Amount: >= 1000000m }`).
- Aturan _first match wins_: urutan kondisi menentukan hasil.
- Challenge Order Validation diselesaikan: `ValidateOrder` dengan relational, property, dan `or` pattern.
- Evaluasi kuis 4 soal: skor 100%.

## **Kendala:**

- ID order duplikat (`ORD-003` dipakai dua kali) pada data uji challenge sehingga output membingungkan.
- Cabang `"Delivered"` dan `"Unknown status"` belum teruji pada data uji awal.
- Pola `Email: null` belum menangani email berupa string kosong.

## **Tindakan Berikutnya:**

- Perbaiki data uji challenge (ID unik, tambah order `Delivered` dan status tak dikenal).
- Kerjakan Tugas Harian Pattern Matching.
- Lanjut ke Phase 2: LINQ (materi berikutnya).

using System;
using System.Collections.Generic;
using System.Linq;

public class LinqPractice
{
	private record Product(string Name, string Category, decimal Price, int Stock);
	private record Sales(string Id, string Customer, string Category, decimal Amount, string Status);

	public static void Run()
	{
		Console.WriteLine("\n=======================================================");
		Console.WriteLine("===          BUKU CATATAN BELAJAR LINQ              ===");
		Console.WriteLine("=======================================================");

		#region PREPARASI DATA SOURCE
		var products = new List<Product>
		{
			new("Keyboard", "Electronics", 350000m, 10),
			new("Mouse", "Electronics", 150000m, 25),
			new("Monitor", "Electronics", 2000000m, 5),
			new("Notebook", "Stationery", 15000m, 100),
			new("Pen", "Stationery", 5000m, 200),
			new("Desk Lamp", "Furniture", 250000m, 0),
			new("Chair", "Furniture", 1200000m, 8)
		};

		List<Sales> sales = new()
		{
			new("S001", "Duta Sawita", "Elektronik", 1500000, "Paid"),
			new("S002", "Rian Wijaya", "Fashion", 350000, "Pending"),
			new("S003", "Siti Aminah", "Buku", 85000, "Paid"),
			new("S004", "Duta Sawita", "Fashion", 450000, "Cancelled"),
			new("S005", "Rian Wijaya", "Elektronik", 2800000, "Paid"),
			new("S006", "Siti Aminah", "Fashion", 620000, "Pending"),
			new("S007", "Duta Sawita", "Buku", 120000, "Paid"),
			new("S008", "Budi Santoso", "Elektronik", 4950000, "Pending")
		};
		#endregion

		#region [CONTOH MODUL] - DATA PRODUCT
		Console.WriteLine("\n>>> [CONTOH MODUL] DEMO OPERATOR LINQ PADA DATA PRODUCT <<<");

		var expensive = products.Where(p => p.Price >= 1000000m);
		Console.WriteLine("Expensive: " + string.Join(", ", expensive.Select(p => p.Name)));

		var names = products.Select(p => p.Name);
		Console.WriteLine("Names: " + string.Join(", ", names));

		var cheapestFirst = products.OrderBy(p => p.Price).Select(p => p.Name);
		Console.WriteLine("By price asc: " + string.Join(", ", cheapestFirst));

		var byCategoryThenName = products
			.OrderBy(p => p.Category)
			.ThenBy(p => p.Name)
			.Select(p => $"{p.Category}/{p.Name}");
		Console.WriteLine("By category, name: " + string.Join(", ", byCategoryThenName));

		var available = products
			.Where(p => p.Stock > 0)
			.OrderByDescending(p => p.Price)
			.Select(p => new { p.Name, p.Price });

		Console.WriteLine("Available (price desc):");
		foreach (var item in available)
		{
			Console.WriteLine($"  {item.Name}: {item.Price:N0}");
		}

		Console.WriteLine($"Total products: {products.Count}");
		Console.WriteLine($"Out of stock: {products.Count(p => p.Stock == 0)}");
		Console.WriteLine($"Total stock: {products.Sum(p => p.Stock)}");
		Console.WriteLine($"Max price: {products.Max(p => p.Price):N0}");
		Console.WriteLine($"Any out of stock: {products.Any(p => p.Stock == 0)}");

		var summary = products
			.GroupBy(p => p.Category)
			.Select(g => new { Category = g.Key, Count = g.Count(), TotalStock = g.Sum(p => p.Stock), AvgPrice = g.Average(p => p.Price) });

		Console.WriteLine("Summary per category:");
		foreach (var s in summary)
		{
			Console.WriteLine($"  {s.Category}: {s.Count} items, stock {s.TotalStock}, avg price {s.AvgPrice:N0}");
		}

		var numbers = new List<int> { 1, 2, 3 };
		var deferred = numbers.Where(x => x > 1);
		var snapshot = numbers.Where(x => x > 1).ToList();
		numbers.Add(4);
		Console.WriteLine("Deferred: " + string.Join(", ", deferred));
		Console.WriteLine("Snapshot: " + string.Join(", ", snapshot));
		#endregion // PERBAIKAN: Meletakkan penutup region contoh modul di sini

		Console.WriteLine("\n=======================================================");
		Console.WriteLine("===       LEMBAR JAWABAN TUGAS DATA SALES           ===");
		Console.WriteLine("=======================================================");

		#region TUGAS 11: WHERE / SELECT / ORDERBY
		Console.WriteLine("\n>>> [TUGAS 11] WHERE / SELECT / ORDERBY <<<");

		// 1. Penjualan Paid dengan Amount >= 500.000, urut dari terbesar.
		Console.WriteLine("\n1. Penjualan Paid >= 500.000 (Urut Terbesar):");
		var filteredSales = sales
			.Where(s => s.Status == "Paid" && s.Amount >= 500000)
			.OrderByDescending(s => s.Amount);

		foreach (var sale in filteredSales)
		{
			Console.WriteLine($"  -> ID: {sale.Id} | Pelanggan: {sale.Customer} | Total: Rp{sale.Amount:N0}");
		}

		// 2. 3 penjualan Paid terbesar, hanya Id dan Amount (Take).
		Console.WriteLine("\n2. Top 3 Penjualan Paid Terbesar (Hanya ID & Amount):");
		var top3Paid = sales
			.Where(s => s.Status == "Paid")
			.OrderByDescending(s => s.Amount)
			.Take(3)
			.Select(s => new { s.Id, s.Amount });

		foreach (var item in top3Paid)
		{
			Console.WriteLine($"  -> ID: {item.Id} | Amount: Rp{item.Amount:N0}");
		}

		// 3. Daftar customer unik yang punya penjualan Paid, urut abjad (Select + Distinct + OrderBy).
		Console.WriteLine("\n3. Daftar Customer Unik yang Sukses Membayar (Urut Abjad):");
		var uniquePaidCustomers = sales
			.Where(s => s.Status == "Paid")
			.Select(s => s.Customer)
			.Distinct()
			.OrderBy(c => c);

		foreach (var customer in uniquePaidCustomers)
		{
			Console.WriteLine($"  -> Customer: {customer}");
		}

		// 4. Penjualan berstatus Pending atau Cancelled. Gunakan pattern is "Pending" or "Cancelled".
		Console.WriteLine("\n4. Penjualan Bermasalah (Pending / Cancelled) via C# Pattern Matching:");
		var issueSales = sales
			.Where(s => s.Status is "Pending" or "Cancelled");

		foreach (var sale in issueSales)
		{
			Console.WriteLine($"  -> ID: {sale.Id} | Pelanggan: {sale.Customer} | Status: {sale.Status}");
		}

		// 5. Cari penjualan berdasarkan Id dengan FirstOrDefault, lalu tangani hasil null.
		Console.WriteLine("\n5. Pencarian ID via FirstOrDefault & Null Handling:");

		string searchId1 = "S001";
		Sales? foundSale = sales.FirstOrDefault(s => s.Id == searchId1);
		Console.WriteLine(foundSale is null
			? $"  -> Pencarian {searchId1}: Data tidak ditemukan."
			: $"  -> Pencarian {searchId1}: Ditemukan! Pelanggan = {foundSale.Customer}, Total = Rp{foundSale.Amount:N0}");

		string searchId2 = "S999";
		Sales? missingSale = sales.FirstOrDefault(s => s.Id == searchId2);
		string customerName = missingSale?.Customer ?? "Tamu Anonim (Data Null)";
		Console.WriteLine($"  -> Pencarian {searchId2}: Nama Pelanggan = {customerName}");
		#endregion

		#region TUGAS 12: GROUPBY + AGREGASI
		Console.WriteLine("\n>>> [TUGAS 12] GROUPBY + AGREGASI <<<");

		// 6. Total Amount penjualan Paid per customer, urut dari terbesar.
		Console.WriteLine("\n6. Total Amount Paid Per Customer (Urut Terbesar):");
		var customerPaidTotals = sales
			.Where(s => s.Status == "Paid")
			.GroupBy(s => s.Customer)
			.Select(g => new { CustomerName = g.Key, TotalAmount = g.Sum(s => s.Amount) })
			.OrderByDescending(result => result.TotalAmount);

		foreach (var item in customerPaidTotals)
		{
			Console.WriteLine($"  -> Pelanggan: {item.CustomerName,-15} | Total Paid: Rp{item.TotalAmount:N0}");
		}

		// 7. Jumlah transaksi dan rata-rata Amount per category.
		Console.WriteLine("\n7. Jumlah Transaksi & Rata-rata per Kategori:");
		var categoryStats = sales
			.GroupBy(s => s.Category)
			.Select(g => new { CategoryName = g.Key, TotalTransactions = g.Count(), AverageAmount = g.Average(s => s.Amount) })
			.OrderByDescending(result => result.AverageAmount);

		foreach (var item in categoryStats)
		{
			Console.WriteLine($"  -> Kategori: {item.CategoryName,-12} | Jumlah Transaksi: {item.TotalTransactions} | Rata-rata: Rp{item.AverageAmount:N0}");
		}

		// 8. Jumlah transaksi per status.
		Console.WriteLine("\n8. Jumlah Transaksi Per Status:");
		var statusStats = sales
			.GroupBy(s => s.Status)
			.Select(g => new { StatusType = g.Key, Count = g.Count() });

		foreach (var item in statusStats)
		{
			Console.WriteLine($"  -> Status: {item.StatusType,-10} | Total Transaksi: {item.Count}");
		}

		// 9. Customer dengan total Paid tertinggi.
		Console.WriteLine("\n9. Customer Dengan Nilai Pembayaran Terbesar (Top Spender):");
		var topSpender = sales
			.Where(s => s.Status == "Paid")
			.GroupBy(s => s.Customer)
			.Select(g => new { CustomerName = g.Key, TotalPaid = g.Sum(s => s.Amount) })
			.OrderByDescending(x => x.TotalPaid)
			.FirstOrDefault();

		if (topSpender is not null)
		{
			Console.WriteLine($"  -> Top Spender: {topSpender.CustomerName} dengan Total Paid Rp{topSpender.TotalPaid:N0}");
		}

		// 10. Cek apakah ada penjualan Pending dengan Any, tampilkan juga jumlahnya.
		Console.WriteLine("\n10. Pengecekan Keberadaan & Jumlah Penjualan Pending:");
		bool hasPending = sales.Any(s => s.Status == "Pending");
		int pendingCount = sales.Count(s => s.Status == "Pending");

		Console.WriteLine(hasPending
			? $"  -> Ada penjualan Pending: {pendingCount} transaksi."
			: "  -> Tidak ada penjualan Pending.");
		#endregion
	}
}
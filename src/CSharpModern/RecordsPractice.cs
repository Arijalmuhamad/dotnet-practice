
using System;

public class RecordsPractice
{
	public static void Run()
	{
		Console.WriteLine("\n=== 1. RECORDS AND INIT-ONLY ===");

		// ---------------------------------
		// LATIHAN 1: RECORD PRODUCT
		// ---------------------------------

		Console.WriteLine("\n--- Product Record ---");

		// Membuat dua object dengan nilai yang sama
		Product product1 = new Product("Monitor", 2500000);
		Product product2 = new Product("Monitor", 2500000);

		// Membandingkan kedua object
		Console.WriteLine($"Product 1 == Product 2: {product1 == product2}");

		// Membuat salinan dengan perubahan harga
		Product product3 = product1 with
		{
			Price = 2300000
		};

		// Menampilkan object
		Console.WriteLine("\nOriginal Product:");
		Console.WriteLine(product1);

		Console.WriteLine("\nCopied Product:");
		Console.WriteLine(product3);


		// ---------------------------------
		// LATIHAN 2: CLASS CUSTOMER
		// ---------------------------------

		Console.WriteLine("\n--- Customer Class ---");

		Customer customer1 = new Customer
		{
			Name = "Arijal"
		};

		Console.WriteLine($"Customer Name: {customer1.Name}");

		// Coba hilangkan komentar di bawah untuk melihat error compiler:
		// customer1.Name = "Budi";

		// Assignment tersebut tidak diperbolehkan karena
		// Name menggunakan init-only property.


		// ---------------------------------
		// KESIMPULAN: RECORD VS CLASS
		// ---------------------------------

		/*
         * Gunakan RECORD ketika:
         * - Object berfokus pada penyimpanan data.
         * - Perbandingan berdasarkan nilai property.
         * - Membutuhkan kemudahan membuat salinan dengan with.
         *
         * Gunakan CLASS ketika:
         * - Object memiliki behavior atau business logic.
         * - Object memiliki identitas tersendiri.
         * - Membutuhkan kontrol perubahan state dan lifecycle.
         */
	}
}


// Positional Record
public record Product(string Name, decimal Price);


// Class dengan init-only property
public class Customer
{
	public required string Name { get; init; }
}

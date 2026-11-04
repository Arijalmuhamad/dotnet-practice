using System;

public class PatternPractice
{
	private record Transaction(string Id, decimal Amount, string Status, string? Note);

	// Challenge
	private record Order(string Id, decimal Total, string Status, string? Email);

	public static void Run()
	{
		Console.WriteLine("\n=== PATTERN MATCHING ===");

		// 1. Relational pattern dengan switch expression
		int score = 65;
		string grade = score switch
		{
			>= 90 => "A",
			>= 80 => "B",
			>= 70 => "C",
			_ => "D"
		};
		Console.WriteLine($"Score {score} => Grade {grade}");

		// 2. Logical pattern
		int age = 25;
		bool isWorkingAge = age is >= 18 and <= 60;
		Console.WriteLine($"Age {age} working age: {isWorkingAge}");

		string? nickname = null;
		Console.WriteLine($"Nickname is null: {nickname is null}");
		Console.WriteLine($"Nickname is not null: {nickname is not null}");

		// 3. Type pattern
		object?[] items = { 42, "Hello", 3.14, null, true };
		foreach (var item in items)
		{
			Console.WriteLine(Describe(item));
		}

		// 4. Property pattern
		var transactions = new[]
		{
			new Transaction("TRX-001", 1500000m, "Paid", null),
			new Transaction("TRX-002", 250000m, "Paid", "Cash"),
			new Transaction("TRX-003", 0m, "Pending", null),
			new Transaction("TRX-004", 500000m, "Cancelled", "Duplicate"),
			new Transaction("TRX-005", 100000m, "Refunded", null)
		};

		foreach (var trx in transactions)
		{
			Console.WriteLine($"{trx.Id}: {ValidateTransaction(trx)}");
		}



		Console.WriteLine($"==== Challenge mandiri ====");

		var orders = new[]
		{
			new Order("ORD-001", 250000m,"Paid","anggi@gmail.com"),
			new Order("ORD-002", 200000m,"Paid",null),
			new Order("ORD-003", 0m,"Paid",null),
			new Order("ORD-004", 300000m,"Shipped","arijal@gmail.com"),

		};

		foreach (var ord in orders)
		{
			Console.WriteLine($"{ord.Id}: {ValidateOrder(ord)}");
		}

		
	}

	private static string Describe(object? value) => value switch
	{
		null => "null value",
		int n => $"int: {n}",
		string s => $"string: \"{s}\" (length {s.Length})",
		double d => $"double: {d}",
		_ => $"other type: {value.GetType().Name}"
	};

	private static string ValidateTransaction(Transaction trx) => trx switch
	{
		{ Amount: <= 0 } => "Invalid: amount must be greater than 0",
		{ Status: "Paid", Amount: >= 1000000m } => "Paid (high value, needs review)",
		{ Status: "Paid" } => "Paid",
		{ Status: "Pending" } => "Pending",
		{ Status: "Cancelled", Note: not null } => "Cancelled with note",
		{ Status: "Cancelled" } => "Cancelled",
		_ => "Unknown status"
	};

	
	// === CHALLANGE PATTERN ===

	private static string ValidateOrder(Order ord) => ord switch
	{
		{ Total: <= 0 } => "Invalid: total tidak valid",
		{ Status: "Paid", Email: null } => "Paid (email belum tersedia)",
		{ Status: "Paid" } => "Paid",
		{ Status: "Shipped" or "Delivered"} => "On the way / Done",
		_ => "Unknown status"

	};

}
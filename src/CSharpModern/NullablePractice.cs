
using System;

public class NullablePractice
{
	public static void Run()
	{
		Console.WriteLine("\n=== NULLABLE REFERENCE TYPES ===");

		//1.Nullable dan Non-nullable
		//string name = "Arijal";
		//string? nickname = null;

		//Console.WriteLine($"Name: {name}");
		//Console.WriteLine($"Nickname: {nickname ?? "No nickname"}");

		////2.Null checking
		//if (nickname != null)
		//{
		//	Console.WriteLine($"Nickname Length: {nickname.Length}");
		//}
		//else
		//{
		//	Console.WriteLine("Nickname is empty");
		//}

		////3.Null - coalescing
		//string displayName = nickname ?? name;
		//Console.WriteLine($"Display Name: {displayName}");

		////4.Null - conditional
		//int? nicknameLength = nickname?.Length;
		//Console.WriteLine($"Nickname Length: {nicknameLength}");

		// === CHALLANGE NULLABLE ===


		string name = "arijal";
		string? email = null;

		int? emailLength = email?.Length;

		Console.WriteLine($"Customer: {name}");
		Console.WriteLine($"Email: {email ?? "Email belum tersedia"}");

		if (email != null)
		{
			Console.WriteLine($"Email Length: {emailLength}");
		}
		else
		{
			Console.WriteLine("Email Length: Tidak tersedia");
		}



	}
}

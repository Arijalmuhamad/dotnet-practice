# C# Cheat Sheet – Nullable Reference Types

## 1. Nullable vs Non-nullable

```csharp
string name = "Arijal";
string? nickname = null;
```

- `string`: tidak diharapkan bernilai null.
- `string?`: diperbolehkan bernilai null.

## 2. Null Checking

```csharp
if (nickname != null)
{
    Console.WriteLine(nickname.Length);
}
```

Compiler mengetahui nickname tidak null dalam blok if.

## 3. Null-Coalescing Operator (??)

```csharp
string displayName = nickname ?? "Guest";
```

Jika nickname null, gunakan Guest.

## 4. Null-Conditional Operator (?.)

```csharp
int? length = nickname?.Length;
```

- Jika nickname null → hasil null.
- Jika nickname tersedia → hasil Length.
- Hasilnya int? karena bisa null.

## 5. Nullable Value Type

```csharp
int? age = null;
decimal? price = null;
bool? isActive = null;
```

Value type nullable dapat menampung nilai null.

## 6. Key Takeaways

- Nullable Reference Types membantu compiler mendeteksi potensi null.
- `string?` bukan berarti object otomatis dibuat nullable saat runtime.
- `??` digunakan untuk fallback value.
- `?.` digunakan untuk akses member secara null-safe.
- `int?` dapat menyimpan integer atau null.
- Null handling penting untuk API, DTO, dan database.

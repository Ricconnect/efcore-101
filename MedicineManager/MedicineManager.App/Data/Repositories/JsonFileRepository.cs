using System.Text.Json;
using MedicineManager.App.Data.Models;

namespace MedicineManager.App.Data.Repositories;

/// <summary>
/// A file-system backed repository that persists entities as JSON.
/// One JSON file is created per entity type (e.g., medicines.json, intakeschedules.json).
///
/// This is a teaching placeholder — it will be replaced by an EF Core DbContext
/// implementation once Entity Framework Core is introduced.
/// </summary>
public class JsonFileRepository<T> : IRepository<T> where T : class, IEntity
{
    private readonly string _filePath;
    private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };

    public JsonFileRepository(IConfiguration configuration)
    {
        var dataStorePath = configuration["DataStorePath"]
            ?? Path.Combine(Directory.GetCurrentDirectory(), "data");

        Directory.CreateDirectory(dataStorePath);

        _filePath = Path.Combine(dataStorePath, $"{typeof(T).Name.ToLowerInvariant()}s.json");
    }

    private async Task<List<T>> ReadAllAsync()
    {
        if (!File.Exists(_filePath))
            return [];

        var json = await File.ReadAllTextAsync(_filePath);
        return JsonSerializer.Deserialize<List<T>>(json, JsonOptions) ?? [];
    }

    private async Task WriteAllAsync(List<T> entities)
    {
        var json = JsonSerializer.Serialize(entities, JsonOptions);
        await File.WriteAllTextAsync(_filePath, json);
    }

    public async Task<IEnumerable<T>> GetAllAsync() => await ReadAllAsync();

    public async Task<T?> GetByIdAsync(Guid id)
    {
        var all = await ReadAllAsync();
        return all.FirstOrDefault(e => e.Id == id);
    }

    public async Task AddAsync(T entity)
    {
        var all = await ReadAllAsync();
        all.Add(entity);
        await WriteAllAsync(all);
    }

    public async Task UpdateAsync(T entity)
    {
        var all = await ReadAllAsync();
        var index = all.FindIndex(e => e.Id == entity.Id);
        if (index >= 0)
        {
            all[index] = entity;
            await WriteAllAsync(all);
        }
    }

    public async Task DeleteAsync(Guid id)
    {
        var all = await ReadAllAsync();
        all.RemoveAll(e => e.Id == id);
        await WriteAllAsync(all);
    }
}

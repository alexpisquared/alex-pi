
namespace AzureLogParser;

internal class UniMapper
{
  readonly string _filename;
  readonly Dictionary<string, string> _dictionary;

  public UniMapper(string filename)
  {
    _filename = filename;
    _dictionary = File.Exists(_filename) ? JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(_filename)) ?? [] : [];
  }

  internal void AddIfNew(string key, string val)
  {
    if (!_dictionary.ContainsKey(key))
    {
      _dictionary.Add(key, val);
      File.WriteAllText(_filename, JsonSerializer.Serialize(_dictionary, new JsonSerializerOptions { WriteIndented = true }));
    }
  }
  internal async Task UpdateIfDifferentAsync(string key, string val)
  {
    if (_dictionary.TryGetValue(key, out var value) && value != val)
    {
      _dictionary[key] = val;
      await File.WriteAllTextAsync(_filename, JsonSerializer.Serialize(_dictionary, new JsonSerializerOptions { WriteIndented = true }));
    }
  }
  internal string GetOrCreateFromId(string key)
  {
    AddIfNew(key, key);
    return _dictionary[key];
  }

  public async Task<bool> UpdateIfNewAsync(ICollectionView items)
  {
    foreach (var item in items)
    {
      if (item is WebsiteUser user)
      {
        var duplicates = items.Cast<WebsiteUser>().Where(r => r.Nickname == user.Nickname);
        if (duplicates.Count() > 1)
          user.Nickname += "·";

        await UpdateIfDifferentAsync(user.MemberSinceKey, user.Nickname);
      }
      else if (item is EventtGroup ware)
      {
        var duplicates = items.Cast<EventtGroup>().Where(r => r.NickWare == ware.NickWare);
        if (duplicates.Count() > 1)
          ware.NickWare += "·";

        await UpdateIfDifferentAsync(ware.PseudoKey, ware.NickWare);
      }
    }

    return true;
  }
}

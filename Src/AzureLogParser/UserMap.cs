
namespace AzureLogParser;

internal class UserMap
{
  readonly string _filename;
  readonly Dictionary<string, string> _dictionary;

  public UserMap(string filename)
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
  internal async Task UpdateIfDifferentAsync(string key, string val, int displayIndex)
  {
    // displayIndex: 2 for key, 6 for notes ... but where is the full time stored?
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
      if (item is WebsiteUser user) await UpdateIfDifferentAsync(user.MemberSinceKey, user.Nickname, 0);
      else
      if (item is EventtGroup ware) await UpdateIfDifferentAsync(ware.PseudoKey, ware.NickWare, 0);
    }

    return true;
  }
}

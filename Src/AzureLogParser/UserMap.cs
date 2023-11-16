using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AzureLogParser
{
  internal class UserMap
  {
    readonly string _filename;
    readonly Dictionary<string, string> _dictionary;

    public UserMap(string filename = @"C:\Users\alexp\source\repos\alex-pi\AzureLogParser\UserMap.json")
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
    internal void UpdateIfDifferent(string key, string val, int displayIndex)
    {
      // displayIndex: 2 for key, 6 for notes ... but where is the full time stored?
      if (_dictionary.ContainsKey(key) && _dictionary[key]!=val)
      {
        _dictionary[key] = val;
        File.WriteAllText(_filename, JsonSerializer.Serialize(_dictionary, new JsonSerializerOptions { WriteIndented = true }));
      }
    }
    internal string GetOrCreateUsernameFromId(string key)
    {
      AddIfNew(key, key);
      return _dictionary[key];
    }
  }
}

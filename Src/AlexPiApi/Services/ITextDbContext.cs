using System.Threading.Tasks;

namespace AlexPiApi.Services;

public interface ITextDbContext
{
  Task AddStringAsync(string text);
  string Audit();
}

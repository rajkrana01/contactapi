// Services/ContactService.cs
using contactapi.Models;
using System.Text.Json;

namespace ContactsApi.Services
{
    public class ContactService : IContactService
    {
        private readonly string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "contacts.json");
        private List<Contact> _contacts = new();

        public ContactService()
        {
            if (File.Exists(_filePath))
            {
                var json = File.ReadAllText(_filePath);
                _contacts = JsonSerializer.Deserialize<List<Contact>>(json) ?? new List<Contact>();
            }
            else
            {
                Directory.CreateDirectory(Path.GetDirectoryName(_filePath)!);
                File.WriteAllText(_filePath, JsonSerializer.Serialize(_contacts));
            }
        }

        public async Task<IEnumerable<Contact>> GetAllAsync()
        {
            return await Task.FromResult(_contacts);
        }

        public async Task<Contact?> GetByIdAsync(int id)
        {
            var contact = _contacts.FirstOrDefault(c => c.ID == id);
            return await Task.FromResult(contact);
        }

        public async Task<Contact> CreateAsync(Contact contact)
        {
            contact.ID = _contacts.Any() ? _contacts.Max(c => c.ID) + 1 : 1;
            _contacts.Add(contact);
            await SaveChangesAsync();
            return contact;
        }

        public async Task<bool> UpdateAsync(int id, Contact contact)
        {
            var existing = _contacts.FirstOrDefault(c => c.ID == id);
            if (existing == null)
                return false;

            existing.FirstName = contact.FirstName;
            existing.LastName = contact.LastName;
            existing.Email = contact.Email;

            await SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var contact = _contacts.FirstOrDefault(c => c.ID == id);
            if (contact == null)
                return false;

            _contacts.Remove(contact);
            await SaveChangesAsync();
            return true;
        }

        private async Task SaveChangesAsync()
        {
            var json = JsonSerializer.Serialize(_contacts, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_filePath, json);
        }
    }
}

using System.Threading.Tasks;
using Back_GroceryToDo.Models;

namespace Back_GroceryToDo.Data
{
    public interface IRecordsService
    {
        Task<Record> GetRecordByIdAsync(int id);
        Task<bool> AddItemToRecordAsync(Item item);
        Task<bool> RemoveItemFromRecordAsync(int itemId);
        public Task<bool> WipeRecordAsync();
    }
}
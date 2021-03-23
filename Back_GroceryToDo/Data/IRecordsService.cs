using System.Threading.Tasks;
using Back_GroceryToDo.Models;

namespace Back_GroceryToDo.Data
{
    public interface IRecordsService
    {
        Task<Record> GetRecordByIdAsync(int id);
        Task<Item> AddItemToRecordAsync(Item item, int recordId);
        Task<Item> UpdateItemInRecordAsync(Item item, int recordId);
        Task RemoveItemFromRecordAsync(int itemId, int recordId);
        public Task<bool> WipeRecordAsync();
    }
}
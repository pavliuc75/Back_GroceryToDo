using System.Threading.Tasks;
using Back_GroceryToDo.Models;

namespace Back_GroceryToDo.Data
{
    public interface IRecordsService
    {
        Task<Record> GetRecordByIdAsync(int id);
        Task<Record> CreateRecordAsync();
        Task<Item> AddItemToRecordAsync(Item item, int recordId);
        Task<Item> UpdateItemInRecordAsync(Item item, int recordId);
        Task UpdateRecordDescriptionAsync(int recordId, string description);
        Task RemoveItemFromRecordAsync(int itemId, int recordId);
        Task WipeRecordAsync(int recordId);
    }
}
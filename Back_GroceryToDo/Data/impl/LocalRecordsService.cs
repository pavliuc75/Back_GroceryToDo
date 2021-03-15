using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Back_GroceryToDo.Models;

namespace Back_GroceryToDo.Data.impl
{
    public class LocalRecordsService : IRecordsService
    {
        private string recordsFile = "records.json";
        private IList<Record> records;

        public LocalRecordsService()
        {
            if (!File.Exists(recordsFile))
            {
                Seed();
                WriteRecordsToFile();
            }
            else
            {
                string content = File.ReadAllText(recordsFile);
                records = JsonSerializer.Deserialize<IList<Record>>(content);
            }
        }

        private void WriteRecordsToFile()
        {
            string recordsAsJson = JsonSerializer.Serialize(records);
            File.WriteAllText(recordsFile, recordsAsJson);
        }

        private void Seed()
        {
            Item i0 = new Item() {Id = 0, Name = "Milk", Quantity = 2};
            Item i1 = new Item() {Id = 1, Name = "Chocolate", Quantity = 3};
            Item i2 = new Item() {Id = 2, Name = "Chips"};
            Record r1000 = new Record() {Id = 1000};
            Record r1001 = new Record() {Id = 1001};
            Record r9999 = new Record() {Id = 9999};
            r9999.Items = new List<Item>() {i0, i1, i2};
            Record[] rs =
            {
                r1000, r1001, r9999
            };
            records = rs.ToList();
        }

        public async Task<Record> GetRecordByIdAsync(int id)
        {
            try
            {
                Record record = records.First(r => r.Id == id);
                return record;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<Item> AddItemToRecordAsync(Item item, int recordId)
        {
            Record record = await GetRecordByIdAsync(recordId);
            int recordIndex = records.IndexOf(record);
            int max = record.Items.Max(i => i.Id);
            item.Id = (++max);
            record.Items.Add(item);
            records[recordIndex] = record;
            WriteRecordsToFile();
            return item;
        }

        public async Task<bool> RemoveItemFromRecordAsync(int itemId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> WipeRecordAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
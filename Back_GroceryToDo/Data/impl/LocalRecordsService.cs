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
            Record r1002 = new Record() {Id = 9999};
            r1002.Items = new List<Item>() {i0, i1, i2};
            Record[] rs =
            {
                r1000, r1001, r1002
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

        public async Task<Record> CreateRecordAsync()
        {
            Record record;
            if (records == null || !records.Any())
            {
                records = new List<Record>();
                record = new Record {Id = 1000};
            }
            else
            {
                int max = records.Max(i => i.Id);
                record = new Record {Id = (++max)};
            }

            record.Items = new List<Item>();
            records.Add(record);
            WriteRecordsToFile();
            return record;
        }

        public async Task<Item> AddItemToRecordAsync(Item item, int recordId)
        {
            Record record = await GetRecordByIdAsync(recordId);
            int recordIndex = records.IndexOf(record);
            if (record.Items == null || !record.Items.Any())
            {
                record.Items = new List<Item>();
                item.Id = 1;
            }
            else
            {
                int max = record.Items.Max(i => i.Id);
                item.Id = (++max);
            }

            record.Items.Add(item);
            records[recordIndex] = record;
            WriteRecordsToFile();
            return item;
        }

        public async Task<Item> UpdateItemInRecordAsync(Item item, int recordId)
        {
            Record record = await GetRecordByIdAsync(recordId);
            int recordIndex = records.IndexOf(record);
            Item oldItem = record.Items.FirstOrDefault(i => i.Id == item.Id);
            int itemIndex = record.Items.IndexOf(oldItem);
            record.Items[itemIndex] = item;
            records[recordIndex] = record;
            WriteRecordsToFile();
            return item;
        }

        public async Task UpdateRecordDescriptionAsync(int recordId, string description)
        {
            Record record = await GetRecordByIdAsync(recordId);
            int recordIndex = records.IndexOf(record);
            record.Description = description;
            records[recordIndex] = record;
            WriteRecordsToFile();
        }

        public async Task RemoveItemFromRecordAsync(int itemId, int recordId)
        {
            Record record = await GetRecordByIdAsync(recordId);
            int recordIndex = records.IndexOf(record);
            Item toBeRemoved = record.Items.FirstOrDefault(i => i.Id == itemId);
            record.Items.Remove(toBeRemoved);
            records[recordIndex] = record;
            WriteRecordsToFile();
        }


        public async Task WipeRecordAsync(int recordId)
        {
            Record record = await GetRecordByIdAsync(recordId);
            int recordIndex = records.IndexOf(record);
            record.Items.Clear();
            records[recordIndex] = record;
            WriteRecordsToFile();
        }
    }
}
﻿using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Back_GroceryToDo.Models
{
    public class Record
    {
        [JsonPropertyName("id")] public int Id { get; set; }
        [JsonPropertyName("items")] public List<Item> Items { get; set; }
    }
}
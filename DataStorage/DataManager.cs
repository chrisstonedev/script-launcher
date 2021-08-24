using System;
using System.Collections.Generic;
using ScriptLauncher.Models;

namespace ScriptLauncher.DataStorage
{
    public static class DataManager
    {
        public static List<ChartModel> GetData()
        {
            var random = new Random();
            return new List<ChartModel>
            {
                new() { Data = new List<int> { random.Next(1, 40) }, Label = "Data1" },
                new() { Data = new List<int> { random.Next(1, 40) }, Label = "Data2" },
                new() { Data = new List<int> { random.Next(1, 40) }, Label = "Data3" },
                new() { Data = new List<int> { random.Next(1, 40) }, Label = "Data4" }
            };
        }
    }
}
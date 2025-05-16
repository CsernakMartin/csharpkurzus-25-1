using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace projekt
{
    class Save
    {
        public Save(Player p)
        {
            DataForSave player = new DataForSave
            {
                Name = p.Name,
                Points = p.Points,
                Damage = p.Damage,
                HP = p.HP
            };

            try
            {
                string filePath = "playerPoints.json";
                List<DataForSave> saveList = new List<DataForSave>();

                if (File.Exists(filePath))
                {
                    string existingJson = File.ReadAllText(filePath);
                    Console.WriteLine(existingJson);
                    if (!string.IsNullOrWhiteSpace(existingJson))
                    {
                        saveList = JsonSerializer.Deserialize<List<DataForSave>>(existingJson) ?? new List<DataForSave>();
                    }
                }
                int existingIndex = saveList.FindIndex(s => s.Name == player.Name);

                if (existingIndex >= 0)
                {
                    saveList[existingIndex] = player; 
                    Console.WriteLine("Mentés frissítve!");
                }
                else
                {
                    saveList.Add(player); 
                    Console.WriteLine("Mentés hozzáadva!");
                }

                string jsonString = JsonSerializer.Serialize(saveList, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hiba a mentés során: " + ex.Message);
            }
        }
    }
}

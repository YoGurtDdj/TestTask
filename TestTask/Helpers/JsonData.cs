﻿using Microsoft.AspNetCore.Http.HttpResults;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TestTask.Helpers
{
    public static class JsonData
    {
        public static readonly string jsonPath = "data.json";
        public static readonly string resultJsonPath = "result.json";
        public static void Serialize<T>(List<T> data)
        {
            Logger.WriteToLog("Writing filtered data to a result.json");
            Logger.WriteToLog("Json serialization...");

            try
            {
                string jsonString = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(resultJsonPath, jsonString);
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("Exception during serialization" + ex);
                throw new Exception("Произошла ошибка при сериализации: " + ex.Message);
            }
            finally
            {
                Logger.WriteToLog("Serialization complete");
            }
            
        }
        public static List<T> Desirialize<T>()
        {
            Logger.WriteToLog("Getting data from data.json");
            Logger.WriteToLog("Json deserialization...");

            string jsonString = File.ReadAllText(jsonPath);
            List<T> data;

            try
            {
                data = JsonSerializer.Deserialize<List<T>>(jsonString);

                if (data == null || data.Count == 0)
                {
                    Logger.WriteToLog("No data or not found");
                    throw new Exception("Нет данных или они не найдены. ");
                }
            }
            catch(JsonException ex)
            {
                Logger.WriteToLog("Exception during deserialization" + ex);
                throw new Exception("Ошибка при десериализации. Проверьте корректность введенных данных: " + ex.Message);
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("Exception:" + ex);
                throw new Exception("Ошибка: " + ex.Message);
            }

            Logger.WriteToLog("Deserialization complete");
            return data;
        }
    }
}
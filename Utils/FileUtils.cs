﻿using System.Text.Json;
using task_cli.Model;

namespace task_cli.Utils
{
	internal class FileUtils
	{
		private static readonly string fileName = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\task-cli\\tasklist.json";

		private static readonly string weatherConfig = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\task-cli\\tasklist.json";

		private static readonly JsonSerializerOptions opt = new JsonSerializerOptions()
		{
			WriteIndented = true
		};

		/// <summary>
		/// A function to create a file for persisting the list stored in the program, also can be used to wipe the file per requested
		/// </summary>
		internal static void checkAndCreateDataFile(bool clear)
		{
			Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\task-cli");
			// Check whether a file exists, if it does NOT exists, create the file in the current user folder
			// Or if user requested that the file will be cleared, clear everything in the file
			if (File.Exists(fileName) == false || clear == true)
			{
				File.Open(fileName, FileMode.Create).Dispose();
			}
		}

		/// <summary>
		/// A function for saving the list into the created json file for persistency
		/// </summary>
		/// <param name="tasksList">The list need to be saved</param>
		internal static void WriteJsonFile(List<Tasks> tasksList)
		{
			var json = JsonSerializer.Serialize(tasksList, opt);
			File.WriteAllText(fileName, json);
		}

		/// <summary>
		/// A function to dump the saved json file into the app list
		/// </summary>
		/// <returns>The dumped list from the json file</returns>
		internal static List<Tasks>? ReadJsonFile()
		{
			string json = File.ReadAllText(fileName);
			return json == "" || json == "[]" ? new List<Tasks>() : JsonSerializer.Deserialize<List<Tasks>>(json);
		}

		internal static void writeWeatherConfig(WeatherConfig config)
		{
			var json = JsonSerializer.Serialize(config, opt);
			File.WriteAllText(weatherConfig, json);
		}

		internal static WeatherConfig getWeatherConfig()
		{
			string json = File.ReadAllText(weatherConfig);
			return JsonSerializer.Deserialize<WeatherConfig>(json)!;
		}
	}
}

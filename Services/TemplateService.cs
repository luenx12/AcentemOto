using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace AcentemOto.Services
{
    public class TemplateService
    {
        private readonly string _filePath;

        public TemplateService()
        {
            string dir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "AcentemOto");
            Directory.CreateDirectory(dir);
            _filePath = Path.Combine(dir, "templates.json");
        }

        public Dictionary<string, string> LoadTemplates()
        {
            if (!File.Exists(_filePath))
                return new Dictionary<string, string>();

            try
            {
                string json = File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<Dictionary<string, string>>(json)
                       ?? new Dictionary<string, string>();
            }
            catch { return new Dictionary<string, string>(); }
        }

        public void SaveTemplate(string name, string content)
        {
            var templates = LoadTemplates();
            templates[name] = content;
            File.WriteAllText(_filePath, JsonSerializer.Serialize(templates,
                new JsonSerializerOptions { WriteIndented = true }));
        }

        public void DeleteTemplate(string name)
        {
            var templates = LoadTemplates();
            templates.Remove(name);
            File.WriteAllText(_filePath, JsonSerializer.Serialize(templates,
                new JsonSerializerOptions { WriteIndented = true }));
        }
    }
}

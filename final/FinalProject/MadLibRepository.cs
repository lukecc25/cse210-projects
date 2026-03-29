using System;
using System.Collections.Generic;
using System.IO;

public class MadLibRepository
{
    private string _baseDirectory;

    public MadLibRepository(string baseDirectory)
    {
        _baseDirectory = baseDirectory ?? "templates";
        if (!Directory.Exists(_baseDirectory))
            Directory.CreateDirectory(_baseDirectory);
    }

    public void SaveTemplate(MadLibTemplate template, string filename)
    {
        string path = Path.Combine(_baseDirectory, filename);
        File.WriteAllText(path, template.GetStringRepresentation());
    }

    public MadLibTemplate LoadTemplate(string filename)
    {
        string path = Path.Combine(_baseDirectory, filename);
        if (!File.Exists(path))
            return null;

        string content = File.ReadAllText(path);
        string sep = Environment.NewLine + "---" + Environment.NewLine;
        string[] parts = content.Split(new[] { sep }, StringSplitOptions.None);
        if (parts.Length >= 2)
            return new MadLibTemplate(parts[0], parts[1]);
        return new MadLibTemplate(filename, content);
    }

    public List<string> ListTemplateFiles()
    {
        List<string> names = new List<string>();
        if (!Directory.Exists(_baseDirectory))
            return names;
        foreach (string file in Directory.GetFiles(_baseDirectory, "*.txt"))
            names.Add(Path.GetFileName(file));
        return names;
    }
}

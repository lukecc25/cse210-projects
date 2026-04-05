using System;
using System.Collections.Generic;
using System.IO;

public class MadLibRepository
{
    private string _baseDirectory;

    public MadLibRepository(string baseDirectory)
    {
        _baseDirectory = baseDirectory ?? "templates";
        if (!Path.IsPathRooted(_baseDirectory))
            _baseDirectory = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), _baseDirectory));
        if (!Directory.Exists(_baseDirectory))
            Directory.CreateDirectory(_baseDirectory);
    }

    public string GetBaseDirectory()
    {
        return _baseDirectory;
    }

    public void SaveTemplate(MadLibTemplate template, string filename)
    {
        if (template == null)
            throw new ArgumentNullException(nameof(template));
        if (string.IsNullOrWhiteSpace(filename))
            throw new ArgumentException("Filename cannot be empty.", nameof(filename));

        if (!filename.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
            filename += ".txt";

        string path = Path.Combine(_baseDirectory, Path.GetFileName(filename));
        File.WriteAllText(path, template.GetStringRepresentation());
    }

    public MadLibTemplate LoadTemplate(string filename)
    {
        if (string.IsNullOrWhiteSpace(filename))
            return null;

        if (!filename.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
            filename += ".txt";

        string path = Path.Combine(_baseDirectory, Path.GetFileName(filename));
        if (!File.Exists(path))
            return null;

        string content = File.ReadAllText(path);
        return ParseSavedContent(content, Path.GetFileNameWithoutExtension(path));
    }

    public static MadLibTemplate ParseSavedContent(string content, string fallbackTitle)
    {
        if (string.IsNullOrEmpty(content))
            return new MadLibTemplate(fallbackTitle ?? "Untitled", "");

        string sep = Environment.NewLine + "---" + Environment.NewLine;
        int idx = content.IndexOf(sep, StringComparison.Ordinal);
        if (idx < 0)
        {
            sep = "\n---\n";
            idx = content.IndexOf(sep, StringComparison.Ordinal);
        }
        if (idx < 0)
            return new MadLibTemplate(fallbackTitle ?? "Imported", content);

        string title = content.Substring(0, idx).TrimEnd('\r', '\n');
        string body = content.Substring(idx + sep.Length);
        return new MadLibTemplate(string.IsNullOrEmpty(title) ? fallbackTitle : title, body);
    }

    public List<string> ListTemplateFiles()
    {
        List<string> names = new List<string>();
        if (!Directory.Exists(_baseDirectory))
            return names;
        foreach (string file in Directory.GetFiles(_baseDirectory, "*.txt"))
            names.Add(Path.GetFileName(file));
        names.Sort(StringComparer.OrdinalIgnoreCase);
        return names;
    }
}

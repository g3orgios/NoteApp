using System;
using System.Collections.Generic;
using System.IO;

class Note
{
    public string Title { get; set; }
    public string Body { get; set; }
}

class NoteApp
{
    private List<Note> notes;
    private string filename;

    public NoteApp(string filename)
    {
        this.notes = new List<Note>();
        this.filename = filename;
    }

    public void Run()
    {
        Console.WriteLine("Welcome to NoteApp!");

        LoadNotes();

        while (true)
        {
            Console.WriteLine("Enter '1' to create a new note, '2' to view existing notes, '3' to save notes to a file, or '4' to exit:");
            string input = Console.ReadLine();

            if (input == "1")
            {
                Note note = new Note();

                Console.WriteLine("Enter a title for your note:");
                note.Title = Console.ReadLine();

                Console.WriteLine("Enter the body of your note:");
                note.Body = Console.ReadLine();

                notes.Add(note);
            }
            else if (input == "2")
            {
                foreach (Note note in notes)
                {
                    Console.WriteLine($"Title: {note.Title}");
                    Console.WriteLine($"Body: {note.Body}");
                    Console.WriteLine();
                }
            }
            else if (input == "3")
            {
                SaveNotes();
            }
            else if (input == "4")
            {
                SaveNotes();
                Console.WriteLine("Goodbye!");
                break;
            }
            else
            {
                Console.WriteLine("Invalid input. Please try again.");
            }
        }
    }

    private void LoadNotes()
    {
        if (File.Exists(filename))
        {
            using (StreamReader reader = new StreamReader(filename))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Note note = new Note();
                    note.Title = line;
                    note.Body = reader.ReadLine();
                    notes.Add(note);
                }
            }

            Console.WriteLine($"Loaded {notes.Count} notes from {filename}.");
        }
        else
        {
            Console.WriteLine($"Could not find file {filename}. Starting with no notes.");
        }
    }

    private void SaveNotes()
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (Note note in notes)
            {
                writer.WriteLine(note.Title);
                writer.WriteLine(note.Body);
            }
        }

        Console.WriteLine($"Notes saved to {filename}.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        NoteApp app = new NoteApp("notes.txt");
        app.Run();
    }
}

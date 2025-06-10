using System;
using System.Collections.Generic;

public class Comment
{
    public string Name;
    public string Text;

    public Comment(string name, string text)
    {
        Name = name;
        Text = text;
    }
}

public class Video
{
    public string Title;
    public string Author;
    public int Length;
    public List<Comment> Comments;

    public Video()
    {
        Comments = new List<Comment>();
    }

    public void AddComment(Comment comment)
    {
        Comments.Add(comment);
    }

    public int CountComments()
    {
        return Comments.Count;
    }

    public void DisplayInfo()
    {
        Console.WriteLine("Title: " + Title);
        Console.WriteLine("Author: " + Author);
        Console.WriteLine("Length: " + Length + " seconds");
        Console.WriteLine("Number of Comments: " + CountComments());
        Console.WriteLine("Comments:");
        foreach (Comment comment in Comments)
        {
            Console.WriteLine("- " + comment.Name + ": " + comment.Text);
        }
        Console.WriteLine();
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World! This is the YouTube Videos Project.\n");

        Video video1 = new Video();
        video1.Title = "How to Bake Bread";
        video1.Author = "Mike's Kitchen";
        video1.Length = 300;
        video1.AddComment(new Comment("John", "Yummy!"));
        video1.AddComment(new Comment("Anna", "Easy to follow."));
        video1.AddComment(new Comment("Siviwe", "Thanks!"));

        Video video2 = new Video();
        video2.Title = "Stick Shift Driving for Beginners";
        video2.Author = "DrivingExpert";
        video2.Length = 450;
        video2.AddComment(new Comment("Sam", "Great lesson!"));
        video2.AddComment(new Comment("Siyamthanda", "I learned a lot."));
        video2.AddComment(new Comment("Lela", "Nice video."));

        Video video3 = new Video();
        video3.Title = "Learn C# Programming";
        video3.Author = "W3Schools";
        video3.Length = 600;
        video3.AddComment(new Comment("Alice", "Very informative!"));
        video3.AddComment(new Comment("Bob", "I love C#!"));
        video3.AddComment(new Comment("Charlie", "Great tutorial!"));

        List<Video> videos = new List<Video>();
        videos.Add(video1);
        videos.Add(video2);
        videos.Add(video3);

        foreach (Video video in videos)
        {
            video.DisplayInfo();
        }
    }
}

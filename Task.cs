// See https://aka.ms/new-console-template for more information
using System;
using System.Runtime.CompilerServices;


Task task1 = Task1();
Task task2 = Task2();
Dosomething("M", ConsoleColor.White);
Console.ReadKey();



// Task Run
static async Task<int> UpdateList()
{

    Task<int> task = Task.Run(async() =>
    {
        Console.WriteLine("tr1");
        await Task.Delay(1000);
        Console.WriteLine("tr2");
        return 1;
    });
    await task;
    Console.WriteLine("tr3");
    return task.Result;   
}

// Task
static async Task Task1 ()
{
    Dosomething("\tT1", ConsoleColor.Green);
    await Task.Delay(100);
    Task task1 = new Task(() =>
    {
        Dosomething("\tT1", ConsoleColor.Green);
    });
    //task1.Start();
    //await task1;
    Console.WriteLine("\tT1 Done");
} 
static Task Task2()
{
    Task task2 = new Task(() =>
    {
        Dosomething("\t\tT2",ConsoleColor.Red);
    });
    task2.Start();
    return task2;
}
static void Dosomething(string t, ConsoleColor color)
{
    for (int i = 0; i < 5; i++)
    {
        lock (Console.Out)
        {
            Console.ForegroundColor = color;
            Console.WriteLine($"{t}: {i}");
            Console.ResetColor();
            Task.Delay(1000).Wait();
        }

    }
}


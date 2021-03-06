﻿using BlazorLife.Game;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace SpectreLife.ConsoleClient
{
    class Program
    {
        private static GameService _gameService;
        private static Timer _timer;

        const int GenerationTime = 100;
        const int PixelSize = 2;
        const int Padding = 5;


        static void Main(string[] args)
        {
            IEnumerable<LifeInstance> creature = CreateCreatureFromInput();

            _gameService = new GameService();
            _timer = new Timer();
            _timer.Elapsed += GenerationTick;
            _timer.Interval = GenerationTime;

            _gameService.AddLife(creature);

            AnsiConsole.Console.Clear(true);
            _timer.Start();

            AnsiConsole.Cursor.Hide();
            Console.Read();
        }

        private static void GenerationTick(object sender, ElapsedEventArgs e)
        {
            _gameService.CreateNextGeneration();
            DrawCurrentGeneration();
        }

        private static void DrawCurrentGeneration()
        {
            AnsiConsole.Cursor.Hide();

            var canvas = new Canvas((AnsiConsole.Width / PixelSize) - Padding, Console.WindowHeight - Padding);
            canvas.PixelWidth = PixelSize;

            // fill background of canvas
            for (int x = 0; x < canvas.Width; x++)
                for (int y = 0; y < canvas.Height; y++)
                    canvas.SetPixel(x, y, Color.NavajoWhite1);

            // draw the current generation of life
            foreach (var life in _gameService.AllLife)
            {
                if (life.X >= 0 && life.X < canvas.Width && life.Y >= 0 && life.Y < canvas.Height)
                    canvas.SetPixel(life.X, life.Y, Color.Red3_1);
            }

            AnsiConsole.Render(canvas);

            Console.CursorTop = 0;
            Console.CursorLeft = 0;
        }

        private static IEnumerable<LifeInstance> CreateCreatureFromInput()
        {
            IEnumerable<LifeInstance> creature;
            var pattern = AnsiConsole.Prompt(
                new TextPrompt<string>("Render glider or infinite pattern?")
                    .InvalidChoiceMessage("[red]That's not a valid pattern[/]")
                    .DefaultValue("glider")
                    .AddChoice("glider")
                    .AddChoice("infinite"));
            var xOffest = AnsiConsole.Prompt(
                new TextPrompt<int>("x offset?")
                    .Validate(x =>
                    {
                        return x switch
                        {
                            < 0 => ValidationResult.Error("[red]Too low[/]"),
                            _ => ValidationResult.Success(),
                        };
                    }));
            var yOffest = AnsiConsole.Prompt(
                new TextPrompt<int>("y offset?")
                    .Validate(y =>
                    {
                        return y switch
                        {
                            < 0 => ValidationResult.Error("[red]Too low[/]"),
                            _ => ValidationResult.Success(),
                        };
                    }));

            if (pattern == "glider")
                creature = Creatures.CreateGlider(xOffest, yOffest);
            else
                creature = Creatures.CreateInfinite(xOffest, yOffest);
            return creature;
        }
    }
}

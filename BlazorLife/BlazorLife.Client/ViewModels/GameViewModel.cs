using BlazorLife.Client.Interop;
using BlazorLife.Game;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace BlazorLife.Client.ViewModels
{
    public enum Tool
    {
        Cell,
        Glider,
        Infinite
    }

    public class GameViewModel
    {
        const int SleepTimeBetweenGenerations = 10;
        const int CanvasCellSize = 5;
     
        private Timer _timer;
        private GameService _gameService;
        private JavascriptService _javascriptService;
        private Stopwatch _watch;

        public bool IsRunning { get; set; }
        public Tool SelectedTool;

        public event EventHandler StateChanged;

        public int NumberOfLiveCells {
            get
            {
                return _gameService.AllLife.Count;
            }
        }

        public int CurrentGeneration
        {
            get
            {
                return _gameService.CurrentGenerationNumber;
            }
        }

        public long AverageGenerationTime { get; private set; }

        public GameViewModel(GameService gameService, JavascriptService javascriptService)
        {
            _gameService = gameService;
            _javascriptService = javascriptService;

            _timer = new Timer();
            _timer.Interval = SleepTimeBetweenGenerations;
            _timer.Elapsed += TimerElapsed;

            _watch = new Stopwatch();
            
            SelectedTool = Tool.Cell;

            Console.WriteLine("GameViewModel created");
        }

        public void Init()
        {
            _javascriptService.AddCanvasMouseEvent();
            DrawCurrentGeneration();
        }

        protected void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            StepOneGeneration();
            DrawCurrentGeneration();
            AverageGenerationTime = _watch.ElapsedMilliseconds / this.CurrentGeneration;
        }

        public void AddLife(int xCoordinate, int yCoordinate)
        {
            // Translate from UI coordinates to game model coordinate space
            int x = xCoordinate / CanvasCellSize;
            int y = yCoordinate / CanvasCellSize;

            switch (SelectedTool)
            {
                case Tool.Cell:
                    _gameService.AddLife(x, y);
                    break;
                case Tool.Glider:
                    _gameService.AddLife(Creatures.CreateGlider(x, y));
                    break;
                case Tool.Infinite:
                    _gameService.AddLife(Creatures.CreateInfinite(x, y));
                    break;
            }
            
            DrawCurrentGeneration();
        }

        public void StepOneGeneration()
        {
            _gameService.CreateNextGeneration();
            DrawCurrentGeneration();

            StateChanged?.Invoke(this, EventArgs.Empty);
        }

        public void Start()
        {
            IsRunning = true;
            _timer.Enabled = true;
            _watch.Start();
        }

        public void Stop()
        {
            IsRunning = false;
            _timer.Enabled = false;
            _watch.Stop();
        }

        public void Reset()
        {
            Stop();
            _gameService.Reset();
            _javascriptService.ClearCanvas();
            _gameService.AddLife(Creatures.CreateGlider(10, 10));

            _watch.Reset();
            DrawCurrentGeneration();
        }

        private void DrawCurrentGeneration()
        {
            // translate game model coordinates to UI coordinate space
            var xCoordinates = new List<int>(_gameService.AllLife.Select(life => life.X * CanvasCellSize)).ToList();
            var yCoordinates = new List<int>(_gameService.AllLife.Select(life => life.Y * CanvasCellSize)).ToList();

            _javascriptService.ClearCanvas();
            _javascriptService.DrawCellsOnCanvas(xCoordinates, yCoordinates, CanvasCellSize);
        }


    }
}

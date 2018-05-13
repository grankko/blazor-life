using BlazorLife.Client.Interop;
using BlazorLife.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace BlazorLife.Client.ViewModels
{
    public enum Tool
    {
        Cell,
        Glider
    }

    public class GameViewModel
    {
        const int SleepTimeBetweenGenerations = 100;
        const int CanvasCellSize = 5;
     
        private Timer _timer;
        private GameService _gameService;

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

        public GameViewModel(GameService gameService)
        {
            _gameService = gameService;
            _timer = new Timer();
            _timer.Interval = SleepTimeBetweenGenerations;
            _timer.Elapsed += TimerElapsed;

            _gameService.AddLife(Creatures.CreateGlider(10, 10));
            SelectedTool = Tool.Cell;
        }

        public void Init()
        {
            CanvasFunctions.AddCanvasMouseEvent();
            DrawCurrentGeneration();
        }

        protected void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            StepOneGeneration();
            DrawCurrentGeneration();
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
        }

        public void Stop()
        {
            IsRunning = false;
            _timer.Enabled = false;
        }

        public void Reset()
        {
            Stop();
            _gameService.Reset();
            CanvasFunctions.ClearCanvas();
            _gameService.AddLife(Creatures.CreateGlider(10, 10));

            DrawCurrentGeneration();
        }

        private void DrawCurrentGeneration()
        {
            // translate game model coordinates to UI coordinate space
            var xCoordinates = new List<int>(_gameService.AllLife.Select(life => life.X * CanvasCellSize)).ToList();
            var yCoordinates = new List<int>(_gameService.AllLife.Select(life => life.Y * CanvasCellSize)).ToList();

            CanvasFunctions.ClearCanvas();
            CanvasFunctions.DrawCellsOnCanvas(xCoordinates, yCoordinates, CanvasCellSize);
        }


    }
}

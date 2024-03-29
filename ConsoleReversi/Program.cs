﻿using GreenTeaReversi;
using GreenTeaReversiAIBots;

internal class Program
{
    private const int ComputerThinkingDelayMilliseconds = 1500;

    private static void Main(string[] args)
    {
        var game = new ReversiGame();

        var whitePlayerType = GetPlayerType(PlayerColor.White);
        var blackPlayerType = GetPlayerType(PlayerColor.Black);

        do
        {
            DisplayBoard(game);

            var currentPlayerType = game.CurrentPlayerColor == PlayerColor.White ? whitePlayerType : blackPlayerType;

            var move = currentPlayerType == PlayerType.Human ? GetHumanMove(game.CurrentPlayerColor) : GetAIMove(game);

            if(currentPlayerType == PlayerType.Computer)
            {
                Console.WriteLine($"{game.CurrentPlayerColor} is thinking...");
                Thread.Sleep(ComputerThinkingDelayMilliseconds);
            }

            var results = game.PlaceCurrentPlayerDisk(move);

            foreach(var result in results)
            {
                Console.WriteLine(result);
            }

        } while (game.GameInProgress);
    }

    private enum PlayerType
    {
        Human,
        Computer
    }

    private static PlayerType GetPlayerType(PlayerColor color)
    {
        do
        {
            Console.WriteLine($"{color} is a:");
            Console.WriteLine("1. Human Player");
            Console.WriteLine("2. Computer Player");
            var type = Console.ReadLine();

            if(type != null && type.Trim() == "1")
            {
                return PlayerType.Human;
            }

            if(type != null && type.Trim() == "2")
            {
                return PlayerType.Computer;
            }

            Console.WriteLine("Invalid entry");

        } while (true);
    }

    private static Coordinate GetHumanMove(PlayerColor color)
    {
        Coordinate? move;
        do
        {
            Console.Write($"{color}, enter your move (e.g. 'A1'): ");
            var moveInput = Console.ReadLine();

            move = ParseCoordinate(moveInput);
        } while (!move.HasValue);

        return move.Value;
    }

    private static Coordinate GetAIMove(ReversiGame game)
    {
        var aiBot = new OneMoveAheadAIBot();
        return aiBot.GetMove(game);
    }

    private static void DisplayBoard(ReversiGame game)
    {
        var grid = game.GetGrid();
        var possibleMoves = game.GetValidMovesForCurrentPlayer();

        Console.WriteLine();
        Console.WriteLine("  ABCDEFGH");
        for (var row = 0; row < grid.GetLength(0); row++)
        {
            Console.Write($"{row + 1} ");

            for (var column = 0; column < grid.GetLength(1); column++)
            {
                var coordinate = new Coordinate(row, column);

                var validMove = possibleMoves.Contains(coordinate);

                var disk = grid[row, column];
                var display = disk switch
                {
                    PlayerColor.Black => "B",
                    PlayerColor.White => "W",
                    null => validMove ? "." : "_",
                    _ => throw new NotImplementedException($"Unknown PlayerColor {disk}")
                }; ;
                Console.Write(display);
            }

            Console.WriteLine();
        }
    }

    private static Coordinate? ParseCoordinate(string? source)
    {
        if(source == null)
        {
            return null;
        }

        var cleaned = source.Trim().ToUpperInvariant();

        if(cleaned.Length != 2)
        {
            return null;
        }

        var column = cleaned[0];
        var row = cleaned[1];

        var columnIndex = -1;
        switch(column)
        {
            case 'A':
                columnIndex = 0;
                break;
            case 'B':
                columnIndex = 1;
                break;
            case 'C':
                columnIndex = 2;
                break;
            case 'D':
                columnIndex = 3;
                break;
            case 'E':
                columnIndex = 4;
                break;
            case 'F':
                columnIndex = 5;
                break;
            case 'G':
                columnIndex = 6;
                break;
            case 'H':
                columnIndex = 7;
                break;
            default:
                return null;
        }

        if(!int.TryParse(row.ToString(), out var rowIndex))
        {
            return null;
        }

        return new Coordinate(rowIndex - 1, columnIndex);
    }
}
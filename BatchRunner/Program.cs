using GreenTeaReversi;
using GreenTeaReversiAIBots;

internal class Program
{
    private const int GamesToRun = 10000;

    private static void Main(string[] args)
    {
        var aWonCount = 0;
        var bWonCount = 0;
        var tieCount = 0;
        var aIsBlackCount = 0;

        Parallel.For(0, GamesToRun, (index) =>
        {
            var gameInProgress = true;

            var game = new ReversiGame();
            var a = new RandomAI();
            var b = new OneMoveAheadAIBot();
            IList<ActionResult> lastResults;

            var aIsBlack = Random.Shared.Next(0, 2) == 0;

            if(aIsBlack)
            {
                Interlocked.Increment(ref aIsBlackCount);
            }

            do
            {
                Coordinate move;
                if(game.CurrentPlayerColor == PlayerColor.Black)
                {
                    move = aIsBlack ? a.GetMove(game) : b.GetMove(game);
                }
                else
                {
                    move = aIsBlack ? b.GetMove(game) : a.GetMove(game);
                }

                lastResults = game.PlaceCurrentPlayerDisk(move);
            } while (game.GameInProgress);

            if(lastResults.Last() == ActionResult.GameOverBlackWins)
            {
                if(aIsBlack)
                {
                    Interlocked.Increment(ref aWonCount);
                }
                else
                {
                    Interlocked.Increment(ref bWonCount);
                }    
            }
            else if(lastResults.Last() == ActionResult.GameOverWhiteWins)
            {
                if (aIsBlack)
                {
                    Interlocked.Increment(ref bWonCount);
                }
                else
                {
                    Interlocked.Increment(ref aWonCount);
                }
            }
            else if (lastResults.Last() == ActionResult.GameOverTie)
            {
                Interlocked.Increment(ref tieCount);
            }
            else
            {
                throw new NotImplementedException($"Invalid last state {lastResults.Last()}");
            }

            Console.WriteLine($"Game {index} complete.");
        });

        Console.WriteLine($"Ran {GamesToRun} Game(s).  Results:");
        Console.WriteLine($"  A: {aWonCount}");
        Console.WriteLine($"  B: {bWonCount}");
        Console.WriteLine($"Tie: {tieCount}");
    }
}
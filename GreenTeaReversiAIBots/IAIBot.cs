using GreenTeaReversi;

namespace GreenTeaReversiAIBots
{
    public interface IAIBot
    {
        public Coordinate GetMove(ReversiGame game);
    }
}

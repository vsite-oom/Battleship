using static BattleshipGame.Services.GameServiceFactory;

namespace BattleshipGame.Services;

public class GameServiceManager
{
    private BaseGameService _currentGameService;

    public BaseGameService CurrentGameService => _currentGameService;

    public GameServiceManager()
    {
        _currentGameService = GameServiceFactory.CreateGameService(GameMode.Normal);
    }

    public void SetGameMode(GameMode mode)
    {
        _currentGameService = GameServiceFactory.CreateGameService(mode);
    }

    public void InitializeGame(int gridSize, Dictionary<int, int> shipConfiguration)
    {
        _currentGameService.InitializeGame(gridSize, shipConfiguration);
    }
}
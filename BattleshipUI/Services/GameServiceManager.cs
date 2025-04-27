using static BattleshipGame.Services.GameServiceFactory;
namespace BattleshipGame.Services;

public class GameServiceManager
{
    private BaseGameService _currentGameService;
    public BaseGameService CurrentGameService => _currentGameService;

    public int GridSize { get; set; } = 10;
    public GameMode GameMode { get; set; } = GameMode.Normal;
    public Dictionary<int, int> ShipConfiguration { get; private set; } = new Dictionary<int, int>
    {
        { 5, 1 },
        { 4, 1 },
        { 3, 2 },
        { 2, 1 },
        { 1, 0 }
    };

    public GameServiceManager()
    {
        _currentGameService = GameServiceFactory.CreateGameService(GameMode);
    }

    public void SetGameMode(GameMode mode)
    {
        GameMode = mode;
        _currentGameService = GameServiceFactory.CreateGameService(mode);
    }

    public void SetShipConfiguration(Dictionary<int, int> shipConfig)
    {
        ShipConfiguration = new Dictionary<int, int>(shipConfig);
    }

    public void SetGridSize(int size)
    {
        GridSize = size;
    }

    public void InitializeGame()
    {
        _currentGameService.InitializeGame(GridSize, ShipConfiguration);
    }

    public void InitializeGame(int gridSize, Dictionary<int, int> shipConfiguration)
    {
        GridSize = gridSize;
        ShipConfiguration = new Dictionary<int, int>(shipConfiguration);
        _currentGameService.InitializeGame(gridSize, shipConfiguration);
    }
}
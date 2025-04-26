namespace BattleshipGame.Services;

public static class GameServiceFactory
{
    public static BaseGameService CreateGameService(GameMode mode)
    {
        return mode switch
        {
            GameMode.Normal => new NormalGameService(),
            GameMode.Pro => new ProGameService(),
            _ => throw new ArgumentOutOfRangeException(nameof(mode), "Unsupported game mode")
        };
    }

    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<GameServiceManager>();
    }
}

public enum GameMode
{
    Normal,
    Pro
}
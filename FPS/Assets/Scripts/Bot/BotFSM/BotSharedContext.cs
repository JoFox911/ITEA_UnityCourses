public class BotSharedContext
{
    public readonly BotWeaponManager WeaponManager;
    public readonly BotEnemySpyManager EnemySpyManager;
    public readonly BotMovementManager MovementManager;
    public readonly BotMapHelper MapHelper;
    public readonly BotItemDetectionManager ItemDetectionManager;

    public BotSharedContext()
    {
        WeaponManager = new BotWeaponManager();
        EnemySpyManager = new BotEnemySpyManager();
        MovementManager = new BotMovementManager();
        MapHelper = new BotMapHelper();
        ItemDetectionManager = new BotItemDetectionManager();
    }
}

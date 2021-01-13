public class BotSharedContext
{
    public readonly BotWeaponManager WeaponManager;
    public readonly BotEnemySpyManager EnemySpyManager;
    public readonly BotMovementHelper MovementHelper;
    public readonly BotMapHelper MapHelper;
    public readonly BotItemDetectionManager ItemDetectionManager;

    public BotSharedContext()
    {
        WeaponManager = new BotWeaponManager();
        EnemySpyManager = new BotEnemySpyManager();
        MovementHelper = new BotMovementHelper();
        MapHelper = new BotMapHelper();
        ItemDetectionManager = new BotItemDetectionManager();
    }
}

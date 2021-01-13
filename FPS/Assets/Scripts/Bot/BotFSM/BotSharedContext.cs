public class BotSharedContext
{
    public readonly CheckEnemyHelper EnemySpyManager;
    public readonly BotMovementManager MovementManager;
    public readonly BotMapHelper MapHelper;
    public readonly PickUpHelper ItemDetectionManager;
    public readonly SoldierWeaponManager WeaponManager;


    public BotSharedContext(BotMovementManager movementHelper, PickUpHelper pickUpHelper, BotMapHelper mapHelper, CheckEnemyHelper enemySpyManager, SoldierWeaponManager soldierWeaponManager)
    {

        MovementManager = movementHelper;
        ItemDetectionManager = pickUpHelper;
        MapHelper = mapHelper;
        EnemySpyManager = enemySpyManager;
        WeaponManager = soldierWeaponManager;
    }
}

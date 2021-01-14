public class BotSharedContext
{
    public readonly CheckEnemyHelper EnemySpyManager;
    public readonly BotMovementManager MovementManager;
    public readonly BotMapHelper MapHelper;
    public readonly PickUpHelper ItemDetectionManager;
    public readonly SoldierWeaponManager WeaponManager;
    public readonly Soldier SoldierState;


    public BotSharedContext(BotMovementManager movementHelper, 
                            PickUpHelper pickUpHelper, 
                            BotMapHelper mapHelper, 
                            CheckEnemyHelper enemySpyManager, 
                            SoldierWeaponManager soldierWeaponManager,
                            Soldier soldierState)
    {

        MovementManager = movementHelper;
        ItemDetectionManager = pickUpHelper;
        MapHelper = mapHelper;
        EnemySpyManager = enemySpyManager;
        WeaponManager = soldierWeaponManager;
        SoldierState = soldierState;
    }
}

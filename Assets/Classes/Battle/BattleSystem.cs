public class BattleSystem : MonoBehaviour
{
    public BattleState state;

    void Start()
    {
        BattleSystemController.instance.onBattleHudTitleButton += OnHUDTitleButton;

        if(battleScriptable.enemy != null && battleScriptable.enemy != ""){           
            enemyPrefab = Resources.Load("Prefabs/Characters/" + battleScriptable.enemy) as GameObject;
        }

        state = new BattleStateSetup();
    }


    void Update() {
        state = state.execute();
    }
}
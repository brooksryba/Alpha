public class BattleSystem : MonoBehaviour
{
    public BattleState state;

    void Start()
    {
        state = new BattleStateSetup();
    }


    void Update() {
        state = state.execute();
    }
}
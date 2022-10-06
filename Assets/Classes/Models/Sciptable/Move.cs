using UnityEngine;

[CreateAssetMenu(fileName = "New Move", menuName = "Assets/New Move")]
public class Move : ScriptableObject
{
    public string title;
    public enum type {Attack, Spell};
    public int hpEffect;
    public int hpCost;
    public int manaEffect;
    public int manaCost;
    public enum minigame {none, tapButtons, stopSlider, targeting};
    public enum target {self, singleEnemy, singleFriendly, multiEnemy, multiFriendly};
    public BattleEffect battleEffectArchetype;
}

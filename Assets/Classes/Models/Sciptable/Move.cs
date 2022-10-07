using UnityEngine;

[CreateAssetMenu(fileName = "New Move", menuName = "Assets/New Move")]
public class Move : ScriptableObject
{
    public string title;
    public enum Type {Attack, Spell};
    public Type type;
    public int hpEffect;
    public int hpCost;
    public int manaEffect;
    public int manaCost;
    public enum Minigame {none, tapButtons, stopSlider, targeting};
    public Minigame minigame;
    public enum Target {self, singleEnemy, singleFriendly, multiEnemy, multiFriendly};
    public Target target;
    public BattleEffect battleEffectArchetype;
}

public class BattleSystemMenu 
{

    public void OpenSubmenu(Character character, GameObject target)
    {
        closeOptionSubmenu();
        createOptionSubmenu(character);
        positionOptionSubmenu(target);
    }

    public void positionOptionSubmenu(GameObject target)
    {
        GameObject obj = GameObject.Find("MenuList");
        Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        obj.transform.position = cam.WorldToScreenPoint(target.transform.position);
        obj.transform.position += new Vector3(0, 100, 0);
    }

    public void closeOptionSubmenu()
    {
        GameObject obj = GameObject.Find("Menu(Clone)");
        if(obj) 
        {
            Destroy(obj);
        }
    }

    public void createOptionSubmenu(Character character)
    {
        GameObject obj = Instantiate(Resources.Load("Prefabs/Menu"), transform.position, transform.rotation) as GameObject;
        DynamicMenu menu = obj.GetComponent<DynamicMenu>();

        Dictionary<string, Action> attacks = new Dictionary<string, Action>();
        Dictionary<string, Action> spells = new Dictionary<string, Action>();
        Dictionary<string, Action> strategies = new Dictionary<string, Action>();

        foreach( var attackRef in character.getAttacks() ) {
            attacks.Add(attackRef.Key, () => { attackReference = attackRef.Value; AwaitTarget();});
        }
        attacks.Add("Return", () => { });
        
        spells.Add("Heal", () => { });
        spells.Add("Fire Damage", () => { });
        spells.Add("Return", () => { });

        strategies.Add("Charge Mana", () => { });
        strategies.Add("Return", () => { });

        Dictionary<string, Action> items = new Dictionary<string, Action>();
        if(character.GetComponent<Player>() != null){
            foreach (var item in character.GetComponent<Player>().items)
            {
                items.Add(item.title, () => { });
            }
        }
        items.Add("Return", () => { });

        menu.Open(new Dictionary<string, Action>(){
        {"Attacks >>", delegate { menu.SubMenu(attacks); }},
        {"Spells >>", delegate { menu.SubMenu(spells); }},
        {"Items >>", delegate { menu.SubMenu(items); }},
        {"Strategies >>", delegate { menu.SubMenu(strategies); }},
        {"Resign", delegate { state = BattleState.RESIGN; EndBattle(); }},
        {"Return", () => {}},
        });
    
    
    }    

}
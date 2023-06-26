using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsvReader
{
    private static string basePath = "Assets/Resources/Fixtures/";

    public static void ReadAllCsvFiles(){
        ArchetypeManager.LoadData();
        MoveManager.LoadData();
        BattleEffectManager.LoadData();
        CharacterManager.LoadData();
    }

    
    public static Dictionary<string, Character> ReadCharacterCsv() {
        Dictionary<string, Character> characters = new Dictionary<string, Character>();
        using (StreamReader reader = new StreamReader(basePath + "characters.csv")) {
            string headerLine = reader.ReadLine(); // skip first row
            string line;
            while ((line = reader.ReadLine()) != null) {
                string[] values = line.Split(',');
                Character character = ScriptableObject.CreateInstance<Character>();
                character.characterID = values[0];
                character.title = LocalizationData.data[character.characterID];
                
                if (!ArchetypeManager.refs.ContainsKey(values[1]))
                {
                   ArchetypeManager.LoadData();
                }   
                character.archetype = ArchetypeManager.Get(values[1]);
                character.condition = ConditionManager.Get(character);
                characters.Add(character.characterID, character);
            }
        }
        return characters;
    }

    public static Dictionary<string, Move> ReadMoveCsv() {
        Dictionary<string, Move> moves = new Dictionary<string, Move>();
        using (StreamReader reader = new StreamReader(basePath + "moves.csv")) {
            string headerLine = reader.ReadLine(); // skip first row
            string line;
            while ((line = reader.ReadLine()) != null) {
                string[] values = line.Split(',');
                Move move = ScriptableObject.CreateInstance<Move>();
                move.moveID = values[0];
                Enum.TryParse(values[1], out move.type);
                move.hpEffect = Int16.Parse(values[2]);
                move.hpCost = Int16.Parse(values[3]);
                move.manaEffect = Int16.Parse(values[4]);
                move.manaCost = Int16.Parse(values[5]);
                Enum.TryParse(values[6], out move.minigame);
                Enum.TryParse(values[7], out move.target);
                move.title = LocalizationData.data[move.moveID];
                moves.Add(move.moveID, move);
            }
        }
        return moves;
    }


    public static Dictionary<string, Archetype> ReadArchetypeCsv() {
        List<(string, int, string, string)> moveProgressions = new List<(string, int, string, string)>();
        Dictionary<string, Archetype> archetypes = new Dictionary<string, Archetype>();

        using (StreamReader reader = new StreamReader(basePath + "archetypeMoveProgression.csv")) {
            string headerLine = reader.ReadLine(); // skip first row
            string line;
            while ((line = reader.ReadLine()) != null) {
                string[] values = line.Split(',');
                moveProgressions.Add((values[0], Int16.Parse(values[1]), values[2], values[3]));
            }
        }


        using (StreamReader reader = new StreamReader(basePath + "archetypes.csv")) {
            string headerLine = reader.ReadLine(); // skip first row
            string line;
            while ((line = reader.ReadLine()) != null) {
                List<(int, string)> attackList = new List<(int, string)>();
                List<(int, string)> spellList = new List<(int, string)>();

                string[] values = line.Split(',');
                Archetype archetype = ScriptableObject.CreateInstance<Archetype>();
                archetype.title = values[0];
                archetype.hp = (Int16.Parse(values[1]), Int16.Parse(values[2]));
                archetype.mana = (Int16.Parse(values[3]), Int16.Parse(values[4]));
                archetype.attackPhysical = (Int16.Parse(values[5]), Int16.Parse(values[6]));
                archetype.attackMagic = (Int16.Parse(values[7]), Int16.Parse(values[8]));
                archetype.defensePhysical = (Int16.Parse(values[9]), Int16.Parse(values[10]));
                archetype.speed = (Int16.Parse(values[11]), Int16.Parse(values[12]));
                for(var i = 0; i < moveProgressions.Count; i++ ){
                    if(moveProgressions[i].Item1==archetype.title){
                        if(moveProgressions[i].Item4 == "Attack")
                            attackList.Add((moveProgressions[i].Item2, moveProgressions[i].Item3));
                        else
                            spellList.Add((moveProgressions[i].Item2, moveProgressions[i].Item3));

                    }
                archetype.attacks = attackList;
                archetype.spells = spellList;
                }
                archetypes.Add(archetype.title, archetype);


            }
        }
        return archetypes;
    }


    public static Dictionary<string, BattleEffect> ReadBattleEffectsCsv() {
        Dictionary<string, BattleEffect> battleEffects = new Dictionary<string, BattleEffect>();
        using (StreamReader reader = new StreamReader(basePath + "battleEffects.csv")) {
            string headerLine = reader.ReadLine(); // skip first row
            string line;
            while ((line = reader.ReadLine()) != null) {
                string[] values = line.Split(',');
                BattleEffect battleEffect = ScriptableObject.CreateInstance<BattleEffect>();
                battleEffect.title = values[0];
                battleEffect.verb = values[1];
                battleEffect.duration = Int16.Parse(values[2]);
                battleEffect.value = (Int16.Parse(values[3]), Int16.Parse(values[4]));
                Enum.TryParse(values[5], out battleEffect.type);
                battleEffects.Add(battleEffect.title, battleEffect);
            }
        }
        return battleEffects;
    }
}

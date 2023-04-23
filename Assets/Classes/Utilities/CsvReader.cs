using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsvReader
{
    private static string basePath = "/Resource/Fixtures/";
    public static Dictionary<string, Character> ReadCharacterCsv() {
        Dictionary<string, Character> characters = new Dictionary<string, Character>();
        using (StreamReader reader = new StreamReader(basePath + "characters.csv")) {
            string headerLine = reader.ReadLine(); // skip first row
            string line;
            while ((line = reader.ReadLine()) != null) {
                string[] values = line.Split(',');
                Character character = new Character();
                character.characterID = values[0];
                character.title = values[1];
                character.condition = ConditionManager.Get(character.characterID);
                character.archetype = ArchetypeManager.Get(values[2]);
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
                Move move = new Move();
                move.title = values[0];
                Enum.TryParse(values[1], out move.type);
                move.hpEffect = Int16.Parse(values[2]);
                move.hpCost = Int16.Parse(values[3]);
                move.manaEffect = Int16.Parse(values[4]);
                move.manaCost = Int16.Parse(values[5]);
                Enum.TryParse(values[6], out move.minigame);
                Enum.TryParse(values[7], out move.target);
            }
        }
        return moves;
    }


    public static Dictionary<string, Archetype> ReadArchetypeCsv() {
        List<(int, string)> attackList = new List<(int, string)>();
        List<(int, string)> spellList = new List<(int, string)>();
        List<(string, int, string)> moveProgressions = new List<(string, int, string)>();
        Dictionary<string, Archetype> archetypes = new Dictionary<string, Archetype>();

        using (StreamReader reader = new StreamReader(basePath + "archetypeMoveProgression.csv")) {
            string headerLine = reader.ReadLine(); // skip first row
            string line;
            while ((line = reader.ReadLine()) != null) {
                string[] values = line.Split(',');
                moveProgressions.Add((values[0], Int16.Parse(values[1]), values[2]));
            }
        }


        using (StreamReader reader = new StreamReader(basePath + "archetypes.csv")) {
            string headerLine = reader.ReadLine(); // skip first row
            string line;
            while ((line = reader.ReadLine()) != null) {
                string[] values = line.Split(',');
                Archetype archetype = new Archetype();
                archetype.title = values[0];
                archetype.hp = (Int16.Parse(values[1]), Int16.Parse(values[2]));
                archetype.mana = (Int16.Parse(values[3]), Int16.Parse(values[4]));
                archetype.attackPhysical = (Int16.Parse(values[5]), Int16.Parse(values[6]));
                archetype.attackMagic = (Int16.Parse(values[7]), Int16.Parse(values[8]));
                archetype.defensePhysical = (Int16.Parse(values[9]), Int16.Parse(values[10]));
                archetype.speed = (Int16.Parse(values[11]), Int16.Parse(values[12]));
                for(var i = 0; i < moveProgressions.Count; i++ ){
                    if(moveProgressions[i].Item1==archetype.title){
                        string moveType;
                        Move move = MoveManager.Get(moveProgressions[i].Item3);
                        if(move.type == Move.Type.Attack)
                            attackList.Add((moveProgressions[i].Item2, moveProgressions[i].Item3));
                        else
                            spellList.Add((moveProgressions[i].Item2, moveProgressions[i].Item3));

                    }
                archetype.attacks = attackList;
                archetype.spells = spellList;
                }


            }
        }
        return archetypes;
    }
}

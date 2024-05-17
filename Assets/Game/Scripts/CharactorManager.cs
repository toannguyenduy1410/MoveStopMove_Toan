using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharactorManager : Singleton<CharactorManager>
{
    private List<Character> characters = new List<Character>();
    public void AddCharacter(Character character)
    {
        characters.Add(character);
    }

    public void RemoveCharacter(Character character)
    {
        characters.Remove(character);
    }

    public void CharactorActiveLevel()
    {
        foreach (Character character in characters)
        {
            character.ActiveLevel();
        }
    }
}


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CharacterSight : MonoBehaviour
{
    [SerializeField] private Character character;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_CHARACTER))
        {
            Character tagert = other.GetComponent<Character>();
            if (!tagert.isDead && character != tagert)
            {
                character.AddTagert(tagert);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {       
        if (other.CompareTag(Constant.TAG_CHARACTER))
        {
            Character target = other.GetComponent<Character>();
            character.RemoveTagert(target);
        }
        if (other.CompareTag(Constant.TAG_BULLET))
        {
            GameUnit gameUnit = other.GetComponent<GameUnit>(); // Lấy đối tượng GameUnit từ GameObject
            if (gameUnit != null)
            {
                SimplePoll.Desspawn(gameUnit);
            }
        }
    }
}

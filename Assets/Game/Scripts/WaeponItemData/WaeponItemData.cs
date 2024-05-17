using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class WaeponItemData
{
    public int index;
    public float speed;    
    public GameObject WaeponPrifab;       
}


public enum WaeponType
{
    hammer = 0,
    Lollipop = 1,
    Knife = 2,
    CandyCane = 3,
    BoomeaRang = 4,
    SwirlyPop = 5,
    //Axe = 6,
    //CreamCone= 7,
    //BattleAxe, Z= 8, 
    //Arrow = 9 , 
    //Uzi = 10
}


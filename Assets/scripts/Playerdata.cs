using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Playerdata
{

    public Dictionary<string, int> _myDictionary;
    public Dictionary<int, string> _myDictionary_Positions;
    public float _health;

    public Playerdata(Dictionary<string, int> myDictionary, Dictionary<int, string> myDictionary_Positions, float health)
    {
        _myDictionary = myDictionary;
        _myDictionary_Positions = myDictionary_Positions;
        _health = health;
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reward : MonoBehaviour
{
    public enum TypeReward
    {
        Lose,
        Gain
    }

    [SerializeField] private TypeReward _type;

    public TypeReward Type => _type; 

}

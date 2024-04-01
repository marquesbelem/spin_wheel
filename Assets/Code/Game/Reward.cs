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

    [SerializeField] private Image _img;
    [SerializeField] private TypeReward _type;

    public TypeReward Type => _type; 

    public void Setup(int value)
    {
        switch (value)
        {
            case 0:
            case 2:
            case 4:
                _type = TypeReward.Gain;
                break;
            case 1:
            case 3:
            case 5:
                _type = TypeReward.Lose;
                break;
        }
    }
}

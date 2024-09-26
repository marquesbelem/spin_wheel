using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour
{
    public enum TypeReward
    {
        Lose = 0,
        Gain = 1
    }

    [SerializeField] private TypeReward _type;
    [SerializeField] private List<string> _name;
    public TypeReward Type => _type;
    public int CountName => _name.Count;


    public string Name
    {
        get
        {
            return GetName();
        }
    }

    int randomIndex = -1;
    private string GetName()
    {
        randomIndex++;

        if (randomIndex >= _name.Count)
        {
            randomIndex = 0;
        }

        Debug.Log($"randomIndex: {randomIndex}");
        return _name[randomIndex];
    }

}

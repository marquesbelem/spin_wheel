using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour
{
    public enum TypeReward
    {
        Lose,
        Gain
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

    private string GetName()
    {
        var randomIndex = 0;
        if (_name.Count != 1)
            randomIndex = UnityEngine.Random.Range(0, _name.Count - 1);

        return _name[randomIndex];
    }

}

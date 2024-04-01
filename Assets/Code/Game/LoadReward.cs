using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadReward : MonoBehaviour
{
    private List<RewardData> _rewardsData = new List<RewardData>();
    public List<RewardData> RewardDatas => _rewardsData;

    public void Awake()
    {
        var filePath = Path.Combine(Application.dataPath, "lista_de_premios.json");
        _rewardsData = JsonConvert.DeserializeObject<List<RewardData>>(File.ReadAllText(filePath));
    }
}

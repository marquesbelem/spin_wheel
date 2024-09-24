using UnityEngine;

public class ValidatorReward : MonoBehaviour
{
    [SerializeField] private Collider2D _collider;

    private void Start()
    {
        EnabledCollider(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
       if (RewardsController.Instance.CanCheckReward == false) return; 

        if(collision.CompareTag("Reward"))
        {
            if (collision.TryGetComponent<Reward>(out var reward))
            {
                Debug.Log($"aaa {reward.Type}");
                RewardsController.Instance.SetCurrentReward(reward);
                EnabledCollider(false);
            }
        }
    }

    public void EnabledCollider(bool value)
    {
        _collider.enabled = value;
    }
}

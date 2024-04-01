using UnityEngine;

public class ValidatorReward : MonoBehaviour
{
    [SerializeField] private Collider2D _collider; 
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (RewardsController.Instance.CanCheckReward == false) return; 

        if(collision.CompareTag("Reward"))
        {
            if (collision.TryGetComponent<Reward>(out var reward))
            {
                Debug.Log("aaa");
                RewardsController.Instance.SetIsGain((int)reward.Type);
                _collider.enabled = false;
            }
        }
    }

    public void EnabledCollider()
    {
        _collider.enabled = true;
    }
}

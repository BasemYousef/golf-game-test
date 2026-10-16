using UnityEngine;

public class HoleTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        BallController ball = other.GetComponent<BallController>();
        if(ball ==null)
            return;

        GameManager.Instance.CompleteLevel();
    }
}

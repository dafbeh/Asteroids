using UnityEngine;

public class PlayerRipple : MonoBehaviour
{
    [SerializeField] ParticleSystem ripple;

    public void ripplePlayer() {
        Instantiate(ripple, transform.position, Quaternion.identity);
    }
}

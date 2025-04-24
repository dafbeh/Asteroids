using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireForce = 20f;
    [SerializeField] public bool autoFire = false;
    private bool firing = false;

    private IAudioSystem audioSystem;

    private void Awake()
    {
        audioSystem = ServiceLocator.Get<IAudioSystem>();
    }

    void Update()
    {
        if(!autoFire) {
            if(Input.GetKeyDown("space"))
            {
                if(!firing) {
                    StartCoroutine(fireCooldown(0.2f));
                }
            }
        } else {
            if(Input.GetKey("space"))
            {
                if(!firing) {
                    StartCoroutine(fireCooldown(0.1f));
                }
            }
        }
    }

    private void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
        audioSystem.PlaySound("Sounds/laser");
    }

    private IEnumerator fireCooldown(float cooldown) {
        firing = true;
        Fire();
        yield return new WaitForSeconds(cooldown);
        firing = false;
    }

    private void OnDisable() {
        firing = false;
    }
}

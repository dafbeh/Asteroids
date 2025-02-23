using System.Collections;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SlotManager : MonoBehaviour
{
    [SerializeField] private GameObject[] slots;
    [SerializeField] private Sprite unselected;
    [SerializeField] private Sprite selected;
    [SerializeField] private GameObject player;
    [SerializeField] private ParticleSystem explosion;

    private PowerUpEffect[] storedPowerUp = new PowerUpEffect[3];
    private bool[] abilityInUse = new bool[3];
    private int activeSlot = 0;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)) {
            if(activeSlot < 2) {
                slots[activeSlot].GetComponent<Image>().sprite = unselected;
                slots[activeSlot+1].GetComponent<Image>().sprite = selected;
                activeSlot++;
            } else if (activeSlot == 2) {
                slots[2].GetComponent<Image>().sprite = unselected;
                slots[0].GetComponent<Image>().sprite = selected;
                activeSlot = 0;
            }
        } 

        if(Input.GetKeyDown(KeyCode.Q)) {
            if(activeSlot > 0) {
                slots[activeSlot].GetComponent<Image>().sprite = unselected;
                slots[activeSlot-1].GetComponent<Image>().sprite = selected;
                activeSlot--;
            } else if (activeSlot == 0) {
                slots[0].GetComponent<Image>().sprite = unselected;
                slots[2].GetComponent<Image>().sprite = selected;
                activeSlot = 2;
            }
        }  

        if(Input.GetKeyDown(KeyCode.LeftShift)) {
            if(!abilityInUse[activeSlot]) {
                useItem();
            }
        }
    }

    public void storeItem(GameObject powerUp)
    {
        for(int i = 0; i < storedPowerUp.Length; i++) {
            if(storedPowerUp[i] == null) {
                Sprite sprite = powerUp.GetComponent<SpriteRenderer>().sprite;
                PowerUpEffect effect = powerUp.GetComponent<PowerUp>().effect;
                storedPowerUp[i] = effect;
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = sprite;
                slots[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
                break;
            } else {
                Instantiate(explosion, player.transform.position, Quaternion.identity);
            }
        }
    }

    public void useItem()
    {
        if(storedPowerUp[activeSlot] == null) {
            return;
        }

        int useSlot = activeSlot;
        Image image = slots[useSlot].transform.GetChild(0).GetComponent<Image>();
        PowerUpEffect effect = storedPowerUp[useSlot];
        Image timerImage = slots[useSlot].transform.GetChild(1).GetComponent<Image>(); 
        float time = 10f;

        StartCoroutine(timer());
        timerImage.enabled = true;
        abilityInUse[useSlot] = true;
        effect.Apply(player);

        IEnumerator timer() {
            float elapsedTime = 0f;

            while (elapsedTime < time)
            {
                timerImage.fillAmount = 1f - (elapsedTime / time);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            image.enabled = false;
            image.sprite = null;
            storedPowerUp[useSlot] = null;

            timerImage.enabled = false;
            timerImage.fillAmount = 1f;
            abilityInUse[useSlot] = false;
        }
    }
}

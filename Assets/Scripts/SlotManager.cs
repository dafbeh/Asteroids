using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SlotManager : MonoBehaviour
{
    [SerializeField] private GameObject[] slots;
    [SerializeField] private Sprite unselected;
    [SerializeField] private Sprite selected;
    [SerializeField] private GameObject player;

    private PowerUpEffect[] storedPowerUp = new PowerUpEffect[3];
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
            useItem();
        }
    }

    public void storeItem(GameObject powerUp)
    {
        Sprite sprite = powerUp.GetComponent<SpriteRenderer>().sprite;
        PowerUpEffect effect = powerUp.GetComponent<PowerUp>().effect;
        int slot = 0;

        for(int i = 0; i < storedPowerUp.Length; i++) {
            if(storedPowerUp[i] == null) {
                storedPowerUp[i] = effect;
                slot = i;
                break;
            }
        }

        slots[slot].transform.GetChild(0).GetComponent<Image>().sprite = sprite;
        slots[slot].transform.GetChild(0).GetComponent<Image>().enabled = true;
    }

    public void useItem()
    {
        if(storedPowerUp[activeSlot] == null) {
            return;
        }

        Image image = slots[activeSlot].transform.GetChild(0).GetComponent<Image>();
        PowerUpEffect effect = storedPowerUp[activeSlot];
        Image timerImage = slots[activeSlot].transform.GetChild(1).GetComponent<Image>(); 
        float time = 10f;

        StartCoroutine(timer());
        timerImage.enabled = true;

        IEnumerator timer() {
            float elapsedTime = 0f;

            while (elapsedTime < time)
            {
                timerImage.fillAmount = 1f - (elapsedTime / time);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            image.sprite = null;
            storedPowerUp[activeSlot] = null;
            image.enabled = false;
            StartCoroutine(timer());
            effect.Apply(player);
            effect.Apply(player);
            timerImage.enabled = false;
        }
    }
}

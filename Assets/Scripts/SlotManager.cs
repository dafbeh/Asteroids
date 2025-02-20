using UnityEngine;
using UnityEngine.UI;

public class SlotManager : MonoBehaviour
{
    [SerializeField] GameObject[] slots;
    [SerializeField] Sprite unselected;
    [SerializeField] Sprite selected;

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
    }


}

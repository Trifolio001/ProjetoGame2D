using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SOUIintUpdate : MonoBehaviour
{
    public SOInfoUI soInfoUI;
    public TextMeshProUGUI uiTextValue;
    public Image a0;
    public Image a1;
    public Image a2;
    public Sprite select;
    public Sprite notselect; 
    public Image slot0;
    public Image slot1;
    public Image slot2;

    // Start is called before the first frame update
    void Start()
    {
        soInfoUI.selectrender = 0;
        soInfoUI.Coins = 0;
        uiTextValue.text = soInfoUI.Coins.ToString();

        for (int i = 0; i < soInfoUI.ListSlotsGuns.Count; i++)
        {
            soInfoUI.ListSlotsGuns[i].slot = soInfoUI.ImageEmpty; 
            soInfoUI.ListSlotsGuns[i].codigo = 0; 
        }

    }

    // Update is called once per frame
    void Update()
    {
        uiTextValue.text = soInfoUI.Coins.ToString();
        slot0.sprite = soInfoUI.ListSlotsGuns[0].slot;
        slot1.sprite = soInfoUI.ListSlotsGuns[1].slot;
        slot2.sprite = soInfoUI.ListSlotsGuns[2].slot;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            soInfoUI.selectrender = 0;
            SelectedOption();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            soInfoUI.selectrender = 1;
            SelectedOption();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            soInfoUI.selectrender = 2;
            SelectedOption();
        }

    }

    private void SelectedOption()
    {

        switch (soInfoUI.selectrender)
        {
            case 0:
                a0.sprite = select;
                a1.sprite = notselect;
                a2.sprite = notselect;
                break;
            case 1:
                a0.sprite = notselect;
                a1.sprite = select;
                a2.sprite = notselect;
                break;
            case 2:
                a0.sprite = notselect;
                a1.sprite = notselect;
                a2.sprite = select;
                break;
        }
    }
}

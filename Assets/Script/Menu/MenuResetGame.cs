using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuResetGame : MonoBehaviour
{
    public GameObject Menu;
    // Start is called before the first frame update
    void Awake()
    {
        DasativeMenu();
    }

    public void DasativeMenu()
    {
        Menu.SetActive(false);
    }
}

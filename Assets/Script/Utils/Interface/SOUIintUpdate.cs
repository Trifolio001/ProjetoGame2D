using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SOUIintUpdate : MonoBehaviour
{
    public SOint soint;
    public TextMeshProUGUI uiTextValue;

    // Start is called before the first frame update
    void Start()
    {
        soint.Value = 0;
        uiTextValue.text = soint.Value.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        uiTextValue.text = soint.Value.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class SOInfoUI : ScriptableObject
{
    public int Coins;
    public int selectrender;
    public Sprite ImageEmpty;

    public List<SlotsGuns> ListSlotsGuns;

    [System.Serializable]
    public class SlotsGuns
    {
        public Sprite slot;
        public int codigo;
    }

}

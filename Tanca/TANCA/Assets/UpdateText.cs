using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateText : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text text;

    public void UpdateVal(float value)
    {
        text.text = value+"";
    }
}

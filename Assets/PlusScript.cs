using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusScript : MonoBehaviour
{
    public GameObject text;
    public int nominal;

    private void Start()
    {
        text.GetComponent<TextMesh>().text = nominal.ToString();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public GameObject text;
    public int nominal;

    private void Update()
    {
        text.GetComponent<TextMesh>().text = nominal.ToString();
    }
}

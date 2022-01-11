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

        this.GetComponent<Renderer>().material.color = new Color(37.0f * (20.0f - nominal) / 20.0f / 255, 64.0f * (20.0f - nominal) / 20.0f / 255, 255.0f * (20.0f - nominal) / 20.0f / 255);


    }
}

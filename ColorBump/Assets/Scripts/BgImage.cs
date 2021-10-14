using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgImage : MonoBehaviour
{
    private Color rdColor;
    void Start()
    {
        rdColor = new Color(Random.Range(0.1f, 1), Random.Range(0.1f, 1), Random.Range(0.1f, 1));
        GetComponent<SpriteRenderer>().color = rdColor;       
    }

}

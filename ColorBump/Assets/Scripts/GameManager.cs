using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text currentText, nextText;
    public Image fill;
    private int level;
    private float startDistance, distance;
    private GameObject player, finish, hand;

    private TextMesh levelNo;

    
    void Awake()
    {
        player = GameObject.Find("Player");
        finish = GameObject.Find("Finish");
        hand = GameObject.Find("Hand");

        levelNo = GameObject.Find("LevelNo").GetComponent<TextMesh>();
    }

    
    void Start()
    {
        level = PlayerPrefs.GetInt("Level");
        levelNo.text = "LEVEL " + level;

        nextText.text = level + 1 + "";
        currentText.text = level.ToString();

        startDistance = Vector3.Distance(player.transform.position, finish.transform.position);
        //SceneManager.LoadScene("Level" + level);
         
    }
    void Update()
    {
        distance = Vector3.Distance(player.transform.position, finish.transform.position);
        if (player.transform.position.z < finish.transform.position.z)
        {
            fill.fillAmount = 1 - (distance / startDistance);
        }
    }
    public void RemoveUI()
    {
        hand.SetActive(false);
    }
}

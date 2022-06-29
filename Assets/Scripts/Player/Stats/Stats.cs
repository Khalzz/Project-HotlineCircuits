using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    public int playerLife;
    public TextMeshPro lifeText;
    public TextMeshPro SpeedText;

    public Rigidbody player;
    // Start is called before the first frame update
    void Start()
    {
        player = transform.GetComponent<Rigidbody>();
        playerLife = 100;
    }

    // Update is called once per frame
    void Update()
    {
        SpeedText.text = ((player.velocity.magnitude * 3.6).ToString("F0") + " Km/h");
        lifeText.text = (playerLife.ToString() + "%");
        if (playerLife <= 0)
        {
            SceneManager.LoadScene(GameObject.Find("Player(Clone)").GetComponent<Restart>().sceneName);
        }
    }

    public void GettingDamage(int damage)
    {
        playerLife -= damage;
        print(playerLife);
    }


}

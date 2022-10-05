using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using Unity.Netcode;
using System;

public class Stats : MonoBehaviour
{
    public int playerLife;
    public GameObject lifeText;

    public GameObject playerPrefab;

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
        // lifeText.GetComponent<TextMeshProUGUI>().text = (playerLife.ToString() + "%");
        if (playerLife <= 0)
        {
            SceneManager.LoadScene(player.GetComponent<Restart>().sceneName);

            /*
            this.gameObject.GetComponent<NetworkObject>().Despawn();
            GameObject newPlayer = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
            newPlayer.transform.name = "Player(Clone)";
            newPlayer.GetComponent<NetworkObject>().SpawnAsPlayerObject(OwnerClientId);
            */
        }
    }

    public void GettingDamage(int damage)
    {
        playerLife -= damage;
        print(playerLife);
    }
}
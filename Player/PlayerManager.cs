using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    [SerializeField] GameObject playerPr;
    GameObject player;

    // Start is called before the first frame update
    void Start(){

    player =  Instantiate(playerPr, ChoosePlace(), Quaternion.identity);

    }

    Vector3 ChoosePlace() {

        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

        return spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{
    public string transferMapName; // 이동할 맵의 이름

    private MovingObject thePlayer;

    void Start()
    {
        thePlayer = FindObjectOfType<MovingObject>(); //hierarchy에 있는 모든 객체의 컴포넌트를 검색해서 리턴
    }

    private void OnTriggerEnter2D(Collider2D  collision)
    {
        if(collision.gameObject.name == "Player")
        {
            thePlayer.currentmapName = transferMapName;
            SceneManager.LoadScene(transferMapName);
        }
    }

}

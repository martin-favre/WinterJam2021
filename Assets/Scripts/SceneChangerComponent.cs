using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChangerComponent : MonoBehaviour
{
    public string targetScene;
    public Vector3 spawnOffset;

    static string sceneWeCameFrom = "";

    void Start()
    {
        if (targetScene == sceneWeCameFrom)
        {
            Vector3 playerPos = PlayerMovementController.Instance.gameObject.transform.position;
            PlayerMovementController.Instance.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, playerPos.z)  + spawnOffset;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<PlayerMovementController>())
        {
            sceneWeCameFrom = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(targetScene);
        }
    }
}

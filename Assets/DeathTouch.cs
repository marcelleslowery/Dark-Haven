using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathTouch : MonoBehaviour {

    // Update is called once per frame
    public void Kill(GameObject player)
    {
        player.GetComponent<Unragdoll>().SetKinematic(false);

        Invoke("Reset", 5f);
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            Kill(col.collider.gameObject);
        }
    }
}

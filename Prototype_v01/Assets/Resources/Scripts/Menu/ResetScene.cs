using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ResetScene : MonoBehaviour
{
	// Update is called once per frame
	void Update ()
    {

        if (Input.GetKeyDown(KeyCode.R)) Reset();
	
	}

    public void Reset()
    {
        SceneManager.LoadScene("PrototypeLevel_01");
    }
}

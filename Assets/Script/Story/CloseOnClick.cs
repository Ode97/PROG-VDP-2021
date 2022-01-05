using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseOnClick : MonoBehaviour
{
    public Button button;

    // Update is called once per frame
    void Start()
    {
        StoryController.pause();
        button.onClick.AddListener(close);
    }

    void close() {
        Time.timeScale = 1;
        StoryController.unpause();
        Destroy(this.gameObject);
    }
}

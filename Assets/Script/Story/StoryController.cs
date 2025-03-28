using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryController : MonoBehaviour
{
    public static bool paused = false;
    public static bool mainMessage = false;
    public static bool labMessage = false;
    public static void pause() {
        paused = true;
        Time.timeScale = 0;
    }
    public static void unpause() {
        paused = false;
        Time.timeScale = 1;
    }
    public static void togglePause() {
        paused = !paused;
        if (paused) Time.timeScale = 0;
        else Time.timeScale = 1;
    }
}

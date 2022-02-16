using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralSoundManager : MonoBehaviour
{

    public AudioSource soundtrack;
    public static GeneralSoundManager instance=null;

    void Awake()
    {
        if (instance==null)
            instance=this;
        else if (instance !=this)
            Destroy (gameObject);

        DontDestroyOnLoad (gameObject);
    }

}



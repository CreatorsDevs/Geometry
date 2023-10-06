using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(50)]
public class GameManager : Singleton<GameManager>
{
    protected override void Awake()
    {
        base.Awake();
        ServiceLocator.Register(this);

        //AudioManager audioManager = ServiceLocator.Get<AudioManager>();
        //audioManager.Play("Background Music");
    }
}

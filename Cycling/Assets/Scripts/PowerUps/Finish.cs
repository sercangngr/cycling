using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using System.IO.Ports;
using Kabuk;


public class Finish : MonoBehaviour, PlayerTriggerListener
{
    public void OnPlayerEnter(Player player)
    {
        GameState.EventGameOver.Fire();
    }

    public void OnPlayerExit(Player player)
    {
    }
}

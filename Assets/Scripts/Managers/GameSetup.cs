using System.Collections.Generic;
using UnityEngine;

public class GameSetup : MonoBehaviour
{
    public Player player;
    public PropData[] availableProps;

    void Start()
    {
        // Set the list of prop data the player will use
        player.runtimePropList = new List<PropData>(availableProps);

        // Switch to prop role
        player.SetRole<Prop>();
    }
}
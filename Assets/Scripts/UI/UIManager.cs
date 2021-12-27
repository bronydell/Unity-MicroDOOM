using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private HUDManager hudManager;
    [SerializeField]
    private DeathScreenManager deathScreenManager;

    public HUDManager HUDManager => hudManager;
    public DeathScreenManager DeathScreenManager => deathScreenManager;
    
    public void SetupPlayer(Player player)
    {
        hudManager.SetupPlayer(player);
    }

}

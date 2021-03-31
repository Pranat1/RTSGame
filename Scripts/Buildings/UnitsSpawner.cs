using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.EventSystems;

public class UnitsSpawner : NetworkBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject unitPrefab = null;
    [SerializeField] private Transform unitSpownPoint = null;

    #region Server

    [Command]
    private void CmdSpawnUnit()
    {
        GameObject unitInstance = Instantiate(
            unitPrefab,
            unitSpownPoint.position,
            unitSpownPoint.rotation);

        NetworkServer.Spawn(unitInstance, connectionToClient);
    }

    #endregion
    #region Client 
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) { return; }
        if (!hasAuthority) { return; }
        CmdSpawnUnit();
    }
    #endregion
}

using FishNet.Object;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCubeSpawner : NetworkBehaviour{
    public GameObject CubePrefab;

    public override void OnStartClient(){
        base.OnStartClient();
        if (IsOwner){
            GetComponent<PlayerInput>().enabled = true; 
            // ^ An sich doppeltgemoppelt, aber sicher(er).
        }
    }

    public void OnAttack(InputValue value){
        if (value.isPressed){
            SpawnCube();
        }
    }
    [ServerRpc] //RPC = Remote Procedure Call. Diese Funktion wird auf dem Server ausgeführt.
    private void SpawnCube(){
        GameObject obj = Instantiate(CubePrefab, transform.position, Quaternion.identity);
        obj.GetComponent<SyncMaterialColor>().Color.Value = 
            Color.HSVToRGB(Random.value, 1, 1);
        Spawn(obj); // Diese Funktion sagt nun Fishnet, dass es das Objekt bei allen erstellen soll, und dass sein Zustand von Fishnet gemanagt wird.
    }
}

using FishNet.Object;
using UnityEngine;

public class PlayerCamera2 : NetworkBehaviour{
    [SerializeField] private Camera _cameraPrefab;
    [SerializeField] private Transform _cameraHolder;

    public override void OnStartClient(){ 
        //Diese Methode wird aufgerufen, sobald dieses Objekt auf dem Client Startet, ähnlich wie MonoBehaviour.Start
        if (IsOwner){
            Instantiate(_cameraPrefab, _cameraHolder); //Instanziiere Kamera-Komponente auf dem Camera Holder.
        }
    }
}

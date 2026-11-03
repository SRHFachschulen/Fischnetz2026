using System;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))] // Diese Komponente verlangt, dass das Objekt einen MeshRenderer hat.
public class SyncMaterialColor : NetworkBehaviour
{
    public readonly SyncVar<Color> Color = new();
    // SyncVars sind Variablen, die von FishNet zwischen allen Teilnehmern synchronisiert werden.
    private void Awake(){
        Color.OnChange += OnColorChanged;
        // SyncVars haben ein Event namens "OnChange". Wir abonieren das Event mit "OnColorChanged".
    }

    private void OnColorChanged(Color previous, Color next, bool asServer){
        //                      ^ Parameter müssen die Typen T, T und bool haben.
        //                          T ist der Typ der SyncVar, also in verschiedenen Fällen anders.
        GetComponent<MeshRenderer>().material.color = Color.Value;
    }
}

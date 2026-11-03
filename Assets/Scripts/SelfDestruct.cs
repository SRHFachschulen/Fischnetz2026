using System.Collections;
using FishNet.Object;
using UnityEngine;

public class SelfDestruct : NetworkBehaviour{
    public float SecondsBeforeDespawn = 7f;

    public override void OnStartServer(){
        base.OnStartServer();
        StartCoroutine(DespawnAfterSeconds());
    }

    private IEnumerator DespawnAfterSeconds(){ //Coroutinen haben als Return Wert IEnumerator. Das ist, was der Unity Engine erlaubt die Funktion zu "pausieren"
        yield return new WaitForSeconds(SecondsBeforeDespawn);
        // ^ Yield übersetzt sich in "Abgeben" oder "Freisetzen". Die Methode löst sich vom Callstack, ohne sich selber zu zerstören.
        Despawn(); // Entfernt das Objekt aus dem Server.
    }
}

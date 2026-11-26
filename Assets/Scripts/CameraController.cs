using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour{
    [SerializeField] private float _flatDistance; //Distanz der Kamera zum eigenen Spieler in der X/Z Ebene
    [SerializeField] private float _heightOffset; //Abstand der Kamera zu zum eigenen Spieler
    [SerializeField] private Transform _myPlayer = null;
    [SerializeField] private Transform _target = null;

    private float nextTargetAcquisition = 1;
    
    void Start(){
        
        AcquireTarget();
    }


    private void AcquireTarget(){
        _myPlayer = GetComponentInParent<PlayerMovement>().transform;
        // ^ Finde eine Komponente in den Eltern, um diesen als eigenen Spieler zu markieren.
        
        // Hole alle Spieler in der Szene
        var allPlayers = GameObject.FindGameObjectsWithTag("Player");

        foreach (var player in allPlayers){
            if (player.transform == _myPlayer){ 
                // Wenn der eigene Spieler gefunden wurde, gehe in die nächste Iteration, da ich mich nicht selber als Ziel haben kann.
                continue;
            }
            _target = player.transform;
            break;
        }
        Debug.Log($"Found a total of {allPlayers.Length} player-tagged objects. My Player is at {_myPlayer.position} and the target at {_target.position}");
    }

    // Update is called once per frame
    void Update()
    {
        if (nextTargetAcquisition < Time.time){
            AcquireTarget();
            nextTargetAcquisition = Time.time + 2;
        }
        
        
        if (_target == null || _myPlayer == null){
            Debug.Log("CameraController Update aborted because of null references!");
            return;
        }
        
        transform.LookAt(_target);

        Vector3 fromTargetToPlayer = _myPlayer.position - _target.position;
        // Dieser Vektor zeigt vom Ziel zum Spieler

        fromTargetToPlayer = Vector3.ProjectOnPlane(fromTargetToPlayer, Vector3.up);
        // Abgeflacht, sodass wir in der X/Z Ebene bleiben.

        Vector3 cameraOffset = fromTargetToPlayer.normalized * _flatDistance;
        // Neuer Vektor mit derselben richtung wie fromTargetToPlayer, aber mit einer Länge, die flatDistance entspricht.

        cameraOffset += Vector3.up * _heightOffset;
        // Schiebe um _heightOffset nach oben.

        transform.position = _myPlayer.position + cameraOffset;

    }
}

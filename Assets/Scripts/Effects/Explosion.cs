using UnityEngine;

public class Explosion : MonoBehaviour
{
    

    void Start() {

    }

    void Update() {

    }

    private void OnTriggerEnter(Collider other) {
        Explode();
    }

    public void Explode() {
        gameObject.SetActive(false);
    }
}

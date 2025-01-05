using Unity.FPS.Gameplay;
using UnityEngine;

public class ObjectiveCheckpoint : MonoBehaviour
{
    [Tooltip("Visible transform that will be destroyed once the objective is completed")]
    public Transform DestroyRoot;

    void OnTriggerEnter(Collider other)
    {
        other.GetComponent<ObjectiveMultipleReachPointsManager>().completeObjectivePoint(this.transform, other);
        // destroy the transform, will remove the compass marker if it has one
        Destroy(DestroyRoot.gameObject);
    }
}

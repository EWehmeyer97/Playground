using UnityEngine;
using UnityEngine.Events;

public class StateChange : MonoBehaviour
{
    [SerializeField] UnityEvent OnObjectEnable;

    private void OnEnable()
    {
        if(OnObjectEnable != null)
            OnObjectEnable.Invoke();
    }
}

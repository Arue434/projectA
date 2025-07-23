using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    [SerializeField] private DialogNode startNode;

    public void TriggerDialog()
    {
        if (DialogManager.Instance != null && startNode != null)
        {
            DialogManager.Instance.StartDialog(startNode);
        }
        else
        {
            Debug.LogError("dialog trigger hilang!");
        }
    }
}

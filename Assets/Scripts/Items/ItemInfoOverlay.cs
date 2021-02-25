using UnityEngine;
using UnityEngine.UI;

public class ItemInfoOverlay : MonoBehaviour
{
    new public Text name;
    public Text description;

    public virtual void SetItem(Item newItem)
    {
        name.text = newItem.name;
        // description.text = newItem.description;
    }

    public virtual void ClearItem()
    {
        name.text = null;
        // description.text = null;
    }
}
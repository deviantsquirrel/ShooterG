using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemScript : MonoBehaviour
{
    public enum ItemType
    {
        Ak,
        Ammo,
        Makar,
    }
    [SerializeField] ItemType itemType;

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            backpack.Instance.GainItem(itemType.ToString());
            Destroy(gameObject);
        }
    }
}

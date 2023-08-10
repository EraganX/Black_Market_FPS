using UnityEngine;

public class CoinScript : MonoBehaviour
{
    private UIManager _uiManager;
    private InventoryScript inventoryScript;
    [SerializeField] private AudioClip _coinSound;

    private void Awake()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        inventoryScript = GameObject.Find("Invemtory Manager").GetComponent<InventoryScript> ();
    }

    private void OnTriggerStay(Collider target)
    {
        if (target.CompareTag("Player"))
        {
            _uiManager.CoinPickUpText(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                PlayerScript player = target.GetComponent<PlayerScript>();
                if (player!=null)
                {
                    inventoryScript.updateCoin(+100);
                    AudioSource.PlayClipAtPoint(_coinSound,transform.position,0.5f);
                    player.hasCoin = true;
                }
                Destroy(gameObject);
                _uiManager.CoinPickUpText(false);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _uiManager.CoinPickUpText(false);
        }
    }
}

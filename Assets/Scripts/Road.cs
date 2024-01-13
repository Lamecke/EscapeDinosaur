using System.Collections;
using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField] private float minute = 1f;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.AddScore();
            StartCoroutine(DisableRoad());
        }
    }
    private IEnumerator DisableRoad()
    {
        yield return new WaitForSeconds(minute);
        if (GameManager.Instance.GetGameState() != GameState.finish)
            gameObject.SetActive(false);
        StopCoroutine(DisableRoad());
    }
}

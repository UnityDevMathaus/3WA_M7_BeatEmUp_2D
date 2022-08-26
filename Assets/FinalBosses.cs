using UnityEngine;

public class FinalBosses : MonoBehaviour
{
    [SerializeField] private CharactersRenderers[] _renderers;

    private void Awake()
    {
        for (int i = 0; i < _renderers.Length; i++)
        {
            _renderers[i].SetSpritesByIndex(i);
        }  
    }
}
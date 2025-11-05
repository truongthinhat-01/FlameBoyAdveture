using Unity.VisualScripting;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    [SerializeField] float ActiveDuration = 0.5f;
    [SerializeField] Vector3 spikesActivePosition = Vector3.zero;
    [SerializeField] Vector3 spikesIdlePosition = new Vector3(0f, -0.5f, 0f);
    [SerializeField] GameObject spikesMesh;
    [SerializeField] float TransitionDuration = 0.2f;
    private float timer;
   
    enum EState
    {
        Idle,
        TrasitionToActive,
        Active,
        TrasitionToIdle
    }
    EState state = EState.Idle;

    void ChangeState(EState newState)
    {
        this.state = newState;
        timer = 0f;
        if (state == EState.Idle)
        {
            spikesMesh.SetActive(false);
        }
        else
        {
            spikesMesh.SetActive(true);
        }
    }

    private void Start()
    {
        spikesMesh.SetActive(false);
    }
    [ContextMenu("Activate Spike Trap")]

    public void Activate()
    {
        if (state == EState.Idle)
        {
            ChangeState(EState.TrasitionToActive);
        }

        spikesMesh.SetActive (true);
        Invoke("HideSpikes", ActiveDuration);
    }

    void HideSpikes()
    {
        spikesMesh.SetActive (false);
    }

    void Update()
    {
        if (state == EState.TrasitionToActive)
        {
            Vector3 p = Vector3.Lerp(spikesIdlePosition, spikesActivePosition, timer / TransitionDuration);
            spikesMesh.transform.localPosition = p;

            if(timer >= TransitionDuration)
            {
                ChangeState(EState.Active);
            }

        }
        else if(state == EState.TrasitionToIdle)
        {
            Vector3 p = Vector3.Lerp( spikesActivePosition,spikesIdlePosition, timer / TransitionDuration);
            spikesMesh.transform.localPosition = p;
            if (timer >= TransitionDuration)
            {
                ChangeState(EState.Idle);
            }

        }
        else if(state == EState.Active)
        {
            if(timer>= ActiveDuration)
            {
                ChangeState(EState.TrasitionToIdle);
            }
        }
            timer += Time.deltaTime;
    }
}

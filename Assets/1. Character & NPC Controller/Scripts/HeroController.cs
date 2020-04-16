using UnityEngine;
using UnityEngine.AI;

public class HeroController : MonoBehaviour
{
    public float m_separationAmount = .01f;
    public EventFloat OnClickSpaceLemmings;

    Animator animator; // reference to the animator component
    NavMeshAgent agent; // reference to the NavMeshAgent

    void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        animator.SetFloat("Speed", agent.velocity.magnitude);

        if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftControl))
        {
            OnClickSpaceLemmings.Invoke(m_separationAmount);

        }
    }


}

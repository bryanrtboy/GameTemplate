using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using CreatorKitCode;
using CreatorKitCodeInternal;


public class BaitManager : MonoBehaviour
{
    public GameObject m_baitPrefab;

    LemmingController[] lemmings;


    public void SetBait(Vector3 setLocation)
    {
        GameObject g = Instantiate(m_baitPrefab, setLocation, Quaternion.identity);
        UpdateLemmingTargets(g.transform);
    }

    void UpdateLemmingTargets(Transform bait)
    {
        if (lemmings == null)
            lemmings = FindObjectsOfType<LemmingController>();


        for (int i = 0; i < lemmings.Length; i++)
        {
            if (i == 0)
                lemmings[i].UpdateAgentTarget(lemmings[lemmings.Length - 1].transform);
            else
                lemmings[i].UpdateAgentTarget(lemmings[i - 1].transform);

        }


        LemmingController lc = ClosestLemming(lemmings, bait);

        lc.UpdateAgentTarget(bait);

    }

    public void SeparateLemmings(float f)
    {
        if (lemmings == null)
            lemmings = FindObjectsOfType<LemmingController>();

        foreach (LemmingController l in lemmings)
        {
            l.UpdateLemmingSeparation(f);
        }

    }

    LemmingController ClosestLemming(LemmingController[] lemmings, Transform bait)
    {
        LemmingController closestLemming = lemmings[0];

        float distance = 1000000;

        foreach (LemmingController l in lemmings)
        {
            float d = Vector3.Distance(l.transform.position, bait.position);
            if (d < distance)
            {
                distance = d;
                closestLemming = l;
            }
        }


        return closestLemming;
    }


}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kabuk
{
    public class CoroutineTracker
    {
        Coroutine courotine = null;
        System.Action onComplete = null;

        public MonoBehaviour owner;

        public bool Executing
        {
            get; private set;
        }

        public bool CalledBefore
        {
            get; private set;
        }

        public CoroutineTracker(MonoBehaviour owner)
        {
            this.owner = owner;
        }

        public void Start(IEnumerator routine, System.Action onComplete = null)
        {
            courotine = owner.StartCoroutine(Track(routine));
            this.onComplete = onComplete;
        }

        IEnumerator Track(IEnumerator routine)
        {
            CalledBefore = true;
            Executing = true;
            yield return routine;
            Executing = false;
            if (onComplete != null)
            {
                onComplete();
            }

            courotine = null;
            onComplete = null;
        }

        public void Stop(bool complete = false)
        {
            if (courotine != null)
            {
                owner.StopCoroutine(courotine);
                if (complete && onComplete != null)
                {
                    onComplete();
                }
            }
        }

    }
}


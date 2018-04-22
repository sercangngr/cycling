using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kabuk
{
    public class WaitAll : CustomYieldInstruction
    {
        int counter = 0;

        MonoBehaviour owner;
        public WaitAll(MonoBehaviour owner)
        {
            this.owner = owner;
        }

        public WaitAll Add(IEnumerator routine)
        {
            owner.StartCoroutine(Track(routine));
            return this;

        }

        IEnumerator Track(IEnumerator routine)
        {
            counter++;
            yield return routine;
            counter--;
        }


        public override bool keepWaiting
        {
            get
            {
                return counter != 0;
            }
        }
    }
}

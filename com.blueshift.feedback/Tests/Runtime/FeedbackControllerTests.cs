using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace BlueShift.Feedback.Tests
{
    public class FeedbackControllerTests
    {
        [UnityTest]
        public IEnumerator Stretch_FollowsRigidbodySpeed()
        {
            Assert.Ignore("TODO [T3/T8] Créer racine + Visual + Rigidbody, lancer, vérifier l'échelle Z.");
            yield return null;
        }

        [UnityTest]
        public IEnumerator Disable_RestoresOriginalState()
        {
            Assert.Ignore("TODO [T3]");
            yield return null;
        }

        [UnityTest]
        public IEnumerator Trigger_UnknownName_LogsError()
        {
            Assert.Ignore("TODO [T11]");
            yield return null;
        }
    }
}

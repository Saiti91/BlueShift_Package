using NUnit.Framework;

namespace BlueShift.Feedback.Editor.Tests
{
    public class FeedbackMathTests
    {
        [Test]
        public void AxisScale_PreserveVolume_KeepsProductAtOne()
        {
            Assert.Ignore("TODO [T3] x * y * z == 1 à epsilon près.");
        }

        [Test]
        public void WeightScale_ZeroWeight_ReturnsNeutral()
        {
            Assert.Ignore("TODO [T3]");
        }
    }
}

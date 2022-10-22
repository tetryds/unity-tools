using System;
using NUnit.Framework;
using tetryds.Tools;

namespace tetryds.Tests
{
    [TestFixture]
    public class SyncTimerTests
    {
        [Test]
        public void SyncTimerExpires()
        {
            float deltaTime = 1f;
            SyncTimer timer = new SyncTimer(1f, 0f);

            int invoked = 0;
            timer.Timeout += () => invoked++;

            timer.Tick(deltaTime);

            Assert.AreEqual(1, invoked);
        }

        [Test]
        public void SyncTimerExpiresMoreThanOnceIfDeltaTimeIsLarge()
        {
            float deltaTime = 10f;
            SyncTimer timer = new SyncTimer(1f, 0f);

            int invoked = 0;
            timer.Timeout += () => invoked++;

            timer.Tick(deltaTime);

            Assert.AreEqual(10, invoked);
        }

        [Test]
        public void SyncTimerIgnoresNegativeDeltaTime()
        {
            float negativeDeltaTime = -2f;
            float deltaTime = 0.5f;
            SyncTimer timer = new SyncTimer(1f, 0f);

            int invoked = 0;
            timer.Timeout += () => invoked++;

            timer.Tick(deltaTime);
            Assert.AreEqual(0, invoked);
            timer.Tick(deltaTime);
            Assert.AreEqual(1, invoked);
            timer.Tick(negativeDeltaTime);
            Assert.AreEqual(1, invoked);
            timer.Tick(deltaTime);
            Assert.AreEqual(1, invoked);
            timer.Tick(negativeDeltaTime);
            Assert.AreEqual(1, invoked);
            timer.Tick(deltaTime);
            Assert.AreEqual(2, invoked);
        }

        [Test]
        public void SyncTimerAccumulatesCorrectly()
        {
            float deltaTime = 0.5f;
            SyncTimer timer = new SyncTimer(1f, 0f);

            int invoked = 0;
            timer.Timeout += () => invoked++;

            timer.Tick(deltaTime);
            Assert.AreEqual(0, invoked);
            timer.Tick(deltaTime);
            Assert.AreEqual(1, invoked);
            timer.Tick(deltaTime);
            Assert.AreEqual(1, invoked);
            timer.Tick(deltaTime);
            Assert.AreEqual(2, invoked);
        }

        [Test]
        public void SyncTimerCatchesUpToInitialEllapsed()
        {
            float deltaTime = 0.1f;
            SyncTimer timer = new SyncTimer(1f, 5f);

            int invoked = 0;
            timer.Timeout += () => invoked++;

            timer.Tick(deltaTime);
            Assert.AreEqual(5, invoked);
        }

        [Test]
        public void SyncTimerDelaysStartIfInitialEllapsedIsNegative()
        {
            float deltaTime = 1f;
            SyncTimer timer = new SyncTimer(1f, -5f);

            int invoked = 0;
            timer.Timeout += () => invoked++;

            timer.Tick(deltaTime);
            Assert.AreEqual(0, invoked);
            timer.Tick(deltaTime);
            Assert.AreEqual(0, invoked);
            timer.Tick(deltaTime);
            Assert.AreEqual(0, invoked);
            timer.Tick(deltaTime);
            Assert.AreEqual(0, invoked);
            timer.Tick(deltaTime);
            Assert.AreEqual(0, invoked);
            timer.Tick(deltaTime);
            Assert.AreEqual(1, invoked);
        }

        [Test]
        public void SyncTimerExpiresOncePerTickWhenPeriodIsZero()
        {
            float deltaTime = 1f;
            SyncTimer timer = new SyncTimer(0f, 0f);

            int invoked = 0;
            timer.Timeout += () => invoked++;

            for (int i = 0; i < 5; i++)
            {
                timer.Tick(deltaTime);
            }

            Assert.AreEqual(5, invoked);
        }

        [Test]
        public void SyncTimerIgnoresInitialElapsedWhenPeriodIsZero()
        {
            float deltaTime = 1f;
            SyncTimer timer = new SyncTimer(0f, 10f);

            int invoked = 0;
            timer.Timeout += () => invoked++;

            for (int i = 0; i < 5; i++)
            {
                timer.Tick(deltaTime);
            }

            Assert.AreEqual(5, invoked);
        }
    }
}

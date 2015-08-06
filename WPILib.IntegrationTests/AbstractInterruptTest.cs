﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using WPILib.IntegrationTests.Test;
using static WPILib.Timer;

namespace WPILib.IntegrationTests
{
    public abstract class AbstractInterruptTest : AbstractComsSetup
    {
        private InterruptableSensorBase interrupable = null;

        private InterruptableSensorBase GetInterruptable()
        {
            return interrupable ?? (interrupable = GiveInterruptableSensorBase());
        }

        [TearDown]
        public void InterruptTeardown()
        {
            if (interrupable != null)
            {
                FreeInterruptableSensorBase();
                interrupable = null;
            }
        }

        internal abstract InterruptableSensorBase GiveInterruptableSensorBase();

        internal abstract void FreeInterruptableSensorBase();

        internal abstract void SetInterruptHigh();

        internal abstract void SetInterruptLow();

        internal class InterruptCounter
        {
            private int count = 0;

            internal void Increment()
            {
                Interlocked.Increment(ref count);
            }

            internal int GetCount()
            {
                return count;
            }
        }

        internal class TestHandlerFunction
        {
            internal long interruptFireTime = 0;

            internal int interruptComplete = 0;

            private readonly InterruptCounter counter;

            internal TestHandlerFunction(InterruptCounter counter)
            {
                this.counter = counter;
            }

            public void fired(uint u, object o)
            {
                Interlocked.Exchange(ref interruptFireTime, Utility.GetFPGATime());
                counter.Increment();
                Assert.AreSame(counter, o);
                Interlocked.Exchange(ref interruptComplete, 1);
                //Console.WriteLine("Fired!!!!");
                return;
            }
        }

        [Test, Timeout(1300)]
        public void TestSingleInterruptsTriggering()
        {
            InterruptCounter counter = new InterruptCounter();



            TestHandlerFunction function = new TestHandlerFunction(counter);

            GetInterruptable().RequestInterrupts(function.fired, counter);
            GetInterruptable().EnableInterrupts();

            SetInterruptLow();
            Delay(0.01);

            long interruptTriggerTime = Utility.GetFPGATime();

            SetInterruptHigh();

            while (function.interruptComplete == 0)
            {
                Delay(0.005);
            }


            Assert.AreEqual(1, counter.GetCount(), "The interrupt did not fire the expected number of times");


            long range = 10000;

            bool firedOnTime = (function.interruptFireTime > (interruptTriggerTime - range)) &&
                               (function.interruptFireTime < interruptTriggerTime + range);

            Assert.IsTrue(firedOnTime,
                "The interrupt did not fire within the expected time period (values in milliseconds)" +
                (interruptTriggerTime - range) + " " + (interruptTriggerTime + range));


            bool risingStamp = (GetInterruptable().ReadRisingTimestanp() > (interruptTriggerTime - range) / 1e6)
                               && (GetInterruptable().ReadRisingTimestanp() < (interruptTriggerTime + range) / 1e6);


            Assert.IsTrue(risingStamp, "The ReadRisingTimestamp() did not return the correct value (values in seconds");

        }

        [Test, Timeout(1800)]
        public void TestMultipleInterruptsTriggering()
        {
            InterruptCounter counter = new InterruptCounter();
            TestHandlerFunction function = new TestHandlerFunction(counter);

            GetInterruptable().RequestInterrupts(function.fired, counter);
            GetInterruptable().EnableInterrupts();

            int fireCount = 50;
            for (int i = 0; i < fireCount; i++)
            {
                SetInterruptLow();
                SetInterruptHigh();
                while (function.interruptComplete == 0)
                {
                    Delay(0.005);
                }
                function.interruptComplete = 0;
            }
            Assert.AreEqual(fireCount, counter.GetCount(), "The interrupt did not fire the expected number of times");

        }

        private const int synchronousTimeout = 5;
        [Test, Timeout((int)(synchronousTimeout * 1e3))]
        public void TestSynchronousInterruptsTriggering()
        {
            GetInterruptable().RequestInterrupts();

            double synchronousDelay = synchronousTimeout / 2;

            long startTimeStamp = Utility.GetFPGATime();
            new Thread(() =>
            {
                Delay(synchronousDelay);
                SetInterruptHigh();
                SetInterruptLow();
            }).Start();

            GetInterruptable().WaitForInterrupt(synchronousTimeout * 2);
            long stopTimeStamp = Utility.GetFPGATime();

            double interruptRunTime = (stopTimeStamp - startTimeStamp) * 1e-6;

            Assert.AreEqual(synchronousDelay, interruptRunTime, 0.1, "The interrupt did not run for the expected ammount of time (units in seconds)");
        }

        [Test, Timeout(3000)]
        public void TestDisableStopsInterruptFiring()
        {
            InterruptCounter counter = new InterruptCounter();
            TestHandlerFunction function = new TestHandlerFunction(counter);

            GetInterruptable().RequestInterrupts(function.fired, counter);
            GetInterruptable().EnableInterrupts();

            int fireCount = 50;
            for (int i = 0; i < fireCount; i++)
            {
                SetInterruptLow();
                SetInterruptHigh();
                while (function.interruptComplete == 0)
                {
                    Delay(0.005);
                }
                function.interruptComplete = 0;
            }

            GetInterruptable().DisableInterrupts();

            for (int i = 0; i < fireCount; i++)
            {
                SetInterruptLow();
                SetInterruptHigh();
                Delay(0.005);
            }

            Assert.AreEqual(fireCount, counter.GetCount(), "The interrupt did not fire the expected number of times");
        }
    }
}

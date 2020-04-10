using System;
using System.Diagnostics;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace BurstDemo
{
    [BurstCompile]
    public struct BigComputationJobWithBurst : IJob 
    {
        public int firstLoopCount;
        public int secondLoopCount;

        public NativeArray<float> sumToReturn; 
        
        public void Execute()
        {
            var sum = 0f;
            for (var i = 0; i < firstLoopCount; i++)
            {
                for (var j = 0; j < secondLoopCount; j++)
                {
                    sum += i * 0.42f + j * 0.51f;
                }
            }

            sumToReturn[0] = sum;
        }
    }
    
    
    public struct BigComputationJob : IJob 
    {
        public int firstLoopCount;
        public int secondLoopCount;

        public NativeArray<float> sumToReturn; 
        
        public void Execute()
        {
            var sum = 0f;
            for (var i = 0; i < firstLoopCount; i++)
            {
                for (var j = 0; j < secondLoopCount; j++)
                {
                    sum += i * 0.42f + j * 0.51f;
                }
            }

            sumToReturn[0] = sum;
        }
    }

    public class BurstVerySimpleBenchmark : MonoBehaviour
    {
        public int firstLoopCount;
        public int secondLoopCount;
        private readonly Stopwatch sw = new Stopwatch();

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                TimeIt(() => BigComputation(), "Simple Big Computation");
            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                TimeIt(() =>
                {
                    BigComputation();
                    BigComputation();
                    BigComputation();
                    BigComputation();
                }, "Simple Big Computation");
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                TimeIt(() =>
                {
                    var (handle1, job1) = LaunchBigComputationInJob();
                    var (handle2, job2) = LaunchBigComputationInJob();
                    var (handle3, job3) = LaunchBigComputationInJob();
                    var (handle4, job4) = LaunchBigComputationInJob();
                    
                    handle1.Complete();
                    handle2.Complete();
                    handle3.Complete();
                    handle4.Complete();

                    job1.sumToReturn.Dispose();
                    job2.sumToReturn.Dispose();
                    job3.sumToReturn.Dispose();
                    job4.sumToReturn.Dispose();

                }, "Simple Big Computation");
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                TimeIt(() =>
                {
                    var (handle1, job1) = LaunchBigComputationInJobWithBurst();
                    var (handle2, job2) = LaunchBigComputationInJobWithBurst();
                    var (handle3, job3) = LaunchBigComputationInJobWithBurst();
                    var (handle4, job4) = LaunchBigComputationInJobWithBurst();
                    
                    handle1.Complete();
                    handle2.Complete();
                    handle3.Complete();
                    handle4.Complete();

                    job1.sumToReturn.Dispose();
                    job2.sumToReturn.Dispose();
                    job3.sumToReturn.Dispose();
                    job4.sumToReturn.Dispose();

                }, "Simple Big Computation");
            }
        }

        private (JobHandle, BigComputationJob) LaunchBigComputationInJob()
        {
            var job = new BigComputationJob
            {
                firstLoopCount = firstLoopCount,
                secondLoopCount = secondLoopCount,
                sumToReturn = new NativeArray<float>(1, Allocator.TempJob)
            };
            return (job.Schedule(), job);
        }

        private (JobHandle, BigComputationJobWithBurst) LaunchBigComputationInJobWithBurst()
        {
            var job = new BigComputationJobWithBurst
            {
                firstLoopCount = firstLoopCount,
                secondLoopCount = secondLoopCount,
                sumToReturn = new NativeArray<float>(1, Allocator.TempJob)
            };
            return (job.Schedule(), job);
        }

        private float BigComputation()
        {
            var sum = 0f;
            for (var i = 0; i < firstLoopCount; i++)
            {
                for (var j = 0; j < secondLoopCount; j++)
                {
                    sum += i * 0.42f + j * 0.51f;
                }
            }

            return sum;
        }

        private void TimeIt(Action method, string label)
        {
            sw.Reset();
            sw.Start();
            method();
            sw.Stop();
            Debug.Log($"It took {sw.ElapsedMilliseconds}ms to execute {label}");
        }
    }
}
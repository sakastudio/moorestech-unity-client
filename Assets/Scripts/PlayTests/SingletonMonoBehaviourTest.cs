using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Util;

namespace PlayTests
{
    public class SingletonMonoBehaviourTest
    {
        [UnityTest]
        public IEnumerator NotInSceneTest()
        {
            try
            {
                var a = TestSingletonClass.Instance;
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.Pass();
            }
            yield return null;
        }

        [UnityTest]
        public IEnumerator OneInSceneTest()
        {
            new GameObject().AddComponent<TestSingletonClass>();
            var a = TestSingletonClass.Instance;
            Assert.Pass();
            yield return null;
        }

        [UnityTest]
        public IEnumerator TwoInSceneTest()
        {
            new GameObject().AddComponent<TestSingletonClass>();
            new GameObject().AddComponent<TestSingletonClass>();
            try
            {
                var a = TestSingletonClass.Instance;
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.Pass();
            }
            yield return null;
        }
    }
    public class TestSingletonClass : SingletonMonoBehaviour<TestSingletonClass>
    {
        
    }
}
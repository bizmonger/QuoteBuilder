using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bizmonger.Patterns; using static Bizmonger.Patterns.MessageBus;

namespace Tests
{
    [TestClass]
    public class _MessageBus
    {
        [TestMethod]
        public void unsubscribe()
        {
            // Setup
            var subscription = "some_event";
            
            // Test
            Subscribe(subscription, SomeEventResponse);
            Publish(subscription);
            Unsubscribe(subscription, SomeEventResponse);

            // Verify
            Assert.IsTrue(!HasSubscriptions());
        }

        [TestMethod]
        public void nested_one_time_subscription()
        {
            // Setup
            var subscription = "some_event";
            

            // Test
            Subscribe(subscription, SomeEventWithNestedFirstTimeResponse);
            Publish(subscription);
            Unsubscribe(subscription, SomeEventResponse);

            // Verify
            Assert.IsTrue(!HasSubscriptions());
        }

        void SomeEventResponse(object obj)
        {
            

            var subscription = "some_other_event";
            SubscribeFirstPublication(subscription, SomeOtherEventResponse);
            Unsubscribe(subscription, SomeOtherEventResponse);
        }

        void SomeOtherEventResponse(object obj)
        {

        }

        public void SomeEventWithNestedFirstTimeResponse(object obj)
        {
            
            var subscription = "some_event";

            SubscribeFirstPublication(subscription, SomeOtherEventResponse);
        }
    }
}
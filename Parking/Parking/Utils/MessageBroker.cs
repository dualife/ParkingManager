using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingIHM.Utils
{
    interface IHandle<T>
    {
        void Handle(T msg);
    }

    // used for communication between ViewModels
    public static class Broker
    {
        private static readonly ConcurrentDictionary<Object, IEnumerable<Type>> Subscribers = new ConcurrentDictionary<Object, IEnumerable<Type>>();

        public static void Subscribe<T>(T subscriber) where T : class
        {
            var handles = typeof(T).GetInterfaces().Where(i => i != null && (i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IHandle<>)));
            Subscribers.TryAdd(subscriber, handles.Select(i => i.GetGenericArguments().First()).ToList());
        }

        public static void Publish<T>(T msg)
        {
            foreach (var subscriber in Subscribers.Where(s => s.Value.Any(su => su == typeof(T))).Select(sn => sn.Key).Cast<IHandle<T>>())
                subscriber.Handle(msg);
        }

        public static void Unsubscribe<T>(T subscriber) where T : class
        {
            IEnumerable<Type> res;
            Subscribers.TryRemove(subscriber, out res);
        }
    }
}

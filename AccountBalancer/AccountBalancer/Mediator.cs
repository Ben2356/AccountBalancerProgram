using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountBalancer
{
    public static class Mediator
    {
        private static IDictionary<string, List<Action<object>>> actionMappings = new Dictionary<string, List<Action<object>>>();

        public static void Subscribe(string token, Action<object> callback)
        {
            if(!actionMappings.ContainsKey(token))
            {
                var list = new List<Action<object>> { callback };
                actionMappings.Add(token, list);
            }
            else
            {
                bool found = false;
                foreach(var item in actionMappings[token])
                {
                    if(item.Method.ToString() == callback.Method.ToString())
                    {
                        found = true;
                        break;
                    }
                }
                if(!found)
                {
                    actionMappings[token].Add(callback);
                }
            }
        }

        public static void Unsubscribe(string token, Action<object> callback)
        {
            if(actionMappings.ContainsKey(token))
            {
                actionMappings[token].Remove(callback);
            }
        }

        public static void Notify(string token, object args=null)
        {
            if(actionMappings.ContainsKey(token))
            {
                foreach(var callback in actionMappings[token])
                {
                    callback(args);
                }
            }
        }
    }
}

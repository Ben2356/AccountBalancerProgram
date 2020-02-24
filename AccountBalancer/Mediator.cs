using System;
using System.Collections.Generic;

namespace AccountBalancer
{
    /// <summary>
    /// Mediator class to handle the interactions between ViewModels
    /// </summary>
    public class Mediator
    {
        private readonly IDictionary<string, Action<object>> actionMappings = new Dictionary<string, Action<object>>();

        /// <summary>
        /// Adds an function with a token identifier. If the identifier already has an associated function then the old function will be replaced by the new one
        /// </summary>
        /// <param name="token">The function identifier</param>
        /// <param name="action">The function</param>
        /// <exception cref="ArgumentNullException">if the token or callback is null</exception>
        public void Add(string token, Action<object> action)
        {
            if (token == null)
            {
                throw new ArgumentNullException("token");
            }
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }
            if (!actionMappings.ContainsKey(token))
            {
                actionMappings.Add(token, action);
            }
            else
            {
                actionMappings.Remove(token);
                actionMappings.Add(token, action);
            }
        }

        /// <summary>
        /// Calls the function mapped to the identifier token with an optional argument
        /// </summary>
        /// <param name="token">The function identifier</param>
        /// <param name="args">Optional argument for the function</param>
        /// <exception cref="ArgumentNullException">if token is null</exception>
        /// <exception cref="MissingMethodException">if the there is no action for the provided token</exception>
        public void Invoke(string token, object args=null)
        {
            if (token == null)
            {
                throw new ArgumentNullException("token");
            }
            if (actionMappings.ContainsKey(token))
            {
                actionMappings[token](args);
            }
            else
            {
                throw new MissingMethodException(token + " does not exist");
            }
        }
    }
}

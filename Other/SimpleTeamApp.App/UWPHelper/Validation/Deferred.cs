using System;
using System.Collections.Generic;

namespace SimpleTeamApp.App.UWPHelper.Validation
{
    public class Deferred
    {
        private Deferred() { }

        private static Dictionary<System.Action, DeferredExecutor> m_ActionToExecutor = new Dictionary<System.Action, DeferredExecutor>();
        public static void Execute(System.Action action, int executionTimeoutMilliseconds = 500)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            DeferredExecutor executor = null;

            lock (m_ActionToExecutor)
            {
                if (!m_ActionToExecutor.ContainsKey(action))
                {
                    m_ActionToExecutor.Add(action, new DeferredExecutor(action, executionTimeoutMilliseconds));
                }

                executor = m_ActionToExecutor[action];
            }

            executor.ExecutionTimeoutMilliseconds = executionTimeoutMilliseconds;
            executor.Execute();
        }
    }
}

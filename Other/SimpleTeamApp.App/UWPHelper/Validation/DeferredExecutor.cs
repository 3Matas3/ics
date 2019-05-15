using System;
using System.Threading;

namespace SimpleTeamApp.App.UWPHelper.Validation
{
    public class DeferredExecutor
    {
        public int ExecutionTimeoutMilliseconds { get; set; }
        System.Action m_ExecutionAction = null;
        bool m_Started = false;
        object m_Locker = new object();
        DateTime? m_TimeToExecute = null;
        public string Name { get; set; }

        public int ExecuteCallsCount { get; private set; }

        public DeferredExecutor(System.Action executionAction, int executionTimeoutMilliseconds = 500)
        {
            Name = "(unnamed)";

            if (executionAction == null)
            {
                throw new ArgumentNullException("executionAction");
            }

            ExecutionTimeoutMilliseconds = executionTimeoutMilliseconds;
            m_ExecutionAction = executionAction;
        }

        bool m_NeedRestart = false;
        public void Execute(int? executionTimeoutMilliseconds = null)
        {
            lock (m_Locker)
            {
                ExecuteCallsCount++;

                if (m_TimeToExecute == null) m_TimeToExecute = DateTime.Now;
                if (executionTimeoutMilliseconds == null) executionTimeoutMilliseconds = ExecutionTimeoutMilliseconds;
                m_TimeToExecute = DateTime.Now.AddMilliseconds(executionTimeoutMilliseconds.Value);
                m_NeedRestart = true;

                if (!m_Started)
                {
                    m_Started = true;

                    ThreadPool.QueueUserWorkItem(new WaitCallback((object o) =>
                    {
                        try
                        {
                            while (DateTime.Now <= m_TimeToExecute.Value)
                            {
                                Thread.Sleep(1);
                            }
                            if (m_ExecutionAction == null) throw new ApplicationException(string.Format("m_ExecutionAction == null in deferred executor {0}", Name));

                            m_NeedRestart = false;
                            try
                            {
                                m_ExecutionAction();
                            }
                            catch (Exception) { }
                            finally
                            {
                                m_Started = false;
                                m_TimeToExecute = null;
                                ExecuteCallsCount = 0;
                            }

                            if (m_NeedRestart) Execute();
                        }
                        catch (Exception) { }
                    }));
                }
            }
        }
    }
}

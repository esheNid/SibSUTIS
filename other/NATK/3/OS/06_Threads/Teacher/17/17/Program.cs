﻿/*
Метод RegisterWaitForSingleObject
В следующем примере показаны некоторые возможности потоков. 
•	Помещение задачи в очередь для выполнения с помощью потоков ThreadPool, используя метод 
 RegisterWaitForSingleObject.
•	Отправка сигнала задаче о выполнении, используя AutoResetEvent. См. раздел EventWaitHandle, 
 AutoResetEvent и ManualResetEvent.
•	Обработка времени ожидания и сигналов с помощью делегата WaitOrTimerCallback.
•	Отмена задачи в очереди с помощью RegisteredWaitHandle.
*/
using System;
using System.Threading;

// TaskInfo contains data that will be passed to the callback
// method.
public class TaskInfo {
    public RegisteredWaitHandle Handle = null;
//    public string OtherInfo = "default";
//    private RegisteredWaitHandle Handle;
    private string OtherInfo;

    public TaskInfo()
    {
//	this.Handle = null;
	this.OtherInfo = "default";
    }

//    public TaskInfo(RegisteredWaitHandle Handle, string OtherInfo)
    public TaskInfo(string OtherInfo)
    {
//	this.Handle = Handle;
	this.OtherInfo = OtherInfo;
    }

    public string GetInfo()
    {
	return OtherInfo;
    }

    public RegisteredWaitHandle GetHandle()
    {
	return Handle;
    }

    public void SetHandle()
    {
	Handle.Unregister(null); 
    }
}

public class Example {
    public static void Main(string[] args) {
        // The main thread uses AutoResetEvent to signal the
        // registered wait handle, which executes the callback
        // method.
        AutoResetEvent ev = new AutoResetEvent(false);
	
	TaskInfo ti = new TaskInfo("First task");
/*
        RegisteredWaitHandle rwh = ThreadPool.RegisterWaitForSingleObject(
            ev,
            new WaitOrTimerCallback(WaitProc),
            ti,
            1000,
            false); 
*/
//        TaskInfo ti1 = new TaskInfo(rwh, "First task");
//        ti.OtherInfo = "First task";
        // The TaskInfo for the task includes the registered wait
        // handle returned by RegisterWaitForSingleObject.  This
        // allows the wait to be terminated when the object has
        // been signaled once (see WaitProc).

        ti.Handle = ThreadPool.RegisterWaitForSingleObject(
            ev,
            new WaitOrTimerCallback(WaitProc),
            ti,
            1000,
            false
        );

        // The main thread waits three seconds, to demonstrate the
        // time-outs on the queued thread, and then signals.
        Thread.Sleep(3100);
        Console.WriteLine("Main thread signals.");
        ev.Set();

        // The main thread sleeps, which should give the callback
        // method time to execute.  If you comment out this line, the
        // program usually ends before the ThreadPool thread can execute.
        Thread.Sleep(1000);
        // If you start a thread yourself, you can wait for it to end
        // by calling Thread.Join.  This option is not available with 
        // thread pool threads.
        Console.ReadLine();
    }
   
    // The callback method executes when the registered wait times out,
    // or when the WaitHandle (in this case AutoResetEvent) is signaled.
    // WaitProc unregisters the WaitHandle the first time the event is 
    // signaled.
    public static void WaitProc(object state, bool timedOut) {
        // The state object must be cast to the correct type, because the
        // signature of the WaitOrTimerCallback delegate specifies type
        // Object.
        TaskInfo ti = (TaskInfo) state;

        string cause = "TIMED OUT";
        if (!timedOut) {
            cause = "SIGNALED";
            // If the callback method executes because the WaitHandle is
            // signaled, stop future execution of the callback method
            // by unregistering the WaitHandle.
/*
            if (ti.Handle != null)
                ti.Handle.Unregister(null);
*/
	    if (ti.GetHandle() != null)
		ti.SetHandle();
        } 

        Console.WriteLine("WaitProc( {0} ) executes on thread {1}; cause = {2}.",
            ti.GetInfo(),	//ti.OtherInfo, 
            Thread.CurrentThread.GetHashCode().ToString(), 
            cause
        );
    }
}


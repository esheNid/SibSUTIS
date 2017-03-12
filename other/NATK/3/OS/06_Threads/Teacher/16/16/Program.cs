﻿/*
В следующем примере в очередь добавляется задача с помощью метода QueueUserWorkItem, затем 
 методу предоставляются данные задачи.
*/
using System;
using System.Threading;

// TaskInfo holds state information for a task that will be
// executed by a ThreadPool thread.
public class TaskInfo {
    // State information for the task.  These members
    // can be implemented as read-only properties, read/write
    // properties with validation, and so on, as required.
//    public string Boilerplate;
//    public int Value;
    private string Boilerplate;
    private int Value;

    // Public constructor provides an easy way to supply all
    // the information needed for the task.
    public TaskInfo(string text, int number) {
        Boilerplate = text;
        Value = number;
    }
    public void DoWork()
    {
	Console.WriteLine(Boilerplate, Value);
    }
}

public class Example {
    public static void Main() {
        // Create an object containing the information needed
        // for the task.
        TaskInfo ti = new TaskInfo("This report displays the number {0}.", 42);

        // Queue the task and data.
        if (ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadProc), ti)) {    
            Console.WriteLine("Main thread does some work, then sleeps.");

            // If you comment out the Sleep, the main thread exits before
            // the ThreadPool task has a chance to run.  ThreadPool uses 
            // background threads, which do not keep the application 
            // running.  (This is a simple example of a race condition.)
            Thread.Sleep(1000);

            Console.WriteLine("Main thread exits.");
        }
        else {
            Console.WriteLine("Unable to queue ThreadPool request."); 
        }
        Console.ReadLine();
    }

    // The thread procedure performs the independent task, in this case
    // formatting and printing a very simple report.
    //
    static void ThreadProc(Object stateInfo) {
        TaskInfo ti = (TaskInfo) stateInfo;
//        Console.WriteLine(ti.Boilerplate, ti.Value); 
	ti.DoWork();
    }
}

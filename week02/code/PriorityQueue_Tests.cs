using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue three items with different priorities and dequeue them one by one.
    // Expected Result: Items are returned in priority order (highest first): "high", "medium", "low"
    // Defect(s) Found: 
    //   1. Loop used `_queue.Count - 1` as upper bound, skipping the last element entirely.
    //      Fixed to `_queue.Count`.
    //   2. The winning item was never removed from the queue (`_queue.RemoveAt` was missing).
    //      Fixed by adding `_queue.RemoveAt(highPriorityIndex)` before returning.
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("low", 1);
        priorityQueue.Enqueue("medium", 5);
        priorityQueue.Enqueue("high", 10);

        Assert.AreEqual("high",   priorityQueue.Dequeue());
        Assert.AreEqual("medium", priorityQueue.Dequeue());
        Assert.AreEqual("low",    priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue items where two have the same highest priority.
    //           The one added first (closer to the front) should be dequeued first (FIFO tie-break).
    // Expected Result: "first" is returned before "second" even though both have priority 10.
    // Defect(s) Found:
    //   The loop used `>=` when comparing priorities, which caused later items with equal
    //   priority to overwrite the winner index, breaking FIFO tie-breaking.
    //   Fixed by changing `>=` to `>` so the first (earliest) highest-priority item wins.
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("first",  10);
        priorityQueue.Enqueue("second", 10);
        priorityQueue.Enqueue("low",     1);

        Assert.AreEqual("first",  priorityQueue.Dequeue());
        Assert.AreEqual("second", priorityQueue.Dequeue());
        Assert.AreEqual("low",    priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Dequeue from an empty queue.
    // Expected Result: InvalidOperationException with message "The queue is empty." is thrown.
    // Defect(s) Found: None — this was already implemented correctly.
    public void TestPriorityQueue_3()
    {
        var priorityQueue = new PriorityQueue();

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Expected InvalidOperationException was not thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
    }

    [TestMethod]
    // Scenario: Enqueue a single item and dequeue it. Queue should then be empty.
    // Expected Result: The single item's value is returned; further dequeue throws an exception.
    // Defect(s) Found: The item was never removed from the queue (RemoveAt was missing),
    //   so the queue was never empty and the second dequeue returned the same item instead of throwing.
    public void TestPriorityQueue_4()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("only", 3);

        Assert.AreEqual("only", priorityQueue.Dequeue());

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Expected InvalidOperationException was not thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
    }

    [TestMethod]
    // Scenario: The last item added to the queue has the highest priority.
    //           This specifically tests that the loop does NOT skip the last element.
    // Expected Result: "last" (added last, priority 99) is dequeued first.
    // Defect(s) Found: Loop bound was `_queue.Count - 1`, so the last element was never
    //   checked. When the highest-priority item was at the end, a lower-priority item was
    //   incorrectly returned. Fixed by changing bound to `_queue.Count`.
    public void TestPriorityQueue_5()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("first",  5);
        priorityQueue.Enqueue("second", 3);
        priorityQueue.Enqueue("last",  99);

        Assert.AreEqual("last",   priorityQueue.Dequeue());
        Assert.AreEqual("first",  priorityQueue.Dequeue());
        Assert.AreEqual("second", priorityQueue.Dequeue());
    }

    // Add more test cases as needed below.
}
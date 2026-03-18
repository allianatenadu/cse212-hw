/// <summary>
/// Maintain a Customer Service Queue.  Allows new customers to be 
/// added and allows customers to be serviced.
/// </summary>
public class CustomerService {
    public static void Run() {
        // Test 1
        // Scenario: Add one customer, then serve that customer.
        // Expected Result: The customer's details should be displayed.
        Console.WriteLine("Test 1");
        var cs = new CustomerService(4);
        cs.AddNewCustomer("Alice", "A001", "Cannot login");
        cs.ServeCustomer();
        // Defect(s) Found: ServeCustomer was removing index 0 BEFORE reading it,
        // then reading index 0 again (which was the next customer). Fixed by
        // reading the customer first, then removing.

        Console.WriteLine("=================");

        // Test 2
        // Scenario: Add two customers, serve both. Check they come out in FIFO order.
        // Expected Result: Customer 1 is served first, then Customer 2.
        Console.WriteLine("Test 2");
        cs = new CustomerService(4);
        cs.AddNewCustomer("Bob", "B002", "Billing issue");
        cs.AddNewCustomer("Carol", "C003", "Password reset");
        Console.WriteLine($"Before serving: {cs}");
        cs.ServeCustomer();  // Should display Bob
        cs.ServeCustomer();  // Should display Carol
        Console.WriteLine($"After serving: {cs}");
        // Defect(s) Found: None (once Bug 1 was fixed)

        Console.WriteLine("=================");

        // Test 3
        // Scenario: Call ServeCustomer when the queue is empty.
        // Expected Result: An error message is displayed (no crash).
        Console.WriteLine("Test 3");
        cs = new CustomerService(4);
        cs.ServeCustomer();
        // Defect(s) Found: No empty-queue guard existed. Added check for
        // _queue.Count == 0 and display an error message.

        Console.WriteLine("=================");

        // Test 4
        // Scenario: Add customers up to and beyond the max size (4).
        // Expected Result: 4 customers added successfully; 5th triggers error message.
        Console.WriteLine("Test 4");
        cs = new CustomerService(4);
        cs.AddNewCustomer("Dave",  "D004", "Slow connection");
        cs.AddNewCustomer("Eve",   "E005", "App crashes");
        cs.AddNewCustomer("Frank", "F006", "Wrong charge");
        cs.AddNewCustomer("Grace", "G007", "Lost data");
        cs.AddNewCustomer("Hank",  "H008", "Cannot update"); // Should show error
        Console.WriteLine($"Queue (should have 4): {cs}");
        // Defect(s) Found: AddNewCustomer used > instead of >= so it allowed
        // one extra customer through. Fixed to >=.

        Console.WriteLine("=================");

        // Test 5
        // Scenario: Create a CustomerService with an invalid max size of 0.
        // Expected Result: Max size defaults to 10.
        Console.WriteLine("Test 5");
        cs = new CustomerService(0);
        Console.WriteLine($"Size should default to 10: {cs}");
        // Defect(s) Found: None — constructor already handles this correctly.

        Console.WriteLine("=================");
    }

    private readonly List<Customer> _queue = new();
    private readonly int _maxSize;

    public CustomerService(int maxSize) {
        if (maxSize <= 0)
            _maxSize = 10;
        else
            _maxSize = maxSize;
    }

    // Helper for automated tests (bypasses Console.ReadLine)
    public void AddNewCustomer(string name, string accountId, string problem) {
        // BUG 3 FIX: Changed > to >= so the queue never exceeds _maxSize
        if (_queue.Count >= _maxSize) {
            Console.WriteLine("Maximum Number of Customers in Queue.");
            return;
        }
        var customer = new Customer(name, accountId, problem);
        _queue.Add(customer);
    }

    /// <summary>
    /// Defines a Customer record for the service queue.
    /// This is an inner class.  Its real name is CustomerService.Customer
    /// </summary>
    private class Customer {
        public Customer(string name, string accountId, string problem) {
            Name = name;
            AccountId = accountId;
            Problem = problem;
        }

        private string Name { get; }
        private string AccountId { get; }
        private string Problem { get; }

        public override string ToString() {
            return $"{Name} ({AccountId})  : {Problem}";
        }
    }

    /// <summary>
    /// Dequeue the next customer and display the information.
    /// </summary>
    private void ServeCustomer() {
        // BUG 2 FIX: Check for empty queue before trying to serve
        if (_queue.Count == 0) {
            Console.WriteLine("No customers in the queue.");
            return;
        }
        // BUG 1 FIX: Read the customer BEFORE removing from the queue
        var customer = _queue[0];
        _queue.RemoveAt(0);
        Console.WriteLine(customer);
    }

    /// <summary>
    /// Support the WriteLine function to provide a string representation of the
    /// customer service queue object. This is useful for debugging.
    /// </summary>
    public override string ToString() {
        return $"[size={_queue.Count} max_size={_maxSize} => " + string.Join(", ", _queue) + "]";
    }
}
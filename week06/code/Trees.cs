public static class Trees
{
    /// <summary>
    /// Given a sorted list, create a balanced BST by always inserting the middle element.
    /// </summary>
    public static BinarySearchTree CreateTreeFromSortedList(int[] sortedNumbers)
    {
        var bst = new BinarySearchTree();
        InsertMiddle(sortedNumbers, 0, sortedNumbers.Length - 1, bst);
        return bst;
    }

    /// <summary>
    /// Problem 5: Insert the middle value of sortedNumbers[first..last] into bst,
    /// then recurse on the left half and right half.
    /// </summary>
    private static void InsertMiddle(int[] sortedNumbers, int first, int last, BinarySearchTree bst)
    {
        // Base case: nothing left to insert
        if (first > last)
            return;

        // Find the middle index and insert it
        int mid = (first + last) / 2;
        bst.Insert(sortedNumbers[mid]);

        // Recurse on the left half (values before mid)
        InsertMiddle(sortedNumbers, first, mid - 1, bst);

        // Recurse on the right half (values after mid)
        InsertMiddle(sortedNumbers, mid + 1, last, bst);
    }
}

public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // TODO Problem 1 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        // Plan:
        // Step 1: Create a new double array with size equal to 'length'.
        // Step 2: Loop from index 0 up to (but not including) 'length'.
        // Step 3: At each index i, calculate the multiple: number * (i + 1).
        //         (i + 1) because the first multiple is number * 1, the second is number * 2, etc.
        // Step 4: Store the calculated value in the array at position i.
        // Step 5: After the loop, return the filled array.

        double[] result = new double[length];

        for (int i = 0; i < length; i++)
        {
            result[i] = number * (i + 1);
        }

        return result;
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // TODO Problem 2 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        // Plan:
        // Step 1: Identify the split point. The last 'amount' elements will move to the front.
        //         The split index is: data.Count - amount.
        //         Example: data = {1,2,3,4,5,6,7,8,9}, amount = 3 → split at index 6.
        // Step 2: Extract the last 'amount' elements as a new list (the "tail").
        //         Using GetRange(splitIndex, amount) gives us {7, 8, 9}.
        // Step 3: Remove those elements from the end of the original list.
        //         Using RemoveRange(splitIndex, amount) leaves {1, 2, 3, 4, 5, 6}.
        // Step 4: Insert the extracted tail at the front of the list (index 0).
        //         Using InsertRange(0, tail) gives {7, 8, 9, 1, 2, 3, 4, 5, 6}.
        // Step 5: The list is modified in place — no need to return anything.

        int splitIndex = data.Count - amount;

        List<int> tail = data.GetRange(splitIndex, amount);

        data.RemoveRange(splitIndex, amount);

        data.InsertRange(0, tail);
    }
}
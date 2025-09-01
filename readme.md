# Sorting Algorithm Tutorial

This project allows you to implement custom sorting algorithms in C# using a simple interface. Each algorithm is automatically detected and run by the program.

## Table of Contents

- [Overview](#overview)
- [Interface](#interface)
- [Creating a New Algorithm](#creating-a-new-algorithm)
- [Adding Your Algorithm](#adding-your-algorithm)
- [Example](#example)
- [Running the Program](#running-the-program)

---

## Overview

The program searches the `algorithms` folder for any class that implements the `IAlgorithm` interface. Each algorithm is run on a randomized integer array, and the program will measure the time taken to sort it.

---

## Interface

All sorting algorithms must implement the `IAlgorithm` interface:

```csharp
public interface IAlgorithm
{
    public string Name => System.Text.RegularExpressions.Regex.Replace(GetType().Name, "(\\B[A-Z])", " $1");

    public int[] Sort(int[] nums);
}
```

**Details:**

- `Name`: Automatically generates a readable name from the class name.\
  Example: `DotnetSort` → "Dotnet Sort".
- `Sort(int[] nums)`: The method where your sorting logic goes.
  - Input: `nums` — an integer array to sort.
  - Output: The sorted integer array.

---

## Creating a New Algorithm

1. Open your project in your IDE.
2. Navigate to the `algorithms` folder.
3. Create a new C# file for your algorithm, for example: `MySortingAlgorithm.cs`.
4. Implement the `IAlgorithm` interface:

```csharp
using System;

public class MySortingAlgorithm, : IAlgorithm
{
    public int[] Sort(int[] nums)
    {
        int[] array = nums;

        //Logic goes here

        return array;
    }
}
```

**Notes:**

- Always clone the input array if your algorithm modifies it in-place.
- Make sure the `Sort` method returns the sorted array.
- The `Program.cs` file will automatically run the algorithm until the array is sorted, no need for any while(true) statements

---

## Adding Your Algorithm

- Save your new `.cs` file inside the `algorithms` folder.
- The main program will automatically detect any class implementing `IAlgorithm` inside this folder.

---

## Example

Suppose you created `DotnetSort.cs`:

```csharp
public class DotnetSort : IAlgorithm
{
    public int[] Sort(int[] nums)
    {
        int[] numbers = nums;
        Array.Sort(numbers);
        return numbers;
    }
}
```

When you run the program:

```
Found: Dotnet Sort

Algorithms are sorting this array: {8, 0, 7, 5, 7, 9, 5, 2, 1, 8}

Running: Dotnet Sort

Dotnet Sort took 3 ms to sort the array
```

---

## Running the Program

1. Make sure your algorithm `.cs` files are inside the `algorithms` folder.
2. Compile and run the program.
3. The program will:
   - Detect all algorithms implementing `IAlgorithm`.
   - Generate a random array.
   - Run each algorithm and display timing and progress.

---

This setup makes it easy to add, test, and benchmark new sorting algorithms without touching the main program.

